# 文本格式化

> [上一章: HTML属性](./04-attributes.md) | [返回主目录](./README.md) | [下一章: 列表元素](./06-lists.md)

文本格式化是HTML的基本功能之一，通过各种标签可以实现丰富的文本显示效果。HTML提供了语义化和样式化两种方式来格式化文本。

## 标题标签

HTML提供了六个级别的标题标签，从最重要到最不重要：

### 标题层次结构
```html
<h1>一级标题 - 最重要</h1>
<h2>二级标题</h2>
<h3>三级标题</h3>
<h4>四级标题</h4>
<h5>五级标题</h5>
<h6>六级标题 - 最不重要</h6>
```

### 标题使用建议

#### ✅ 正确用法
```html
<!DOCTYPE html>
<html>
<head>
    <title>文章标题</title>
</head>
<body>
    <h1>网站主标题</h1>
    
    <article>
        <h2>文章标题</h2>
        
        <section>
            <h3>章节标题</h3>
            <h4>小节标题</h4>
            
            <section>
                <h4>子小节标题</h4>
                <h5>更小的标题</h5>
            </section>
        </section>
    </article>
</body>
</html>
```

#### ❌ 错误用法
```html
<!-- 不要跳级使用标题 -->
<h1>主标题</h1>
<h3>直接使用h3</h3>  <!-- 应该用h2 -->

<!-- 不要为了样式而选择标题标签 -->
<h3>我想要中等大小的文字</h3>  <!-- 应该用CSS控制大小 -->

<!-- 一个页面不要有多个h1 -->
<h1>第一个主标题</h1>
<h1>第二个主标题</h1>  <!-- 应该用h2 -->
```

### 标题的最佳实践
- 🎯 **语义优先**: 根据内容层次选择标题级别，而非外观
- 📱 **SEO友好**: 搜索引擎使用标题了解页面结构
- 🔢 **按顺序**: 不要跳跃式使用标题标签
- 1️⃣ **单一h1**: 每个页面通常只有一个h1标签

## 段落和换行

### 段落标签 `<p>`

段落是文档中最基本的文本组织单位：

```html
<p>这是第一个段落。段落会自动在前后添加间距，将内容分隔开来。</p>
<p>这是第二个段落。浏览器会自动处理段落间的间距。</p>
<p>段落可以包含很长的文本内容，浏览器会根据容器宽度自动换行。当文本超出容器宽度时，会自动折行到下一行继续显示。</p>
```

### 换行标签 `<br>`

在段落内强制换行：

```html
<p>第一行文本<br>
第二行文本<br>
第三行文本</p>

<address>
    张三<br>
    北京市朝阳区<br>
    邮编：100000<br>
    电话：138-0000-0000
</address>
```

#### 换行使用注意事项
```html
<!-- ✅ 适当使用换行 -->
<p>诗歌：<br>
床前明月光，<br>
疑是地上霜。<br>
举头望明月，<br>
低头思故乡。</p>

<!-- ❌ 滥用换行代替段落 -->
<p>第一段内容<br><br>
第二段内容<br><br>
第三段内容</p>

<!-- ✅ 正确使用段落 -->
<p>第一段内容</p>
<p>第二段内容</p>
<p>第三段内容</p>
```

### 水平分隔线 `<hr>`

用于在内容之间创建主题分隔：

```html
<article>
    <h2>第一章：介绍</h2>
    <p>这是第一章的内容...</p>
    
    <hr>
    
    <h2>第二章：详细说明</h2>
    <p>这是第二章的内容...</p>
    
    <hr>
    
    <h2>第三章：总结</h2>
    <p>这是第三章的内容...</p>
</article>
```

## 文本强调和重要性

HTML提供了语义化的文本强调标签，用于表达内容的重要性和强调程度。

### 语义化强调标签

#### `<strong>` - 重要文本
表示内容具有强烈的重要性、严重性或紧急性：

```html
<p><strong>警告</strong>：请勿在此处停车！</p>
<p>会议将于<strong>明天上午9点</strong>准时开始。</p>
<p><strong>注意</strong>：未保存的更改将会丢失。</p>
```

#### `<em>` - 强调文本
表示内容需要强调，通常改变句子的意思：

```html
<p>我<em>真的</em>很喜欢这个设计。</p>
<p>这不是<em>我</em>的错，是<em>系统</em>的问题。</p>
<p><em>请注意</em>：会议室已更改。</p>
```

#### 嵌套使用
```html
<p><strong>重要提醒：<em>所有</em>参会者都必须准时到达。</strong></p>
<p>这个功能<strong>非常<em>非常</em>重要</strong>，请仔细阅读说明。</p>
```

### 视觉样式标签

这些标签主要用于视觉表现，不带语义含义：

#### `<b>` - 粗体文字
```html
<p>产品名称：<b>超级计算器 Pro</b></p>
<p><b>关键词</b>：HTML、CSS、JavaScript</p>
```

#### `<i>` - 斜体文字
```html
<p>我最喜欢的书是<i>《百年孤独》</i>。</p>
<p><i>注</i>：本产品不含电池。</p>
```

#### `<u>` - 下划线文字
```html
<p>请在<u>重要信息</u>下方签名。</p>
<p><u>注意事项</u>请仔细阅读。</p>
```

### 语义化 vs 视觉化选择

```html
<!-- ✅ 推荐：语义化标签 -->
<p>这是一个<strong>非常重要</strong>的通知。</p>
<p>请<em>仔细</em>阅读以下内容。</p>

<!-- ⚠️ 可用但不推荐：仅为视觉效果 -->
<p>这是一个<b>粗体</b>文字。</p>
<p>这是<i>斜体</i>文字。</p>

<!-- ✅ 最佳实践：结合CSS -->
<p>这是一个<span class="important">重要</span>的通知。</p>
```

## 其他文本格式标签

### 上标和下标

#### `<sup>` - 上标
```html
<p>爱因斯坦的质能方程：E=MC<sup>2</sup></p>
<p>面积：10<sup>2</sup>平方米</p>
<p>引用<sup>[1]</sup>显示相关数据。</p>
<p>2<sup>nd</sup> floor（二楼）</p>
```

#### `<sub>` - 下标
```html
<p>水的化学分子式：H<sub>2</sub>O</p>
<p>二氧化碳：CO<sub>2</sub></p>
<p>化学反应：C<sub>6</sub>H<sub>12</sub>O<sub>6</sub></p>
```

### 文本修改标记

#### `<ins>` - 插入文本
表示新添加的内容：

```html
<p>会议时间：<del>下午2点</del> <ins>下午3点</ins></p>
<p>价格：<ins>￥199</ins>（新价格）</p>
<p>更新内容：<ins cite="update-log.html" datetime="2023-01-15">增加了新功能模块</ins></p>
```

#### `<del>` - 删除文本
表示已删除或过时的内容：

```html
<p>原价：<del>￥299</del> 现价：￥199</p>
<p><del>此功能已停用</del></p>
<p>联系方式：<del>旧邮箱@example.com</del> <ins>新邮箱@newsite.com</ins></p>
```

#### 组合使用
```html
<article>
    <h3>产品信息更新</h3>
    <p>产品名称：<del>基础版</del> <ins>专业版</ins></p>
    <p>价格：<del>￥99</del> <ins>￥149</ins></p>
    <p>功能：<del>基本功能</del> <ins>基本功能 + 高级分析</ins></p>
    <p><small>更新时间：2023年1月15日</small></p>
</article>
```

### 高亮和标记

#### `<mark>` - 高亮文本
用于标记文档中需要引起注意的文本：

```html
<p>搜索结果中包含关键词"<mark>HTML</mark>"的相关内容。</p>
<p>重要提醒：请在<mark>2023年12月31日</mark>前完成注册。</p>
<p>系统检测到<mark>3个错误</mark>需要修复。</p>

<!-- 配合搜索功能 -->
<article>
    <h3>搜索结果</h3>
    <p><mark>HTML</mark>是超文本标记语言，用于创建网页的标准<mark>标记语言</mark>。</p>
</article>
```

### 文本尺寸和样式

#### `<small>` - 小号文字
用于版权声明、法律条文、注释等辅助信息：

```html
<p>这是正常大小的文字。<small>这是小号文字注释。</small></p>

<footer>
    <p>&copy; 2023 我的网站 <small>保留所有权利</small></p>
    <p><small>本网站使用Cookie来提升用户体验。继续浏览即表示您同意我们的Cookie政策。</small></p>
</footer>

<article>
    <h3>产品介绍</h3>
    <p>这是我们最新的产品...</p>
    <p><small>*产品图片仅供参考，实物以收到货品为准。</small></p>
</article>
```

## 代码和计算机相关文本

### `<code>` - 行内代码
用于标记短的代码片段或计算机指令：

```html
<p>使用 <code>console.log()</code> 函数输出调试信息。</p>
<p>按 <code>Ctrl+C</code> 复制选中的文本。</p>
<p>在HTML中，段落标签是 <code>&lt;p&gt;</code>。</p>
<p>JavaScript变量声明：<code>let name = "张三";</code></p>
```

### `<kbd>` - 键盘输入
表示用户需要输入的键盘按键：

```html
<p>保存文件：按 <kbd>Ctrl</kbd> + <kbd>S</kbd></p>
<p>复制：<kbd>Ctrl</kbd> + <kbd>C</kbd></p>
<p>粘贴：<kbd>Ctrl</kbd> + <kbd>V</kbd></p>
<p>退出程序：按 <kbd>Alt</kbd> + <kbd>F4</kbd></p>
<p>在终端中输入：<kbd>npm install</kbd></p>
```

### `<samp>` - 程序输出
表示计算机程序的输出结果：

```html
<p>运行程序后显示：<samp>Hello, World!</samp></p>
<p>编译成功：<samp>Build succeeded. 0 errors, 0 warnings.</samp></p>
<p>系统提示：<samp>文件已保存到 /users/documents/</samp></p>
```

### `<var>` - 变量
表示数学表达式或程序中的变量：

```html
<p>在方程 <var>y</var> = <var>mx</var> + <var>b</var> 中，<var>m</var> 是斜率。</p>
<p>计算公式：<var>面积</var> = <var>长</var> × <var>宽</var></p>
<p>函数定义：<code>function calculate(<var>x</var>, <var>y</var>)</code></p>
```

### 组合使用示例
```html
<article>
    <h3>JavaScript教程：变量声明</h3>
    <p>在JavaScript中，声明变量使用 <code>let</code> 关键字：</p>
    <p><code>let <var>variableName</var> = <var>value</var>;</code></p>
    <p>例如：<code>let name = "张三";</code></p>
    <p>在控制台中输入 <kbd>console.log(name)</kbd>，将输出：<samp>张三</samp></p>
</article>
```

## 预格式化文本

### `<pre>` - 预格式化文本
保持文本的原始格式，包括空格、换行和缩进：

```html
<pre>
这是预格式化文本
    它保持原有的空格
        和缩进
            以及换行
</pre>

<!-- 显示代码块 -->
<pre><code>
function greet(name) {
    if (name) {
        console.log("Hello, " + name + "!");
    } else {
        console.log("Hello, World!");
    }
}
</code></pre>

<!-- ASCII艺术 -->
<pre>
    /\_/\  
   ( o.o ) 
    > ^ <  
</pre>

<!-- 保持格式的表格 -->
<pre>
姓名      年龄    职业
张三      25     工程师
李四      30     设计师
王五      28     产品经理
</pre>
```

## 引用和引文

### `<blockquote>` - 块级引用
用于长段落引用：

```html
<blockquote cite="https://example.com/article">
    <p>学而时习之，不亦说乎？有朋自远方来，不亦乐乎？人不知而不愠，不亦君子乎？</p>
    <footer>—— <cite>《论语》</cite></footer>
</blockquote>

<blockquote>
    <p>创新是区分领导者和追随者的关键因素。</p>
    <footer>—— <cite>史蒂夫·乔布斯</cite></footer>
</blockquote>
```

### `<q>` - 行内引用
用于短引用，浏览器会自动添加引号：

```html
<p>孔子说过：<q>学而时习之，不亦说乎？</q></p>
<p>正如那句话所说：<q>Practice makes perfect</q>（熟能生巧）。</p>
<p>他回答说：<q>我觉得这个想法很不错。</q></p>
```

### `<cite>` - 引用来源
标识作品标题或引用来源：

```html
<p>我最近在读<cite>《百年孤独》</cite>这本书。</p>
<p>根据<cite>《Web标准指南》</cite>的建议...</p>
<p>这个观点来自<cite>MDN Web文档</cite>。</p>

<blockquote>
    <p>设计不只是看起来如何和感觉如何，设计是如何运作的。</p>
    <footer>—— <cite>史蒂夫·乔布斯</cite></footer>
</blockquote>
```

## 缩写和定义

### `<abbr>` - 缩写
为缩写提供完整说明：

```html
<p><abbr title="HyperText Markup Language">HTML</abbr>是网页的标准标记语言。</p>
<p><abbr title="Cascading Style Sheets">CSS</abbr>用于控制网页样式。</p>
<p><abbr title="World Wide Web Consortium">W3C</abbr>制定Web标准。</p>
<p>请在<abbr title="尽快">ASAP</abbr>回复邮件。</p>
```

### `<dfn>` - 术语定义
定义术语或概念：

```html
<p><dfn>HTML</dfn>（超文本标记语言）是创建网页的标准标记语言。</p>
<p><dfn>响应式设计</dfn>是一种网页设计方法，使网页能够在不同设备上良好显示。</p>
<p>在编程中，<dfn>变量</dfn>是用来存储数据值的容器。</p>
```

## 时间和日期

### `<time>` - 时间标记
为时间和日期提供机器可读的格式：

```html
<!-- 日期 -->
<p>文章发布于 <time datetime="2023-01-15">2023年1月15日</time>。</p>

<!-- 时间 -->
<p>会议将在 <time datetime="14:30">下午2:30</time> 开始。</p>

<!-- 日期时间 -->
<p>系统更新时间：<time datetime="2023-01-15T10:30:00">2023年1月15日上午10:30</time></p>

<!-- 相对时间 -->
<p>这篇文章写于 <time datetime="2023-01-01" title="2023年1月1日">新年第一天</time>。</p>

<!-- 持续时间 -->
<p>视频时长：<time datetime="PT1H30M">1小时30分钟</time></p>
```

## 实际应用示例

### 文章内容格式化
```html
<article>
    <header>
        <h1>Web前端开发入门指南</h1>
        <p><small>发布于 <time datetime="2023-01-15">2023年1月15日</time></small></p>
    </header>
    
    <section>
        <h2>什么是前端开发？</h2>
        <p><dfn>前端开发</dfn>是指创建用户直接交互的网页界面的过程。它主要涉及三种核心技术：</p>
        <ul>
            <li><abbr title="HyperText Markup Language">HTML</abbr> - 页面结构</li>
            <li><abbr title="Cascading Style Sheets">CSS</abbr> - 页面样式</li>
            <li><abbr title="JavaScript">JS</abbr> - 页面交互</li>
        </ul>
        
        <blockquote cite="https://developer.mozilla.org">
            <p>HTML为网页内容提供含义和结构，CSS为内容提供设计和布局。</p>
            <footer>—— <cite>MDN Web Docs</cite></footer>
        </blockquote>
    </section>
    
    <section>
        <h2>开始学习</h2>
        <p><strong>重要提示</strong>：学习前端开发需要<em>循序渐进</em>。</p>
        <p>首先学习 <code>HTML</code>，然后是 <code>CSS</code>，最后学习 <code>JavaScript</code>。</p>
        
        <h3>HTML基础</h3>
        <p>HTML标签的基本语法：<code>&lt;tagname&gt;content&lt;/tagname&gt;</code></p>
        <p>例如段落标签：<code>&lt;p&gt;这是段落内容&lt;/p&gt;</code></p>
        
        <h3>常见快捷键</h3>
        <p>在开发工具中：</p>
        <ul>
            <li>查看源码：<kbd>Ctrl</kbd> + <kbd>U</kbd></li>
            <li>开发者工具：<kbd>F12</kbd></li>
            <li>刷新页面：<kbd>F5</kbd></li>
        </ul>
    </section>
    
    <section>
        <h2>代码示例</h2>
        <p>一个简单的HTML页面结构：</p>
        <pre><code>&lt;!DOCTYPE html&gt;
&lt;html lang="zh-CN"&gt;
&lt;head&gt;
    &lt;title&gt;页面标题&lt;/title&gt;
&lt;/head&gt;
&lt;body&gt;
    &lt;h1&gt;欢迎来到我的网站&lt;/h1&gt;
    &lt;p&gt;这是页面内容。&lt;/p&gt;
&lt;/body&gt;
&lt;/html&gt;</code></pre>
    </section>
    
    <footer>
        <hr>
        <p><small>
            版权声明：本文内容遵循 
            <abbr title="Creative Commons">CC</abbr> 
            <abbr title="Attribution-ShareAlike">BY-SA</abbr> 4.0 许可协议。
        </small></p>
    </footer>
</article>
```

### 产品信息展示
```html
<div class="product-info">
    <h2>超级计算器 <small>专业版</small></h2>
    
    <p><strong>价格更新</strong>：</p>
    <p>原价：<del>￥299</del> <mark>现价：￥199</mark></p>
    
    <p><strong>产品特性</strong>：</p>
    <ul>
        <li>支持科学计算：sin(<var>x</var>)、cos(<var>x</var>)、tan(<var>x</var>)</li>
        <li>内存功能：按 <kbd>M+</kbd> 存储，<kbd>MR</kbd> 读取</li>
        <li>进制转换：支持2<sup>进制</sup>、8<sup>进制</sup>、16<sup>进制</sup></li>
    </ul>
    
    <blockquote>
        <p>这是我用过最好的计算器应用！</p>
        <footer>—— 用户评价</footer>
    </blockquote>
    
    <p><small>
        * 本产品需要 <abbr title="Android">Android</abbr> 5.0+ 
        或 <abbr title="iOS">iOS</abbr> 10.0+ 系统支持
    </small></p>
</div>
```

## 最佳实践

### 1. 语义化优先
```html
<!-- ✅ 好的做法：语义化 -->
<p>这是一个<strong>重要</strong>的通知：会议<em>推迟</em>到明天。</p>
<p>化学分子式：H<sub>2</sub>SO<sub>4</sub></p>
<p>数学公式：a<sup>2</sup> + b<sup>2</sup> = c<sup>2</sup></p>

<!-- ❌ 不好的做法：仅为外观 -->
<p>这是一个<b>粗体</b>通知：会议<i>斜体</i>到明天。</p>
<p style="font-weight: bold;">重要信息</p>
```

### 2. 正确使用引用
```html
<!-- ✅ 正确使用 -->
<blockquote cite="source-url">
    <p>长段落引用内容...</p>
    <footer>—— <cite>引用来源</cite></footer>
</blockquote>

<p>他说：<q>这是个好主意。</q></p>

<!-- ❌ 错误使用 -->
<blockquote>
    这不是引用，只是普通文本
</blockquote>
```

### 3. 合理使用强调
```html
<!-- ✅ 合理使用 -->
<p><strong>警告</strong>：操作不可撤销！</p>
<p>请<em>仔细</em>检查输入信息。</p>

<!-- ❌ 滥用强调 -->
<p><strong>这</strong>是<strong>一个</strong><strong>普通</strong>段落。</p>
```

### 4. 代码相关标签的使用
```html
<!-- ✅ 正确组合使用 -->
<p>在终端输入 <kbd>node --version</kbd> 检查版本，输出类似 <samp>v16.14.0</samp>。</p>
<p>JavaScript中声明变量：<code>let <var>name</var> = "张三";</code></p>

<!-- 代码块用pre + code -->
<pre><code>function hello() {
    console.log("Hello, World!");
}</code></pre>
```

## 下一步学习

现在您已经掌握了HTML文本格式化的各种方法，接下来学习：

1. [列表元素](./06-lists.md) - 学习创建有序和无序列表
2. [链接和图片](./07-links-images.md) - 添加链接和多媒体内容

---

> 💡 **记住**: 文本格式化应该优先考虑语义，而不是视觉效果。语义化的标签对SEO和无障碍访问都有重要意义！

*[上一章: HTML属性](./04-attributes.md) | [返回主目录](./README.md) | [下一章: 列表元素](./06-lists.md)*
