# HTML标签速查表

> [返回主目录](../README.md)

## 文档结构标签

| 标签 | 描述 | 示例 |
|------|------|------|
| `<!DOCTYPE html>` | 声明HTML5文档类型 | `<!DOCTYPE html>` |
| `<html>` | 根元素 | `<html lang="zh-CN">` |
| `<head>` | 文档头部信息 | `<head>...</head>` |
| `<body>` | 文档主体内容 | `<body>...</body>` |
| `<title>` | 文档标题 | `<title>页面标题</title>` |
| `<meta>` | 元数据 | `<meta charset="UTF-8">` |
| `<link>` | 外部资源链接 | `<link rel="stylesheet" href="style.css">` |
| `<script>` | 脚本 | `<script src="script.js"></script>` |

## 文本内容标签

| 标签 | 描述 | 示例 |
|------|------|------|
| `<h1>` - `<h6>` | 标题（1级到6级） | `<h1>主标题</h1>` |
| `<p>` | 段落 | `<p>这是一个段落</p>` |
| `<div>` | 通用容器（块级） | `<div class="container">内容</div>` |
| `<span>` | 通用容器（行内） | `<span class="highlight">文本</span>` |
| `<br>` | 换行 | `第一行<br>第二行` |
| `<hr>` | 水平分隔线 | `<hr>` |
| `<pre>` | 预格式化文本 | `<pre>保持格式的文本</pre>` |
| `<blockquote>` | 引用块 | `<blockquote cite="source">引用内容</blockquote>` |
| `<address>` | 地址信息 | `<address>联系地址</address>` |

## 文本格式化标签

| 标签 | 描述 | 示例 |
|------|------|------|
| `<strong>` | 重要文本（粗体） | `<strong>重要</strong>` |
| `<em>` | 强调文本（斜体） | `<em>强调</em>` |
| `<b>` | 粗体样式 | `<b>粗体</b>` |
| `<i>` | 斜体样式 | `<i>斜体</i>` |
| `<u>` | 下划线 | `<u>下划线</u>` |
| `<small>` | 小号文字 | `<small>小字</small>` |
| `<mark>` | 高亮标记 | `<mark>高亮</mark>` |
| `<del>` | 删除线 | `<del>删除的文本</del>` |
| `<ins>` | 插入文本 | `<ins>插入的文本</ins>` |
| `<sub>` | 下标 | `H<sub>2</sub>O` |
| `<sup>` | 上标 | `E=MC<sup>2</sup>` |
| `<code>` | 行内代码 | `<code>console.log()</code>` |
| `<kbd>` | 键盘输入 | `<kbd>Ctrl</kbd>+<kbd>C</kbd>` |
| `<samp>` | 示例输出 | `<samp>Hello World</samp>` |
| `<var>` | 变量 | `<var>x</var> = 10` |
| `<abbr>` | 缩写 | `<abbr title="HyperText Markup Language">HTML</abbr>` |
| `<cite>` | 引用来源 | `<cite>《HTML权威指南》</cite>` |
| `<q>` | 短引用 | `<q>简短引用</q>` |
| `<time>` | 时间日期 | `<time datetime="2023-01-01">2023年1月1日</time>` |

## 列表标签

| 标签 | 描述 | 示例 |
|------|------|------|
| `<ul>` | 无序列表 | `<ul><li>项目1</li><li>项目2</li></ul>` |
| `<ol>` | 有序列表 | `<ol><li>第一项</li><li>第二项</li></ol>` |
| `<li>` | 列表项 | `<li>列表项内容</li>` |
| `<dl>` | 描述列表 | `<dl><dt>术语</dt><dd>描述</dd></dl>` |
| `<dt>` | 描述术语 | `<dt>HTML</dt>` |
| `<dd>` | 术语描述 | `<dd>标记语言</dd>` |

## 链接和多媒体标签

| 标签 | 描述 | 示例 |
|------|------|------|
| `<a>` | 超链接 | `<a href="page.html">链接</a>` |
| `<img>` | 图片 | `<img src="image.jpg" alt="描述">` |
| `<audio>` | 音频 | `<audio src="music.mp3" controls></audio>` |
| `<video>` | 视频 | `<video src="video.mp4" controls></video>` |
| `<source>` | 媒体资源 | `<source src="video.webm" type="video/webm">` |
| `<track>` | 字幕轨道 | `<track src="subtitles.vtt" kind="subtitles">` |
| `<picture>` | 响应式图片容器 | `<picture><source><img></picture>` |
| `<figure>` | 图表容器 | `<figure><img><figcaption></figure>` |
| `<figcaption>` | 图表标题 | `<figcaption>图表说明</figcaption>` |

## 表格标签

| 标签 | 描述 | 示例 |
|------|------|------|
| `<table>` | 表格 | `<table>...</table>` |
| `<thead>` | 表格头部 | `<thead><tr><th>标题</th></tr></thead>` |
| `<tbody>` | 表格主体 | `<tbody><tr><td>数据</td></tr></tbody>` |
| `<tfoot>` | 表格底部 | `<tfoot><tr><td>总计</td></tr></tfoot>` |
| `<tr>` | 表格行 | `<tr><td>单元格</td></tr>` |
| `<th>` | 表头单元格 | `<th scope="col">列标题</th>` |
| `<td>` | 数据单元格 | `<td>数据内容</td>` |
| `<caption>` | 表格标题 | `<caption>表格标题</caption>` |
| `<colgroup>` | 列组 | `<colgroup><col span="2"></colgroup>` |
| `<col>` | 列 | `<col style="background-color: yellow">` |

## 表单标签

| 标签 | 描述 | 示例 |
|------|------|------|
| `<form>` | 表单 | `<form action="submit.php" method="post">` |
| `<input>` | 输入控件 | `<input type="text" name="username">` |
| `<textarea>` | 多行文本输入 | `<textarea rows="4" cols="50"></textarea>` |
| `<select>` | 下拉选择 | `<select><option>选项1</option></select>` |
| `<option>` | 选择项 | `<option value="1">选项1</option>` |
| `<optgroup>` | 选项分组 | `<optgroup label="分组"><option>选项</option></optgroup>` |
| `<button>` | 按钮 | `<button type="submit">提交</button>` |
| `<label>` | 标签 | `<label for="username">用户名</label>` |
| `<fieldset>` | 字段分组 | `<fieldset><legend>分组</legend></fieldset>` |
| `<legend>` | 分组标题 | `<legend>个人信息</legend>` |
| `<datalist>` | 预定义选项 | `<datalist id="browsers"><option>Chrome</option></datalist>` |
| `<output>` | 输出结果 | `<output for="range">50</output>` |

## HTML5语义化标签

| 标签 | 描述 | 示例 |
|------|------|------|
| `<header>` | 页面或区块头部 | `<header><h1>网站标题</h1></header>` |
| `<nav>` | 导航链接 | `<nav><ul><li><a href="/">首页</a></li></ul></nav>` |
| `<main>` | 主要内容 | `<main><article>文章内容</article></main>` |
| `<article>` | 独立文章 | `<article><h2>文章标题</h2><p>内容</p></article>` |
| `<section>` | 文档章节 | `<section><h3>章节标题</h3><p>内容</p></section>` |
| `<aside>` | 侧边栏内容 | `<aside><h4>相关文章</h4><ul>...</ul></aside>` |
| `<footer>` | 页面或区块底部 | `<footer><p>&copy; 2023 网站名称</p></footer>` |
| `<details>` | 可展开详情 | `<details><summary>点击展开</summary><p>详细内容</p></details>` |
| `<summary>` | 详情摘要 | `<summary>展开查看更多</summary>` |
| `<dialog>` | 对话框 | `<dialog open><p>对话框内容</p></dialog>` |

## 嵌入内容标签

| 标签 | 描述 | 示例 |
|------|------|------|
| `<iframe>` | 内嵌框架 | `<iframe src="page.html"></iframe>` |
| `<embed>` | 嵌入内容 | `<embed src="file.swf" type="application/x-shockwave-flash">` |
| `<object>` | 对象 | `<object data="file.pdf" type="application/pdf"></object>` |
| `<param>` | 对象参数 | `<param name="autoplay" value="false">` |
| `<canvas>` | 画布 | `<canvas width="300" height="200"></canvas>` |
| `<svg>` | 矢量图形 | `<svg width="100" height="100"><circle cx="50" cy="50" r="40"></svg>` |
| `<math>` | 数学公式 | `<math><mi>x</mi><mo>=</mo><mn>2</mn></math>` |

## 交互元素标签

| 标签 | 描述 | 示例 |
|------|------|------|
| `<details>` | 可折叠详情 | `<details><summary>标题</summary>内容</details>` |
| `<summary>` | 详情摘要 | `<summary>点击展开</summary>` |
| `<menu>` | 菜单 | `<menu><li><button>选项1</button></li></menu>` |
| `<menuitem>` | 菜单项（已废弃） | 不推荐使用 |

## 已废弃的标签（不推荐使用）

| 标签 | 替代方案 |
|------|----------|
| `<center>` | 使用CSS `text-align: center` |
| `<font>` | 使用CSS字体属性 |
| `<big>` | 使用CSS `font-size` |
| `<strike>` | 使用`<del>`或CSS `text-decoration: line-through` |
| `<tt>` | 使用`<code>`或CSS `font-family: monospace` |
| `<frame>` | 使用`<iframe>`或现代布局方案 |
| `<frameset>` | 使用现代布局方案 |
| `<noframes>` | 不再需要 |
| `<applet>` | 使用`<object>`或`<embed>` |
| `<basefont>` | 使用CSS字体属性 |
| `<dir>` | 使用`<ul>` |
| `<isindex>` | 使用表单控件 |
| `<listing>` | 使用`<pre>`或`<code>` |
| `<plaintext>` | 使用`<pre>` |
| `<xmp>` | 使用`<pre>`或`<code>` |

## 按功能分类快速查找

### 页面结构
```html
<!DOCTYPE html>
<html lang="zh-CN">
<head>
    <meta charset="UTF-8">
    <title>标题</title>
</head>
<body>
    <header>头部</header>
    <nav>导航</nav>
    <main>
        <article>文章</article>
        <section>章节</section>
    </main>
    <aside>侧边栏</aside>
    <footer>底部</footer>
</body>
</html>
```

### 文本内容
```html
<h1>标题</h1>
<p>段落</p>
<strong>重要</strong>
<em>强调</em>
<mark>高亮</mark>
<code>代码</code>
```

### 列表
```html
<ul>
    <li>无序列表项</li>
</ul>

<ol>
    <li>有序列表项</li>
</ol>

<dl>
    <dt>术语</dt>
    <dd>描述</dd>
</dl>
```

### 媒体
```html
<img src="image.jpg" alt="图片">
<audio src="music.mp3" controls></audio>
<video src="video.mp4" controls></video>
```

### 表格
```html
<table>
    <thead>
        <tr><th>表头</th></tr>
    </thead>
    <tbody>
        <tr><td>数据</td></tr>
    </tbody>
</table>
```

### 表单
```html
<form>
    <label>标签</label>
    <input type="text">
    <textarea></textarea>
    <select><option>选项</option></select>
    <button>按钮</button>
</form>
```

---

> 💡 **提示**: 现代HTML开发推荐使用语义化标签和CSS样式，避免使用已废弃的标签！

*[返回主目录](../README.md)*
