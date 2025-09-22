-- PersonalBlog数据库初始化脚本
-- 文件路径: Backend/DataBase/init.sql

-- 创建用户表
CREATE TABLE IF NOT EXISTS Users (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Email TEXT UNIQUE NOT NULL,
    Name TEXT NOT NULL,
    Password TEXT NOT NULL,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- 创建内容特性表 (对应前端Props接口)
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

-- 创建内容索引表 (管理用户与内容的关系)
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

-- 创建索引以提高查询性能
CREATE UNIQUE INDEX IF NOT EXISTS IX_ContentIndexes_UserId_ContentId 
ON ContentIndexes(UserId, ContentId);

CREATE INDEX IF NOT EXISTS IX_ContentIndexes_UserId 
ON ContentIndexes(UserId);

CREATE INDEX IF NOT EXISTS IX_ContentIndexes_ContentType 
ON ContentIndexes(ContentType);

CREATE INDEX IF NOT EXISTS IX_ContentIndexes_Featured 
ON ContentIndexes(IsFeatured);

CREATE INDEX IF NOT EXISTS IX_ContentAttributes_Featured 
ON ContentAttributes(Featured);

CREATE INDEX IF NOT EXISTS IX_ContentAttributes_Title 
ON ContentAttributes(Title);

CREATE INDEX IF NOT EXISTS IX_Users_Email 
ON Users(Email);

-- 插入示例数据
INSERT OR IGNORE INTO Users (Id, Email, Name, Password) VALUES 
(1, 'admin@example.com', '管理员', 'hashed_password_here');

-- 插入示例内容
INSERT OR IGNORE INTO ContentAttributes (
    ContentId, Title, Description, Icon, Badge, TagsJson, Featured, StatsJson, UpdateTime, ClickAction
) VALUES 
(1, 'Vue 3 进阶指南', '深入探索Vue 3的Composition API、响应式系统和性能优化技巧，构建现代化的前端应用。', 'code', '热门', '["Vue3", "JavaScript", "前端"]', 1, '{"views": 1250, "likes": 89, "comments": 23}', '2天前', '/posts/vue3-advanced'),
(2, '全栈博客系统', '使用Vue 3 + ASP.NET Core构建的现代化个人博客系统，包含完整的前后端实现。', 'project', '项目', '["Vue3", "ASP.NET", "全栈"]', 0, '{"views": 890, "likes": 45, "comments": 12}', '5天前', '/projects/fullstack-blog'),
(3, 'TypeScript 最佳实践', '从基础语法到高级类型，掌握TypeScript在大型项目中的应用和最佳实践模式。', 'book', '教程', '["TypeScript", "JavaScript", "最佳实践"]', 0, '{"views": 756, "likes": 67, "comments": 18}', '1周前', '/posts/typescript-best-practices');

-- 插入内容索引
INSERT OR IGNORE INTO ContentIndexes (UserId, ContentId, ContentType, IsFeatured, SortOrder) VALUES 
(1, 1, 'article', 1, 100),
(1, 2, 'project', 0, 90),
(1, 3, 'article', 0, 80);