using System.Text.Json;

namespace Backend.Models
{
    public static class ContentAttributesExtensions
    {
        public static ContentAttributesDto ToDto(this ContentAttributes entity)
        {
            return new ContentAttributesDto
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                Icon = entity.Icon,
                Badge = entity.Badge,
                Tags = string.IsNullOrEmpty(entity.Tags) 
                    ? null 
                    : JsonSerializer.Deserialize<string[]>(entity.Tags),
                Featured = entity.Featured,
                Stats = new ContentStatsDto
                {
                    Views = entity.Views,
                    Likes = entity.Likes,
                    Comments = entity.Comments
                },
                UpdateTime = entity.UpdateTime,
                ClickAction = entity.ClickAction
            };
        }

        public static ContentAttributes ToEntity(this CreateContentAttributesDto dto)
        {
            return new ContentAttributes
            {
                Title = dto.Title,
                Description = dto.Description,
                Icon = dto.Icon,
                Badge = dto.Badge,
                Tags = dto.Tags != null ? JsonSerializer.Serialize(dto.Tags) : null,
                Featured = dto.Featured,
                ClickAction = dto.ClickAction,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }

        public static void UpdateFromDto(this ContentAttributes entity, UpdateContentAttributesDto dto)
        {
            if (!string.IsNullOrEmpty(dto.Title))
                entity.Title = dto.Title;
            
            if (!string.IsNullOrEmpty(dto.Description))
                entity.Description = dto.Description;
            
            if (dto.Icon != null)
                entity.Icon = dto.Icon;
            
            if (dto.Badge != null)
                entity.Badge = dto.Badge;
            
            if (dto.Tags != null)
                entity.Tags = JsonSerializer.Serialize(dto.Tags);
            
            if (dto.Featured.HasValue)
                entity.Featured = dto.Featured.Value;
            
            if (dto.ClickAction != null)
                entity.ClickAction = dto.ClickAction;
            
            entity.UpdatedAt = DateTime.UtcNow;
        }

        public static void IncrementViews(this ContentAttributes entity)
        {
            entity.Views++;
            entity.UpdatedAt = DateTime.UtcNow;
        }

        public static void IncrementLikes(this ContentAttributes entity)
        {
            entity.Likes++;
            entity.UpdatedAt = DateTime.UtcNow;
        }

        public static void IncrementComments(this ContentAttributes entity)
        {
            entity.Comments++;
            entity.UpdatedAt = DateTime.UtcNow;
        }
    }
}