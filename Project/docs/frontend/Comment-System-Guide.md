# 评论系统使用指南

## 概述

本项目实现了一套完整的评论系统，参考了 Bilibili 等主流平台的设计风格，支持评论、回复、点赞、删除等功能。

## 组件结构

### 1. CommentBox.vue - 评论展示组件

**功能特性：**
- 显示用户头像和昵称
- 展示评论内容和发表时间
- 支持点赞/取消点赞
- 支持回复评论
- 支持删除自己的评论（右上角删除按钮）
- 递归显示子评论（嵌套回复）

**Props：**
```typescript
interface Props {
  comment: Comment         // 评论数据
  currentUserId?: string   // 当前登录用户ID
  isReply?: boolean        // 是否为子评论（用于缩进显示）
}
```

**Events：**
- `@delete` - 删除评论事件，传递 commentId
- `@like` - 点赞评论事件，传递 commentId
- `@reply` - 回复评论事件，传递完整的 comment 对象

**样式特点：**
- GitHub 暗色主题风格
- 悬停时背景高亮
- 子评论左侧缩进 48px
- 响应式设计，移动端适配

### 2. BottomComment.vue - 底部评论输入组件

**功能特性：**
- 固定在屏幕底部
- 自动调整输入框高度（40px - 200px）
- 显示回复目标提示
- 字符计数（最大 1000 字符）
- 支持 Ctrl+Enter / Cmd+Enter 快捷发送
- 取消回复按钮

**Props：**
```typescript
interface Props {
  replyTarget?: Comment | null  // 回复的目标评论
}
```

**Events：**
- `@send` - 发送评论事件，参数：(content: string, parentCommentId?: string)
- `@cancel-reply` - 取消回复事件

**使用方法：**
```vue
<BottomComment
  :reply-target="replyTarget"
  @send="handleSendComment"
  @cancel-reply="handleCancelReply"
/>
```

## 集成到 BlogDetailView

### 1. 导入组件

```typescript
import CommentBox from '@/components/CommentBox.vue'
import BottomComment from '@/components/BottomComment.vue'
```

### 2. 状态管理

```typescript
// 评论相关状态
const comments = ref<Comment[]>([])
const commentsLoading = ref(false)
const replyTarget = ref<Comment | null>(null)
const currentUserId = ref<string>('')
```

### 3. 核心功能实现

#### 获取当前用户信息

```typescript
const getCurrentUser = (): UserData | null => {
  try {
    const isLoggedIn = localStorage.getItem('isLoggedIn') === 'true'
    if (!isLoggedIn) return null
    
    const userId = localStorage.getItem('userId')
    const userName = localStorage.getItem('userName')
    const userEmail = localStorage.getItem('userEmail')
    
    if (!userId || !userName) return null
    
    return {
      id: userId,
      name: userName,
      email: userEmail || '',
      password: ''
    }
  } catch {
    return null
  }
}
```

#### 加载评论

```typescript
const loadComments = async () => {
  if (!blog.value) return
  
  commentsLoading.value = true
  
  try {
    const response = await fetch(`/api/Comment/blog/${blog.value.id}`)
    
    if (response.ok) {
      const data = await response.json()
      comments.value = data
    }
  } catch (err) {
    console.error('加载评论失败:', err)
  } finally {
    commentsLoading.value = false
  }
}
```

#### 发送评论

```typescript
const handleSendComment = async (content: string, parentCommentId?: string) => {
  const user = getCurrentUser()
  
  if (!user) {
    alert('请先登录后再评论')
    return
  }
  
  try {
    const response = await fetch('/api/Comment/create', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({
        content,
        blogId: blog.value.id,
        userId: user.id,
        parentCommentId: parentCommentId || null
      })
    })
    
    if (response.ok) {
      await loadComments()
      replyTarget.value = null
      
      if (blog.value) {
        blog.value.commentsCount = (blog.value.commentsCount || 0) + 1
      }
    }
  } catch (err) {
    console.error('发送评论失败:', err)
    alert('发送评论失败，请稍后重试')
  }
}
```

#### 删除评论

```typescript
const handleDeleteComment = async (commentId: string) => {
  const user = getCurrentUser()
  
  if (!user) {
    alert('请先登录')
    return
  }
  
  try {
    const response = await fetch(`/api/Comment/${commentId}?userId=${user.id}`, {
      method: 'DELETE'
    })
    
    if (response.ok) {
      await loadComments()
      
      if (blog.value && blog.value.commentsCount > 0) {
        blog.value.commentsCount -= 1
      }
    }
  } catch (err) {
    console.error('删除评论失败:', err)
  }
}
```

#### 点赞评论

```typescript
const handleLikeComment = async (commentId: string) => {
  try {
    const response = await fetch(`/api/Comment/${commentId}/like`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      }
    })
    
    if (response.ok) {
      await loadComments()
    }
  } catch (err) {
    console.error('点赞评论失败:', err)
  }
}
```

#### 回复评论

```typescript
const handleReplyComment = (comment: Comment) => {
  replyTarget.value = comment
}

const handleCancelReply = () => {
  replyTarget.value = null
}
```

## LocalStorage 数据结构

### 用户认证信息（简单键值对）
```
isLoggedIn: "true"
userId: "guid-string"
userName: "用户名"
userEmail: "user@example.com"
registerTime: "2025-10-04"
```

### 点赞状态（JSON 数组）

**博客点赞：**
```javascript
likedBlogs: ["blogId1", "blogId2", ...]
```

**评论点赞：**
```javascript
likedComments: ["commentId1", "commentId2", ...]
```

## API 接口

### 评论相关接口

1. **创建评论**
   - POST `/api/Comment/create`
   - Body: `{ content, blogId, userId, parentCommentId? }`

2. **获取博客评论**
   - GET `/api/Comment/blog/{blogId}`
   - 返回嵌套的评论树结构

3. **删除评论**
   - DELETE `/api/Comment/{id}?userId={userId}`

4. **点赞/取消点赞评论**
   - POST `/api/Comment/{id}/like`

5. **获取用户评论**
   - GET `/api/Comment/user/{userId}`

## 样式定制

### 主题色
- 主色调：`#58a6ff` (蓝色)
- 背景色：`#0d1117` (深色)
- 边框色：`#30363d` (灰色)
- 文字色：`#c9d1d9` (浅灰)
- 点赞色：`#f85149` (红色)
- 成功色：`#238636` (绿色)

### 响应式断点
- 移动端：`max-width: 768px`
- 子评论缩进：桌面 48px，移动端 32px
- 头像尺寸：桌面 40px，移动端 32px

## 用户体验优化

1. **时间显示格式化**
   - 1分钟内：刚刚
   - 1小时内：X分钟前
   - 24小时内：X小时前
   - 7天内：X天前
   - 超过7天：显示完整日期

2. **自动高度调整**
   - 输入框根据内容自动调整高度
   - 最小 40px，最大 200px

3. **权限控制**
   - 未登录用户可查看评论
   - 登录后才能发表、点赞、删除评论
   - 只能删除自己的评论

4. **错误处理**
   - 网络错误提示
   - 权限不足提示
   - 操作失败提示

## 未来改进方向

1. **功能增强**
   - 评论编辑功能
   - 评论举报功能
   - @提及用户
   - Markdown 支持
   - 表情包支持
   - 图片上传

2. **性能优化**
   - 虚拟滚动（大量评论）
   - 分页加载
   - 评论缓存

3. **用户体验**
   - 评论排序（最新、最热）
   - 查看更多回复
   - 评论搜索
   - 评论通知

## 测试清单

- [ ] 未登录用户查看评论
- [ ] 登录用户发表评论
- [ ] 回复其他用户评论
- [ ] 回复自己的评论（嵌套）
- [ ] 点赞/取消点赞评论
- [ ] 删除自己的评论
- [ ] 尝试删除他人评论（应失败）
- [ ] 测试评论字符限制（1000字符）
- [ ] 测试 Ctrl+Enter 快捷键
- [ ] 测试移动端响应式
- [ ] 测试评论树结构显示
- [ ] 测试时间格式化显示
