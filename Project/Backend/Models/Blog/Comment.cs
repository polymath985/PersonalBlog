using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Backend.Utils;
using UserDataModel = Backend.Models.UserData.UserData;

namespace Backend.Models.Blog
{
    public class Comment
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        
        // 外键 - 关联博客
        [ForeignKey(nameof(Blog))]
        public required Guid BlogId { get; set; }
        
        // 外键 - 关联用户
        [ForeignKey(nameof(User))]
        public required Guid UserId { get; set; }
        
        // 评论内容（限制长度）
        [Required]
        [MaxLength(1000)]
        public required string Content { get; set; }
        
        // 创建时间
        public DateTime CreatedAt { get; set; } = DateTimeHelper.GetBeijingTime();
        
        // 点赞数
        public int Likes { get; set; } = 0;
        
        // 父评论ID（用于回复功能，可选）
        [ForeignKey(nameof(ParentComment))]
        public Guid? ParentCommentId { get; set; }
        
        // 导航属性 - 关联的博客
        public Blog? Blog { get; set; }
        
        // 导航属性 - 评论作者
        public UserDataModel? User { get; set; }
        
        // 导航属性 - 父评论（用于回复）
        public Comment? ParentComment { get; set; }
        
        // 导航属性 - 子评论列表（回复列表）
        public ICollection<Comment>? Replies { get; set; }
    }
}