# HTML文档结构

> [上一章: HTML基础概念](./01-html-basics.md) | [返回主目录](./README.md) | [下一章: 标签语法](./03-tag-syntax.md)

## 标准HTML文档结构

每个HTML文档都有一个固定的基本结构，就像建筑物需要地基、框架和屋顶一样：

```html
<!DOCTYPE html>
<html lang="zh-CN">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>网页标题</title>
</head>
<body>
    <!-- 网页内容放在这里 -->
    <h1>欢迎来到我的网页</h1>
    <p>这是一个段落。</p>
</body>
</html>
```

## 各部分详解

### 1. 文档类型声明 `<!DOCTYPE html>`

#### 作用和重要性
- 📋 **声明文档类型**: 告诉浏览器这是HTML5文档
- 🎯 **触发标准模式**: 确保浏览器使用最新标准解析页面
- 📍 **位置要求**: 必须在文档的第一行，任何内容之前

#### 示例
```html
<!DOCTYPE html>  <!-- ✅ 正确：HTML5声明 -->
```

#### ❌ 常见错误
```html
<html>
<!DOCTYPE html>  <!-- ❌ 错误：不能在其他标签后面 -->

<!doctype html>  <!-- ⚠️ 可以但不推荐：大小写不一致 -->
```

### 2. 根元素 `<html>`

#### 基本结构
```html
<html lang="zh-CN">
  <!-- 所有其他内容都在这里 -->
</html>
```

#### 重要属性

**`lang` 属性**
```html
<html lang="zh-CN">  <!-- 中文简体 -->
<html lang="en">     <!-- 英语 -->
<html lang="ja">     <!-- 日语 -->
<html lang="fr">     <!-- 法语 -->
```

**作用**：
- 🌐 **搜索引擎优化**: 帮助搜索引擎理解页面语言
- 🗣️ **屏幕阅读器**: 为视障用户提供正确的语音合成
- 🔍 **浏览器功能**: 启用拼写检查、翻译等功能

### 3. 头部元素 `<head>`

`<head>` 包含页面的元数据，这些信息不会直接显示在页面上，但对页面的功能至关重要。

#### 基本结构
```html
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>页面标题</title>
    <!-- 其他meta信息、CSS链接、JavaScript等 -->
</head>
```

#### 必需的meta标签

**字符编码**
```html
<meta charset="UTF-8">
```
- 🔤 **作用**: 指定页面使用UTF-8编码
- ⚠️ **重要性**: 防止中文乱码问题
- 📍 **位置**: 必须在head的前1024个字节内

**视口设置**
```html
<meta name="viewport" content="width=device-width, initial-scale=1.0">
```
- 📱 **作用**: 控制页面在移动设备上的显示
- 🎯 **参数说明**:
  - `width=device-width`: 宽度等于设备宽度
  - `initial-scale=1.0`: 初始缩放比例为1

#### 页面标题
```html
<title>我的网页标题</title>
```
- 🏷️ **显示位置**: 浏览器标签页、书签、搜索结果
- 📝 **最佳长度**: 50-60个字符
- 🎯 **SEO重要**: 搜索引擎排名的重要因素

#### 常用meta标签

**页面描述**
```html
<meta name="description" content="这是页面的简短描述，用于搜索引擎结果">
```

**关键词**
```html
<meta name="keywords" content="HTML,CSS,JavaScript,网页开发">
```

**作者信息**
```html
<meta name="author" content="张三">
```

**社交媒体分享**
```html
<!-- Open Graph (Facebook, LinkedIn等) -->
<meta property="og:title" content="页面标题">
<meta property="og:description" content="页面描述">
<meta property="og:image" content="https://example.com/image.jpg">
<meta property="og:url" content="https://example.com/page.html">

<!-- Twitter Cards -->
<meta name="twitter:card" content="summary_large_image">
<meta name="twitter:title" content="页面标题">
<meta name="twitter:description" content="页面描述">
```

#### 外部资源链接

**CSS样式表**
```html
<link rel="stylesheet" href="styles.css">
<link rel="stylesheet" href="https://cdn.example.com/bootstrap.css">
```

**网站图标**
```html
<link rel="icon" href="/favicon.ico">
<link rel="apple-touch-icon" href="/apple-touch-icon.png">
```

**字体**
```html
<link rel="preconnect" href="https://fonts.googleapis.com">
<link href="https://fonts.googleapis.com/css2?family=Roboto:wght@300;400;700&display=swap" rel="stylesheet">
```

### 4. 主体元素 `<body>`

`<body>` 包含页面的所有可见内容。

#### 基本结构
```html
<body>
    <!-- 页面头部 -->
    <header>
        <h1>网站标题</h1>
        <nav>导航菜单</nav>
    </header>
    
    <!-- 主要内容 -->
    <main>
        <article>文章内容</article>
        <section>章节内容</section>
    </main>
    
    <!-- 侧边栏 -->
    <aside>相关信息</aside>
    
    <!-- 页面底部 -->
    <footer>版权信息</footer>
</body>
```

## 完整示例

### 基础模板
```html
<!DOCTYPE html>
<html lang="zh-CN">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="这是我的第一个网页">
    <meta name="keywords" content="HTML,学习,教程">
    <meta name="author" content="学习者">
    <title>我的第一个网页</title>
    <link rel="stylesheet" href="style.css">
    <link rel="icon" href="favicon.ico">
</head>
<body>
    <header>
        <h1>欢迎来到我的网站</h1>
        <nav>
            <ul>
                <li><a href="index.html">首页</a></li>
                <li><a href="about.html">关于</a></li>
                <li><a href="contact.html">联系</a></li>
            </ul>
        </nav>
    </header>
    
    <main>
        <section>
            <h2>关于HTML</h2>
            <p>HTML是创建网页的标准标记语言。</p>
        </section>
    </main>
    
    <footer>
        <p>&copy; 2023 我的网站. 保留所有权利。</p>
    </footer>
</body>
</html>
```

### 高级模板
```html
<!DOCTYPE html>
<html lang="zh-CN">
<head>
    <!-- 基础meta信息 -->
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    
    <!-- SEO相关 -->
    <title>专业网页模板 | 高质量HTML示例</title>
    <meta name="description" content="这是一个专业的HTML网页模板，包含完整的结构和最佳实践示例">
    <meta name="keywords" content="HTML模板,网页设计,前端开发,最佳实践">
    <meta name="author" content="专业开发者">
    <meta name="robots" content="index, follow">
    
    <!-- 社交媒体 -->
    <meta property="og:title" content="专业网页模板">
    <meta property="og:description" content="高质量的HTML网页模板示例">
    <meta property="og:type" content="website">
    <meta property="og:url" content="https://example.com">
    <meta property="og:image" content="https://example.com/og-image.jpg">
    
    <!-- 网站图标 -->
    <link rel="icon" type="image/x-icon" href="/favicon.ico">
    <link rel="apple-touch-icon" sizes="180x180" href="/apple-touch-icon.png">
    
    <!-- 样式表 -->
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="stylesheet" href="https://fonts.googleapis.com/css2?family=Inter:wght@300;400;600;700&display=swap">
    <link rel="stylesheet" href="css/normalize.css">
    <link rel="stylesheet" href="css/main.css">
    
    <!-- 预加载重要资源 -->
    <link rel="preload" href="css/main.css" as="style">
    <link rel="preload" href="fonts/main-font.woff2" as="font" type="font/woff2" crossorigin>
</head>
<body>
    <!-- 跳过导航链接 (无障碍访问) -->
    <a href="#main-content" class="skip-link">跳转到主要内容</a>
    
    <!-- 页面头部 -->
    <header role="banner">
        <div class="container">
            <h1>网站标题</h1>
            <nav role="navigation" aria-label="主导航">
                <ul>
                    <li><a href="/" aria-current="page">首页</a></li>
                    <li><a href="/about">关于我们</a></li>
                    <li><a href="/services">服务</a></li>
                    <li><a href="/contact">联系我们</a></li>
                </ul>
            </nav>
        </div>
    </header>
    
    <!-- 主要内容 -->
    <main id="main-content" role="main">
        <div class="container">
            <h1>页面主标题</h1>
            <p>这里是主要内容...</p>
        </div>
    </main>
    
    <!-- 页面底部 -->
    <footer role="contentinfo">
        <div class="container">
            <p>&copy; 2023 我的网站. 保留所有权利。</p>
        </div>
    </footer>
    
    <!-- JavaScript文件 -->
    <script src="js/main.js"></script>
</body>
</html>
```

## 最佳实践

### 1. 文档结构检查清单
- ✅ 包含 `<!DOCTYPE html>`
- ✅ 设置 `lang` 属性
- ✅ 指定字符编码
- ✅ 添加viewport meta标签
- ✅ 设置有意义的标题

### 2. 性能优化
```html
<!-- 预连接到外部域名 -->
<link rel="preconnect" href="https://fonts.googleapis.com">
<link rel="dns-prefetch" href="https://api.example.com">

<!-- 预加载关键资源 -->
<link rel="preload" href="critical.css" as="style">
<link rel="preload" href="hero-image.jpg" as="image">
```

### 3. 无障碍访问
```html
<!-- 设置页面语言 -->
<html lang="zh-CN">

<!-- 跳过导航链接 -->
<a href="#main" class="skip-link">跳转到主内容</a>

<!-- 使用语义化标签 -->
<main role="main">
<nav role="navigation" aria-label="主导航">
```

## 调试和验证

### 常用验证工具
- **W3C HTML验证器**: https://validator.w3.org/
- **Chrome DevTools**: F12 → Elements面板
- **HTML5 Outliner**: 检查文档结构

### 常见错误
```html
<!-- ❌ 错误示例 -->
<html>
  <title>标题</title>  <!-- title应该在head中 -->
  <body>
    <head>             <!-- head不能在body中 -->
      <meta charset="UTF-8">
    </head>
  </body>
</html>
```

```html
<!-- ✅ 正确示例 -->
<!DOCTYPE html>
<html lang="zh-CN">
  <head>
    <meta charset="UTF-8">
    <title>标题</title>
  </head>
  <body>
    <!-- 内容 -->
  </body>
</html>
```

## 下一步学习

现在您已经掌握了HTML文档的基本结构，让我们继续学习：

1. [标签语法](./03-tag-syntax.md) - 深入了解HTML标签的语法规则
2. [HTML属性](./04-attributes.md) - 学习如何使用属性增强元素功能

---

> 💡 **记住**: 良好的文档结构是创建高质量网页的基础！

*[上一章: HTML基础概念](./01-html-basics.md) | [返回主目录](./README.md) | [下一章: 标签语法](./03-tag-syntax.md)*
