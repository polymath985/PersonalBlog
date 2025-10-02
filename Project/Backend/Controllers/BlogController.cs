using Backend.Models.Blog;
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
        public async Task<ActionResult<IEnumerable<Blog>>> GetAllBlogsAsync(Guid userId)
        {
            var blogs = await _dbcontext.Blogs
                .Where(b => b.AuthorId == userId)
                .ToListAsync();
            return Ok(blogs);
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

            await _dbcontext.SaveChangesAsync();
            return Ok(existingBlog);
        }
    }
}