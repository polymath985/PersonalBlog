# 测试博客功能

## 快速开始

### 1. 启动服务
```bash
# 启动后端和前端
使用 VS Code 任务: 🚀 启动全栈应用
```

### 2. 访问路径
- 登录: https://localhost:5156/login
- 主页: https://localhost:5156/home
- 文章列表: https://localhost:5156/blogs
- 文章详情: https://localhost:5156/blog/{id}

### 3. 测试流程

#### 步骤 1: 注册/登录
1. 访问登录页面
2. 注册新用户或使用现有账号登录
3. 记住返回的 userId

#### 步骤 2: 创建测试文章

使用 Backend.http 文件或 API 测试工具:

```http
POST https://localhost:7005/api/Blog/create
Content-Type: application/json

{
  "authorId": "your-user-id-here",
  "title": "我的第一篇技术博客",
  "content": "这是一篇关于 **Vue.js** 和 *ASP.NET Core* 的文章。\n\n学习如何构建现代化的全栈应用是一个有趣的过程。\n\n主要技术栈:\n- Vue 3 + TypeScript\n- ASP.NET Core 9.0\n- Entity Framework Core\n- SQLite/PostgreSQL",
  "tags": "Vue,ASP.NET,技术"
}
```

#### 步骤 3: 访问文章列表
1. 登录后访问 `/blogs`
2. 应该看到刚创建的文章
3. 测试搜索功能
4. 测试标签筛选

#### 步骤 4: 查看文章详情
1. 点击任意文章卡片
2. 应该跳转到文章详情页
3. 查看完整内容
4. 测试点赞和分享功能

## 常见问题

### Q: 文章列表为空?
A: 确保已创建至少一篇文章,并且使用正确的 userId

### Q: 无法加载文章?
A: 检查:
1. 后端服务是否运行在 https://localhost:7005
2. 前端代理配置是否正确
3. 浏览器控制台是否有错误

### Q: 样式显示不正常?
A: 清除浏览器缓存,重新加载页面

### Q: 点击文章后 404?
A: 确保路由配置正确,检查 router/index.ts

## 性能优化建议

### 1. 后端
- [ ] 添加分页功能
- [ ] 添加缓存机制
- [ ] 优化数据库查询

### 2. 前端
- [ ] 实现虚拟滚动(大量文章时)
- [ ] 添加骨架屏加载
- [ ] 图片懒加载

## 下一步开发

1. **Markdown 编辑器**
   - 使用 `@vueup/vue-quill` 或 `tiptap`
   - 支持实时预览

2. **文章编辑**
   - 创建 BlogEditView.vue
   - 添加编辑 API

3. **评论系统**
   - Comment 模型
   - 评论 API
   - 评论组件

4. **图片上传**
   - 文件上传 API
   - 拖拽上传
   - 图片压缩
