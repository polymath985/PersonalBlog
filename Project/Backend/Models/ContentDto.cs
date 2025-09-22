namespace PersonalBlog.Models
{
    /// <summary>
    /// 内容传输对象 - 用于API数据传输
    /// 完全对应前端 Props 接口
    /// </summary>
    public class ContentDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Icon { get; set; }
        public string? Badge { get; set; }
        public List<string>? Tags { get; set; }
        public bool? Featured { get; set; }
        public ContentStatsDto? Stats { get; set; }
        public string? UpdateTime { get; set; }
        public string? ClickAction { get; set; }
    }
    
    /// <summary>
    /// 统计信息传输对象
    /// </summary>
    public class ContentStatsDto
    {
        public int? Views { get; set; }
        public int? Likes { get; set; }
        public int? Comments { get; set; }
    }
    
    /// <summary>
    /// 创建内容请求对象
    /// </summary>
    public class CreateContentRequest
    {
        public int UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public string? Icon { get; set; }
        public string? Badge { get; set; }
        public List<string>? Tags { get; set; }
        public bool Featured { get; set; } = false;
        public string? ContentText { get; set; }
        public string? ClickAction { get; set; }
    }
    
    /// <summary>
    /// 更新内容请求对象
    /// </summary>
    public class UpdateContentRequest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Icon { get; set; }
        public string? Badge { get; set; }
        public List<string>? Tags { get; set; }
        public bool? Featured { get; set; }
        public string? ContentText { get; set; }
        public string? ClickAction { get; set; }
        public ContentStatsDto? Stats { get; set; }
    }
}