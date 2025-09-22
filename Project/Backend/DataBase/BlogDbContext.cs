using Microsoft.EntityFrameworkCore;
using PersonalBlog.Models;
using Backend.Models;

namespace PersonalBlog.DataBase
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) { }
        
        // 现有表
        public DbSet<UserData> Users { get; set; }
        
        // 新增表
        public DbSet<ContentIndex> ContentIndexes { get; set; }
        public DbSet<PersonalBlog.Models.ContentAttributes> ContentAttributes { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // 用户表配置
            modelBuilder.Entity<UserData>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.Name).IsRequired();
            });
            
            // 内容索引表配置
            modelBuilder.Entity<ContentIndex>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                // 复合索引：用户ID + 内容ID 唯一
                entity.HasIndex(e => new { e.UserId, e.ContentId }).IsUnique();
                
                // 单独索引
                entity.HasIndex(e => e.UserId);
                entity.HasIndex(e => e.ContentId);
                entity.HasIndex(e => e.ContentType);
                entity.HasIndex(e => e.IsFeatured);
                
                // 外键关系
                entity.HasOne<UserData>()
                      .WithMany()
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
                      
                entity.HasOne(e => e.Content)
                      .WithMany()
                      .HasForeignKey(e => e.ContentId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
            
            // 内容特性表配置
            modelBuilder.Entity<PersonalBlog.Models.ContentAttributes>(entity =>
            {
                entity.HasKey(e => e.ContentId);
                
                // 索引
                entity.HasIndex(e => e.Title);
                entity.HasIndex(e => e.Featured);
                entity.HasIndex(e => e.CreatedAt);
                entity.HasIndex(e => e.UpdatedAt);
                
                // 属性配置
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).IsRequired().HasMaxLength(1000);
                entity.Property(e => e.Icon).HasMaxLength(50);
                entity.Property(e => e.Badge).HasMaxLength(50);
                entity.Property(e => e.UpdateTime).HasMaxLength(50);
                entity.Property(e => e.ClickAction).HasMaxLength(500);
                
                // JSON字段配置
                entity.Property(e => e.TagsJson)
                      .HasColumnType("TEXT")
                      .HasDefaultValue("[]");
                      
                entity.Property(e => e.StatsJson)
                      .HasColumnType("TEXT")
                      .HasDefaultValue("{}");
                      
                entity.Property(e => e.ContentText)
                      .HasColumnType("TEXT");
            });
        }
    }
}