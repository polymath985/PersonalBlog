using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Backend.Utils;
using UserDataModel = Backend.Models.UserData.UserData;

namespace Backend.Models.Blog
{
    public class Blog
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        
        [Length(1, 20)]
        public required string Title { get; set; }

        [Length(1,5000)]
        public required string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTimeHelper.GetBeijingTime();

        public DateTime UpdatedAt { get; set; } = DateTimeHelper.GetBeijingTime();

        public string Tags { get; set; } = string.Empty;

        public int Likes { get; set; } = 0;

        public int CommentsCount { get; set; } = 0;

        public int Views { get; set; } = 0;

        // 外键属性
        [ForeignKey(nameof(Author))]
        public required Guid AuthorId { get; set; }

        // 导航属性 - 指向父表 UserData
        public UserDataModel? Author { get; set; }
    }
}