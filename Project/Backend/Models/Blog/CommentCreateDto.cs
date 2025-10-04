using System.ComponentModel.DataAnnotations;

namespace Backend.Models.Blog
{
    // 创建评论 DTO
    public class CommentCreateDto
    {
        [Required(ErrorMessage = "博客ID不能为空")]
        public Guid BlogId { get; set; }
        
        [Required(ErrorMessage = "用户ID不能为空")]
        public Guid UserId { get; set; }
        
        [Required(ErrorMessage = "评论内容不能为空")]
        [MaxLength(1000, ErrorMessage = "评论内容不能超过1000个字符")]
        public string Content { get; set; } = string.Empty;
        
        public Guid? ParentCommentId { get; set; } // 可选，用于回复评论
    }
}
