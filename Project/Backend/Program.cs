using Backend.Managers;
using PersonalBlog.DataBase;
using PersonalBlog.Services;
using Microsoft.EntityFrameworkCore;

namespace Backend
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            
            // 获取数据库连接字符串
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
                ?? "Data Source=DataBase/PersonalBlog.db";
            
            // 配置Entity Framework
            builder.Services.AddDbContext<BlogDbContext>(options =>
                options.UseSqlite(connectionString));
            
            // 注册原有服务（UserDataManager需要依赖注入）
            builder.Services.AddScoped<UserDataManager>();
            
            // 注册内容管理服务
            builder.Services.AddScoped<ContentService>();
            
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
            
            // 初始化数据库
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<BlogDbContext>();
                await context.Database.EnsureCreatedAsync();
                
                // 可选：添加种子数据
                await SeedDataAsync(context);
            }

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
        
        /// <summary>
        /// 添加种子数据
        /// </summary>
        private static async Task SeedDataAsync(BlogDbContext context)
        {
            // 检查是否已有数据
            if (await context.ContentAttributes.AnyAsync())
                return;
                
            // 首先添加示例用户
            var sampleUser = new Backend.Models.UserData
            {
                Name = "管理员",
                Account = "admin",
                Email = "admin@blog.com",
                Password = "admin123", // 注意：实际应用中需要加密
                RegisterTime = DateTime.Now
            };
            
            context.Users.Add(sampleUser);
            await context.SaveChangesAsync();
                
            // 添加示例内容
            var sampleContent = new PersonalBlog.Models.ContentAttributes
            {
                Title = "欢迎来到我的博客",
                Description = "这是一个使用Vue.js和ASP.NET Core构建的个人博客系统",
                Icon = "🚀",
                Badge = "New",
                Tags = new List<string> { "博客", "技术", "分享" },
                Featured = true,
                ContentText = "欢迎访问我的个人博客！",
                ClickAction = "/posts/welcome",
                UpdateTime = "刚刚",
                Stats = new PersonalBlog.Models.ContentStats { Views = 0, Likes = 0, Comments = 0 }
            };
            
            context.ContentAttributes.Add(sampleContent);
            await context.SaveChangesAsync();
            
            // 添加内容索引（使用刚创建的用户ID）
            var contentIndex = new PersonalBlog.Models.ContentIndex
            {
                UserId = sampleUser.Id,
                ContentId = sampleContent.ContentId,
                ContentType = "article",
                IsFeatured = true
            };
            
            context.ContentIndexes.Add(contentIndex);
            await context.SaveChangesAsync();
        }
    }
}
