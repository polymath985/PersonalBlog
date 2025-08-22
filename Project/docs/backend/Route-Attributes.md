# ASP.NET Core Route 特性详解

## 🛣️ Route 特性的作用

`[Route]` 特性用于定义 URL 路由规则，决定客户端请求如何映射到控制器的方法。

## 📝 基本语法

### 1. 控制器级别的 Route

```csharp
[Route("[controller]")]  // 模板语法
public class WeatherForecastController : ControllerBase
{
    // 控制器方法
}
```

**解释**：
- `[controller]` 是一个**令牌替换**（Token Replacement）
- 系统会自动将 `WeatherForecastController` 中的 `WeatherForecast` 部分提取出来
- 转换为小写：`weatherforecast`
- 最终路由：`/weatherforecast`

### 2. 路由令牌替换规则

```csharp
// 控制器名称：WeatherForecastController
[Route("[controller]")]     → /weatherforecast

// 控制器名称：UserController  
[Route("[controller]")]     → /user

// 控制器名称：ProductController
[Route("[controller]")]     → /product
```

**规则**：
1. 去掉 `Controller` 后缀
2. 转换为小写
3. 驼峰命名转换为小写（WeatherForecast → weatherforecast）

## 🔧 常见的 Route 用法

### 1. 带前缀的路由

```csharp
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        // 访问路径：GET /api/weatherforecast
    }
}
```

### 2. 固定路由

```csharp
[Route("api/weather")]  // 固定路径，不使用控制器名称
public class WeatherForecastController : ControllerBase
{
    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        // 访问路径：GET /api/weather
    }
}
```

### 3. 方法级别的路由

```csharp
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    [HttpGet]                    // GET /api/weatherforecast
    public IEnumerable<WeatherForecast> GetAll() { }

    [HttpGet("today")]           // GET /api/weatherforecast/today
    public WeatherForecast GetToday() { }

    [HttpGet("{id}")]            // GET /api/weatherforecast/123
    public WeatherForecast GetById(int id) { }

    [HttpGet("{id}/details")]    // GET /api/weatherforecast/123/details
    public object GetDetails(int id) { }
}
```

### 4. 参数路由

```csharp
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    // URL: /api/weatherforecast/123
    [HttpGet("{id}")]
    public WeatherForecast GetById(int id)
    {
        // id 参数会自动从 URL 中绑定
        return GetWeatherById(id);
    }

    // URL: /api/weatherforecast/2024/08/20
    [HttpGet("{year}/{month}/{day}")]
    public WeatherForecast GetByDate(int year, int month, int day)
    {
        var date = new DateTime(year, month, day);
        return GetWeatherByDate(date);
    }

    // URL: /api/weatherforecast/search?city=北京&date=2024-08-20
    [HttpGet("search")]
    public IEnumerable<WeatherForecast> Search([FromQuery] string city, [FromQuery] DateTime date)
    {
        // city 和 date 从查询字符串中绑定
        return SearchWeather(city, date);
    }
}
```

### 5. 约束参数

```csharp
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    // 只匹配数字 ID
    [HttpGet("{id:int}")]
    public WeatherForecast GetById(int id) { }

    // 只匹配 GUID
    [HttpGet("{id:guid}")]
    public WeatherForecast GetByGuid(Guid id) { }

    // 字符串长度约束
    [HttpGet("{city:alpha:minlength(2):maxlength(50)}")]
    public IEnumerable<WeatherForecast> GetByCity(string city) { }

    // 可选参数
    [HttpGet("{id:int?}")]
    public IEnumerable<WeatherForecast> GetOptional(int? id) { }
}
```

## 🌐 实际访问示例

假设你的应用运行在 `https://localhost:5001`：

### 控制器路由示例

```csharp
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    [HttpGet]
    public string Test() => "Hello from WeatherForecast";
}
```

**访问 URL**: `https://localhost:5001/weatherforecast`

### API 前缀示例

```csharp
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    [HttpGet]
    public string Test() => "Hello from API";
}
```

**访问 URL**: `https://localhost:5001/api/weatherforecast`

### 方法级路由组合

```csharp
[Route("api/weather")]  // 控制器基础路由
public class WeatherForecastController : ControllerBase
{
    [HttpGet]              // GET /api/weather
    public string GetAll() => "All weather data";

    [HttpGet("current")]   // GET /api/weather/current  
    public string GetCurrent() => "Current weather";

    [HttpGet("{id}")]      // GET /api/weather/123
    public string GetById(int id) => $"Weather for ID: {id}";
}
```

## 🔄 路由匹配过程

当客户端发送请求时，ASP.NET Core 的路由系统会：

1. **解析 URL**: 分解请求的 URL 路径
2. **匹配模式**: 按照注册顺序匹配路由模板
3. **提取参数**: 从 URL 中提取路由参数
4. **绑定方法**: 调用匹配的控制器方法
5. **参数注入**: 将提取的参数注入到方法参数中

### 匹配示例

```
请求: GET /api/weatherforecast/123

匹配过程:
1. 查找控制器: WeatherForecastController
2. 查找路由模板: api/[controller]/{id:int}
3. 参数提取: id = 123
4. 方法调用: GetById(123)
```

## 📋 Route 特性的完整语法

```csharp
[Route(
    template: "api/[controller]/{action=Index}/{id?}",  // 路由模板
    Name = "WeatherRoute",                              // 路由名称
    Order = 1                                          // 路由优先级
)]
```

### 模板语法元素

- `{parameter}` - 必需参数
- `{parameter?}` - 可选参数  
- `{parameter=default}` - 带默认值的参数
- `{parameter:constraint}` - 带约束的参数
- `[controller]` - 控制器名称令牌
- `[action]` - 方法名称令牌

### 约束类型

```csharp
{id:int}          // 整数
{id:guid}         // GUID
{name:alpha}      // 字母
{code:length(5)}  // 固定长度
{text:minlength(3)} // 最小长度
{text:maxlength(50)} // 最大长度  
{price:decimal}   // 小数
{active:bool}     // 布尔值
{date:datetime}   // 日期时间
```

## 🎯 最佳实践

### 1. RESTful API 设计

```csharp
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    [HttpGet]                    // GET /api/products
    public IEnumerable<Product> GetAll() { }

    [HttpGet("{id}")]           // GET /api/products/1
    public Product GetById(int id) { }

    [HttpPost]                  // POST /api/products
    public IActionResult Create([FromBody] Product product) { }

    [HttpPut("{id}")]          // PUT /api/products/1
    public IActionResult Update(int id, [FromBody] Product product) { }

    [HttpDelete("{id}")]       // DELETE /api/products/1
    public IActionResult Delete(int id) { }
}
```

### 2. 版本控制

```csharp
[Route("api/v1/[controller]")]
public class WeatherV1Controller : ControllerBase { }

[Route("api/v2/[controller]")]  
public class WeatherV2Controller : ControllerBase { }
```

### 3. 嵌套资源

```csharp
[Route("api/users/{userId}/posts")]
public class UserPostsController : ControllerBase
{
    [HttpGet]                    // GET /api/users/123/posts
    public IEnumerable<Post> GetUserPosts(int userId) { }

    [HttpPost]                   // POST /api/users/123/posts
    public IActionResult CreatePost(int userId, [FromBody] Post post) { }
}
```

## 🔧 调试路由

你可以通过以下方式查看和调试路由：

### 1. 启用路由日志

在 `appsettings.json` 中：

```json
{
  "Logging": {
    "LogLevel": {
      "Microsoft.AspNetCore.Routing": "Debug"
    }
  }
}
```

### 2. 使用路由调试中间件

```csharp
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        // 显示路由信息
        app.Map("/routes", builder => {
            builder.Run(async context => {
                var routes = app.ApplicationServices.GetService<IActionDescriptorCollectionProvider>();
                // 输出所有路由信息
            });
        });
    }
}
```

通过理解这些 Route 特性的用法，你就能够灵活地设计和实现 Web API 的 URL 结构了！🚀
