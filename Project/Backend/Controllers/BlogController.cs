using Backend.Models.Blog;
using Backend.Utils;
using Microsoft.AspNetCore.Mvc;
using PersonalBlog.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogController(DataContext _dbcontext) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<ActionResult<Blog>> CreateBlogAsync([FromBody] BlogCreatedDto blog)
        {
            var newBlog = new Blog
            {
                Title = blog.Title,
                Content = blog.Content,
                Tags = blog.Tags,
                AuthorId = blog.AuthorId
            };

            await _dbcontext.Blogs.AddAsync(newBlog);
            await _dbcontext.SaveChangesAsync();
            return Ok(newBlog);
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<object>>> GetAllBlogsByUserIdAsync([FromQuery] Guid? userId = null)
        {
            List<Blog> blogList;
            
            // 如果没有传入 userId 参数,返回所有博客
            if (!userId.HasValue)
            {
                blogList = await _dbcontext.Blogs
                    .Include(b => b.Author) // 包含作者信息
                    .OrderByDescending(b => b.CreatedAt) // 按创建时间倒序排列
                    .ToListAsync();
            }
            else
            {
                // 如果传入了 userId,返回该用户的博客
                blogList = await _dbcontext.Blogs
                    .Include(b => b.Author) // 包含作者信息
                    .Where(b => b.AuthorId == userId.Value)
                    .OrderByDescending(b => b.CreatedAt)
                    .ToListAsync();
            }
            
            // 返回包含统计数据的博客列表
            var result = blogList.Select(blog => new
            {
                id = blog.Id,
                title = blog.Title,
                content = blog.Content,
                tags = blog.Tags,
                createdAt = blog.CreatedAt,
                updatedAt = blog.UpdatedAt,
                authorId = blog.AuthorId,
                authorName = blog.Author?.Name ?? "未知作者",
                likes = blog.Likes,
                views = blog.Views,
                commentsCount = blog.CommentsCount
            });
            
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<object>> GetBlogByIdAsync([FromQuery] Guid id)
        {
            var blog = await _dbcontext.Blogs
                .Include(b => b.Author) // 包含作者信息
                .FirstOrDefaultAsync(b => b.Id == id);
            
            if (blog == null)
            {
                return NotFound("Blog not found.");
            }
            
            // 返回包含作者名称和统计数据的博客信息
            var result = new
            {
                id = blog.Id,
                title = blog.Title,
                content = blog.Content,
                tags = blog.Tags,
                createdAt = blog.CreatedAt,
                updatedAt = blog.UpdatedAt,
                authorId = blog.AuthorId,
                authorName = blog.Author?.Name ?? "未知作者", // 返回作者名称
                likes = blog.Likes,
                views = blog.Views,
                commentsCount = blog.CommentsCount
            };
            
            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<Blog>> UpdateBlogAsync([FromBody] Blog blog)
        {
            var existingBlog = await _dbcontext.Blogs.FindAsync(blog.Id);
            if (existingBlog == null)
            {
                return NotFound("Blog not found.");
            }

            existingBlog.Title = blog.Title;
            existingBlog.Content = blog.Content;
            existingBlog.Tags = blog.Tags;
            existingBlog.UpdatedAt = DateTimeHelper.GetBeijingTime(); // 更新为当前北京时间

            await _dbcontext.SaveChangesAsync();
            return Ok(existingBlog);
        }

        // 增加浏览量
        [HttpPost("{id}/view")]
        public async Task<ActionResult> IncrementViewsAsync(Guid id)
        {
            var blog = await _dbcontext.Blogs.FindAsync(id);
            if (blog == null)
            {
                return NotFound("Blog not found.");
            }

            blog.Views++;
            await _dbcontext.SaveChangesAsync();
            return Ok(new { views = blog.Views });
        }

        // 增加点赞数
        [HttpPost("{id}/like")]
        public async Task<ActionResult> IncrementLikesAsync(Guid id)
        {
            var blog = await _dbcontext.Blogs.FindAsync(id);
            if (blog == null)
            {
                return NotFound("Blog not found.");
            }

            blog.Likes++;
            await _dbcontext.SaveChangesAsync();
            return Ok(new { likes = blog.Likes });
        }
    }
}