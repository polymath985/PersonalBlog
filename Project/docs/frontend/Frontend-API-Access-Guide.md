# 前端路由访问详解 - 从 Vue.js 到 ASP.NET Core API

## 📋 概述

本文档详细介绍前端 Vue.js 应用如何访问后端 ASP.NET Core API 的各种路由，包括不同的请求方式、参数传递、错误处理等实际开发中的应用场景。

---

## 🛣️ 前端路由访问方式总览

### 1. 基础 HTTP 方法对应

| HTTP 方法 | 用途 | 前端实现 | 后端控制器 |
|----------|------|----------|-----------|
| GET | 获取数据 | `fetch()`, `axios.get()` | `[HttpGet]` |
| POST | 创建数据 | `fetch()` + body, `axios.post()` | `[HttpPost]` |
| PUT | 更新数据 | `fetch()` + PUT, `axios.put()` | `[HttpPut]` |
| DELETE | 删除数据 | `fetch()` + DELETE, `axios.delete()` | `[HttpDelete]` |
| PATCH | 部分更新 | `fetch()` + PATCH, `axios.patch()` | `[HttpPatch]` |

---

## 🔗 具体实现示例

### 1. GET 请求 - 获取数据

#### 后端控制器示例
```csharp
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    // GET api/users
    [HttpGet]
    public ActionResult<IEnumerable<UserData>> GetUsers()
    {
        var users = new List<UserData>
        {
            new UserData("1", "张三", "zhangsan@example.com"),
            new UserData("2", "李四", "lisi@example.com")
        };
        return Ok(users);
    }

    // GET api/users/1
    [HttpGet("{id}")]
    public ActionResult<UserData> GetUser(string id)
    {
        var user = new UserData(id, "张三", "zhangsan@example.com");
        return Ok(user);
    }

    // GET api/users?page=1&size=10
    [HttpGet]
    public ActionResult<IEnumerable<UserData>> GetUsersWithPaging(
        [FromQuery] int page = 1, 
        [FromQuery] int size = 10)
    {
        // 分页逻辑
        return Ok(new List<UserData>());
    }
}
```

#### 前端 TypeScript 访问方式

**1. 基础 fetch 请求**
```typescript
// 获取所有用户
async function getUsers(): Promise<UserData[]> {
    try {
        const response = await fetch('/api/users');
        if (!response.ok) {
            throw new Error(`HTTP ${response.status}: ${response.statusText}`);
        }
        const users: UserData[] = await response.json();
        return users;
    } catch (error) {
        console.error('获取用户列表失败:', error);
        throw error;
    }
}

// 获取单个用户
async function getUser(id: string): Promise<UserData> {
    const response = await fetch(`/api/users/${id}`);
    if (!response.ok) {
        throw new Error(`用户 ${id} 不存在`);
    }
    return await response.json();
}

// 带查询参数的请求
async function getUsersWithPaging(page: number = 1, size: number = 10): Promise<UserData[]> {
    const params = new URLSearchParams({
        page: page.toString(),
        size: size.toString()
    });
    
    const response = await fetch(`/api/users?${params}`);
    return await response.json();
}
```

**2. Vue 3 Composition API 集成**
```typescript
import { ref, onMounted } from 'vue';

interface UserData {
    id: string;
    name: string;
    email: string;
}

export default {
    setup() {
        const users = ref<UserData[]>([]);
        const loading = ref(false);
        const error = ref<string | null>(null);

        const fetchUsers = async () => {
            try {
                loading.value = true;
                error.value = null;
                
                const response = await fetch('/api/users');
                if (!response.ok) {
                    throw new Error('获取用户数据失败');
                }
                
                users.value = await response.json();
            } catch (err) {
                error.value = err instanceof Error ? err.message : '未知错误';
            } finally {
                loading.value = false;
            }
        };

        onMounted(() => {
            fetchUsers();
        });

        return {
            users,
            loading,
            error,
            fetchUsers
        };
    }
};
```

### 2. POST 请求 - 创建数据

#### 后端控制器
```csharp
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    // POST api/users
    [HttpPost]
    public ActionResult<UserData> CreateUser([FromBody] CreateUserRequest request)
    {
        var user = new UserData(
            Guid.NewGuid().ToString(),
            request.Name,
            request.Email
        );
        
        // 数据库保存逻辑...
        
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }
}

public class CreateUserRequest
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
```

#### 前端实现
```typescript
interface CreateUserRequest {
    name: string;
    email: string;
}

async function createUser(userData: CreateUserRequest): Promise<UserData> {
    const response = await fetch('/api/users', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(userData)
    });

    if (!response.ok) {
        const errorData = await response.json();
        throw new Error(errorData.message || '创建用户失败');
    }

    return await response.json();
}

// Vue 组件中使用
export default {
    setup() {
        const newUser = ref<CreateUserRequest>({
            name: '',
            email: ''
        });

        const submitUser = async () => {
            try {
                const createdUser = await createUser(newUser.value);
                console.log('用户创建成功:', createdUser);
                
                // 重置表单
                newUser.value = { name: '', email: '' };
                
                // 刷新用户列表或导航到新页面
                
            } catch (error) {
                console.error('创建用户失败:', error);
            }
        };

        return {
            newUser,
            submitUser
        };
    }
};
```

### 3. PUT 请求 - 更新数据

#### 后端控制器
```csharp
// PUT api/users/1
[HttpPut("{id}")]
public ActionResult<UserData> UpdateUser(string id, [FromBody] UpdateUserRequest request)
{
    // 查找用户逻辑...
    var user = new UserData(id, request.Name, request.Email);
    
    // 更新数据库...
    
    return Ok(user);
}

public class UpdateUserRequest
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
```

#### 前端实现
```typescript
async function updateUser(id: string, userData: UpdateUserRequest): Promise<UserData> {
    const response = await fetch(`/api/users/${id}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(userData)
    });

    if (!response.ok) {
        throw new Error('更新用户失败');
    }

    return await response.json();
}
```

### 4. DELETE 请求 - 删除数据

#### 后端控制器
```csharp
// DELETE api/users/1
[HttpDelete("{id}")]
public ActionResult DeleteUser(string id)
{
    // 删除逻辑...
    return NoContent(); // 返回 204 状态码
}
```

#### 前端实现
```typescript
async function deleteUser(id: string): Promise<void> {
    const response = await fetch(`/api/users/${id}`, {
        method: 'DELETE'
    });

    if (!response.ok) {
        throw new Error('删除用户失败');
    }
    
    // DELETE 通常返回 204 No Content，没有响应体
}

// 在 Vue 组件中使用
const confirmDelete = async (userId: string) => {
    if (confirm('确定要删除这个用户吗？')) {
        try {
            await deleteUser(userId);
            // 从本地状态中移除用户
            users.value = users.value.filter(user => user.id !== userId);
            console.log('用户删除成功');
        } catch (error) {
            console.error('删除失败:', error);
        }
    }
};
```

---

## 🔧 高级前端访问技巧

### 1. 统一的 API 客户端类

```typescript
class ApiClient {
    private baseUrl = '/api';

    private async request<T>(
        endpoint: string, 
        options: RequestInit = {}
    ): Promise<T> {
        const url = `${this.baseUrl}${endpoint}`;
        
        const config: RequestInit = {
            headers: {
                'Content-Type': 'application/json',
                ...options.headers,
            },
            ...options,
        };

        const response = await fetch(url, config);
        
        if (!response.ok) {
            const error = await response.text();
            throw new Error(`${response.status}: ${error}`);
        }

        // 如果是 DELETE 请求，可能没有响应体
        if (response.status === 204) {
            return {} as T;
        }

        return await response.json();
    }

    // GET 请求
    async get<T>(endpoint: string): Promise<T> {
        return this.request<T>(endpoint);
    }

    // POST 请求
    async post<T>(endpoint: string, data: any): Promise<T> {
        return this.request<T>(endpoint, {
            method: 'POST',
            body: JSON.stringify(data),
        });
    }

    // PUT 请求
    async put<T>(endpoint: string, data: any): Promise<T> {
        return this.request<T>(endpoint, {
            method: 'PUT',
            body: JSON.stringify(data),
        });
    }

    // DELETE 请求
    async delete<T>(endpoint: string): Promise<T> {
        return this.request<T>(endpoint, {
            method: 'DELETE',
        });
    }
}

// 创建全局实例
export const apiClient = new ApiClient();
```

### 2. 使用 API 客户端

```typescript
// 用户相关的 API 操作
export class UserService {
    static async getUsers(): Promise<UserData[]> {
        return apiClient.get<UserData[]>('/users');
    }

    static async getUser(id: string): Promise<UserData> {
        return apiClient.get<UserData>(`/users/${id}`);
    }

    static async createUser(data: CreateUserRequest): Promise<UserData> {
        return apiClient.post<UserData>('/users', data);
    }

    static async updateUser(id: string, data: UpdateUserRequest): Promise<UserData> {
        return apiClient.put<UserData>(`/users/${id}`, data);
    }

    static async deleteUser(id: string): Promise<void> {
        return apiClient.delete<void>(`/users/${id}`);
    }
}

// 在 Vue 组件中使用
export default {
    setup() {
        const users = ref<UserData[]>([]);

        const loadUsers = async () => {
            try {
                users.value = await UserService.getUsers();
            } catch (error) {
                console.error('加载用户失败:', error);
            }
        };

        const deleteUser = async (id: string) => {
            try {
                await UserService.deleteUser(id);
                users.value = users.value.filter(user => user.id !== id);
            } catch (error) {
                console.error('删除用户失败:', error);
            }
        };

        return {
            users,
            loadUsers,
            deleteUser
        };
    }
};
```

---

## 🌐 路由参数处理详解

### 1. 路径参数 (Path Parameters)

#### 后端定义
```csharp
[HttpGet("{id}")]                    // 单个参数
[HttpGet("{category}/{id}")]         // 多个参数
[HttpGet("users/{id}/posts/{postId}")]  // 嵌套资源
```

#### 前端访问
```typescript
// 单个参数
const user = await fetch(`/api/users/${userId}`);

// 多个参数
const product = await fetch(`/api/products/${category}/${productId}`);

// 嵌套资源
const post = await fetch(`/api/users/${userId}/posts/${postId}`);

// 使用模板字符串构建复杂 URL
const buildUserPostUrl = (userId: string, postId: string) => {
    return `/api/users/${encodeURIComponent(userId)}/posts/${encodeURIComponent(postId)}`;
};
```

### 2. 查询参数 (Query Parameters)

#### 后端定义
```csharp
[HttpGet]
public ActionResult<IEnumerable<UserData>> GetUsers(
    [FromQuery] string? search = null,
    [FromQuery] int page = 1,
    [FromQuery] int size = 10,
    [FromQuery] string? sortBy = null,
    [FromQuery] bool descending = false)
{
    // 查询逻辑...
}
```

#### 前端构建查询参数
```typescript
// 方式1: 使用 URLSearchParams
function buildUserQuery(options: {
    search?: string;
    page?: number;
    size?: number;
    sortBy?: string;
    descending?: boolean;
}) {
    const params = new URLSearchParams();
    
    if (options.search) params.append('search', options.search);
    if (options.page) params.append('page', options.page.toString());
    if (options.size) params.append('size', options.size.toString());
    if (options.sortBy) params.append('sortBy', options.sortBy);
    if (options.descending) params.append('descending', options.descending.toString());
    
    return params.toString();
}

// 使用示例
const queryString = buildUserQuery({
    search: '张三',
    page: 2,
    size: 20,
    sortBy: 'name',
    descending: true
});

const users = await fetch(`/api/users?${queryString}`);

// 方式2: 手动构建查询字符串
const searchUsers = async (searchTerm: string, page: number = 1) => {
    const url = `/api/users?search=${encodeURIComponent(searchTerm)}&page=${page}`;
    const response = await fetch(url);
    return await response.json();
};
```

---

## 🔒 认证和授权访问

### 1. JWT Token 认证

#### 前端 Token 管理
```typescript
class AuthService {
    private static TOKEN_KEY = 'auth_token';

    static setToken(token: string) {
        localStorage.setItem(this.TOKEN_KEY, token);
    }

    static getToken(): string | null {
        return localStorage.getItem(this.TOKEN_KEY);
    }

    static removeToken() {
        localStorage.removeItem(this.TOKEN_KEY);
    }

    static isAuthenticated(): boolean {
        return this.getToken() !== null;
    }
}

// 带认证的 API 请求
class AuthenticatedApiClient extends ApiClient {
    protected async request<T>(endpoint: string, options: RequestInit = {}): Promise<T> {
        const token = AuthService.getToken();
        
        const config: RequestInit = {
            ...options,
            headers: {
                'Content-Type': 'application/json',
                ...(token && { 'Authorization': `Bearer ${token}` }),
                ...options.headers,
            },
        };

        return super.request<T>(endpoint, config);
    }
}
```

#### 后端认证控制器
```csharp
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public ActionResult<LoginResponse> Login([FromBody] LoginRequest request)
    {
        // 验证用户凭据...
        
        var token = GenerateJwtToken(request.Username);
        return Ok(new LoginResponse { Token = token });
    }
    
    [HttpPost("register")]
    public ActionResult<UserData> Register([FromBody] RegisterRequest request)
    {
        // 注册逻辑...
    }
}

[Authorize]  // 需要认证的控制器
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    // 所有方法都需要认证
}
```

---

## 📱 前端路由守卫集成

### 1. Vue Router 集成

```typescript
// router/index.ts
import { createRouter, createWebHistory } from 'vue-router';
import { AuthService } from '@/services/AuthService';

const routes = [
    {
        path: '/login',
        name: 'Login',
        component: () => import('@/views/Login.vue')
    },
    {
        path: '/users',
        name: 'Users',
        component: () => import('@/views/Users.vue'),
        meta: { requiresAuth: true }  // 需要认证
    }
];

const router = createRouter({
    history: createWebHistory(),
    routes
});

// 路由守卫
router.beforeEach((to, from, next) => {
    if (to.meta.requiresAuth && !AuthService.isAuthenticated()) {
        // 重定向到登录页面
        next({ name: 'Login', query: { redirect: to.fullPath } });
    } else {
        next();
    }
});

export default router;
```

---

## 🚨 错误处理和状态管理

### 1. 全局错误处理

```typescript
// composables/useApi.ts
import { ref, Ref } from 'vue';

export function useApi<T>() {
    const data: Ref<T | null> = ref(null);
    const loading = ref(false);
    const error = ref<string | null>(null);

    const execute = async <U>(apiCall: () => Promise<U>): Promise<U | null> => {
        try {
            loading.value = true;
            error.value = null;
            
            const result = await apiCall();
            data.value = result as unknown as T;
            return result;
            
        } catch (err) {
            error.value = err instanceof Error ? err.message : '请求失败';
            console.error('API 调用错误:', err);
            return null;
            
        } finally {
            loading.value = false;
        }
    };

    return {
        data,
        loading,
        error,
        execute
    };
}

// 在组件中使用
export default {
    setup() {
        const { data: users, loading, error, execute } = useApi<UserData[]>();
        
        const fetchUsers = () => execute(() => UserService.getUsers());
        
        onMounted(() => {
            fetchUsers();
        });
        
        return {
            users,
            loading,
            error,
            fetchUsers
        };
    }
};
```

---

## 📊 性能优化建议

### 1. 请求缓存

```typescript
class CacheableApiClient extends AuthenticatedApiClient {
    private cache = new Map<string, { data: any; timestamp: number }>();
    private cacheTimeout = 5 * 60 * 1000; // 5分钟

    async get<T>(endpoint: string, useCache = true): Promise<T> {
        if (useCache) {
            const cached = this.cache.get(endpoint);
            if (cached && Date.now() - cached.timestamp < this.cacheTimeout) {
                return cached.data;
            }
        }

        const data = await super.get<T>(endpoint);
        
        if (useCache) {
            this.cache.set(endpoint, { data, timestamp: Date.now() });
        }
        
        return data;
    }

    clearCache(endpoint?: string) {
        if (endpoint) {
            this.cache.delete(endpoint);
        } else {
            this.cache.clear();
        }
    }
}
```

### 2. 请求防抖和节流

```typescript
// 防抖搜索
import { debounce } from 'lodash-es';

export default {
    setup() {
        const searchTerm = ref('');
        const searchResults = ref<UserData[]>([]);
        
        const performSearch = async (term: string) => {
            if (term.trim()) {
                searchResults.value = await UserService.searchUsers(term);
            } else {
                searchResults.value = [];
            }
        };
        
        // 防抖搜索，延迟300ms执行
        const debouncedSearch = debounce(performSearch, 300);
        
        watch(searchTerm, (newTerm) => {
            debouncedSearch(newTerm);
        });
        
        return {
            searchTerm,
            searchResults
        };
    }
};
```

---

## 📝 总结

通过本文档，你应该掌握了：

1. **基础 HTTP 方法**的前端实现方式
2. **路径参数和查询参数**的处理技巧  
3. **认证和授权**的集成方案
4. **错误处理和状态管理**的最佳实践
5. **性能优化**的具体方法

### 🎯 最佳实践建议

1. **统一 API 接口**: 使用 ApiClient 类统一管理所有请求
2. **类型安全**: 充分利用 TypeScript 的类型系统
3. **错误处理**: 实现全局错误处理机制
4. **状态管理**: 使用 Composables 或 Pinia 管理应用状态
5. **缓存策略**: 合理使用缓存提升用户体验
6. **安全考虑**: 正确处理认证 Token 和敏感数据

这样你就能够构建出健壮、高效、用户体验良好的前端应用了！🚀
