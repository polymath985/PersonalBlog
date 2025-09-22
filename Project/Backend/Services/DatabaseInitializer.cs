using System.Data.SQLite;

namespace PersonalBlog.DataBase
{
    /// <summary>
    /// SQLite数据库初始化服务
    /// 处理数据库创建、表结构初始化、示例数据插入
    /// </summary>
    public class DatabaseInitializer
    {
        private readonly string _connectionString;
        
        public DatabaseInitializer(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        /// <summary>
        /// 初始化数据库
        /// </summary>
        public async Task InitializeAsync()
        {
            try
            {
                // 确保数据库文件目录存在
                var dbPath = GetDatabasePath();
                var directory = Path.GetDirectoryName(dbPath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                
                // 创建数据库连接
                using var connection = new SQLiteConnection(_connectionString);
                await connection.OpenAsync();
                
                // 读取并执行初始化脚本
                var initScript = await ReadInitScriptAsync();
                await ExecuteScriptAsync(connection, initScript);
                
                Console.WriteLine("✅ 数据库初始化完成！");
                Console.WriteLine($"📁 数据库位置: {dbPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ 数据库初始化失败: {ex.Message}");
                throw;
            }
        }
        
        /// <summary>
        /// 检查数据库是否存在
        /// </summary>
        public bool DatabaseExists()
        {
            var dbPath = GetDatabasePath();
            return File.Exists(dbPath);
        }
        
        /// <summary>
        /// 获取数据库文件路径
        /// </summary>
        private string GetDatabasePath()
        {
            // 从连接字符串中提取数据库路径
            var match = System.Text.RegularExpressions.Regex.Match(_connectionString, @"Data Source=([^;]+)");
            if (match.Success)
            {
                var path = match.Groups[1].Value;
                return Path.IsPathRooted(path) ? path : Path.Combine(Directory.GetCurrentDirectory(), path);
            }
            
            // 默认路径
            return Path.Combine(Directory.GetCurrentDirectory(), "DataBase", "PersonalBlog.db");
        }
        
        /// <summary>
        /// 读取初始化脚本
        /// </summary>
        private async Task<string> ReadInitScriptAsync()
        {
            var scriptPath = Path.Combine(Directory.GetCurrentDirectory(), "DataBase", "init.sql");
            
            if (!File.Exists(scriptPath))
            {
                // 如果脚本文件不存在，返回基本的表创建SQL
                return GetBasicInitScript();
            }
            
            return await File.ReadAllTextAsync(scriptPath);
        }
        
        /// <summary>
        /// 执行SQL脚本
        /// </summary>
        private async Task ExecuteScriptAsync(SQLiteConnection connection, string script)
        {
            // 按分号分割SQL语句
            var statements = script.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            
            foreach (var statement in statements)
            {
                var trimmedStatement = statement.Trim();
                if (string.IsNullOrEmpty(trimmedStatement) || trimmedStatement.StartsWith("--"))
                    continue;
                
                try
                {
                    using var command = new SQLiteCommand(trimmedStatement, connection);
                    await command.ExecuteNonQueryAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"⚠️ 执行SQL语句失败: {trimmedStatement}");
                    Console.WriteLine($"错误: {ex.Message}");
                    // 继续执行其他语句，不抛出异常
                }
            }
        }
        
        /// <summary>
        /// 获取基本的初始化脚本
        /// </summary>
        private string GetBasicInitScript()
        {
            return @"
-- 创建用户表
CREATE TABLE IF NOT EXISTS Users (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Email TEXT UNIQUE NOT NULL,
    Name TEXT NOT NULL,
    Password TEXT NOT NULL,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- 创建内容特性表
CREATE TABLE IF NOT EXISTS ContentAttributes (
    ContentId INTEGER PRIMARY KEY AUTOINCREMENT,
    Title TEXT NOT NULL,
    Description TEXT NOT NULL,
    Icon TEXT,
    Badge TEXT,
    TagsJson TEXT DEFAULT '[]',
    Featured BOOLEAN DEFAULT 0,
    StatsJson TEXT DEFAULT '{}',
    UpdateTime TEXT,
    ClickAction TEXT,
    ContentText TEXT,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- 创建内容索引表
CREATE TABLE IF NOT EXISTS ContentIndexes (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    UserId INTEGER NOT NULL,
    ContentId INTEGER NOT NULL,
    ContentType TEXT NOT NULL,
    IsFeatured BOOLEAN DEFAULT 0,
    SortOrder INTEGER DEFAULT 0,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE,
    FOREIGN KEY (ContentId) REFERENCES ContentAttributes(ContentId) ON DELETE CASCADE
);

-- 创建索引
CREATE UNIQUE INDEX IF NOT EXISTS IX_ContentIndexes_UserId_ContentId ON ContentIndexes(UserId, ContentId);
CREATE INDEX IF NOT EXISTS IX_ContentIndexes_UserId ON ContentIndexes(UserId);
CREATE INDEX IF NOT EXISTS IX_ContentIndexes_ContentType ON ContentIndexes(ContentType);
CREATE INDEX IF NOT EXISTS IX_Users_Email ON Users(Email);
";
        }
        
        /// <summary>
        /// 插入示例数据
        /// </summary>
        public async Task SeedSampleDataAsync()
        {
            using var connection = new SQLiteConnection(_connectionString);
            await connection.OpenAsync();
            
            // 检查是否已有数据
            using var checkCommand = new SQLiteCommand("SELECT COUNT(*) FROM Users", connection);
            var userCount = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());
            
            if (userCount > 0)
            {
                Console.WriteLine("📊 数据库已包含数据，跳过示例数据插入");
                return;
            }
            
            // 插入示例用户
            var insertUserSql = @"
INSERT INTO Users (Email, Name, Password) VALUES 
('admin@example.com', '管理员', 'hashed_password_here');
";
            
            // 插入示例内容
            var insertContentSql = @"
INSERT INTO ContentAttributes (Title, Description, Icon, Badge, TagsJson, Featured, StatsJson, UpdateTime, ClickAction) VALUES 
('Vue 3 进阶指南', '深入探索Vue 3的Composition API、响应式系统和性能优化技巧。', 'code', '热门', '[""Vue3"", ""JavaScript"", ""前端""]', 1, '{""views"": 1250, ""likes"": 89, ""comments"": 23}', '2天前', '/posts/vue3-advanced'),
('全栈博客系统', '使用Vue 3 + ASP.NET Core构建的现代化个人博客系统。', 'project', '项目', '[""Vue3"", ""ASP.NET"", ""全栈""]', 0, '{""views"": 890, ""likes"": 45, ""comments"": 12}', '5天前', '/projects/fullstack-blog');
";
            
            // 插入内容索引
            var insertIndexSql = @"
INSERT INTO ContentIndexes (UserId, ContentId, ContentType, IsFeatured, SortOrder) VALUES 
(1, 1, 'article', 1, 100),
(1, 2, 'project', 0, 90);
";
            
            try
            {
                using var transaction = connection.BeginTransaction();
                
                await ExecuteScriptAsync(connection, insertUserSql);
                await ExecuteScriptAsync(connection, insertContentSql);
                await ExecuteScriptAsync(connection, insertIndexSql);
                
                transaction.Commit();
                Console.WriteLine("✅ 示例数据插入完成！");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ 示例数据插入失败: {ex.Message}");
                throw;
            }
        }
    }
}