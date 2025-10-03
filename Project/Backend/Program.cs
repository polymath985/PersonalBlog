using PersonalBlog.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // 检查是否需要清空数据库
            bool clearDatabase = args.Contains("--clear-db") || args.Contains("--cleardb");

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // 获取数据库连接字符串
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? "Data Source=DataBase/PersonalBlog.db";


            // 配置DbContext以使用SQLite数据库
            builder.Services.AddDbContext<DataContext>(options =>
                options.UseSqlite(connectionString));

            // 添加CORS服务
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowVueApp", policy =>
                {
                    policy.WithOrigins("https://localhost:5156", "http://localhost:5173", "https://localhost:5173") // Vite HTTPS 和 HTTP 端口
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // 处理数据库清空选项
            if (clearDatabase)
            {
                using (var scope = app.Services.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
                    
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("⚠️  检测到 --clear-db 参数，正在清空数据库...");
                    Console.ResetColor();
                    
                    try
                    {
                        // 删除数据库
                        dbContext.Database.EnsureDeleted();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("✓ 数据库已删除");
                        Console.ResetColor();
                        
                        // 重新创建数据库
                        dbContext.Database.EnsureCreated();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("✓ 数据库已重新创建");
                        Console.ResetColor();
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"✗ 清空数据库失败: {ex.Message}");
                        Console.ResetColor();
                    }
                }
            }
            else
            {
                // 确保数据库存在(但不会删除现有数据)
                using (var scope = app.Services.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
                    dbContext.Database.EnsureCreated();
                }
            }

            // 启用静态文件服务 (wwwroot 目录)
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            // 启用CORS
            app.UseCors("AllowVueApp");

            app.UseAuthorization();

            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}

