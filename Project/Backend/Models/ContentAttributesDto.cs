namespace Backend.Models
{
    public class ContentAttributesDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Icon { get; set; }
        public string? Badge { get; set; }
        public string[]? Tags { get; set; }
        public bool Featured { get; set; }
        public ContentStatsDto? Stats { get; set; }
        public string? UpdateTime { get; set; }
        public string? ClickAction { get; set; }
    }

    public class ContentStatsDto
    {
        public int? Views { get; set; }
        public int? Likes { get; set; }
        public int? Comments { get; set; }
    }

    public class CreateContentAttributesDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Icon { get; set; }
        public string? Badge { get; set; }
        public string[]? Tags { get; set; }
        public bool Featured { get; set; } = false;
        public string? ClickAction { get; set; }
    }

    public class UpdateContentAttributesDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Icon { get; set; }
        public string? Badge { get; set; }
        public string[]? Tags { get; set; }
        public bool? Featured { get; set; }
        public string? ClickAction { get; set; }
    }
}