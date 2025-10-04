namespace Backend.Models.Blog
{
    public class CommentDto
    {
        public required Guid Id { get; set; }
        public required Guid BlogId { get; set; }
        public required Guid UserId { get; set; }
        public required string Content { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required int Likes { get; set; }
    }
}