# 表单

> [上一章: 表格](./08-tables.md) | [返回主目录](./README.md) | [下一章: 语义化标签](./10-semantic-tags.md)

表单是网页中收集用户输入的重要工具，用于用户注册、登录、搜索、反馈等场景。

## 表单基础结构

### `<form>` 元素
所有表单控件都应该放在 `<form>` 元素内：

```html
<form action="submit.php" method="post">
    <!-- 表单控件放在这里 -->
</form>
```

### 重要属性
- **`action`**: 表单提交的目标URL
- **`method`**: 提交方法（GET或POST）
- **`enctype`**: 数据编码类型

```html
<form action="process-form.php" 
      method="post" 
      enctype="multipart/form-data">
    <!-- 包含文件上传时需要 multipart/form-data -->
</form>
```

## 输入控件

### 文本输入

#### 单行文本 `<input type="text">`
```html
<label for="username">用户名：</label>
<input type="text" 
       id="username" 
       name="username" 
       placeholder="请输入用户名"
       maxlength="20"
       required>
```

#### 密码输入 `<input type="password">`
```html
<label for="password">密码：</label>
<input type="password" 
       id="password" 
       name="password" 
       placeholder="请输入密码"
       minlength="6"
       required>
```

#### 多行文本 `<textarea>`
```html
<label for="message">留言：</label>
<textarea id="message" 
          name="message" 
          rows="4" 
          cols="50" 
          placeholder="请输入您的留言"
          maxlength="500"></textarea>
```

### 特殊输入类型

#### 邮箱输入
```html
<label for="email">邮箱：</label>
<input type="email" 
       id="email" 
       name="email" 
       placeholder="example@email.com"
       required>
```

#### 电话输入
```html
<label for="phone">电话：</label>
<input type="tel" 
       id="phone" 
       name="phone" 
       placeholder="138-0000-0000"
       pattern="[0-9]{3}-[0-9]{4}-[0-9]{4}">
```

#### 网址输入
```html
<label for="website">网站：</label>
<input type="url" 
       id="website" 
       name="website" 
       placeholder="https://example.com">
```

#### 搜索输入
```html
<label for="search">搜索：</label>
<input type="search" 
       id="search" 
       name="search" 
       placeholder="请输入搜索关键词">
```

### 数字和日期输入

#### 数字输入
```html
<label for="age">年龄：</label>
<input type="number" 
       id="age" 
       name="age" 
       min="1" 
       max="120" 
       value="25"
       step="1">
```

#### 范围滑块
```html
<label for="volume">音量：</label>
<input type="range" 
       id="volume" 
       name="volume" 
       min="0" 
       max="100" 
       value="50"
       step="10">
<output for="volume">50</output>
```

#### 日期输入
```html
<!-- 日期 -->
<label for="birthday">生日：</label>
<input type="date" 
       id="birthday" 
       name="birthday" 
       min="1900-01-01" 
       max="2023-12-31">

<!-- 时间 -->
<label for="appointment">预约时间：</label>
<input type="time" 
       id="appointment" 
       name="appointment" 
       min="09:00" 
       max="18:00">

<!-- 日期时间 -->
<label for="event">活动时间：</label>
<input type="datetime-local" 
       id="event" 
       name="event">

<!-- 月份 -->
<label for="month">选择月份：</label>
<input type="month" 
       id="month" 
       name="month">

<!-- 周 -->
<label for="week">选择周：</label>
<input type="week" 
       id="week" 
       name="week">
```

### 选择控件

#### 单选按钮 `<input type="radio">`
```html
<fieldset>
    <legend>性别：</legend>
    <input type="radio" id="male" name="gender" value="male">
    <label for="male">男</label>
    
    <input type="radio" id="female" name="gender" value="female">
    <label for="female">女</label>
    
    <input type="radio" id="other" name="gender" value="other">
    <label for="other">其他</label>
</fieldset>
```

#### 复选框 `<input type="checkbox">`
```html
<fieldset>
    <legend>兴趣爱好：</legend>
    <input type="checkbox" id="reading" name="hobbies[]" value="reading">
    <label for="reading">阅读</label>
    
    <input type="checkbox" id="music" name="hobbies[]" value="music">
    <label for="music">音乐</label>
    
    <input type="checkbox" id="sports" name="hobbies[]" value="sports">
    <label for="sports">运动</label>
    
    <input type="checkbox" id="travel" name="hobbies[]" value="travel" checked>
    <label for="travel">旅行</label>
</fieldset>
```

#### 下拉列表 `<select>`
```html
<label for="city">居住城市：</label>
<select id="city" name="city" required>
    <option value="">请选择城市</option>
    <option value="beijing">北京</option>
    <option value="shanghai">上海</option>
    <option value="guangzhou">广州</option>
    <option value="shenzhen" selected>深圳</option>
</select>
```

#### 多选下拉列表
```html
<label for="skills">技能（可多选）：</label>
<select id="skills" name="skills[]" multiple size="5">
    <option value="html">HTML</option>
    <option value="css">CSS</option>
    <option value="javascript">JavaScript</option>
    <option value="python" selected>Python</option>
    <option value="java" selected>Java</option>
</select>
```

#### 选项分组
```html
<label for="country">国家：</label>
<select id="country" name="country">
    <optgroup label="亚洲">
        <option value="china">中国</option>
        <option value="japan">日本</option>
        <option value="korea">韩国</option>
    </optgroup>
    <optgroup label="欧洲">
        <option value="uk">英国</option>
        <option value="france">法国</option>
        <option value="germany">德国</option>
    </optgroup>
    <optgroup label="北美洲">
        <option value="usa">美国</option>
        <option value="canada">加拿大</option>
    </optgroup>
</select>
```

### 文件和颜色

#### 文件上传
```html
<label for="avatar">头像：</label>
<input type="file" 
       id="avatar" 
       name="avatar" 
       accept="image/*">

<label for="documents">文档（可多选）：</label>
<input type="file" 
       id="documents" 
       name="documents[]" 
       accept=".pdf,.doc,.docx,.txt" 
       multiple>
```

#### 颜色选择器
```html
<label for="theme-color">主题颜色：</label>
<input type="color" 
       id="theme-color" 
       name="theme-color" 
       value="#3366cc">
```

## 按钮元素

### 提交按钮
```html
<!-- input方式 -->
<input type="submit" value="提交表单">

<!-- button方式（推荐） -->
<button type="submit">
    <img src="submit-icon.svg" alt=""> 提交表单
</button>
```

### 重置按钮
```html
<input type="reset" value="重置">
<button type="reset">重置表单</button>
```

### 普通按钮
```html
<input type="button" value="普通按钮" onclick="doSomething()">
<button type="button" onclick="doSomething()">点击我</button>
```

## 表单标签和分组

### `<label>` 标签
标签与表单控件关联，提高可用性和无障碍访问：

```html
<!-- 方法1：使用for属性 -->
<label for="username">用户名：</label>
<input type="text" id="username" name="username">

<!-- 方法2：嵌套方式 -->
<label>
    邮箱：
    <input type="email" name="email">
</label>
```

### `<fieldset>` 和 `<legend>`
用于分组相关的表单控件：

```html
<form>
    <fieldset>
        <legend>个人信息</legend>
        <label for="fullname">姓名：</label>
        <input type="text" id="fullname" name="fullname">
        
        <label for="email">邮箱：</label>
        <input type="email" id="email" name="email">
    </fieldset>
    
    <fieldset>
        <legend>联系方式</legend>
        <label for="phone">电话：</label>
        <input type="tel" id="phone" name="phone">
        
        <label for="address">地址：</label>
        <textarea id="address" name="address"></textarea>
    </fieldset>
</form>
```

## 表单验证

### HTML5内置验证

#### 必填字段
```html
<input type="text" name="username" required>
<textarea name="message" required></textarea>
<select name="country" required>
    <option value="">请选择</option>
    <option value="china">中国</option>
</select>
```

#### 长度限制
```html
<input type="text" name="username" minlength="3" maxlength="20">
<input type="password" name="password" minlength="8">
<textarea name="bio" maxlength="500"></textarea>
```

#### 数值范围
```html
<input type="number" name="age" min="18" max="65">
<input type="range" name="rating" min="1" max="5" step="0.5">
<input type="date" name="startDate" min="2023-01-01" max="2023-12-31">
```

#### 正则表达式验证
```html
<!-- 电话号码格式 -->
<input type="tel" 
       name="phone" 
       pattern="[0-9]{3}-[0-9]{4}-[0-9]{4}" 
       title="请输入格式：138-0000-0000">

<!-- 邮政编码 -->
<input type="text" 
       name="zipcode" 
       pattern="[0-9]{6}" 
       title="请输入6位数字邮政编码">

<!-- 用户名（字母数字下划线，3-20位） -->
<input type="text" 
       name="username" 
       pattern="[A-Za-z0-9_]{3,20}" 
       title="用户名只能包含字母、数字和下划线，长度3-20位">
```

### 自定义验证消息
```html
<input type="email" 
       name="email" 
       required 
       oninvalid="this.setCustomValidity('请输入有效的邮箱地址')"
       oninput="this.setCustomValidity('')">
```

### 禁用验证
```html
<!-- 整个表单禁用验证 -->
<form novalidate>
    <!-- 表单内容 -->
</form>

<!-- 特定按钮禁用验证 -->
<button type="submit" formnovalidate>跳过验证提交</button>
```

## 完整表单示例

### 用户注册表单
```html
<form action="register.php" method="post" enctype="multipart/form-data">
    <fieldset>
        <legend>账户信息</legend>
        
        <div class="form-group">
            <label for="username">用户名：</label>
            <input type="text" 
                   id="username" 
                   name="username" 
                   required 
                   minlength="3" 
                   maxlength="20"
                   pattern="[A-Za-z0-9_]+"
                   placeholder="3-20位字母数字下划线">
            <small>用户名将用于登录，设置后不可修改</small>
        </div>
        
        <div class="form-group">
            <label for="email">邮箱：</label>
            <input type="email" 
                   id="email" 
                   name="email" 
                   required
                   placeholder="example@email.com">
            <small>用于接收重要通知和找回密码</small>
        </div>
        
        <div class="form-group">
            <label for="password">密码：</label>
            <input type="password" 
                   id="password" 
                   name="password" 
                   required 
                   minlength="8"
                   placeholder="至少8位字符">
        </div>
        
        <div class="form-group">
            <label for="confirm-password">确认密码：</label>
            <input type="password" 
                   id="confirm-password" 
                   name="confirm-password" 
                   required>
        </div>
    </fieldset>
    
    <fieldset>
        <legend>个人信息</legend>
        
        <div class="form-group">
            <label for="fullname">真实姓名：</label>
            <input type="text" 
                   id="fullname" 
                   name="fullname" 
                   required>
        </div>
        
        <div class="form-group">
            <label>性别：</label>
            <div class="radio-group">
                <input type="radio" id="male" name="gender" value="male" required>
                <label for="male">男</label>
                
                <input type="radio" id="female" name="gender" value="female" required>
                <label for="female">女</label>
                
                <input type="radio" id="other" name="gender" value="other" required>
                <label for="other">其他</label>
            </div>
        </div>
        
        <div class="form-group">
            <label for="birthday">出生日期：</label>
            <input type="date" 
                   id="birthday" 
                   name="birthday" 
                   max="2005-12-31">
        </div>
        
        <div class="form-group">
            <label for="city">居住城市：</label>
            <select id="city" name="city" required>
                <option value="">请选择城市</option>
                <optgroup label="一线城市">
                    <option value="beijing">北京</option>
                    <option value="shanghai">上海</option>
                    <option value="guangzhou">广州</option>
                    <option value="shenzhen">深圳</option>
                </optgroup>
                <optgroup label="新一线城市">
                    <option value="chengdu">成都</option>
                    <option value="hangzhou">杭州</option>
                    <option value="wuhan">武汉</option>
                </optgroup>
            </select>
        </div>
        
        <div class="form-group">
            <label for="avatar">头像：</label>
            <input type="file" 
                   id="avatar" 
                   name="avatar" 
                   accept="image/*">
            <small>支持JPG、PNG格式，大小不超过2MB</small>
        </div>
        
        <div class="form-group">
            <label for="bio">个人简介：</label>
            <textarea id="bio" 
                      name="bio" 
                      rows="4" 
                      maxlength="200" 
                      placeholder="简单介绍一下自己..."></textarea>
            <small><span id="bio-count">0</span>/200字</small>
        </div>
    </fieldset>
    
    <fieldset>
        <legend>兴趣爱好</legend>
        
        <div class="form-group">
            <label>兴趣爱好：</label>
            <div class="checkbox-group">
                <input type="checkbox" id="reading" name="hobbies[]" value="reading">
                <label for="reading">阅读</label>
                
                <input type="checkbox" id="music" name="hobbies[]" value="music">
                <label for="music">音乐</label>
                
                <input type="checkbox" id="sports" name="hobbies[]" value="sports">
                <label for="sports">运动</label>
                
                <input type="checkbox" id="travel" name="hobbies[]" value="travel">
                <label for="travel">旅行</label>
                
                <input type="checkbox" id="photography" name="hobbies[]" value="photography">
                <label for="photography">摄影</label>
                
                <input type="checkbox" id="cooking" name="hobbies[]" value="cooking">
                <label for="cooking">烹饪</label>
            </div>
        </div>
    </fieldset>
    
    <fieldset>
        <legend>其他设置</legend>
        
        <div class="form-group">
            <input type="checkbox" id="newsletter" name="newsletter" value="1" checked>
            <label for="newsletter">订阅邮件通知</label>
        </div>
        
        <div class="form-group">
            <input type="checkbox" id="terms" name="terms" value="1" required>
            <label for="terms">我已阅读并同意 <a href="terms.html" target="_blank">用户协议</a> 和 <a href="privacy.html" target="_blank">隐私政策</a></label>
        </div>
    </fieldset>
    
    <div class="form-actions">
        <button type="submit" class="btn-primary">注册账户</button>
        <button type="reset" class="btn-secondary">重置表单</button>
    </div>
</form>
```

### 联系表单
```html
<form action="contact.php" method="post">
    <div class="form-group">
        <label for="name">姓名：</label>
        <input type="text" id="name" name="name" required>
    </div>
    
    <div class="form-group">
        <label for="email">邮箱：</label>
        <input type="email" id="email" name="email" required>
    </div>
    
    <div class="form-group">
        <label for="subject">主题：</label>
        <select id="subject" name="subject" required>
            <option value="">请选择主题</option>
            <option value="general">一般咨询</option>
            <option value="technical">技术支持</option>
            <option value="business">商务合作</option>
            <option value="complaint">投诉建议</option>
        </select>
    </div>
    
    <div class="form-group">
        <label for="priority">优先级：</label>
        <div class="radio-group">
            <input type="radio" id="low" name="priority" value="low" checked>
            <label for="low">低</label>
            
            <input type="radio" id="normal" name="priority" value="normal">
            <label for="normal">普通</label>
            
            <input type="radio" id="high" name="priority" value="high">
            <label for="high">紧急</label>
        </div>
    </div>
    
    <div class="form-group">
        <label for="message">留言内容：</label>
        <textarea id="message" 
                  name="message" 
                  rows="6" 
                  required 
                  placeholder="请详细描述您的问题或需求..."></textarea>
    </div>
    
    <div class="form-group">
        <label for="attachments">附件：</label>
        <input type="file" 
               id="attachments" 
               name="attachments[]" 
               multiple 
               accept=".pdf,.doc,.docx,.jpg,.jpeg,.png">
        <small>可上传PDF、Word文档或图片文件</small>
    </div>
    
    <div class="form-actions">
        <button type="submit">发送消息</button>
        <button type="reset">清空表单</button>
    </div>
</form>
```

## 表单样式和用户体验

### CSS基础样式
```css
.form-group {
    margin-bottom: 1rem;
}

label {
    display: block;
    font-weight: bold;
    margin-bottom: 0.25rem;
}

input, textarea, select {
    width: 100%;
    padding: 0.5rem;
    border: 1px solid #ccc;
    border-radius: 4px;
    font-size: 1rem;
}

input:focus, textarea:focus, select:focus {
    outline: none;
    border-color: #007bff;
    box-shadow: 0 0 0 2px rgba(0,123,255,0.25);
}

.radio-group, .checkbox-group {
    display: flex;
    gap: 1rem;
}

.radio-group input, .checkbox-group input {
    width: auto;
}

button {
    padding: 0.75rem 1.5rem;
    border: none;
    border-radius: 4px;
    cursor: pointer;
    font-size: 1rem;
}

.btn-primary {
    background-color: #007bff;
    color: white;
}

.btn-secondary {
    background-color: #6c757d;
    color: white;
}
```

### 错误状态样式
```css
.form-group.error input,
.form-group.error textarea,
.form-group.error select {
    border-color: #dc3545;
}

.error-message {
    color: #dc3545;
    font-size: 0.875rem;
    margin-top: 0.25rem;
}

.form-group.success input {
    border-color: #28a745;
}
```

## 最佳实践

### 1. 可用性原则
- ✅ 使用清晰的标签和说明
- ✅ 提供合适的输入类型和验证
- ✅ 使用fieldset分组相关字段
- ✅ 为必填字段添加明显标识

### 2. 无障碍访问
- ✅ 每个输入控件都有关联的label
- ✅ 使用适当的ARIA属性
- ✅ 确保键盘导航的可用性
- ✅ 提供清晰的错误信息

### 3. 移动友好
- ✅ 使用合适的input类型（自动调用相应键盘）
- ✅ 设置适当的字体大小
- ✅ 确保触摸目标足够大

### 4. 性能优化
- ✅ 避免不必要的验证
- ✅ 使用适当的enctype
- ✅ 考虑表单的加载速度

## 下一步学习

现在您已经掌握了HTML表单的创建和使用，让我们继续学习：

1. [语义化标签](./10-semantic-tags.md) - 学习使用HTML5语义化元素
2. [最佳实践](./11-best-practices.md) - 掌握HTML编码规范和优化技巧

---

> 💡 **记住**: 好的表单设计不仅要功能完整，更要用户体验友好！

*[上一章: 表格](./08-tables.md) | [返回主目录](./README.md) | [下一章: 语义化标签](./10-semantic-tags.md)*
