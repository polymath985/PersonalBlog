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
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 配置 Blog 和 UserData 的一对多关系
            modelBuilder.Entity<Blog>()
                .HasOne(b => b.Author)              // Blog 有一个 Author
                .WithMany(u => u.Blogs)              // UserData 有多个 Blogs
                .HasForeignKey(b => b.AuthorId)      // 外键是 AuthorId
                .OnDelete(DeleteBehavior.ClientCascade);  // 删除用户时级联删除其博客

            // 配置 Comment 和 Blog 的一对多关系
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Blog)
                .WithMany()
                .HasForeignKey(c => c.BlogId)
                .OnDelete(DeleteBehavior.Cascade);  // 删除博客时级联删除评论

            // 配置 Comment 和 UserData 的一对多关系
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.ClientCascade);  // 删除用户时级联删除评论

            // 配置 Comment 的自引用关系（父评论-子评论）
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.ParentComment)
                .WithMany(c => c.Replies)
                .HasForeignKey(c => c.ParentCommentId)
                .OnDelete(DeleteBehavior.Restrict);  // 防止循环删除
        }
    }

}