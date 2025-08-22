# Singleton与Controller配合使用指南

## 📋 概述

在ASP.NET Core中，Singleton服务与Controller的配合是处理共享状态和业务逻辑的核心模式。

## 🎯 设计原则

### 职责分离
- **Controller**：处理HTTP请求，无状态，短生命周期
- **Singleton Service**：管理共享数据，有状态，长生命周期

### 生命周期对比

| 特性 | Controller | Singleton Service |
|------|------------|-------------------|
| 生命周期 | 每个请求 | 应用程序生命周期 |
| 实例数量 | 多个（并发） | 单一实例 |
| 状态管理 | 无状态 | 可以维护状态 |
| 线程安全 | 天然隔离 | 必须考虑 |

## 🏗️ 实现模式

### 1. 基本模式

```csharp
// Singleton服务
public class UserDataManager
{
    private static readonly Lazy<UserDataManager> _instance = 
        new(() => new UserDataManager());
    
    public static UserDataManager Instance => _instance.Value;
    
    private readonly List<UserData> _users = new();
    
    private UserDataManager() 
    {
        // 初始化数据
    }
    
    public IEnumerable<UserData> GetAllUsers() => _users.ToList();
}

// Controller使用
[ApiController]
[Route("[controller]")]
public class UserDataController : ControllerBase
{
    [HttpGet]
    public IActionResult GetUsers()
    {
        var users = UserDataManager.Instance.GetAllUsers();
        return Ok(users);
    }
}
```

### 2. 依赖注入模式（推荐）

```csharp
// 服务接口
public interface IUserDataService
{
    IEnumerable<UserData> GetAllUsers();
    UserData GetUserById(int id);
    void AddUser(UserData user);
}

// 服务实现
public class UserDataService : IUserDataService
{
    private readonly List<UserData> _users = new();
    
    public IEnumerable<UserData> GetAllUsers() => _users.ToList();
    
    public UserData GetUserById(int id) => 
        _users.FirstOrDefault(u => u.Id == id);
    
    public void AddUser(UserData user)
    {
        if (!_users.Any(u => u.Id == user.Id))
        {
            _users.Add(user);
        }
    }
}

// Program.cs注册
builder.Services.AddSingleton<IUserDataService, UserDataService>();

// Controller使用
public class UserDataController : ControllerBase
{
    private readonly IUserDataService _userService;
    
    public UserDataController(IUserDataService userService)
    {
        _userService = userService;
    }
    
    [HttpGet]
    public IActionResult GetUsers()
    {
        var users = _userService.GetAllUsers();
        return Ok(users);
    }
}
```

## 🔒 线程安全

### 问题场景
```csharp
// ❌ 不安全的实现
public class UserDataService
{
    private List<UserData> _users = new();
    
    public void AddUser(UserData user)
    {
        // 多线程同时调用可能出现竞态条件
        _users.Add(user);
    }
}
```

### 安全实现
```csharp
// ✅ 线程安全的实现
public class UserDataService
{
    private readonly ConcurrentBag<UserData> _users = new();
    private readonly object _lock = new();
    
    public void AddUser(UserData user)
    {
        lock (_lock)
        {
            if (!_users.Any(u => u.Id == user.Id))
            {
                _users.Add(user);
            }
        }
    }
    
    public IEnumerable<UserData> GetAllUsers()
    {
        lock (_lock)
        {
            return _users.ToList(); // 返回副本
        }
    }
}
```

## 🚀 数据初始化策略

### 1. 懒加载初始化
```csharp
public class UserDataService : IUserDataService
{
    private readonly Lazy<List<UserData>> _users = 
        new(() => InitializeData());
    
    private static List<UserData> InitializeData()
    {
        return new List<UserData>
        {
            new(1, "张三", "zhangsan@example.com"),
            new(2, "李四", "lisi@example.com")
        };
    }
    
    public IEnumerable<UserData> GetAllUsers() => _users.Value;
}
```

### 2. 构造函数初始化
```csharp
public class UserDataService : IUserDataService
{
    private readonly List<UserData> _users;
    
    public UserDataService()
    {
        _users = new List<UserData>
        {
            new(1, "张三", "zhangsan@example.com"),
            new(2, "李四", "lisi@example.com")
        };
    }
}
```

### 3. HostedService初始化（推荐生产环境）
```csharp
public class DataInitializationService : IHostedService
{
    private readonly IUserDataService _userService;
    
    public DataInitializationService(IUserDataService userService)
    {
        _userService = userService;
    }
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        // 应用启动时初始化数据
        InitializeSampleData();
        return Task.CompletedTask;
    }
    
    private void InitializeSampleData()
    {
        _userService.AddUser(new UserData(1, "张三", "zhangsan@example.com"));
        _userService.AddUser(new UserData(2, "李四", "lisi@example.com"));
    }
    
    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}

// Program.cs注册
builder.Services.AddHostedService<DataInitializationService>();
```

## 🎯 最佳实践

### 1. 分层架构
```
Controller → Service → Repository/Manager
```

### 2. 接口隔离
```csharp
// 读操作接口
public interface IUserDataReader
{
    IEnumerable<UserData> GetAllUsers();
    UserData GetUserById(int id);
}

// 写操作接口
public interface IUserDataWriter
{
    void AddUser(UserData user);
    void UpdateUser(UserData user);
    void DeleteUser(int id);
}

// 完整接口
public interface IUserDataService : IUserDataReader, IUserDataWriter
{
}
```

### 3. 错误处理
```csharp
public class UserDataController : ControllerBase
{
    private readonly IUserDataService _userService;
    private readonly ILogger<UserDataController> _logger;
    
    [HttpGet("{id}")]
    public IActionResult GetUser(int id)
    {
        try
        {
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound($"用户 {id} 不存在");
            }
            return Ok(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "获取用户 {Id} 时发生错误", id);
            return StatusCode(500, "服务器内部错误");
        }
    }
}
```

## 📚 相关文档

- [Controller生命周期](./Controller-Lifecycle.md)
- [依赖注入最佳实践](./Dependency-Injection.md)
- [线程安全编程指南](./Thread-Safety-Guide.md)
