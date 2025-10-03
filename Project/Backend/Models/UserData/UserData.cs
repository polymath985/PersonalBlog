using System.ComponentModel.DataAnnotations;
using Backend.Models.Blog;
using Backend.Utils;

namespace Backend.Models.UserData
{
    public class UserData
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Length(1,10)]
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public DateTime RegisterTime { get; set; } = DateTimeHelper.GetBeijingTime();

        // 导航属性 - 一个用户可以有多篇博客
        public ICollection<Blog.Blog> Blogs { get; set; } = new List<Blog.Blog>();
    }
}
