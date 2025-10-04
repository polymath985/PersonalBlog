using Backend.Models.Blog;
using Backend.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalBlog.DataBase;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController(DataContext _dbContext) : ControllerBase
    {
        // 创建评论
        [HttpPost("create")]
        public async Task<ActionResult<object>> CreateCommentAsync([FromBody] CommentCreateDto commentDto)
        {
            try
            {
                Console.WriteLine("开始创建评论");
                // 验证模型
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // 验证博客是否存在
                var blog = await _dbContext.Blogs.FindAsync(commentDto.BlogId);
                if (blog == null)
                {
                    return NotFound(new { message = "博客不存在" });
                }

                // 验证用户是否存在
                var user = await _dbContext.Users.FindAsync(commentDto.UserId);
                if (user == null)
                {
                    return NotFound(new { message = "用户不存在" });
                }

                // 如果是回复评论，验证父评论是否存在
                if (commentDto.ParentCommentId.HasValue && commentDto.ParentCommentId.Value != Guid.Empty)
                {
                    var parentComment = await _dbContext.Comments.FindAsync(commentDto.ParentCommentId.Value);
                    if (parentComment == null)
                    {
                        return NotFound(new { message = "父评论不存在" });
                    }
                    if (parentComment.BlogId != commentDto.BlogId)
                    {
                        return BadRequest(new { message = "父评论不属于该博客" });
                    }
                }

                Console.WriteLine("验证通过，准备创建评论");
                // 创建新评论
                var comment = new Comment
                {
                    BlogId = commentDto.BlogId,
                    UserId = commentDto.UserId,
                    Content = commentDto.Content.Trim(),
                    ParentCommentId = commentDto.ParentCommentId
                };

                await _dbContext.Comments.AddAsync(comment);
                Console.WriteLine($"创建评论成功，评论ID: {comment.Id}");
                // 更新博客的评论数
                blog.CommentsCount++;
                
                await _dbContext.SaveChangesAsync();

                return Ok(new
                {
                    id = comment.Id,
                    blogId = comment.BlogId,
                    userId = comment.UserId,
                    userName = user.Name,
                    content = comment.Content,
                    createdAt = comment.CreatedAt,
                    likes = comment.Likes,
                    parentCommentId = comment.ParentCommentId
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"创建评论时发生错误: {ex.Message}");
                Console.WriteLine($"堆栈跟踪: {ex.StackTrace}");
                return StatusCode(500, new { message = "服务器内部错误", error = ex.Message });
            }
        }

        // 获取博客的所有评论（包含回复）
        [HttpGet("blog/{blogId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetCommentsByBlogIdAsync(Guid blogId)
        {
            // 验证博客是否存在
            var blogExists = await _dbContext.Blogs.AnyAsync(b => b.Id == blogId);
            if (!blogExists)
            {
                return NotFound("博客不存在");
            }

            // 获取所有评论（包括顶级和回复）
            var allComments = await _dbContext.Comments
                .Include(c => c.User)
                .Where(c => c.BlogId == blogId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

            // 在内存中筛选顶级评论
            var topLevelComments = allComments
                .Where(c => c.ParentCommentId == null)
                .ToList();

            // 手动构建评论树（在内存中关联回复）
            foreach (var comment in allComments.Where(c => c.ParentCommentId.HasValue))
            {
                var parent = allComments.FirstOrDefault(c => c.Id == comment.ParentCommentId);
                if (parent != null)
                {
                    // 初始化为 List 以便后续操作
                    if (parent.Replies == null)
                    {
                        parent.Replies = new List<Comment>();
                    }
                    parent.Replies.Add(comment);
                }
            }

            // 递归构建评论树
            var result = topLevelComments.Select(c => BuildCommentTree(c)).ToList();

            return Ok(result);
        }

        // 递归构建评论树的辅助方法
        private object BuildCommentTree(Comment comment)
        {
            return new
            {
                id = comment.Id,
                blogId = comment.BlogId,
                userId = comment.UserId,
                userName = comment.User?.Name ?? "未知用户",
                content = comment.Content,
                createdAt = comment.CreatedAt,
                likes = comment.Likes,
                parentCommentId = comment.ParentCommentId,
                replies = comment.Replies?
                    .OrderBy(r => r.CreatedAt)
                    .Select(r => BuildCommentTree(r))
                    .ToList() ?? new List<object>()
            };
        }

        // 获取单个评论详情
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetCommentByIdAsync(Guid id)
        {
            var comment = await _dbContext.Comments
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (comment == null)
            {
                return NotFound("评论不存在");
            }

            return Ok(new
            {
                id = comment.Id,
                blogId = comment.BlogId,
                userId = comment.UserId,
                userName = comment.User?.Name ?? "未知用户",
                content = comment.Content,
                createdAt = comment.CreatedAt,
                likes = comment.Likes,
                parentCommentId = comment.ParentCommentId
            });
        }

        // 删除评论
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCommentAsync(Guid id)
        {
            var comment = await _dbContext.Comments
                .Include(c => c.Replies)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (comment == null)
            {
                return NotFound("评论不存在");
            }

            // 获取博客以更新评论数
            var blog = await _dbContext.Blogs.FindAsync(comment.BlogId);
            if (blog != null)
            {
                // 计算要删除的评论总数（包括所有子评论）
                int totalDeleteCount = 1 + CountReplies(comment);
                blog.CommentsCount -= totalDeleteCount;
            }

            _dbContext.Comments.Remove(comment);
            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "评论删除成功" });
        }

        // 递归计算回复数量
        private int CountReplies(Comment comment)
        {
            if (comment.Replies == null || !comment.Replies.Any())
                return 0;

            int count = comment.Replies.Count;
            foreach (var reply in comment.Replies)
            {
                count += CountReplies(reply);
            }
            return count;
        }

        // 更新评论内容
        [HttpPut("{id}")]
        public async Task<ActionResult<object>> UpdateCommentAsync(Guid id, [FromBody] CommentUpdateDto updateDto)
        {
            var comment = await _dbContext.Comments
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (comment == null)
            {
                return NotFound("评论不存在");
            }

            // 验证用户权限（只能编辑自己的评论）
            if (comment.UserId != updateDto.UserId)
            {
                return Forbid();
            }

            comment.Content = updateDto.Content;
            await _dbContext.SaveChangesAsync();

            return Ok(new
            {
                id = comment.Id,
                blogId = comment.BlogId,
                userId = comment.UserId,
                userName = comment.User?.Name ?? "未知用户",
                content = comment.Content,
                createdAt = comment.CreatedAt,
                likes = comment.Likes,
                parentCommentId = comment.ParentCommentId
            });
        }

        // 点赞/取消点赞评论
        [HttpPost("{id}/like")]
        public async Task<ActionResult> ToggleCommentLikeAsync(Guid id, [FromBody] LikeActionDto action)
        {
            var comment = await _dbContext.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound("评论不存在");
            }

            if (action.IsLiked)
            {
                comment.Likes++;
            }
            else
            {
                if (comment.Likes > 0)
                {
                    comment.Likes--;
                }
            }

            await _dbContext.SaveChangesAsync();
            return Ok(new { likes = comment.Likes, isLiked = action.IsLiked });
        }

        // 获取用户的所有评论
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetCommentsByUserIdAsync(Guid userId)
        {
            var comments = await _dbContext.Comments
                .Include(c => c.User)
                .Include(c => c.Blog)
                .Where(c => c.UserId == userId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

            var result = comments.Select(c => new
            {
                id = c.Id,
                blogId = c.BlogId,
                blogTitle = c.Blog?.Title ?? "未知博客",
                userId = c.UserId,
                userName = c.User?.Name ?? "未知用户",
                content = c.Content,
                createdAt = c.CreatedAt,
                likes = c.Likes,
                parentCommentId = c.ParentCommentId
            });

            return Ok(result);
        }
    }
}
