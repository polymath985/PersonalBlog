using Backend.Managers;
using Backend.DataBase;

namespace Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //启动时删除数据库所有表格并创建新表
            DataBaseOperator.InitializeUserDataTable();

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            
            // 添加CORS服务
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowVueApp", policy =>
                {
                    policy.WithOrigins("http://localhost:5173", "https://localhost:5173") // Vite默认端口
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });
            
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            app.UseDefaultFiles();
            app.MapStaticAssets();

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
