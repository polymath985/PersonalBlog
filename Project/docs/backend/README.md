# 后端开发文档

## ⚙️ 概述

本目录包含 ASP.NET Core 后端 API 的开发文档，涵盖控制器设计、路由配置、数据模型等后端开发的各个方面。

## 📚 文档列表

### 即将添加的文档:
- **Route-Attribute-Guide.md** - ASP.NET Core Route 特性详解
- **Controller-Design-Guide.md** - 控制器设计最佳实践
- **Data-Models-Guide.md** - C# 数据模型和 DTO 设计
- **API-Documentation-Guide.md** - Swagger/OpenAPI 文档生成
- **Authentication-Guide.md** - JWT 认证和授权
- **Database-Integration-Guide.md** - Entity Framework Core 集成

---

## 🏗️ 后端技术栈

### 核心框架
- **[ASP.NET Core](https://docs.microsoft.com/aspnet/core/)** `v9.0`
  - Web API 模板
  - 依赖注入容器
  - 中间件管道
  - 内置 JSON 序列化

### 开发工具
- **[.NET CLI](https://docs.microsoft.com/dotnet/core/tools/)** - 命令行工具
- **[C#](https://docs.microsoft.com/dotnet/csharp/)** `v12.0` - 现代编程语言
- **[Visual Studio](https://visualstudio.microsoft.com/)** - 集成开发环境

### 可扩展组件
- **Entity Framework Core** - ORM 框架
- **AutoMapper** - 对象映射
- **FluentValidation** - 数据验证
- **Serilog** - 结构化日志

---

## 🎯 后端架构模式

### 1. MVC 架构
```
Controllers/          # 控制器层
├── WeatherForecastController.cs
└── UsersController.cs (示例)

Models/              # 数据模型
├── WeatherForecast.cs
└── UserData.cs

Services/            # 业务逻辑层 (可扩展)
└── IWeatherService.cs
```

### 2. 依赖注入模式
```csharp
// Program.cs 中注册服务
builder.Services.AddScoped<IWeatherService, WeatherService>();
builder.Services.AddDbContext<AppDbContext>(options => ...);

// 控制器中使用服务
public class WeatherController : ControllerBase
{
    private readonly IWeatherService _weatherService;
    
    public WeatherController(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }
}
```

### 3. API 响应模式
```csharp
// 统一响应格式
public class ApiResponse<T>
{
    public bool Success { get; set; }
    public T Data { get; set; }
    public string Message { get; set; }
    public List<string> Errors { get; set; }
}
```

---

## 🛣️ 路由系统

### 1. 控制器级别路由
```csharp
[ApiController]
[Route("api/[controller]")]  // /api/weatherforecast
public class WeatherForecastController : ControllerBase
```

### 2. 方法级别路由
```csharp
[HttpGet]                    // GET /api/weatherforecast
[HttpGet("{id}")]           // GET /api/weatherforecast/1
[HttpPost]                  // POST /api/weatherforecast
[HttpPut("{id}")]          // PUT /api/weatherforecast/1
[HttpDelete("{id}")]       // DELETE /api/weatherforecast/1
```

### 3. 路由约束
```csharp
[HttpGet("{id:int}")]       // 仅接受整数 ID
[HttpGet("{date:datetime}")]// 日期时间约束
[HttpGet("{name:alpha}")]   // 仅接受字母
```

---

## 📊 数据处理流程

### 1. 请求处理流程
```
HTTP Request → Routing → Model Binding → Validation → Controller Action → Business Logic → Response
```

### 2. 数据序列化
```csharp
// 自动 JSON 序列化配置
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.SerializerOptions.WriteIndented = true;
});
```

### 3. 错误处理
```csharp
// 全局异常处理
app.UseExceptionHandler("/Error");
app.UseStatusCodePages();
```

---

## 🔒 安全和认证

### 1. CORS 配置
```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueApp", policy =>
    {
        policy.WithOrigins("https://localhost:5157")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
```

### 2. JWT 认证 (可扩展)
```csharp
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
        };
    });
```

---

## 📈 API 设计原则

### 1. RESTful API 设计
```
GET    /api/users          # 获取用户列表
GET    /api/users/{id}     # 获取单个用户
POST   /api/users          # 创建新用户
PUT    /api/users/{id}     # 更新用户
DELETE /api/users/{id}     # 删除用户
```

### 2. HTTP 状态码使用
- **200 OK**: 成功获取/更新数据
- **201 Created**: 成功创建资源
- **204 No Content**: 成功删除资源
- **400 Bad Request**: 客户端请求错误
- **404 Not Found**: 资源不存在
- **500 Internal Server Error**: 服务器内部错误

### 3. 响应格式标准化
```json
{
  "success": true,
  "data": { /* 实际数据 */ },
  "message": "操作成功",
  "timestamp": "2025-08-21T10:30:00Z"
}
```

---

## 🚀 开发工作流

### 1. 开发环境启动
```bash
cd VueApp1.Server
dotnet restore       # 恢复 NuGet 包
dotnet build         # 编译项目
dotnet run           # 启动开发服务器
dotnet watch run     # 启动热重载模式
```

### 2. 调试和测试
```bash
dotnet test          # 运行单元测试
dotnet format        # 代码格式化
dotnet clean         # 清理输出目录
```

### 3. API 测试工具
- **Swagger UI**: 内置 API 文档和测试
- **Postman**: 第三方 API 测试工具  
- **Thunder Client**: VS Code 扩展
- **curl**: 命令行测试工具

---

## 📋 最佳实践

### 1. 控制器设计
- **轻量级控制器**: 业务逻辑放在服务层
- **依赖注入**: 使用 DI 容器管理依赖
- **异步编程**: 使用 `async/await` 提升性能
- **参数验证**: 使用模型验证和自定义验证

### 2. 错误处理
- **全局异常处理**: 统一错误响应格式
- **日志记录**: 记录关键操作和错误信息
- **用户友好**: 不暴露敏感的系统信息
- **状态码规范**: 使用恰当的 HTTP 状态码

### 3. 性能优化
- **缓存策略**: Response Caching 和 Memory Cache
- **数据库优化**: 使用 EF Core 查询优化
- **异步操作**: 避免阻塞 I/O 操作
- **分页查询**: 大数据量时使用分页

### 4. 安全考虑
- **输入验证**: 严格验证所有用户输入
- **SQL 注入防护**: 使用参数化查询
- **XSS 防护**: 自动 HTML 编码
- **认证授权**: 保护敏感 API 端点

---

## 🔗 相关文档

- **[项目架构](../architecture/)** - 整体架构设计
- **[前端集成](../frontend/)** - Vue.js 前端对接
- **[开发指南](../guides/)** - 具体开发教程

---

## 📈 扩展计划

### 即将添加的后端功能:
- **数据库集成** - Entity Framework Core
- **用户认证系统** - JWT + Identity
- **文件上传处理** - IFormFile 和文件存储
- **实时通信** - SignalR 集成
- **缓存策略** - Redis 和 Memory Cache
- **后台任务** - Hangfire 或 Background Services
- **API 文档** - Swagger/OpenAPI 自动生成
- **健康检查** - Health Checks 和监控
- **单元测试** - xUnit 测试框架
- **Docker 部署** - 容器化部署方案
