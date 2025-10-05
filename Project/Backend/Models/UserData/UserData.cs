using System.ComponentModel.DataAnnotations;
using Backend.Models.Blog;
using Backend.Utils;

namespace Backend.Models.UserData
{
    public class UserData
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Length(1,10)]
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public DateTime RegisterTime { get; set; } = DateTimeHelper.GetBeijingTime();
        
        // 用户头像 URL (相对路径或完整 URL)
        // 示例: "/uploads/20241005_abc123.jpg" 或 "https://cdn.example.com/avatar.jpg"
        [StringLength(500)]
        public string? Avatar { get; set; }
        
        // 个人简介/签名
        [StringLength(200)]
        public string? Bio { get; set; }
        
        // 详细介绍 (支持 Markdown)
        [StringLength(5000)]
        public string? Introduction { get; set; }
        
        // 社交链接
        [StringLength(200)]
        public string? GitHub { get; set; }
        
        [StringLength(200)]
        public string? Twitter { get; set; }
        
        [StringLength(200)]
        public string? Website { get; set; }
        
        // 技能标签 (逗号分隔)
        [StringLength(500)]
        public string? Skills { get; set; }

        // 导航属性 - 一个用户可以有多篇博客
        public ICollection<Blog.Blog> Blogs { get; set; } = new List<Blog.Blog>();
    }
}
