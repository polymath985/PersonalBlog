namespace Backend.Models.Blog
{
    // 更新评论 DTO
    public class CommentUpdateDto
    {
        public required Guid UserId { get; set; } // 用于验证权限
        public required string Content { get; set; }
    }
}
