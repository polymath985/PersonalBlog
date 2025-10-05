using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FileController(IWebHostEnvironment environment) : ControllerBase
{
    private readonly string _wwwrootPath = environment.WebRootPath;
    private const long MaxFileSize = 5 * 1024 * 1024; // 5MB
    private static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png", ".gif", ".webp", ".bmp"];

    // 支持的文件类型枚举
    public enum FileCategory
    {
        Avatar,     // 头像: images/avatars
        BlogImage,  // 博客图片: images/blog
        Other       // 其他: uploads
    }

    /// <summary>
    /// 获取文件存储路径
    /// </summary>
    private string GetStoragePath(FileCategory category)
    {
        return category switch
        {
            FileCategory.Avatar => Path.Combine(_wwwrootPath, "images", "avatars"),
            FileCategory.BlogImage => Path.Combine(_wwwrootPath, "images", "blog"),
            _ => Path.Combine(_wwwrootPath, "uploads")
        };
    }

    /// <summary>
    /// 获取文件URL前缀
    /// </summary>
    private string GetUrlPrefix(FileCategory category)
    {
        return category switch
        {
            FileCategory.Avatar => "/images/avatars",
            FileCategory.BlogImage => "/images/blog",
            _ => "/uploads"
        };
    }

    /// <summary>
    /// 上传文件
    /// </summary>
    /// <param name="file">上传的文件</param>
    /// <param name="category">文件分类 (avatar/blog/other)</param>
    [HttpPost("upload")]
    public async Task<ActionResult<object>> UploadFile(IFormFile file, [FromQuery] string category = "other")
    {
        try
        {
            // 验证文件是否存在
            if (file == null || file.Length == 0)
            {
                return BadRequest(new { error = "请选择文件" });
            }

            // 验证文件大小
            if (file.Length > MaxFileSize)
            {
                return BadRequest(new { error = $"文件大小超过限制（最大 {MaxFileSize / 1024 / 1024}MB）" });
            }

            // 验证文件类型
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (string.IsNullOrEmpty(extension) || !AllowedExtensions.Contains(extension))
            {
                return BadRequest(new { error = "不支持的文件格式，仅支持: " + string.Join(", ", AllowedExtensions) });
            }

            // 解析文件分类
            var fileCategory = category.ToLower() switch
            {
                "avatar" => FileCategory.Avatar,
                "blog" => FileCategory.BlogImage,
                _ => FileCategory.Other
            };

            // 获取存储路径
            var storagePath = GetStoragePath(fileCategory);
            Directory.CreateDirectory(storagePath);

            // 生成唯一文件名（使用时间戳 + GUID 确保唯一性）
            var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            var uniqueId = Guid.NewGuid().ToString("N")[..8]; // 取前8位
            var fileName = $"{timestamp}_{uniqueId}{extension}";
            var filePath = Path.Combine(storagePath, fileName);

            // 保存文件
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // 返回文件访问 URL
            var urlPrefix = GetUrlPrefix(fileCategory);
            var fileUrl = $"{urlPrefix}/{fileName}";
            
            return Ok(new
            {
                url = fileUrl,
                fileName = fileName,
                category = category.ToLower(),
                size = file.Length,
                contentType = file.ContentType,
                uploadTime = DateTime.Now
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"文件上传失败: {ex.Message}");
            return StatusCode(500, new { error = "文件上传失败，请稍后重试" });
        }
    }

    /// <summary>
    /// 删除文件 (通过完整URL路径)
    /// </summary>
    /// <param name="fileUrl">文件URL (如: /images/avatars/xxx.jpg)</param>
    [HttpDelete]
    public IActionResult DeleteFile([FromQuery] string fileUrl)
    {
        try
        {
            // 验证URL
            if (string.IsNullOrWhiteSpace(fileUrl))
            {
                return BadRequest(new { error = "文件URL不能为空" });
            }

            // 验证URL格式，防止路径遍历攻击
            if (fileUrl.Contains(".."))
            {
                return BadRequest(new { error = "无效的文件URL" });
            }

            // 移除开头的斜杠，转换为相对路径
            var relativePath = fileUrl.TrimStart('/');
            var filePath = Path.Combine(_wwwrootPath, relativePath);

            // 验证文件路径必须在 wwwroot 目录内
            var fullPath = Path.GetFullPath(filePath);
            var wwwrootFullPath = Path.GetFullPath(_wwwrootPath);
            if (!fullPath.StartsWith(wwwrootFullPath, StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest(new { error = "无效的文件路径" });
            }

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound(new { error = "文件不存在" });
            }

            System.IO.File.Delete(filePath);

            return Ok(new { message = "文件删除成功" });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"文件删除失败: {ex.Message}");
            return StatusCode(500, new { error = "文件删除失败" });
        }
    }

    /// <summary>
    /// 获取文件列表
    /// </summary>
    /// <param name="category">文件分类 (avatar/blog/other, 默认返回所有)</param>
    [HttpGet("list")]
    public ActionResult<object> GetFileList([FromQuery] string? category = null)
    {
        try
        {
            var allFiles = new List<object>();

            // 如果指定了分类，只返回该分类的文件
            if (!string.IsNullOrEmpty(category))
            {
                var fileCategory = category.ToLower() switch
                {
                    "avatar" => FileCategory.Avatar,
                    "blog" => FileCategory.BlogImage,
                    _ => FileCategory.Other
                };

                var storagePath = GetStoragePath(fileCategory);
                var urlPrefix = GetUrlPrefix(fileCategory);

                if (Directory.Exists(storagePath))
                {
                    allFiles = Directory.GetFiles(storagePath)
                        .Select(filePath =>
                        {
                            var fileInfo = new FileInfo(filePath);
                            return new
                            {
                                fileName = fileInfo.Name,
                                url = $"{urlPrefix}/{fileInfo.Name}",
                                category = category.ToLower(),
                                size = fileInfo.Length,
                                uploadTime = fileInfo.CreationTime
                            } as object;
                        })
                        .OrderByDescending(f => ((dynamic)f).uploadTime)
                        .ToList();
                }
            }
            else
            {
                // 返回所有分类的文件
                foreach (FileCategory cat in Enum.GetValues(typeof(FileCategory)))
                {
                    var storagePath = GetStoragePath(cat);
                    var urlPrefix = GetUrlPrefix(cat);
                    var categoryName = cat.ToString().ToLower();

                    if (Directory.Exists(storagePath))
                    {
                        var categoryFiles = Directory.GetFiles(storagePath)
                            .Select(filePath =>
                            {
                                var fileInfo = new FileInfo(filePath);
                                return new
                                {
                                    fileName = fileInfo.Name,
                                    url = $"{urlPrefix}/{fileInfo.Name}",
                                    category = categoryName,
                                    size = fileInfo.Length,
                                    uploadTime = fileInfo.CreationTime
                                } as object;
                            });
                        allFiles.AddRange(categoryFiles);
                    }
                }

                allFiles = allFiles
                    .OrderByDescending(f => ((dynamic)f).uploadTime)
                    .ToList();
            }

            return Ok(new { files = allFiles });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"获取文件列表失败: {ex.Message}");
            return StatusCode(500, new { error = "获取文件列表失败" });
        }
    }
}
