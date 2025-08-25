# HTML标签语法

> [上一章: 文档结构](./02-document-structure.md) | [返回主目录](./README.md) | [下一章: HTML属性](./04-attributes.md)

## 标签的基本语法

HTML标签是HTML语言的核心，用于标记和定义网页内容的结构和含义。

### 标签的基本结构
```html
<标签名 属性名="属性值">内容</标签名>
```

### 语法要素
- **尖括号** `< >`: 标签的边界标记
- **标签名**: 定义元素的类型
- **属性**: 为元素提供额外信息
- **内容**: 标签包围的文本或其他元素
- **结束标签**: 以 `/` 开始的闭合标签

## 标签的类型

### 1. 双标签（容器标签）

双标签有开始标签和结束标签，可以包含内容。

#### 基本语法
```html
<开始标签>内容</结束标签>
```

#### 常见双标签示例
```html
<!-- 段落 -->
<p>这是一个段落。</p>

<!-- 标题 -->
<h1>这是一级标题</h1>
<h2>这是二级标题</h2>

<!-- 容器 -->
<div>这是一个通用容器</div>
<span>这是一个行内容器</span>

<!-- 强调 -->
<strong>重要文本</strong>
<em>强调文本</em>

<!-- 链接 -->
<a href="https://example.com">这是一个链接</a>
```

#### 特点
- ✅ **可以包含内容**: 文本、其他标签等
- ✅ **可以嵌套**: 标签内可以包含其他标签
- ✅ **必须闭合**: 每个开始标签都要有对应的结束标签

### 2. 单标签（自闭合标签）

单标签只有开始标签，没有结束标签，通常用于插入特定内容或创建分隔。

#### 基本语法
```html
<标签名 属性="值">
<!-- 或者 -->
<标签名 属性="值" />  <!-- XHTML风格，可选 -->
```

#### 常见单标签示例
```html
<!-- 图片 -->
<img src="photo.jpg" alt="照片描述">

<!-- 换行 -->
<br>

<!-- 水平分隔线 -->
<hr>

<!-- 输入框 -->
<input type="text" name="username">

<!-- 元数据 -->
<meta charset="UTF-8">

<!-- 链接外部资源 -->
<link rel="stylesheet" href="style.css">
```

#### 特点
- ❌ **不能包含内容**: 不能在标签内放置文本或其他元素
- ❌ **不能嵌套**: 单标签内不能包含其他标签
- ✅ **自闭合**: 不需要结束标签

## 标签嵌套规则

### 正确的嵌套
标签必须正确嵌套，内层标签必须在外层标签内完全闭合。

#### ✅ 正确示例
```html
<!-- 基本嵌套 -->
<div>
    <h1>标题</h1>
    <p>这是一个<strong>重要</strong>的段落。</p>
</div>

<!-- 多层嵌套 -->
<article>
    <header>
        <h2>文章标题</h2>
        <p>发布时间：<time datetime="2023-01-01">2023年1月1日</time></p>
    </header>
    <section>
        <p>文章内容<em>强调部分</em>继续内容。</p>
    </section>
</article>

<!-- 列表嵌套 -->
<ul>
    <li>项目1</li>
    <li>项目2
        <ul>
            <li>子项目2.1</li>
            <li>子项目2.2</li>
        </ul>
    </li>
</ul>
```

#### ❌ 错误示例
```html
<!-- 交叉嵌套 - 错误 -->
<p><strong>重要文本</p></strong>

<!-- 应该是 -->
<p><strong>重要文本</strong></p>

<!-- 标签未闭合 - 错误 -->
<div>
    <p>段落1
    <p>段落2
</div>

<!-- 应该是 -->
<div>
    <p>段落1</p>
    <p>段落2</p>
</div>
```

### 嵌套规则要点
1. **先开后闭**: 后打开的标签必须先闭合
2. **完全包含**: 内层标签必须完全在外层标签内
3. **不能交叉**: 标签不能交叉嵌套

## 标签的分类

### 按显示特性分类

#### 1. 块级元素（Block Elements）
- **特点**: 独占一行，可设置宽高
- **默认宽度**: 100%（填满父容器）
- **可包含**: 其他块级元素和行内元素

```html
<div>块级容器</div>
<p>段落</p>
<h1>标题</h1>
<section>章节</section>
<article>文章</article>
<header>头部</header>
<footer>底部</footer>
<nav>导航</nav>
<aside>侧边栏</aside>
<main>主内容</main>
```

#### 2. 行内元素（Inline Elements）
- **特点**: 不独占一行，不可设置宽高
- **宽度**: 由内容决定
- **可包含**: 文本和其他行内元素

```html
<span>行内容器</span>
<a href="#">链接</a>
<strong>重要文本</strong>
<em>强调文本</em>
<code>代码</code>
<small>小字</small>
<sub>下标</sub>
<sup>上标</sup>
```

#### 3. 行内块元素（Inline-Block Elements）
- **特点**: 不独占一行，但可设置宽高
- **结合优点**: 既有行内元素的排列特性，又有块级元素的尺寸控制

```html
<img src="image.jpg" alt="图片">
<input type="text">
<button>按钮</button>
<select>下拉框</select>
```

### 按语义分类

#### 1. 结构性标签
```html
<html>、<head>、<body>、<header>、<nav>、<main>、<section>、<article>、<aside>、<footer>
```

#### 2. 文本内容标签
```html
<h1>-<h6>、<p>、<div>、<span>、<pre>、<blockquote>
```

#### 3. 列表标签
```html
<ul>、<ol>、<li>、<dl>、<dt>、<dd>
```

#### 4. 表格标签
```html
<table>、<thead>、<tbody>、<tfoot>、<tr>、<th>、<td>
```

#### 5. 表单标签
```html
<form>、<input>、<textarea>、<select>、<option>、<button>、<label>
```

#### 6. 多媒体标签
```html
<img>、<audio>、<video>、<source>、<track>
```

## 标签命名规则

### HTML标签命名特点
- **小写字母**: HTML标签不区分大小写，但建议使用小写
- **预定义**: 标签名称是HTML规范预定义的，不能自定义
- **语义化**: 标签名称通常反映其语义含义

#### ✅ 推荐写法
```html
<div>容器</div>
<p>段落</p>
<img src="image.jpg" alt="图片">
```

#### ⚠️ 不推荐但可用
```html
<DIV>容器</DIV>
<P>段落</P>
<IMG SRC="image.jpg" ALT="图片">
```

### 标签选择原则

#### 1. 语义优先
选择最能表达内容含义的标签：

```html
<!-- ✅ 好的做法 -->
<article>
    <h1>文章标题</h1>
    <p>文章内容...</p>
</article>

<nav>
    <ul>
        <li><a href="/">首页</a></li>
        <li><a href="/about">关于</a></li>
    </ul>
</nav>

<!-- ❌ 不好的做法 -->
<div class="article">
    <div class="title">文章标题</div>
    <div class="content">文章内容...</div>
</div>

<div class="navigation">
    <div class="menu-item"><a href="/">首页</a></div>
    <div class="menu-item"><a href="/about">关于</a></div>
</div>
```

#### 2. 结构清晰
使用合适的标签建立清晰的文档结构：

```html
<body>
    <header>
        <h1>网站标题</h1>
        <nav>导航菜单</nav>
    </header>
    
    <main>
        <article>
            <header>
                <h2>文章标题</h2>
                <time>发布时间</time>
            </header>
            <section>
                <h3>章节标题</h3>
                <p>章节内容</p>
            </section>
        </article>
    </main>
    
    <aside>
        <section>
            <h3>相关文章</h3>
            <ul>相关文章列表</ul>
        </section>
    </aside>
    
    <footer>
        <p>版权信息</p>
    </footer>
</body>
```

## 注释语法

HTML注释用于在代码中添加说明，不会在浏览器中显示。

### 注释语法
```html
<!-- 这是一个注释 -->

<!-- 
这是一个
多行注释
-->
```

### 注释的用途
```html
<!-- 页面头部开始 -->
<header>
    <h1>网站标题</h1>
    <!-- TODO: 添加搜索功能 -->
    <nav>
        <!-- 主导航菜单 -->
        <ul>
            <li><a href="/">首页</a></li>
            <li><a href="/about">关于</a></li>
        </ul>
    </nav>
</header>
<!-- 页面头部结束 -->

<!-- 
版本信息：
- 创建时间：2023-01-01
- 最后修改：2023-01-15
- 作者：张三
-->
```

### 注释最佳实践

#### ✅ 好的注释
```html
<!-- 用户登录表单 -->
<form id="login-form">
    <!-- 必填字段 -->
    <input type="email" name="email" required>
    <input type="password" name="password" required>
    
    <!-- 可选记住登录状态 -->
    <label>
        <input type="checkbox" name="remember"> 记住我
    </label>
    
    <button type="submit">登录</button>
</form>
```

#### ❌ 过度的注释
```html
<!-- 这是一个div -->
<div>
    <!-- 这是一个段落 -->
    <p>这是段落文本</p> <!-- 段落结束 -->
</div> <!-- div结束 -->
```

## 常见错误和解决方法

### 1. 标签未闭合
```html
<!-- ❌ 错误 -->
<div>
    <p>段落1
    <p>段落2
</div>

<!-- ✅ 正确 -->
<div>
    <p>段落1</p>
    <p>段落2</p>
</div>
```

### 2. 标签嵌套错误
```html
<!-- ❌ 错误 -->
<p><div>不能在段落中放块级元素</div></p>

<!-- ✅ 正确 -->
<div>
    <p>段落在容器中</p>
</div>
```

### 3. 属性值未加引号
```html
<!-- ❌ 错误（在某些情况下） -->
<img src=image.jpg alt=图片>

<!-- ✅ 正确 -->
<img src="image.jpg" alt="图片">
```

### 4. 大小写不一致
```html
<!-- ❌ 不推荐 -->
<DIV Class="container">
    <P>段落</p>
</div>

<!-- ✅ 推荐 -->
<div class="container">
    <p>段落</p>
</div>
```

## 验证工具

### 在线验证
- **W3C HTML验证器**: https://validator.w3.org/
- **Nu Html Checker**: https://validator.w3.org/nu/

### 浏览器开发工具
- **Chrome DevTools**: F12 → Elements → 检查HTML结构
- **Firefox Developer Tools**: F12 → Inspector

### 编辑器插件
- **VS Code**: HTML snippets, Auto Close Tag
- **Sublime Text**: Emmet, HTML-CSS-JS Prettify

## 下一步学习

现在您已经掌握了HTML标签的基本语法，让我们继续学习：

1. [HTML属性](./04-attributes.md) - 学习如何使用属性增强元素功能
2. [文本格式化](./05-text-formatting.md) - 掌握文本显示和格式化技巧

---

> 💡 **练习建议**: 创建一个简单的HTML页面，尝试使用不同类型的标签，并注意正确的嵌套关系！

*[上一章: 文档结构](./02-document-structure.md) | [返回主目录](./README.md) | [下一章: HTML属性](./04-attributes.md)*
