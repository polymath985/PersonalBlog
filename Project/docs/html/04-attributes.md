# HTML属性

> [上一章: 标签语法](./03-tag-syntax.md) | [返回主目录](./README.md) | [下一章: 文本格式化](./05-text-formatting.md)

## 属性的基本概念

HTML属性为元素提供额外的信息，它们总是在开始标签中指定，以名称-值对的形式出现。

### 属性的语法
```html
<标签名 属性名="属性值" 属性名2="属性值2">内容</标签名>
```

### 语法规则
- 🔤 **属性名**: 不区分大小写，但推荐小写
- 📝 **属性值**: 推荐使用双引号包围
- 🔢 **多个属性**: 用空格分隔
- 📍 **位置**: 只能在开始标签中使用

#### 正确示例
```html
<img src="photo.jpg" alt="美丽的风景" width="300" height="200">
<a href="https://example.com" target="_blank" rel="noopener">链接</a>
<input type="text" name="username" placeholder="请输入用户名" required>
```

## 全局属性

全局属性是所有HTML元素都可以使用的属性。

### 1. `id` 属性

#### 基本用法
```html
<div id="header">页面头部</div>
<p id="intro">介绍段落</p>
<button id="submit-btn">提交按钮</button>
```

#### 特点和规则
- 🆔 **唯一性**: 在整个页面中必须唯一
- 🎯 **用途**: CSS选择器、JavaScript操作、锚点链接
- 📝 **命名规则**: 不能包含空格，建议使用连字符或下划线

#### 实际应用
```html
<!-- CSS选择器 -->
<style>
#header { background-color: blue; }
#intro { font-size: 18px; }
</style>

<!-- JavaScript操作 -->
<script>
document.getElementById('submit-btn').onclick = function() {
    alert('按钮被点击了！');
};
</script>

<!-- 锚点链接 -->
<a href="#section1">跳转到第一章</a>
<h2 id="section1">第一章</h2>
```

### 2. `class` 属性

#### 基本用法
```html
<div class="container">容器</div>
<p class="text-large">大字体段落</p>
<button class="btn btn-primary">主要按钮</button>
```

#### 特点和用法
- 🔄 **可重复**: 多个元素可以有相同的class
- 📚 **多个类**: 一个元素可以有多个类，用空格分隔
- 🎨 **主要用途**: CSS样式选择器

#### 实际应用
```html
<!-- 多个类名 -->
<div class="card shadow rounded">
    <h3 class="card-title text-center">卡片标题</h3>
    <p class="card-content text-muted">卡片内容</p>
</div>

<!-- CSS样式 -->
<style>
.card { border: 1px solid #ccc; padding: 20px; }
.shadow { box-shadow: 0 2px 4px rgba(0,0,0,0.1); }
.rounded { border-radius: 8px; }
.text-center { text-align: center; }
.text-muted { color: #666; }
</style>
```

### 3. `style` 属性

#### 基本用法
```html
<p style="color: red; font-size: 18px;">红色大字体段落</p>
<div style="background-color: yellow; padding: 10px; margin: 5px;">
    黄色背景的容器
</div>
```

#### CSS属性语法
```html
<element style="property1: value1; property2: value2;">
```

#### 常用CSS属性示例
```html
<!-- 文本样式 -->
<p style="color: blue; font-size: 20px; font-weight: bold;">蓝色粗体大字</p>

<!-- 背景和边框 -->
<div style="background-color: #f0f0f0; border: 2px solid #333; border-radius: 5px;">
    有边框的容器
</div>

<!-- 间距 -->
<p style="margin: 10px; padding: 15px;">有内外边距的段落</p>

<!-- 尺寸 -->
<div style="width: 300px; height: 200px; background-color: lightblue;">
    固定尺寸的容器
</div>
```

#### 最佳实践
- ⚠️ **限制使用**: 仅用于临时样式或动态样式
- 📄 **推荐方式**: 使用外部CSS文件
- 🎯 **优先级**: style属性的优先级最高

### 4. `title` 属性

#### 基本用法
```html
<p title="这是一个提示信息">将鼠标悬停在这里查看提示</p>
<img src="image.jpg" alt="图片" title="这是一张美丽的风景图片">
<button title="点击提交表单">提交</button>
```

#### 应用场景
```html
<!-- 缩写解释 -->
<abbr title="HyperText Markup Language">HTML</abbr>
<abbr title="Cascading Style Sheets">CSS</abbr>

<!-- 链接说明 -->
<a href="document.pdf" title="下载PDF文档，大小约2MB">下载文档</a>

<!-- 表单帮助 -->
<input type="password" name="password" title="密码必须包含至少8个字符">

<!-- 图标按钮说明 -->
<button class="icon-btn" title="删除此项">🗑️</button>
```

### 5. `lang` 属性

#### 基本用法
```html
<html lang="zh-CN">  <!-- 整个页面的语言 -->
<p lang="en">This is an English paragraph.</p>
<span lang="ja">こんにちは</span>
<quote lang="fr">Bonjour le monde</quote>
```

#### 常用语言代码
```html
<p lang="zh-CN">中文简体</p>
<p lang="zh-TW">中文繁體</p>
<p lang="en">English</p>
<p lang="en-US">American English</p>
<p lang="en-GB">British English</p>
<p lang="ja">日本語</p>
<p lang="ko">한국어</p>
<p lang="fr">Français</p>
<p lang="de">Deutsch</p>
<p lang="es">Español</p>
```

#### 重要性
- 🗣️ **无障碍访问**: 帮助屏幕阅读器正确发音
- 🔍 **SEO优化**: 搜索引擎理解内容语言
- 🌐 **浏览器功能**: 启用拼写检查、翻译等

### 6. 其他重要全局属性

#### `data-*` 自定义属性
```html
<div data-user-id="123" data-role="admin" data-status="active">
    用户信息
</div>

<button data-action="delete" data-confirm="确定要删除吗？">删除</button>
```

JavaScript中使用：
```html
<script>
const element = document.querySelector('[data-user-id="123"]');
console.log(element.dataset.userId);    // "123"
console.log(element.dataset.role);      // "admin"
console.log(element.dataset.status);    // "active"
</script>
```

#### `hidden` 属性
```html
<p>这段文字是可见的</p>
<p hidden>这段文字被隐藏了</p>
<div hidden>这个容器也被隐藏了</div>
```

#### `contenteditable` 属性
```html
<p contenteditable="true">这段文字可以编辑</p>
<div contenteditable="false">这个容器不可编辑</div>
```

#### `draggable` 属性
```html
<img src="photo.jpg" alt="可拖拽的图片" draggable="true">
<div draggable="true">可拖拽的容器</div>
```

## 特定元素属性

### 链接属性 (`<a>`)
```html
<a href="https://example.com"     <!-- 链接地址 -->
   target="_blank"                 <!-- 打开方式 -->
   rel="noopener nofollow"        <!-- 链接关系 -->
   title="访问示例网站"            <!-- 提示信息 -->
   download="filename.pdf">       <!-- 下载文件名 -->
    点击访问
</a>
```

#### `target` 属性值
- `_blank`: 新窗口或标签页
- `_self`: 当前窗口（默认）
- `_parent`: 父框架
- `_top`: 顶层框架

#### `rel` 属性值
```html
<a href="external-site.com" rel="noopener nofollow">外部链接</a>
<a href="next-page.html" rel="next">下一页</a>
<a href="prev-page.html" rel="prev">上一页</a>
<a href="author-profile.html" rel="author">作者</a>
```

### 图片属性 (`<img>`)
```html
<img src="images/photo.jpg"        <!-- 图片路径（必需） -->
     alt="美丽的日落景色"           <!-- 替代文字（必需） -->
     width="400"                    <!-- 宽度 -->
     height="300"                   <!-- 高度 -->
     title="点击查看大图"           <!-- 提示文字 -->
     loading="lazy"                 <!-- 懒加载 -->
     crossorigin="anonymous"        <!-- 跨域设置 -->
     usemap="#imagemap">           <!-- 图片映射 -->
```

#### 响应式图片属性
```html
<img src="default.jpg" 
     srcset="small.jpg 300w, medium.jpg 600w, large.jpg 1200w"
     sizes="(max-width: 300px) 300px, (max-width: 600px) 600px, 1200px"
     alt="响应式图片">
```

### 表单属性详解

#### `<form>` 属性
```html
<form action="submit.php"          <!-- 提交地址 -->
      method="post"                <!-- 提交方法 -->
      enctype="multipart/form-data" <!-- 编码类型 -->
      target="_blank"              <!-- 提交目标 -->
      novalidate>                  <!-- 禁用验证 -->
```

#### `<input>` 属性
```html
<input type="text"                 <!-- 输入类型 -->
       name="username"             <!-- 字段名称 -->
       id="username"               <!-- 元素ID -->
       value="默认值"              <!-- 默认值 -->
       placeholder="请输入用户名"   <!-- 占位符 -->
       required                    <!-- 必填 -->
       readonly                    <!-- 只读 -->
       disabled                    <!-- 禁用 -->
       maxlength="20"              <!-- 最大长度 -->
       minlength="3"               <!-- 最小长度 -->
       pattern="[A-Za-z0-9]+"      <!-- 正则验证 -->
       autocomplete="username">    <!-- 自动完成 -->
```

#### 输入类型详解
```html
<!-- 文本输入 -->
<input type="text" placeholder="文本输入">
<input type="password" placeholder="密码输入">
<input type="email" placeholder="邮箱输入">
<input type="tel" placeholder="电话输入">
<input type="url" placeholder="网址输入">
<input type="search" placeholder="搜索输入">

<!-- 数字输入 -->
<input type="number" min="0" max="100" step="1" value="50">
<input type="range" min="0" max="100" step="10" value="30">

<!-- 日期时间输入 -->
<input type="date" value="2023-01-01">
<input type="time" value="12:00">
<input type="datetime-local" value="2023-01-01T12:00">
<input type="month" value="2023-01">
<input type="week" value="2023-W01">

<!-- 选择输入 -->
<input type="checkbox" checked>
<input type="radio" name="group" checked>

<!-- 文件输入 -->
<input type="file" accept="image/*" multiple>
<input type="file" accept=".pdf,.doc,.docx">

<!-- 其他输入 -->
<input type="color" value="#ff0000">
<input type="hidden" value="secret-value">
```

### 表格属性
```html
<table border="1" cellpadding="10" cellspacing="0">
    <tr>
        <th colspan="2">表头合并列</th>
    </tr>
    <tr>
        <td rowspan="2">合并行</td>
        <td>普通单元格</td>
    </tr>
    <tr>
        <td>另一个单元格</td>
    </tr>
</table>
```

## 布尔属性

布尔属性只需要出现即表示true，不需要赋值。

### 常见布尔属性
```html
<!-- 表单相关 -->
<input type="text" required>           <!-- 必填 -->
<input type="text" readonly>           <!-- 只读 -->
<input type="text" disabled>           <!-- 禁用 -->
<input type="checkbox" checked>        <!-- 选中 -->
<select multiple>                      <!-- 多选 -->
    <option selected>默认选项</option>  <!-- 默认选中 -->
</select>

<!-- 脚本相关 -->
<script async src="script.js"></script>    <!-- 异步加载 -->
<script defer src="script.js"></script>    <!-- 延迟执行 -->

<!-- 其他 -->
<details open>                         <!-- 展开详情 -->
    <summary>点击展开</summary>
    <p>详细内容</p>
</details>
<video controls autoplay muted>        <!-- 视频控制 -->
    <source src="video.mp4" type="video/mp4">
</video>
```

### 布尔属性的写法
```html
<!-- ✅ 推荐写法 -->
<input type="checkbox" checked>
<input type="text" required>

<!-- ✅ 也可以这样写 -->
<input type="checkbox" checked="checked">
<input type="text" required="required">

<!-- ✅ HTML5中这样也行 -->
<input type="checkbox" checked="">
<input type="text" required="">

<!-- ❌ 错误写法 -->
<input type="checkbox" checked="false">  <!-- 仍然是true -->
```

## 最佳实践

### 1. 属性编写规范
```html
<!-- ✅ 推荐格式 -->
<img class="hero-image" 
     id="main-banner" 
     src="banner.jpg" 
     alt="网站主横幅图片"
     width="800" 
     height="400"
     loading="lazy">

<!-- ✅ 属性顺序建议 -->
<!-- 1. class, id -->
<!-- 2. src, href, type -->
<!-- 3. alt, title -->
<!-- 4. 其他功能属性 -->
<!-- 5. 数据属性 -->
```

### 2. 引号使用
```html
<!-- ✅ 推荐：始终使用双引号 -->
<div class="container" id="main">

<!-- ✅ 可以：使用单引号 -->
<div class='container' id='main'>

<!-- ⚠️ 注意：属性值包含引号时 -->
<p title='He said "Hello"'>文本</p>
<p title="He said 'Hello'">文本</p>
```

### 3. 语义化属性使用
```html
<!-- ✅ 语义化命名 -->
<nav class="main-navigation" id="primary-nav">
<article class="blog-post" id="post-123">
<section class="contact-form" id="contact-section">

<!-- ❌ 避免无意义命名 -->
<div class="div1" id="thing">
<span class="red-text" id="text123">
```

### 4. 无障碍访问属性
```html
<!-- ARIA属性 -->
<button aria-label="关闭对话框" aria-expanded="false">×</button>
<input type="text" aria-describedby="username-help">
<div id="username-help">用户名必须是3-20个字符</div>

<!-- 语义化增强 -->
<nav role="navigation" aria-label="主导航">
<main role="main" aria-label="主要内容">
<aside role="complementary" aria-label="侧边栏">
```

## 验证和调试

### 属性验证工具
- **W3C HTML验证器**: 检查属性使用是否正确
- **浏览器开发工具**: 检查元素属性实时状态
- **WAVE**: 无障碍访问检查工具

### 常见错误
```html
<!-- ❌ 属性名拼写错误 -->
<img scr="image.jpg" altt="图片">  <!-- 应该是 src 和 alt -->

<!-- ❌ 必需属性缺失 -->
<img src="image.jpg">             <!-- 缺少 alt 属性 -->
<input type="text">               <!-- 缺少 name 属性 -->

<!-- ❌ 属性值格式错误 -->
<input type="number" max="abc">   <!-- max应该是数字 -->
<img width="100px" height="50px"> <!-- width和height不需要单位 -->
```

## 下一步学习

现在您已经掌握了HTML属性的使用方法，让我们继续学习：

1. [文本格式化](./05-text-formatting.md) - 学习如何格式化和美化文本
2. [列表元素](./06-lists.md) - 掌握创建各种类型的列表

---

> 💡 **练习建议**: 创建一个包含不同属性的HTML页面，体验各种属性的效果和用法！

*[上一章: 标签语法](./03-tag-syntax.md) | [返回主目录](./README.md) | [下一章: 文本格式化](./05-text-formatting.md)*
