# 依赖注入最佳实践

## 📋 概述

ASP.NET Core内置的依赖注入容器提供了三种服务生命周期，合理选择是构建可维护应用的关键。

## 🔄 服务生命周期

### 1. Transient（瞬态）
- **特点**：每次请求都创建新实例
- **适用场景**：轻量级、无状态的服务
- **注册方式**：`AddTransient<TInterface, TImplementation>()`

### 2. Scoped（作用域）
- **特点**：在同一个请求内共享实例
- **适用场景**：数据库上下文、请求相关的服务
- **注册方式**：`AddScoped<TInterface, TImplementation>()`

### 3. Singleton（单例）
- **特点**：整个应用程序生命周期内只有一个实例
- **适用场景**：配置服务、缓存、共享状态管理
- **注册方式**：`AddSingleton<TInterface, TImplementation>()`

## 🎯 选择指南

### 决策树
```
是否需要在多个请求间共享状态？
├─ 是 → Singleton
└─ 否 → 是否需要在同一请求内共享状态？
    ├─ 是 → Scoped  
    └─ 否 → Transient
```

### 常见场景分类

| 服务类型 | 推荐生命周期 | 原因 |
|---------|-------------|------|
| 数据库上下文 | Scoped | 请求内事务一致性 |
| HTTP客户端 | Singleton | 连接池复用 |
| 缓存服务 | Singleton | 全局数据共享 |
| 业务服务 | Scoped/Transient | 取决于状态需求 |
| 配置服务 | Singleton | 配置数据不变 |

## 🏗️ 注册示例

### Program.cs配置
```csharp
var builder = WebApplication.CreateBuilder(args);

// Singleton - 全局共享状态
builder.Services.AddSingleton<IUserDataService, UserDataService>();
builder.Services.AddSingleton<ICacheService, MemoryCacheService>();

// Scoped - 请求级别共享
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IEmailService, EmailService>();

// Transient - 每次创建新实例
builder.Services.AddTransient<ILoggerService, LoggerService>();
builder.Services.AddTransient<IValidationService, ValidationService>();

var app = builder.Build();
```

## ⚠️ 常见陷阱

### 1. 生命周期不匹配
```csharp
// ❌ 错误：Singleton中注入Scoped服务
public class UserDataService // Singleton
{
    private readonly IDbContext _context; // Scoped - 危险！
    
    public UserDataService(IDbContext context)
    {
        _context = context; // 可能导致内存泄漏
    }
}

// ✅ 正确：使用IServiceProvider按需获取
public class UserDataService
{
    private readonly IServiceProvider _serviceProvider;
    
    public UserDataService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public void DoSomething()
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IDbContext>();
        // 使用context...
    }
}
```

### 2. Singleton的线程安全
```csharp
// ❌ 不安全的Singleton
public class CounterService
{
    private int _count = 0;
    
    public int Increment() => ++_count; // 竞态条件
}

// ✅ 线程安全的Singleton
public class CounterService
{
    private int _count = 0;
    
    public int Increment() => Interlocked.Increment(ref _count);
}
```

## 🔧 实用技巧

### 1. 条件注册
```csharp
// 根据环境注册不同实现
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSingleton<IEmailService, MockEmailService>();
}
else
{
    builder.Services.AddSingleton<IEmailService, SmtpEmailService>();
}
```

### 2. 工厂模式注册
```csharp
// 复杂对象创建
builder.Services.AddSingleton<IUserDataService>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("Default");
    return new UserDataService(connectionString);
});
```

### 3. 装饰器模式
```csharp
// 基础服务
builder.Services.AddScoped<IUserService, UserService>();

// 添加缓存装饰器
builder.Services.Decorate<IUserService, CachedUserService>();

// 添加日志装饰器
builder.Services.Decorate<IUserService, LoggedUserService>();
```

## 📚 相关文档

- [Controller生命周期](./Controller-Lifecycle.md)
- [Singleton与Controller配合](./Singleton-Controller.md)
