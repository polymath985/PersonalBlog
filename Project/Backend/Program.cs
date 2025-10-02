using PersonalBlog.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
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

