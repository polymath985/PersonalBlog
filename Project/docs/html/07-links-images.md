# 链接和图片

> [上一章: 列表元素](./06-lists.md) | [返回主目录](./README.md) | [下一章: 表格](./08-tables.md)

链接和图片是网页的核心元素。链接连接不同的页面和资源，图片增强视觉效果和信息传达。掌握这些元素的使用是创建现代网页的关键。

## 超链接 `<a>`

超链接（Hyperlink）是网页最重要的特性之一，它允许用户在不同页面和资源之间导航。

### 基本语法
```html
<a href="URL">链接文本</a>
```

### 外部链接
```html
<!-- 链接到其他网站 -->
<p>访问 <a href="https://www.google.com">Google</a> 搜索信息。</p>

<p>学习前端开发可以访问：</p>
<ul>
    <li><a href="https://developer.mozilla.org">MDN Web Docs</a></li>
    <li><a href="https://www.w3schools.com">W3Schools</a></li>
    <li><a href="https://github.com">GitHub</a></li>
    <li><a href="https://stackoverflow.com">Stack Overflow</a></li>
</ul>

<!-- 链接到特定的页面 -->
<p>了解更多关于HTML的信息：</p>
<a href="https://html.spec.whatwg.org/">HTML Living Standard</a>
```

### 内部链接（相对路径）
```html
<!-- 同级目录文件 -->
<a href="about.html">关于我们</a>
<a href="contact.html">联系我们</a>
<a href="products.html">产品介绍</a>

<!-- 上级目录文件 -->
<a href="../index.html">返回首页</a>
<a href="../images/logo.png">查看标志</a>

<!-- 下级目录文件 -->
<a href="pages/gallery.html">图片画廊</a>
<a href="docs/manual.pdf">下载手册</a>
<a href="assets/styles.css">样式文件</a>

<!-- 多级目录导航 -->
<nav>
    <ul>
        <li><a href="../../index.html">首页</a></li>
        <li><a href="../products/index.html">产品</a></li>
        <li><a href="./team.html">团队</a></li>
    </ul>
</nav>
```

### 页面内锚点链接
```html
<!DOCTYPE html>
<html>
<head>
    <title>长页面导航示例</title>
</head>
<body>
    <!-- 页面导航目录 -->
    <nav>
        <h2>页面目录</h2>
        <ul>
            <li><a href="#introduction">简介</a></li>
            <li><a href="#features">功能特性</a></li>
            <li><a href="#pricing">价格方案</a></li>
            <li><a href="#contact">联系方式</a></li>
            <li><a href="#faq">常见问题</a></li>
        </ul>
    </nav>

    <!-- 页面内容 -->
    <main>
        <section id="introduction">
            <h2>简介</h2>
            <p>这里是产品简介内容...</p>
            <p><a href="#top">返回顶部</a></p>
        </section>

        <section id="features">
            <h2>功能特性</h2>
            <p>这里介绍产品的功能特性...</p>
            
            <h3 id="feature-responsive">响应式设计</h3>
            <p>支持各种设备尺寸...</p>
            
            <h3 id="feature-performance">高性能</h3>
            <p>优化的性能表现...</p>
            
            <p><a href="#top">返回顶部</a></p>
        </section>

        <section id="pricing">
            <h2>价格方案</h2>
            <p>查看我们的价格方案...</p>
            <p><a href="#contact">联系我们获取报价</a></p>
        </section>

        <section id="contact">
            <h2>联系方式</h2>
            <p>电话：400-123-4567</p>
            <p>邮箱：contact@example.com</p>
            <p><a href="#faq">查看常见问题</a></p>
        </section>

        <section id="faq">
            <h2>常见问题</h2>
            <details>
                <summary>如何开始使用？</summary>
                <p>首先需要注册账户，然后...</p>
                <p><a href="#introduction">回到简介部分</a></p>
            </details>
        </section>
    </main>

    <!-- 返回顶部锚点 -->
    <div id="top"></div>
</body>
</html>
```

### 邮件和电话链接
```html
<!-- 邮件链接 -->
<p>有问题请联系我们：<a href="mailto:support@example.com">support@example.com</a></p>

<!-- 带主题和内容的邮件 -->
<a href="mailto:sales@example.com?subject=产品咨询&body=您好，我想了解更多关于...">发送邮件咨询</a>

<!-- 抄送和密送 -->
<a href="mailto:primary@example.com?cc=manager@example.com&bcc=archive@example.com&subject=重要通知">发送邮件</a>

<!-- 电话链接 -->
<p>客服热线：<a href="tel:+86-400-123-4567">400-123-4567</a></p>
<p>紧急联系：<a href="tel:+8613800138000">138-0013-8000</a></p>

<!-- 短信链接 -->
<p>发送短信：<a href="sms:+8613800138000">点击发送短信</a></p>
<p>预设内容：<a href="sms:+8613800138000?body=您好，我想咨询...">发送咨询短信</a></p>
```

### 文件下载链接
```html
<!-- 各种文件类型下载 -->
<h3>资料下载</h3>
<ul>
    <li><a href="downloads/user-manual.pdf" download>用户手册 (PDF)</a></li>
    <li><a href="downloads/template.docx" download>模板文件 (Word)</a></li>
    <li><a href="downloads/data.xlsx" download>数据表格 (Excel)</a></li>
    <li><a href="downloads/presentation.pptx" download>演示文稿 (PowerPoint)</a></li>
</ul>

<!-- 指定下载文件名 -->
<a href="files/report-2024.pdf" download="年度报告-2024.pdf">下载年度报告</a>

<!-- 图片下载 -->
<h3>图片下载</h3>
<ul>
    <li><a href="images/logo-high-res.png" download="公司标志.png">高分辨率标志</a></li>
    <li><a href="images/product-photo.jpg" download>产品照片</a></li>
</ul>

<!-- 软件下载 -->
<h3>软件下载</h3>
<ul>
    <li><a href="software/app-windows.exe" download>Windows版本</a></li>
    <li><a href="software/app-macos.dmg" download>macOS版本</a></li>
    <li><a href="software/app-linux.deb" download>Linux版本</a></li>
</ul>
```

### 链接属性详解

#### `target` 属性 - 控制链接打开方式
```html
<!-- 在新窗口/标签页打开 -->
<a href="https://www.example.com" target="_blank">在新窗口打开</a>

<!-- 在当前窗口打开（默认） -->
<a href="page.html" target="_self">在当前窗口打开</a>

<!-- 在父框架打开 -->
<a href="page.html" target="_parent">在父框架打开</a>

<!-- 在顶级窗口打开 -->
<a href="page.html" target="_top">在顶级窗口打开</a>

<!-- 在指定框架打开 -->
<a href="page.html" target="frameName">在指定框架打开</a>

<!-- 实际应用示例 -->
<nav>
    <h3>外部资源</h3>
    <ul>
        <li><a href="https://github.com" target="_blank" rel="noopener">GitHub</a></li>
        <li><a href="https://stackoverflow.com" target="_blank" rel="noopener">Stack Overflow</a></li>
        <li><a href="https://developer.mozilla.org" target="_blank" rel="noopener">MDN</a></li>
    </ul>
</nav>
```

#### `rel` 属性 - 描述链接关系
```html
<!-- 安全的外部链接 -->
<a href="https://external-site.com" target="_blank" rel="noopener noreferrer">外部网站</a>

<!-- 搜索引擎相关 -->
<a href="https://partner-site.com" rel="nofollow">合作伙伴网站</a>

<!-- 赞助链接 -->
<a href="https://sponsor.com" rel="sponsored">赞助商链接</a>

<!-- 用户生成内容 -->
<a href="https://user-content.com" rel="ugc">用户分享内容</a>

<!-- 备用链接 -->
<a href="mobile-version.html" rel="alternate">移动版本</a>

<!-- 帮助信息 -->
<a href="help.html" rel="help">帮助文档</a>

<!-- 许可证信息 -->
<a href="license.html" rel="license">使用许可</a>
```

#### `title` 属性 - 提供额外信息
```html
<!-- 为链接提供工具提示 -->
<p>查看 <a href="report.pdf" title="2024年第一季度财务报告，PDF格式，2.5MB">财务报告</a></p>

<p>访问 <a href="https://api.example.com" title="API文档 - 包含所有接口说明和示例代码">API文档</a></p>

<!-- 导航链接提示 -->
<nav>
    <ul>
        <li><a href="/" title="返回网站首页">首页</a></li>
        <li><a href="/products/" title="浏览所有产品信息">产品</a></li>
        <li><a href="/about/" title="了解我们的公司历史和团队">关于我们</a></li>
        <li><a href="/contact/" title="获取联系方式和在线表单">联系我们</a></li>
    </ul>
</nav>
```

## 图片 `<img>`

图片是增强网页视觉效果和传达信息的重要元素。

### 基本语法
```html
<img src="图片路径" alt="替代文本">
```

### 基本图片使用
```html
<!-- 本地图片 -->
<img src="images/logo.png" alt="公司标志">
<img src="photos/team.jpg" alt="我们的团队合影">

<!-- 相对路径 -->
<img src="../images/banner.jpg" alt="网站横幅">
<img src="./assets/icon.svg" alt="网站图标">

<!-- 绝对路径 -->
<img src="/images/hero-bg.jpg" alt="首页背景图">

<!-- 网络图片 -->
<img src="https://example.com/images/sample.jpg" alt="示例图片">
```

### `alt` 属性的重要性
```html
<!-- ✅ 好的alt文本：描述性且简洁 -->
<img src="chart.png" alt="2024年销售额增长图表，显示比去年增长了25%">

<img src="product.jpg" alt="红色iPhone 15 Pro，正面展示">

<img src="team-photo.jpg" alt="开发团队5人在办公室的合影">

<!-- ✅ 装饰性图片使用空alt -->
<img src="decoration.png" alt="" role="presentation">

<!-- ❌ 不好的alt文本 -->
<img src="img001.jpg" alt="图片">
<img src="photo.jpg" alt="照片">
<img src="banner.jpg" alt="这是一张banner图片">
```

### 图片尺寸控制
```html
<!-- 使用width和height属性 -->
<img src="avatar.jpg" alt="用户头像" width="100" height="100">

<img src="banner.jpg" alt="页面横幅" width="800" height="200">

<!-- 响应式图片（推荐使用CSS） -->
<img src="responsive.jpg" alt="响应式图片" style="width: 100%; max-width: 600px; height: auto;">

<!-- 固定尺寸的缩略图 -->
<div class="thumbnails">
    <img src="thumb1.jpg" alt="图片1缩略图" width="150" height="100">
    <img src="thumb2.jpg" alt="图片2缩略图" width="150" height="100">
    <img src="thumb3.jpg" alt="图片3缩略图" width="150" height="100">
</div>
```

### 图片格式选择
```html
<!-- JPG格式：适合照片 -->
<img src="landscape.jpg" alt="美丽的山水风景照片">
<img src="portrait.jpg" alt="人物肖像照">

<!-- PNG格式：适合透明背景和图标 -->
<img src="logo.png" alt="公司标志">
<img src="icon-transparent.png" alt="透明背景图标">

<!-- SVG格式：适合矢量图形 -->
<img src="chart.svg" alt="数据统计图表">
<img src="icon.svg" alt="可缩放的图标">

<!-- WebP格式：现代浏览器优选 -->
<img src="optimized.webp" alt="优化的网络图片">

<!-- GIF格式：适合简单动画 -->
<img src="loading.gif" alt="加载中动画">
```

### 响应式图片
```html
<!-- 使用srcset提供多种分辨率 -->
<img src="image.jpg"
     srcset="image-480w.jpg 480w,
             image-800w.jpg 800w,
             image-1200w.jpg 1200w"
     sizes="(max-width: 480px) 100vw,
            (max-width: 800px) 50vw,
            25vw"
     alt="响应式图片示例">

<!-- 高分辨率显示屏支持 -->
<img src="logo.png"
     srcset="logo.png 1x, logo-2x.png 2x, logo-3x.png 3x"
     alt="支持高分辨率的标志">

<!-- picture元素实现艺术指导 -->
<picture>
    <source media="(max-width: 480px)" srcset="mobile-banner.jpg">
    <source media="(max-width: 800px)" srcset="tablet-banner.jpg">
    <img src="desktop-banner.jpg" alt="响应式横幅图片">
</picture>

<!-- 不同格式的回退 -->
<picture>
    <source srcset="image.avif" type="image/avif">
    <source srcset="image.webp" type="image/webp">
    <img src="image.jpg" alt="多格式支持的图片">
</picture>
```

### 图片与链接结合
```html
<!-- 图片链接 -->
<a href="gallery.html">
    <img src="gallery-thumb.jpg" alt="查看完整图片库">
</a>

<!-- 点击图片查看大图 -->
<a href="full-size-image.jpg" target="_blank">
    <img src="thumbnail.jpg" alt="点击查看大图">
</a>

<!-- 图片导航菜单 -->
<nav class="image-nav">
    <a href="home.html">
        <img src="icons/home.svg" alt="首页">
        <span>首页</span>
    </a>
    <a href="products.html">
        <img src="icons/products.svg" alt="产品">
        <span>产品</span>
    </a>
    <a href="contact.html">
        <img src="icons/contact.svg" alt="联系">
        <span>联系</span>
    </a>
</nav>

<!-- 产品展示 -->
<div class="product-grid">
    <div class="product">
        <a href="product1.html">
            <img src="product1.jpg" alt="产品1 - 无线耳机">
            <h3>无线耳机</h3>
            <p>¥299</p>
        </a>
    </div>
    <div class="product">
        <a href="product2.html">
            <img src="product2.jpg" alt="产品2 - 智能手表">
            <h3>智能手表</h3>
            <p>¥999</p>
        </a>
    </div>
</div>
```

### 图片画廊示例
```html
<section class="gallery">
    <h2>作品展示</h2>
    
    <!-- 缩略图网格 -->
    <div class="gallery-grid">
        <div class="gallery-item">
            <a href="full/image1.jpg" target="_blank">
                <img src="thumbs/image1.jpg" alt="抽象艺术作品1">
                <div class="overlay">
                    <span>查看大图</span>
                </div>
            </a>
        </div>
        
        <div class="gallery-item">
            <a href="full/image2.jpg" target="_blank">
                <img src="thumbs/image2.jpg" alt="风景摄影作品">
                <div class="overlay">
                    <span>查看大图</span>
                </div>
            </a>
        </div>
        
        <div class="gallery-item">
            <a href="full/image3.jpg" target="_blank">
                <img src="thumbs/image3.jpg" alt="人像摄影作品">
                <div class="overlay">
                    <span>查看大图</span>
                </div>
            </a>
        </div>
        
        <div class="gallery-item">
            <a href="full/image4.jpg" target="_blank">
                <img src="thumbs/image4.jpg" alt="建筑摄影作品">
                <div class="overlay">
                    <span>查看大图</span>
                </div>
            </a>
        </div>
    </div>
</section>
```

## 图片地图 `<map>` 和 `<area>`

图片地图允许在单张图片的不同区域创建可点击的链接。

### 基本图片地图
```html
<img src="office-map.jpg" alt="办公室平面图" usemap="#office-map" width="600" height="400">

<map name="office-map">
    <!-- 矩形区域 -->
    <area shape="rect" coords="10,10,100,50" href="reception.html" alt="前台接待">
    <area shape="rect" coords="120,10,250,100" href="meeting-room.html" alt="会议室">
    
    <!-- 圆形区域 -->
    <area shape="circle" coords="300,200,50" href="rest-area.html" alt="休息区">
    
    <!-- 多边形区域 -->
    <area shape="poly" coords="400,100,500,120,480,200,420,180" href="workspace.html" alt="工作区域">
</map>
```

### 世界地图导航示例
```html
<section class="world-map">
    <h2>我们的全球办公室</h2>
    <img src="world-map.png" alt="世界地图" usemap="#world-offices" width="800" height="400">
    
    <map name="world-offices">
        <!-- 北美洲 -->
        <area shape="circle" coords="200,150,20" 
              href="offices/north-america.html" 
              alt="北美办公室 - 纽约、洛杉矶、多伦多"
              title="点击查看北美办公室信息">
              
        <!-- 欧洲 -->
        <area shape="circle" coords="400,120,25" 
              href="offices/europe.html" 
              alt="欧洲办公室 - 伦敦、巴黎、柏林"
              title="点击查看欧洲办公室信息">
              
        <!-- 亚洲 -->
        <area shape="circle" coords="600,180,30" 
              href="offices/asia.html" 
              alt="亚洲办公室 - 东京、上海、新加坡"
              title="点击查看亚洲办公室信息">
    </map>
</section>
```

### 产品特性展示
```html
<section class="product-features">
    <h2>产品功能展示</h2>
    <img src="smartphone.png" alt="智能手机功能图" usemap="#phone-features" width="300" height="600">
    
    <map name="phone-features">
        <!-- 摄像头 -->
        <area shape="circle" coords="150,80,25" 
              href="features/camera.html" 
              alt="高清摄像头"
              title="了解摄像头功能">
              
        <!-- 屏幕 -->
        <area shape="rect" coords="30,120,270,480" 
              href="features/display.html" 
              alt="高分辨率显示屏"
              title="了解显示屏特性">
              
        <!-- 指纹识别 -->
        <area shape="circle" coords="150,520,20" 
              href="features/fingerprint.html" 
              alt="指纹识别按钮"
              title="了解指纹识别功能">
              
        <!-- 音量键 -->
        <area shape="rect" coords="10,200,20,250" 
              href="features/controls.html" 
              alt="音量控制键"
              title="了解控制按钮">
    </map>
</section>
```

## 实际应用场景

### 博客文章中的媒体内容
```html
<article class="blog-post">
    <header>
        <h1>前端开发最佳实践</h1>
        <p>发布时间：2024年1月15日</p>
    </header>
    
    <figure>
        <img src="featured-image.jpg" alt="前端开发工作环境，显示代码编辑器和浏览器">
        <figcaption>现代前端开发环境</figcaption>
    </figure>
    
    <p>在现代web开发中，掌握正确的开发实践至关重要。本文将介绍一些关键的前端开发技巧。</p>
    
    <h2>开发工具</h2>
    <p>推荐使用以下开发工具：</p>
    <ul>
        <li><a href="https://code.visualstudio.com" target="_blank" rel="noopener">VS Code</a> - 强大的代码编辑器</li>
        <li><a href="https://git-scm.com" target="_blank" rel="noopener">Git</a> - 版本控制系统</li>
        <li><a href="https://nodejs.org" target="_blank" rel="noopener">Node.js</a> - JavaScript运行环境</li>
    </ul>
    
    <figure>
        <img src="development-tools.png" alt="开发工具截图，包括VS Code、终端和浏览器开发者工具">
        <figcaption>常用的前端开发工具界面</figcaption>
    </figure>
    
    <p>更多详细信息请参考我们的 <a href="developer-guide.html">开发者指南</a>。</p>
</article>
```

### 电商产品页面
```html
<div class="product-page">
    <div class="product-images">
        <div class="main-image">
            <img src="product-main.jpg" alt="无线蓝牙耳机主图" id="main-product-image">
        </div>
        
        <div class="thumbnail-images">
            <img src="product-thumb1.jpg" alt="耳机正面视图" onclick="changeMainImage(this)">
            <img src="product-thumb2.jpg" alt="耳机侧面视图" onclick="changeMainImage(this)">
            <img src="product-thumb3.jpg" alt="耳机充电盒" onclick="changeMainImage(this)">
            <img src="product-thumb4.jpg" alt="耳机佩戴效果" onclick="changeMainImage(this)">
        </div>
    </div>
    
    <div class="product-info">
        <h1>高音质无线蓝牙耳机</h1>
        <p class="price">¥299.00</p>
        
        <div class="product-links">
            <a href="#specifications">查看规格参数</a>
            <a href="#reviews">用户评价</a>
            <a href="#faq">常见问题</a>
        </div>
        
        <div class="social-share">
            <p>分享到：</p>
            <a href="https://weibo.com/share?url=..." target="_blank">
                <img src="icons/weibo.svg" alt="分享到微博">
            </a>
            <a href="https://wx.qq.com/share?url=..." target="_blank">
                <img src="icons/wechat.svg" alt="分享到微信">
            </a>
        </div>
    </div>
</div>
```

### 团队介绍页面
```html
<section class="team-section">
    <h2>我们的团队</h2>
    
    <div class="team-grid">
        <div class="team-member">
            <a href="profiles/zhang-san.html">
                <img src="team/zhang-san.jpg" alt="张三 - 项目经理">
                <h3>张三</h3>
                <p>项目经理</p>
            </a>
            <div class="contact-info">
                <a href="mailto:zhangsan@example.com">发送邮件</a>
                <a href="https://linkedin.com/in/zhangsan" target="_blank" rel="noopener">
                    <img src="icons/linkedin.svg" alt="LinkedIn">
                </a>
            </div>
        </div>
        
        <div class="team-member">
            <a href="profiles/li-si.html">
                <img src="team/li-si.jpg" alt="李四 - 前端开发工程师">
                <h3>李四</h3>
                <p>前端开发工程师</p>
            </a>
            <div class="contact-info">
                <a href="mailto:lisi@example.com">发送邮件</a>
                <a href="https://github.com/lisi" target="_blank" rel="noopener">
                    <img src="icons/github.svg" alt="GitHub">
                </a>
            </div>
        </div>
        
        <div class="team-member">
            <a href="profiles/wang-wu.html">
                <img src="team/wang-wu.jpg" alt="王五 - 后端开发工程师">
                <h3>王五</h3>
                <p>后端开发工程师</p>
            </a>
            <div class="contact-info">
                <a href="mailto:wangwu@example.com">发送邮件</a>
                <a href="https://twitter.com/wangwu" target="_blank" rel="noopener">
                    <img src="icons/twitter.svg" alt="Twitter">
                </a>
            </div>
        </div>
    </div>
</section>
```

## 最佳实践

### 1. 链接的可访问性
```html
<!-- ✅ 好的做法：描述性链接文本 -->
<p><a href="annual-report.pdf">下载2024年年度报告</a></p>
<p><a href="contact.html">联系我们的客服团队</a></p>

<!-- ❌ 避免的做法：不具描述性的链接 -->
<p>年度报告已经发布，<a href="report.pdf">点击这里</a>下载。</p>
<p>如有问题请<a href="contact.html">点击此处</a>。</p>

<!-- ✅ 为链接提供上下文 -->
<p>查看更多信息：<a href="details.html" aria-describedby="link-description">产品详细介绍</a></p>
<span id="link-description" class="sr-only">包含产品规格、价格和购买选项</span>
```

### 2. 图片的性能优化
```html
<!-- ✅ 懒加载图片 -->
<img src="placeholder.jpg" data-src="actual-image.jpg" alt="产品图片" loading="lazy">

<!-- ✅ 预加载重要图片 -->
<link rel="preload" href="hero-image.jpg" as="image">

<!-- ✅ 使用适当的图片格式和尺寸 -->
<picture>
    <source srcset="image.avif" type="image/avif">
    <source srcset="image.webp" type="image/webp">
    <img src="image.jpg" alt="描述" width="800" height="600">
</picture>
```

### 3. 链接的安全性
```html
<!-- ✅ 外部链接安全处理 -->
<a href="https://external-site.com" 
   target="_blank" 
   rel="noopener noreferrer">
   外部网站
</a>

<!-- ✅ 下载链接的安全提示 -->
<a href="software.exe" 
   download 
   onclick="return confirm('确定要下载此文件吗？')">
   下载软件
</a>
```

### 4. 移动端优化
```html
<!-- ✅ 适合触摸的链接区域 -->
<a href="page.html" style="display: block; padding: 15px;">
    <img src="icon.png" alt="图标" style="vertical-align: middle;">
    <span style="margin-left: 10px;">链接文本</span>
</a>

<!-- ✅ 响应式图片 -->
<img src="mobile.jpg"
     srcset="mobile.jpg 480w, tablet.jpg 768w, desktop.jpg 1200w"
     sizes="(max-width: 480px) 100vw, (max-width: 768px) 50vw, 25vw"
     alt="响应式图片">
```

## 下一步学习

掌握了链接和图片的使用后，接下来学习：

1. [表格](./08-tables.md) - 了解如何创建和格式化数据表格
2. [标签参考](./reference/tag-reference.md) - 查看完整的HTML标签列表

---

> 💡 **记住**: 始终为图片提供有意义的alt文本，为链接使用描述性文本，这不仅提高可访问性，还有助于SEO优化！

*[上一章: 列表元素](./06-lists.md) | [返回主目录](./README.md) | [下一章: 表格](./08-tables.md)*
