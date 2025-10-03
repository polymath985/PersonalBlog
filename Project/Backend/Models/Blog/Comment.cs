using Backend.Utils;

namespace Backend.Models.Blog
{
    public class Comment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required Guid BlogId { get; set; }
        public required Guid UserId { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTimeHelper.GetBeijingTime();
        public int Likes { get; set; } = 0;
        // 导航属性 - 关联的博客
        public Blog? Blog { get; set; }
    }
}