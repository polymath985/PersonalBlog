using Backend.Models;
using Backend.Models.UserData;
using Backend.Models.Blog;
using Microsoft.EntityFrameworkCore;

namespace PersonalBlog.DataBase
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public DbSet<UserData> Users { get; set; }
        public DbSet<Blog> Blogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 配置 Blog 和 UserData 的一对多关系
            modelBuilder.Entity<Blog>()
                .HasOne(b => b.Author)              // Blog 有一个 Author
                .WithMany(u => u.Blogs)              // UserData 有多个 Blogs
                .HasForeignKey(b => b.AuthorId)      // 外键是 AuthorId
                .OnDelete(DeleteBehavior.ClientCascade);  // 删除用户时级联删除其博客
        }
    }

}