using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Backend.Models;

namespace PersonalBlog.Models
{
    /// <summary>
    /// 内容索引表 - 记录用户拥有的所有内容ID
    /// </summary>
    [Table("ContentIndexes")]
    public class ContentIndex
    {
        /// <summary>
        /// 主键 - 自增ID
        /// </summary>
        [Key]
        public int Id { get; set; }
        
        /// <summary>
        /// 用户ID - 外键
        /// </summary>
        [Required]
        public int UserId { get; set; }
        
        /// <summary>
        /// 内容ID - 外键
        /// </summary>
        [Required]
        public int ContentId { get; set; }
        
        /// <summary>
        /// 内容类型 (article, project, note, portfolio等)
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string ContentType { get; set; } = string.Empty;
        
        /// <summary>
        /// 是否为特色内容
        /// </summary>
        public bool IsFeatured { get; set; } = false;
        
        /// <summary>
        /// 排序权重 (数字越大越靠前)
        /// </summary>
        public int SortOrder { get; set; } = 0;
        
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        // 导航属性
        public UserData? User { get; set; }
        public ContentAttributes? Content { get; set; }
    }
}