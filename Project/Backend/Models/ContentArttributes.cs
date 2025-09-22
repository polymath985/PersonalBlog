using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class ContentAttributes
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        [StringLength(50)]
        public string? Icon { get; set; }

        [StringLength(50)]
        public string? Badge { get; set; }

        public string? Tags { get; set; } // JSON格式存储字符串数组

        public bool Featured { get; set; } = false;

        // 统计信息
        public int Views { get; set; } = 0;
        public int Likes { get; set; } = 0;
        public int Comments { get; set; } = 0;

        [StringLength(50)]
        public string? UpdateTime { get; set; }

        [StringLength(500)]
        public string? ClickAction { get; set; } // 存储路由路径或动作类型

        // 审计字段
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [StringLength(100)]
        public string? CreatedBy { get; set; }

        [StringLength(100)]
        public string? UpdatedBy { get; set; }
    }
}