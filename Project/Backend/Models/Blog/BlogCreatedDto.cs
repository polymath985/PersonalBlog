namespace Backend.Models.Blog
{
    public class BlogCreatedDto
    {
        public required Guid AuthorId { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public string Tags { get; set; } = string.Empty;
    }
}