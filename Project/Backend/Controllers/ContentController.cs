using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Models;
using PersonalBlog.Services;

namespace PersonalBlog.Controllers
{
    /// <summary>
    /// 内容管理控制器
    /// 提供内容的CRUD操作API
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ContentController : ControllerBase
    {
        private readonly ContentService _contentService;
        
        public ContentController(ContentService contentService)
        {
            _contentService = contentService;
        }
        
        /// <summary>
        /// 获取特色内容 - 用于首页展示
        /// </summary>
        [HttpGet("featured")]
        public async Task<ActionResult<List<ContentDto>>> GetFeaturedContents([FromQuery] int limit = 6)
        {
            var contents = await _contentService.GetFeaturedContentsAsync(limit);
            return Ok(contents);
        }
        
        /// <summary>
        /// 获取用户的所有内容
        /// </summary>
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<ContentDto>>> GetUserContents(int userId)
        {
            var contents = await _contentService.GetUserContentsAsync(userId);
            return Ok(contents);
        }
        
        /// <summary>
        /// 根据类型获取内容
        /// </summary>
        [HttpGet("type/{contentType}")]
        public async Task<ActionResult<List<ContentDto>>> GetContentsByType(string contentType, [FromQuery] int limit = 10)
        {
            var contents = await _contentService.GetContentsByTypeAsync(contentType, limit);
            return Ok(contents);
        }
        
        /// <summary>
        /// 创建新内容
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ContentDto>> CreateContent([FromBody] CreateContentRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
                
            try
            {
                var content = await _contentService.CreateContentAsync(request);
                return CreatedAtAction(nameof(GetUserContents), new { userId = request.UserId }, content);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "创建内容失败", error = ex.Message });
            }
        }
        
        /// <summary>
        /// 更新内容
        /// </summary>
        [HttpPut("{contentId}")]
        public async Task<ActionResult<ContentDto>> UpdateContent(int contentId, [FromBody] UpdateContentRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
                
            var content = await _contentService.UpdateContentAsync(contentId, request);
            if (content == null)
                return NotFound(new { message = "内容不存在" });
                
            return Ok(content);
        }
        
        /// <summary>
        /// 删除内容
        /// </summary>
        [HttpDelete("{contentId}")]
        public async Task<ActionResult> DeleteContent(int contentId)
        {
            var success = await _contentService.DeleteContentAsync(contentId);
            if (!success)
                return NotFound(new { message = "内容不存在" });
                
            return NoContent();
        }
        
        /// <summary>
        /// 增加浏览次数
        /// </summary>
        [HttpPost("{contentId}/view")]
        public async Task<ActionResult> IncrementViews(int contentId)
        {
            await _contentService.IncrementViewsAsync(contentId);
            return Ok(new { message = "浏览次数已更新" });
        }
    }
}