using VueApp1.Server.Managers;
using VueApp1.Server.Examples;

namespace VueApp1.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // 检查是否运行CRUD测试
            if (args.Length > 0 && args[0] == "test-crud")
            {
                Console.WriteLine("开始运行UserData CRUD操作测试...\n");
                
                // 运行CRUD示例
                UserDataExample.RunExample();
                
                // 运行错误情况测试
                UserDataExample.TestErrorCases();
                
                Console.WriteLine("测试完成，按任意键退出...");
                Console.ReadKey();
                return;
            }

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
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

            app.UseAuthorization();


            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
