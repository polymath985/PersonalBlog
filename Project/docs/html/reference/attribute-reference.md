# HTML属性速查表

> [返回主目录](../README.md)

## 全局属性（所有元素通用）

| 属性 | 描述 | 示例 | 值类型 |
|------|------|------|--------|
| `id` | 元素唯一标识符 | `<div id="header">` | 字符串（页面唯一） |
| `class` | CSS类名 | `<p class="text-large red">` | 字符串（空格分隔多个） |
| `style` | 内联CSS样式 | `<span style="color: red;">` | CSS声明 |
| `title` | 元素提示信息 | `<p title="这是提示">` | 字符串 |
| `lang` | 元素语言 | `<span lang="en">` | 语言代码 |
| `dir` | 文本方向 | `<p dir="rtl">` | `ltr`、`rtl`、`auto` |
| `hidden` | 隐藏元素 | `<div hidden>` | 布尔属性 |
| `tabindex` | Tab键顺序 | `<input tabindex="1">` | 整数 |
| `accesskey` | 快捷键 | `<button accesskey="s">` | 字符 |
| `contenteditable` | 可编辑内容 | `<div contenteditable="true">` | `true`、`false` |
| `draggable` | 可拖拽 | `<img draggable="true">` | `true`、`false`、`auto` |
| `spellcheck` | 拼写检查 | `<textarea spellcheck="true">` | `true`、`false` |
| `translate` | 翻译控制 | `<p translate="no">` | `yes`、`no` |

## 数据属性

| 格式 | 描述 | 示例 | JavaScript访问 |
|------|------|------|----------------|
| `data-*` | 自定义数据属性 | `<div data-user-id="123">` | `element.dataset.userId` |
| | | `<span data-role="admin">` | `element.dataset.role` |
| | | `<p data-toggle-state="open">` | `element.dataset.toggleState` |

## ARIA属性（无障碍访问）

| 属性 | 描述 | 示例 |
|------|------|------|
| `role` | 元素角色 | `<div role="button">` |
| `aria-label` | 无障碍标签 | `<button aria-label="关闭">×</button>` |
| `aria-labelledby` | 标签引用 | `<input aria-labelledby="username-label">` |
| `aria-describedby` | 描述引用 | `<input aria-describedby="pwd-help">` |
| `aria-expanded` | 展开状态 | `<button aria-expanded="false">` |
| `aria-hidden` | 对屏幕阅读器隐藏 | `<span aria-hidden="true">👍</span>` |
| `aria-live` | 动态内容通知 | `<div aria-live="polite">` |
| `aria-required` | 必填标识 | `<input aria-required="true">` |

## 链接标签 `<a>` 属性

| 属性 | 描述 | 示例 | 可选值 |
|------|------|------|--------|
| `href` | 链接地址 | `<a href="page.html">` | URL、锚点、邮件、电话 |
| `target` | 打开方式 | `<a target="_blank">` | `_blank`、`_self`、`_parent`、`_top` |
| `rel` | 链接关系 | `<a rel="noopener nofollow">` | 见下表 |
| `download` | 下载文件名 | `<a download="file.pdf">` | 文件名 |
| `hreflang` | 链接语言 | `<a hreflang="en">` | 语言代码 |
| `type` | MIME类型 | `<a type="application/pdf">` | MIME类型 |

### `rel` 属性值详解

| 值 | 描述 | 使用场景 |
|----|------|----------|
| `noopener` | 不访问opener对象 | 外部链接安全 |
| `noreferrer` | 不发送referrer | 隐私保护 |
| `nofollow` | 不传递权重 | SEO控制 |
| `external` | 外部链接 | 标识外部资源 |
| `next` | 下一页 | 分页导航 |
| `prev` | 上一页 | 分页导航 |
| `bookmark` | 书签 | 永久链接 |
| `help` | 帮助文档 | 帮助链接 |
| `license` | 许可证 | 版权信息 |
| `author` | 作者 | 作者链接 |
| `alternate` | 替代版本 | RSS、多语言 |
| `canonical` | 规范链接 | SEO优化 |

## 图片标签 `<img>` 属性

| 属性 | 描述 | 示例 | 必需性 |
|------|------|------|--------|
| `src` | 图片地址 | `<img src="photo.jpg">` | 必需 |
| `alt` | 替代文字 | `<img alt="风景照片">` | 必需 |
| `width` | 宽度（像素） | `<img width="300">` | 可选 |
| `height` | 高度（像素） | `<img height="200">` | 可选 |
| `loading` | 加载方式 | `<img loading="lazy">` | `lazy`、`eager` |
| `decoding` | 解码方式 | `<img decoding="async">` | `async`、`sync`、`auto` |
| `crossorigin` | 跨域设置 | `<img crossorigin="anonymous">` | `anonymous`、`use-credentials` |
| `usemap` | 图片映射 | `<img usemap="#map1">` | 映射名称 |
| `ismap` | 服务端图片映射 | `<img ismap>` | 布尔属性 |

### 响应式图片属性

| 属性 | 描述 | 示例 |
|------|------|------|
| `srcset` | 多尺寸图片源 | `<img srcset="small.jpg 300w, large.jpg 1200w">` |
| `sizes` | 视口尺寸 | `<img sizes="(max-width: 600px) 300px, 1200px">` |

## 表单元素属性

### `<form>` 属性

| 属性 | 描述 | 示例 | 可选值 |
|------|------|------|--------|
| `action` | 提交地址 | `<form action="submit.php">` | URL |
| `method` | 提交方法 | `<form method="post">` | `get`、`post` |
| `enctype` | 编码类型 | `<form enctype="multipart/form-data">` | 见下表 |
| `target` | 提交目标 | `<form target="_blank">` | 窗口名称 |
| `autocomplete` | 自动完成 | `<form autocomplete="off">` | `on`、`off` |
| `novalidate` | 禁用验证 | `<form novalidate>` | 布尔属性 |
| `accept-charset` | 字符编码 | `<form accept-charset="UTF-8">` | 编码列表 |

#### `enctype` 值详解

| 值 | 描述 | 使用场景 |
|----|------|----------|
| `application/x-www-form-urlencoded` | URL编码（默认） | 普通表单 |
| `multipart/form-data` | 多部分数据 | 文件上传 |
| `text/plain` | 纯文本 | 简单文本 |

### `<input>` 属性

#### 通用属性

| 属性 | 描述 | 示例 |
|------|------|------|
| `type` | 输入类型 | `<input type="text">` |
| `name` | 字段名称 | `<input name="username">` |
| `value` | 默认值 | `<input value="默认值">` |
| `placeholder` | 占位符文本 | `<input placeholder="请输入...">` |
| `required` | 必填字段 | `<input required>` |
| `disabled` | 禁用控件 | `<input disabled>` |
| `readonly` | 只读 | `<input readonly>` |
| `autofocus` | 自动聚焦 | `<input autofocus>` |
| `autocomplete` | 自动完成 | `<input autocomplete="username">` |
| `form` | 关联表单 | `<input form="form1">` |

#### 验证属性

| 属性 | 描述 | 示例 | 适用类型 |
|------|------|------|----------|
| `required` | 必填 | `<input required>` | 多数类型 |
| `pattern` | 正则验证 | `<input pattern="[0-9]{6}">` | text, search, url, tel, email, password |
| `minlength` | 最小长度 | `<input minlength="3">` | text, search, url, tel, email, password |
| `maxlength` | 最大长度 | `<input maxlength="20">` | text, search, url, tel, email, password |
| `min` | 最小值 | `<input type="number" min="0">` | number, range, date, time |
| `max` | 最大值 | `<input type="number" max="100">` | number, range, date, time |
| `step` | 步长 | `<input type="number" step="0.1">` | number, range |
| `multiple` | 多选 | `<input type="file" multiple>` | file, email |
| `accept` | 接受文件类型 | `<input type="file" accept="image/*">` | file |

### `<input>` 类型属性

| type值 | 描述 | 特殊属性 |
|--------|------|----------|
| `text` | 单行文本（默认） | `minlength`, `maxlength`, `pattern` |
| `password` | 密码输入 | `minlength`, `maxlength`, `pattern` |
| `email` | 邮箱输入 | `multiple`, `pattern` |
| `url` | URL输入 | `pattern` |
| `tel` | 电话输入 | `pattern` |
| `search` | 搜索输入 | `pattern` |
| `number` | 数字输入 | `min`, `max`, `step` |
| `range` | 范围滑块 | `min`, `max`, `step` |
| `date` | 日期选择 | `min`, `max`, `step` |
| `time` | 时间选择 | `min`, `max`, `step` |
| `datetime-local` | 日期时间 | `min`, `max`, `step` |
| `month` | 月份选择 | `min`, `max`, `step` |
| `week` | 周选择 | `min`, `max`, `step` |
| `color` | 颜色选择器 | - |
| `checkbox` | 复选框 | `checked` |
| `radio` | 单选框 | `checked` |
| `file` | 文件上传 | `accept`, `multiple` |
| `hidden` | 隐藏字段 | - |
| `submit` | 提交按钮 | - |
| `reset` | 重置按钮 | - |
| `button` | 普通按钮 | - |
| `image` | 图片按钮 | `src`, `alt`, `width`, `height` |

### `<textarea>` 属性

| 属性 | 描述 | 示例 |
|------|------|------|
| `rows` | 可见行数 | `<textarea rows="4">` |
| `cols` | 可见列数 | `<textarea cols="50">` |
| `wrap` | 文本换行 | `<textarea wrap="soft">` |
| `resize` | 调整大小 | CSS属性：`resize: vertical` |

### `<select>` 属性

| 属性 | 描述 | 示例 |
|------|------|------|
| `multiple` | 多选模式 | `<select multiple>` |
| `size` | 可见选项数 | `<select size="5">` |
| `autofocus` | 自动聚焦 | `<select autofocus>` |

### `<option>` 属性

| 属性 | 描述 | 示例 |
|------|------|------|
| `value` | 选项值 | `<option value="1">` |
| `selected` | 默认选中 | `<option selected>` |
| `disabled` | 禁用选项 | `<option disabled>` |
| `label` | 选项标签 | `<option label="选项1">` |

### `<button>` 属性

| 属性 | 描述 | 示例 | 可选值 |
|------|------|------|--------|
| `type` | 按钮类型 | `<button type="submit">` | `submit`, `reset`, `button` |
| `form` | 关联表单 | `<button form="form1">` | 表单ID |
| `formaction` | 覆盖action | `<button formaction="alt.php">` | URL |
| `formmethod` | 覆盖method | `<button formmethod="get">` | `get`, `post` |
| `formnovalidate` | 跳过验证 | `<button formnovalidate>` | 布尔属性 |
| `formtarget` | 覆盖target | `<button formtarget="_blank">` | 窗口名称 |

## 表格属性

### `<table>` 属性（部分已废弃）

| 属性 | 描述 | 示例 | 状态 |
|------|------|------|------|
| `border` | 边框宽度 | `<table border="1">` | 已废弃，用CSS |
| `cellpadding` | 单元格内边距 | `<table cellpadding="5">` | 已废弃，用CSS |
| `cellspacing` | 单元格间距 | `<table cellspacing="0">` | 已废弃，用CSS |
| `width` | 表格宽度 | `<table width="100%">` | 已废弃，用CSS |

### `<th>`, `<td>` 属性

| 属性 | 描述 | 示例 |
|------|------|------|
| `colspan` | 跨列数 | `<td colspan="2">` |
| `rowspan` | 跨行数 | `<td rowspan="3">` |
| `headers` | 关联表头 | `<td headers="h1 h2">` |
| `scope` | 表头范围 | `<th scope="col">` |

#### `scope` 属性值

| 值 | 描述 | 使用场景 |
|----|------|----------|
| `row` | 行标题 | 行表头 |
| `col` | 列标题 | 列表头 |
| `rowgroup` | 行组标题 | 复杂表格 |
| `colgroup` | 列组标题 | 复杂表格 |

## 媒体元素属性

### `<audio>`, `<video>` 共同属性

| 属性 | 描述 | 示例 |
|------|------|------|
| `src` | 媒体文件地址 | `<video src="movie.mp4">` |
| `controls` | 显示控制条 | `<audio controls>` |
| `autoplay` | 自动播放 | `<video autoplay>` |
| `loop` | 循环播放 | `<audio loop>` |
| `muted` | 静音 | `<video muted>` |
| `preload` | 预加载 | `<audio preload="auto">` |
| `crossorigin` | 跨域设置 | `<video crossorigin="anonymous">` |

#### `preload` 属性值

| 值 | 描述 |
|----|------|
| `auto` | 预加载整个媒体文件 |
| `metadata` | 只预加载元数据 |
| `none` | 不预加载 |

### `<video>` 特有属性

| 属性 | 描述 | 示例 |
|------|------|------|
| `width` | 视频宽度 | `<video width="640">` |
| `height` | 视频高度 | `<video height="480">` |
| `poster` | 封面图片 | `<video poster="cover.jpg">` |
| `playsinline` | 内联播放 | `<video playsinline>` |

### `<source>` 属性

| 属性 | 描述 | 示例 |
|------|------|------|
| `src` | 媒体文件地址 | `<source src="video.webm">` |
| `type` | MIME类型 | `<source type="video/webm">` |
| `media` | 媒体查询 | `<source media="(min-width: 800px)">` |

## Meta 标签属性

### 常用 `<meta>` 标签

| name/property | content示例 | 描述 |
|---------------|-------------|------|
| `charset` | `UTF-8` | 字符编码 |
| `viewport` | `width=device-width, initial-scale=1.0` | 视口设置 |
| `description` | `网页描述` | 页面描述 |
| `keywords` | `关键词1,关键词2` | 关键词 |
| `author` | `作者姓名` | 作者信息 |
| `robots` | `index,follow` | 搜索引擎指令 |
| `refresh` | `30;url=http://example.com` | 页面刷新 |

### Open Graph (Facebook) Meta标签

| property | 描述 | 示例 |
|----------|------|------|
| `og:title` | 分享标题 | `<meta property="og:title" content="页面标题">` |
| `og:description` | 分享描述 | `<meta property="og:description" content="页面描述">` |
| `og:image` | 分享图片 | `<meta property="og:image" content="image.jpg">` |
| `og:url` | 分享链接 | `<meta property="og:url" content="https://example.com">` |
| `og:type` | 内容类型 | `<meta property="og:type" content="website">` |
| `og:site_name` | 站点名称 | `<meta property="og:site_name" content="网站名">` |

### Twitter Cards Meta标签

| name | 描述 | 示例 |
|------|------|------|
| `twitter:card` | 卡片类型 | `<meta name="twitter:card" content="summary">` |
| `twitter:title` | 分享标题 | `<meta name="twitter:title" content="页面标题">` |
| `twitter:description` | 分享描述 | `<meta name="twitter:description" content="描述">` |
| `twitter:image` | 分享图片 | `<meta name="twitter:image" content="image.jpg">` |

## 脚本和样式属性

### `<script>` 属性

| 属性 | 描述 | 示例 |
|------|------|------|
| `src` | 脚本文件路径 | `<script src="script.js">` |
| `type` | MIME类型 | `<script type="text/javascript">` |
| `async` | 异步加载 | `<script async src="script.js">` |
| `defer` | 延迟执行 | `<script defer src="script.js">` |
| `crossorigin` | 跨域设置 | `<script crossorigin="anonymous">` |
| `integrity` | 完整性检查 | `<script integrity="sha384-...">` |
| `nomodule` | 非模块浏览器 | `<script nomodule src="legacy.js">` |

### `<link>` 属性

| 属性 | 描述 | 示例 |
|------|------|------|
| `rel` | 关系类型 | `<link rel="stylesheet">` |
| `href` | 资源地址 | `<link href="style.css">` |
| `type` | MIME类型 | `<link type="text/css">` |
| `media` | 媒体查询 | `<link media="screen">` |
| `crossorigin` | 跨域设置 | `<link crossorigin="anonymous">` |
| `integrity` | 完整性检查 | `<link integrity="sha384-...">` |

#### `<link>` rel 属性值

| 值 | 描述 | 示例 |
|----|------|------|
| `stylesheet` | CSS样式表 | `<link rel="stylesheet" href="style.css">` |
| `icon` | 网站图标 | `<link rel="icon" href="favicon.ico">` |
| `preload` | 预加载资源 | `<link rel="preload" href="font.woff2" as="font">` |
| `prefetch` | 预获取资源 | `<link rel="prefetch" href="next-page.html">` |
| `preconnect` | 预连接 | `<link rel="preconnect" href="https://fonts.googleapis.com">` |
| `dns-prefetch` | DNS预解析 | `<link rel="dns-prefetch" href="https://example.com">` |
| `canonical` | 规范链接 | `<link rel="canonical" href="https://example.com/page">` |
| `alternate` | 替代版本 | `<link rel="alternate" type="application/rss+xml">` |

## 布尔属性列表

布尔属性的存在表示true，不存在表示false：

| 属性 | 适用元素 | 描述 |
|------|----------|------|
| `autofocus` | 表单控件 | 自动聚焦 |
| `autoplay` | audio, video | 自动播放 |
| `checked` | input (checkbox, radio) | 选中状态 |
| `controls` | audio, video | 显示控制条 |
| `defer` | script | 延迟执行 |
| `disabled` | 表单控件, button | 禁用状态 |
| `hidden` | 全局 | 隐藏元素 |
| `loop` | audio, video | 循环播放 |
| `multiple` | input (file), select | 多选 |
| `muted` | audio, video | 静音 |
| `open` | details | 展开状态 |
| `readonly` | 输入控件 | 只读状态 |
| `required` | 表单控件 | 必填字段 |
| `reversed` | ol | 倒序编号 |
| `selected` | option | 选中状态 |

## 常用属性值枚举

### `target` 属性值
- `_blank`: 新窗口
- `_self`: 当前窗口（默认）
- `_parent`: 父框架
- `_top`: 顶层窗口

### `method` 属性值（表单）
- `get`: GET方法（默认）
- `post`: POST方法

### `type` 属性值（按钮）
- `submit`: 提交按钮（默认）
- `reset`: 重置按钮
- `button`: 普通按钮

### `wrap` 属性值（textarea）
- `soft`: 软换行（默认）
- `hard`: 硬换行

### `dir` 属性值（文本方向）
- `ltr`: 从左到右（默认）
- `rtl`: 从右到左
- `auto`: 自动判断

---

> 💡 **提示**: 使用HTML验证器检查属性使用是否正确，现代开发推荐使用语义化的属性名称！

*[返回主目录](../README.md)*
