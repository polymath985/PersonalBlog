using Microsoft.EntityFrameworkCore;
using PersonalBlog.DataBase;
using PersonalBlog.Models;

namespace PersonalBlog.Services
{
    /// <summary>
    /// 内容管理服务 - 使用Entity Framework Core
    /// 处理内容的创建、更新、查询等操作
    /// </summary>
    public class ContentService
    {
        private readonly BlogDbContext _context;
        
        public ContentService(BlogDbContext context)
        {
            _context = context;
        }
        
        /// <summary>
        /// 创建新内容
        /// </summary>
        public async Task<ContentDto> CreateContentAsync(CreateContentRequest request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            
            try
            {
                // 1. 创建内容特性
                var contentAttributes = new ContentAttributes
                {
                    Title = request.Title,
                    Description = request.Description,
                    Icon = request.Icon,
                    Badge = request.Badge,
                    Tags = request.Tags ?? new List<string>(),
                    Featured = request.Featured,
                    ContentText = request.ContentText,
                    ClickAction = request.ClickAction,
                    UpdateTime = GetRelativeTimeString(DateTime.UtcNow),
                    Stats = new ContentStats()
                };
                
                _context.ContentAttributes.Add(contentAttributes);
                await _context.SaveChangesAsync();
                
                // 2. 创建内容索引
                var contentIndex = new ContentIndex
                {
                    UserId = request.UserId,
                    ContentId = contentAttributes.ContentId,
                    ContentType = request.ContentType,
                    IsFeatured = request.Featured
                };
                
                _context.ContentIndexes.Add(contentIndex);
                await _context.SaveChangesAsync();
                
                await transaction.CommitAsync();
                
                return MapToDto(contentAttributes);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        
        /// <summary>
        /// 获取用户的所有内容
        /// </summary>
        public async Task<List<ContentDto>> GetUserContentsAsync(int userId)
        {
            var contents = await _context.ContentIndexes
                .Where(ci => ci.UserId == userId)
                .Include(ci => ci.Content)
                .OrderByDescending(ci => ci.Content!.UpdatedAt)
                .Select(ci => ci.Content!)
                .ToListAsync();
                
            return contents.Select(MapToDto).ToList();
        }
        
        /// <summary>
        /// 获取特色内容
        /// </summary>
        public async Task<List<ContentDto>> GetFeaturedContentsAsync(int limit = 6)
        {
            var contents = await _context.ContentIndexes
                .Where(ci => ci.IsFeatured)
                .Include(ci => ci.Content)
                .OrderByDescending(ci => ci.Content!.UpdatedAt)
                .Take(limit)
                .Select(ci => ci.Content!)
                .ToListAsync();
                
            return contents.Select(MapToDto).ToList();
        }
        
        /// <summary>
        /// 根据类型获取内容
        /// </summary>
        public async Task<List<ContentDto>> GetContentsByTypeAsync(string contentType, int limit = 10)
        {
            var contents = await _context.ContentIndexes
                .Where(ci => ci.ContentType == contentType)
                .Include(ci => ci.Content)
                .OrderByDescending(ci => ci.Content!.UpdatedAt)
                .Take(limit)
                .Select(ci => ci.Content!)
                .ToListAsync();
                
            return contents.Select(MapToDto).ToList();
        }
        
        /// <summary>
        /// 更新内容
        /// </summary>
        public async Task<ContentDto?> UpdateContentAsync(int contentId, UpdateContentRequest request)
        {
            var content = await _context.ContentAttributes.FindAsync(contentId);
            if (content == null) return null;
            
            // 更新属性
            if (!string.IsNullOrEmpty(request.Title))
                content.Title = request.Title;
            if (!string.IsNullOrEmpty(request.Description))
                content.Description = request.Description;
            if (request.Icon != null)
                content.Icon = request.Icon;
            if (request.Badge != null)
                content.Badge = request.Badge;
            if (request.Tags != null)
                content.Tags = request.Tags;
            if (request.Featured.HasValue)
                content.Featured = request.Featured.Value;
            if (request.ContentText != null)
                content.ContentText = request.ContentText;
            if (request.ClickAction != null)
                content.ClickAction = request.ClickAction;
            if (request.Stats != null)
            {
                var currentStats = content.Stats;
                content.Stats = new ContentStats
                {
                    Views = request.Stats.Views ?? currentStats.Views,
                    Likes = request.Stats.Likes ?? currentStats.Likes,
                    Comments = request.Stats.Comments ?? currentStats.Comments
                };
            }
            
            content.UpdatedAt = DateTime.UtcNow;
            content.UpdateTime = GetRelativeTimeString(content.UpdatedAt);
            
            await _context.SaveChangesAsync();
            
            return MapToDto(content);
        }
        
        /// <summary>
        /// 删除内容
        /// </summary>
        public async Task<bool> DeleteContentAsync(int contentId)
        {
            var content = await _context.ContentAttributes.FindAsync(contentId);
            if (content == null) return false;
            
            _context.ContentAttributes.Remove(content);
            await _context.SaveChangesAsync();
            
            return true;
        }
        
        /// <summary>
        /// 增加浏览次数
        /// </summary>
        public async Task IncrementViewsAsync(int contentId)
        {
            var content = await _context.ContentAttributes.FindAsync(contentId);
            if (content == null) return;
            
            var stats = content.Stats;
            stats.Views = (stats.Views ?? 0) + 1;
            content.Stats = stats;
            
            await _context.SaveChangesAsync();
        }
        
        /// <summary>
        /// 将实体转换为DTO
        /// </summary>
        private static ContentDto MapToDto(ContentAttributes content)
        {
            return new ContentDto
            {
                Title = content.Title,
                Description = content.Description,
                Icon = content.Icon,
                Badge = content.Badge,
                Tags = content.Tags,
                Featured = content.Featured,
                Stats = new ContentStatsDto
                {
                    Views = content.Stats.Views,
                    Likes = content.Stats.Likes,
                    Comments = content.Stats.Comments
                },
                UpdateTime = content.UpdateTime,
                ClickAction = content.ClickAction
            };
        }
        
        /// <summary>
        /// 获取相对时间字符串
        /// </summary>
        private static string GetRelativeTimeString(DateTime dateTime)
        {
            var timeSpan = DateTime.UtcNow - dateTime;
            
            if (timeSpan.Days > 0)
                return $"{timeSpan.Days}天前";
            if (timeSpan.Hours > 0)
                return $"{timeSpan.Hours}小时前";
            if (timeSpan.Minutes > 0)
                return $"{timeSpan.Minutes}分钟前";
            
            return "刚刚";
        }
    }
}