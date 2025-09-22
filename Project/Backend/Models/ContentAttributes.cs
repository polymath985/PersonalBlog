using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace PersonalBlog.Models
{
    /// <summary>
    /// 内容特性表 - 存储内容的所有属性信息
    /// 对应前端 Props 接口结构
    /// </summary>
    [Table("ContentAttributes")]
    public class ContentAttributes
    {
        /// <summary>
        /// 主键 - 内容ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContentId { get; set; }
        
        /// <summary>
        /// 标题 - 必填
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;
        
        /// <summary>
        /// 描述 - 必填
        /// </summary>
        [Required]
        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;
        
        /// <summary>
        /// 图标名称 (可选)
        /// </summary>
        [MaxLength(50)]
        public string? Icon { get; set; }
        
        /// <summary>
        /// 徽章文本 (可选)
        /// </summary>
        [MaxLength(50)]
        public string? Badge { get; set; }
        
        /// <summary>
        /// 标签数组 - 存储为JSON字符串 (可选)
        /// </summary>
        [Column(TypeName = "TEXT")]
        public string TagsJson { get; set; } = "[]";
        
        /// <summary>
        /// 是否为特色内容 (可选，默认false)
        /// </summary>
        public bool Featured { get; set; } = false;
        
        /// <summary>
        /// 统计信息 - 存储为JSON字符串 (可选)
        /// 包含: views, likes, comments
        /// </summary>
        [Column(TypeName = "TEXT")]
        public string StatsJson { get; set; } = "{}";
        
        /// <summary>
        /// 更新时间显示文本 (可选)
        /// </summary>
        [MaxLength(50)]
        public string? UpdateTime { get; set; }
        
        /// <summary>
        /// 点击行为 - 存储路径或行为类型 (可选)
        /// </summary>
        [MaxLength(500)]
        public string? ClickAction { get; set; }
        
        /// <summary>
        /// 内容正文 (完整内容)
        /// </summary>
        [Column(TypeName = "TEXT")]
        public string? ContentText { get; set; }
        
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        // 导航属性
        public ICollection<ContentIndex> ContentIndexes { get; set; } = new List<ContentIndex>();
        
        // 辅助属性：将JSON转换为强类型对象
        
        /// <summary>
        /// 标签列表 - 自动序列化/反序列化JSON
        /// </summary>
        [NotMapped]
        public List<string> Tags
        {
            get => string.IsNullOrEmpty(TagsJson) ? 
                   new List<string>() : 
                   JsonSerializer.Deserialize<List<string>>(TagsJson) ?? new List<string>();
            set => TagsJson = JsonSerializer.Serialize(value);
        }
        
        /// <summary>
        /// 统计信息 - 自动序列化/反序列化JSON
        /// </summary>
        [NotMapped]
        public ContentStats Stats
        {
            get => string.IsNullOrEmpty(StatsJson) ? 
                   new ContentStats() : 
                   JsonSerializer.Deserialize<ContentStats>(StatsJson) ?? new ContentStats();
            set => StatsJson = JsonSerializer.Serialize(value);
        }
    }
    
    /// <summary>
    /// 内容统计信息
    /// 对应前端 Props.stats 结构
    /// </summary>
    public class ContentStats
    {
        /// <summary>
        /// 浏览次数
        /// </summary>
        public int? Views { get; set; }
        
        /// <summary>
        /// 点赞数
        /// </summary>
        public int? Likes { get; set; }
        
        /// <summary>
        /// 评论数
        /// </summary>
        public int? Comments { get; set; }
    }
}