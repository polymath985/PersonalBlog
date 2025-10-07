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
                authorAvatar = blog.Author?.Avatar ?? "", // 返回作者头像
                likes = blog.Likes,
                views = blog.Views,
                commentsCount = blog.CommentsCount
            });
            Console.WriteLine($"返回博客列表，数量: {blogList.Count}");
            return Ok(result);
        }

<<<<<<< Updated upstream
=======
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<object>> GetBlogsByUserIdWithPaginationAsync(
            Guid userId,
            [FromQuery] int pageSize = 10,
            [FromQuery] int page = 1,
            [FromQuery] string sortBy = "createdAt")
        {
            // 构建基础查询
            var query = _dbcontext.Blogs
                .Include(b => b.Author)
                .Where(b => b.AuthorId == userId);

            // 根据 sortBy 参数排序
            query = sortBy.ToLower() switch
            {
                "views" => query.OrderByDescending(b => b.Views),
                "likes" => query.OrderByDescending(b => b.Likes),
                "comments" => query.OrderByDescending(b => b.CommentsCount),
                _ => query.OrderByDescending(b => b.CreatedAt)
            };

            // 获取总数
            var totalBlogs = await query.CountAsync();

            // 分页
            var blogList = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // 返回包含统计数据的博客列表
            var result = new
            {
                blogs = blogList.Select(blog => new
                {
                    id = blog.Id,
                    title = blog.Title,
                    content = blog.Content,
                    tags = blog.Tags,
                    createdAt = blog.CreatedAt,
                    updatedAt = blog.UpdatedAt,
                    authorId = blog.AuthorId,
                    authorName = blog.Author?.Name ?? "未知作者",
                    authorAvatar = blog.Author?.Avatar ?? "",
                    likes = blog.Likes,
                    views = blog.Views,
                    commentsCount = blog.CommentsCount,
                    commentCount = blog.CommentsCount // 保持兼容性
                }),
                totalBlogs,
                page,
                pageSize
            };

            Console.WriteLine($"返回用户 {userId} 的博客列表，数量: {blogList.Count}");
            return Ok(result);
        }

>>>>>>> Stashed changes
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

            // 返回包含作者名称、头像和统计数据的博客信息
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
                authorAvatar = blog.Author?.Avatar ?? "", // 返回作者头像
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

        // 切换点赞状态（点赞/取消点赞）
        [HttpPost("{id}/like")]
        public async Task<ActionResult> ToggleLikeAsync(Guid id, [FromBody] LikeActionDto action)
        {
            var blog = await _dbcontext.Blogs.FindAsync(id);
            if (blog == null)
            {
                return NotFound("Blog not found.");
            }

            if (action.IsLiked)
            {
                // 点赞
                blog.Likes++;
            }
            else
            {
                // 取消点赞
                if (blog.Likes > 0)
                {
                    blog.Likes--;
                }
            }

            await _dbcontext.SaveChangesAsync();
            return Ok(new { likes = blog.Likes, isLiked = action.IsLiked });
        }

        // 搜索博客建议 - 根据标题或标签模糊查询
        [HttpGet("search/suggestions")]
        public async Task<ActionResult<IEnumerable<object>>> GetBlogSuggestions(
            [FromQuery] string query,
            [FromQuery] int limit = 5)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest("Search query cannot be empty.");
            }

            var suggestions = await _dbcontext.Blogs
                .Include(b => b.Author)
                .Where(b => b.Title.ToLower().Contains(query.ToLower()) ||
                           (b.Tags != null && b.Tags.ToLower().Contains(query.ToLower())))
                .OrderByDescending(b => b.Views)
                .Take(limit)
                .Select(b => new
                {
                    id = b.Id,
                    title = b.Title,
                    tags = b.Tags,
                    authorName = b.Author != null ? b.Author.Name : "未知作者",
                    views = b.Views
                })
                .ToListAsync();

            Console.WriteLine($"搜索博客建议 '{query}', 找到 {suggestions.Count} 个结果");
            return Ok(suggestions);
        }

        // 搜索标签建议 - 获取所有唯一标签
        [HttpGet("tags/suggestions")]
        public async Task<ActionResult<IEnumerable<string>>> GetTagSuggestions(
            [FromQuery] string query,
            [FromQuery] int limit = 5)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest("Search query cannot be empty.");
            }

            // 获取所有博客的标签
            var allTags = await _dbcontext.Blogs
                .Where(b => b.Tags != null && b.Tags.ToLower().Contains(query.ToLower()))
                .Select(b => b.Tags)
                .ToListAsync();

            // 解析并去重标签
            var tagSet = new HashSet<string>();
            foreach (var tags in allTags)
            {
                if (string.IsNullOrWhiteSpace(tags)) continue;

                var tagArray = tags.Split(',', StringSplitOptions.RemoveEmptyEntries);
                foreach (var tag in tagArray)
                {
                    var trimmedTag = tag.Trim();
                    if (trimmedTag.ToLower().Contains(query.ToLower()))
                    {
                        tagSet.Add(trimmedTag);
                    }
                }
            }

            var suggestions = tagSet.Take(limit).ToList();
            Console.WriteLine($"搜索标签建议 '{query}', 找到 {suggestions.Count} 个结果");
            return Ok(suggestions);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBlogAsync(Guid id)
        {
            var blog = await _dbcontext.Blogs.FindAsync(id);
            if (blog == null)
            {
                return NotFound("Blog not found.");
            }

            // 先删除该博客的所有评论（包括子评论）
            var comments = await _dbcontext.Comments
                .Where(c => c.BlogId == id)
                .ToListAsync();
            
            if (comments.Any())
            {
                _dbcontext.Comments.RemoveRange(comments);
            }

            // 然后删除博客
            _dbcontext.Blogs.Remove(blog);
            
            // 保存更改
            await _dbcontext.SaveChangesAsync();
            
            return NoContent();
        }
    }
}