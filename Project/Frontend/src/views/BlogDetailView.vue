<template>
  <div class="blog-detail-view">
    <!-- 加载状态 -->
    <div v-if="loading" class="loading-container">
      <div class="loading-spinner"></div>
      <p>加载文章中...</p>
    </div>

    <!-- 错误状态 -->
    <div v-else-if="error" class="error-container">
      <svg width="64" height="64" viewBox="0 0 16 16">
        <path d="M8 0a8 8 0 1 1 0 16A8 8 0 0 1 8 0ZM1.5 8a6.5 6.5 0 1 0 13 0 6.5 6.5 0 0 0-13 0Zm9.78-2.22a.751.751 0 0 0-1.042-.018.751.751 0 0 0-.018 1.042L9.94 8.5l-1.72 1.696a.75.75 0 1 0 1.06 1.06L11 9.56l1.72 1.696a.75.75 0 1 0 1.06-1.06L12.06 8.5l1.72-1.696a.75.75 0 1 0-1.06-1.06L11 7.44 9.28 5.744Z" fill="currentColor"/>
      </svg>
      <h2>{{ error }}</h2>
      <button @click="loadBlog" class="retry-button">重试</button>
      <button @click="goBack" class="back-button">返回列表</button>
    </div>

    <!-- 文章内容 -->
    <article v-else-if="blog" class="blog-article">
      <!-- 返回按钮 -->
      <button @click="goBack" class="nav-back-button">
        <svg width="16" height="16" viewBox="0 0 16 16">
          <path d="M9.78 12.78a.75.75 0 0 1-1.06 0L4.47 8.53a.75.75 0 0 1 0-1.06l4.25-4.25a.751.751 0 0 1 1.042.018.751.751 0 0 1 .018 1.042L6.06 8l3.72 3.72a.75.75 0 0 1 0 1.06Z" fill="currentColor"/>
        </svg>
        返回列表
      </button>

      <!-- 文章头部 -->
      <header class="article-header">
        <h1 class="article-title">{{ blog.title }}</h1>
        
        <div class="article-meta">
          <div class="meta-item">
            <svg width="16" height="16" viewBox="0 0 16 16">
              <path d="M8 0a8 8 0 1 1 0 16A8 8 0 0 1 8 0ZM1.5 8a6.5 6.5 0 1 0 13 0 6.5 6.5 0 0 0-13 0Zm7-3.25v2.992l2.028.812a.75.75 0 0 1-.557 1.392l-2.5-1A.751.751 0 0 1 7 8.25v-3.5a.75.75 0 0 1 1.5 0Z" fill="currentColor"/>
            </svg>
            <span>{{ formatDate(blog.createdAt) }}</span>
          </div>
          
          <div v-if="blog.updatedAt && blog.updatedAt !== blog.createdAt" class="meta-item">
            <svg width="16" height="16" viewBox="0 0 16 16">
              <path d="M1.705 8.005a.75.75 0 0 1 .834.656 5.5 5.5 0 0 0 9.592 2.97l-1.204-1.204a.25.25 0 0 1 .177-.427h3.646a.25.25 0 0 1 .25.25v3.646a.25.25 0 0 1-.427.177l-1.38-1.38A7.002 7.002 0 0 1 1.05 8.84a.75.75 0 0 1 .656-.834ZM8 2.5a5.487 5.487 0 0 0-4.131 1.869l1.204 1.204A.25.25 0 0 1 4.896 6H1.25A.25.25 0 0 1 1 5.75V2.104a.25.25 0 0 1 .427-.177l1.38 1.38A7.002 7.002 0 0 1 14.95 7.16a.75.75 0 0 1-1.49.178A5.5 5.5 0 0 0 8 2.5Z" fill="currentColor"/>
            </svg>
            <span>更新于 {{ formatDate(blog.updatedAt) }}</span>
          </div>
        </div>

        <!-- 标签 -->
        <div v-if="blog.tags" class="article-tags">
          <span 
            v-for="tag in parseTags(blog.tags)" 
            :key="tag" 
            class="tag"
          >
            <svg width="16" height="16" viewBox="0 0 16 16">
              <path d="M1 7.775V2.75C1 1.784 1.784 1 2.75 1h5.025c.464 0 .91.184 1.238.513l6.25 6.25a1.75 1.75 0 0 1 0 2.474l-5.026 5.026a1.75 1.75 0 0 1-2.474 0l-6.25-6.25A1.752 1.752 0 0 1 1 7.775Zm1.5 0c0 .066.026.13.073.177l6.25 6.25a.25.25 0 0 0 .354 0l5.025-5.025a.25.25 0 0 0 0-.354l-6.25-6.25a.25.25 0 0 0-.177-.073H2.75a.25.25 0 0 0-.25.25ZM6 5a1 1 0 1 1 0 2 1 1 0 0 1 0-2Z" fill="currentColor"/>
            </svg>
            {{ tag }}
          </span>
        </div>
      </header>

      <!-- 文章内容 -->
      <div class="article-content">
        <div class="content-wrapper" v-html="renderContent(blog.content)"></div>
      </div>

      <!-- 文章底部 -->
      <footer class="article-footer">
        <div class="footer-actions">
          <button class="action-button like-button" @click="toggleLike">
            <svg width="20" height="20" viewBox="0 0 16 16">
              <path d="m8 14.25.345.666a.75.75 0 0 1-.69 0l-.008-.004-.018-.01a7.152 7.152 0 0 1-.31-.17 22.055 22.055 0 0 1-3.434-2.414C2.045 10.731 0 8.35 0 5.5 0 2.836 2.086 1 4.25 1 5.797 1 7.153 1.802 8 3.02 8.847 1.802 10.203 1 11.75 1 13.914 1 16 2.836 16 5.5c0 2.85-2.045 5.231-3.885 6.818a22.066 22.066 0 0 1-3.744 2.584l-.018.01-.006.003h-.002ZM4.25 2.5c-1.336 0-2.75 1.164-2.75 3 0 2.15 1.58 4.144 3.365 5.682A20.58 20.58 0 0 0 8 13.393a20.58 20.58 0 0 0 3.135-2.211C12.92 9.644 14.5 7.65 14.5 5.5c0-1.836-1.414-3-2.75-3-1.373 0-2.609.986-3.029 2.456a.749.749 0 0 1-1.442 0C6.859 3.486 5.623 2.5 4.25 2.5Z" fill="currentColor"/>
            </svg>
            <span>{{ liked ? '已点赞' : '点赞' }}</span>
          </button>
          
          <button class="action-button share-button" @click="shareArticle">
            <svg width="20" height="20" viewBox="0 0 16 16">
              <path d="M7.775 3.275a.75.75 0 0 0 1.06 1.06l1.25-1.25a2 2 0 1 1 2.83 2.83l-2.5 2.5a2 2 0 0 1-2.83 0 .75.75 0 0 0-1.06 1.06 3.5 3.5 0 0 0 4.95 0l2.5-2.5a3.5 3.5 0 0 0-4.95-4.95l-1.25 1.25Zm-4.69 9.64a2 2 0 0 1 0-2.83l2.5-2.5a2 2 0 0 1 2.83 0 .75.75 0 0 0 1.06-1.06 3.5 3.5 0 0 0-4.95 0l-2.5 2.5a3.5 3.5 0 0 0 4.95 4.95l1.25-1.25a.75.75 0 0 0-1.06-1.06l-1.25 1.25a2 2 0 0 1-2.83 0Z" fill="currentColor"/>
            </svg>
            <span>分享</span>
          </button>
        </div>

        <div class="author-card">
          <div class="author-avatar">
            <svg width="48" height="48" viewBox="0 0 16 16">
              <path d="M10.561 8.073a6.005 6.005 0 0 1 3.432 5.142.75.75 0 1 1-1.498.07 4.5 4.5 0 0 0-8.99 0 .75.75 0 0 1-1.498-.07 6.004 6.004 0 0 1 3.431-5.142 3.999 3.999 0 1 1 5.123 0ZM10.5 5a2.5 2.5 0 1 0-5 0 2.5 2.5 0 0 0 5 0Z" fill="currentColor"/>
            </svg>
          </div>
          <div class="author-info">
            <h4>作者</h4>
            <p>{{ blog.authorId }}</p>
          </div>
        </div>
      </footer>
    </article>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useRouter, useRoute } from 'vue-router'

const router = useRouter()
const route = useRoute()

// 响应式数据
const blog = ref<any>(null)
const loading = ref(true)
const error = ref('')
const liked = ref(false)

// 加载文章详情
const loadBlog = async () => {
  loading.value = true
  error.value = ''
  
  try {
    const blogId = route.params.id as string
    
    if (!blogId) {
      throw new Error('文章ID无效')
    }

    // 获取所有文章,然后找到对应的文章
    // TODO: 后端应该添加根据 ID 获取单篇文章的 API
    const userId = localStorage.getItem('userId') || '049ac648-0176-4be4-88f1-e14838e15152'
    const response = await fetch(`/api/Blog/all?userId=${userId}`)
    
    if (!response.ok) {
      throw new Error('获取文章失败')
    }
    
    const blogs = await response.json()
    const foundBlog = blogs.find((b: any) => b.id === blogId)
    
    if (!foundBlog) {
      throw new Error('文章不存在')
    }
    
    blog.value = foundBlog
    
  } catch (err) {
    error.value = err instanceof Error ? err.message : '加载失败,请重试'
    console.error('加载文章失败:', err)
  } finally {
    loading.value = false
  }
}

// 解析标签
const parseTags = (tags: string): string[] => {
  if (!tags) return []
  return tags.split(',').map(t => t.trim()).filter(t => t)
}

// 格式化日期
const formatDate = (dateString: string): string => {
  const date = new Date(dateString)
  return date.toLocaleDateString('zh-CN', {
    year: 'numeric',
    month: 'long',
    day: 'numeric',
    hour: '2-digit',
    minute: '2-digit'
  })
}

// 渲染文章内容(简单的 Markdown 渲染)
const renderContent = (content: string): string => {
  if (!content) return ''
  
  // 简单的 Markdown 处理
  let html = content
  
  // 处理段落
  html = html.split('\n\n').map(para => `<p>${para}</p>`).join('')
  
  // 处理粗体
  html = html.replace(/\*\*(.*?)\*\*/g, '<strong>$1</strong>')
  
  // 处理斜体
  html = html.replace(/\*(.*?)\*/g, '<em>$1</em>')
  
  // 处理代码块
  html = html.replace(/`(.*?)`/g, '<code>$1</code>')
  
  // 处理链接
  html = html.replace(/\[(.*?)\]\((.*?)\)/g, '<a href="$2" target="_blank">$1</a>')
  
  return html
}

// 点赞功能
const toggleLike = () => {
  liked.value = !liked.value
  // TODO: 调用后端 API 保存点赞状态
}

// 分享文章
const shareArticle = async () => {
  if (navigator.share) {
    try {
      await navigator.share({
        title: blog.value?.title,
        text: blog.value?.content?.substring(0, 100),
        url: window.location.href
      })
    } catch (err) {
      console.log('分享失败', err)
    }
  } else {
    // 复制链接到剪贴板
    await navigator.clipboard.writeText(window.location.href)
    alert('链接已复制到剪贴板!')
  }
}

// 返回列表
const goBack = () => {
  router.push('/blogs')
}

// 组件挂载时加载数据
onMounted(() => {
  loadBlog()
})
</script>

<style scoped>
.blog-detail-view {
  min-height: 100vh;
  background: linear-gradient(135deg, #0d1117 0%, #161b22 100%);
  color: #c9d1d9;
  padding: 2rem;
}

/* 加载和错误状态 */
.loading-container,
.error-container {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  min-height: 60vh;
  text-align: center;
}

.loading-spinner {
  width: 64px;
  height: 64px;
  border: 4px solid #30363d;
  border-top-color: #58a6ff;
  border-radius: 50%;
  animation: spin 1s linear infinite;
  margin-bottom: 1rem;
}

@keyframes spin {
  to { transform: rotate(360deg); }
}

.error-container svg {
  color: #f85149;
  margin-bottom: 1.5rem;
}

.error-container h2 {
  margin: 0 0 1.5rem 0;
  color: #c9d1d9;
}

.retry-button,
.back-button {
  margin: 0.5rem;
  padding: 0.75rem 1.5rem;
  border: none;
  border-radius: 6px;
  font-size: 1rem;
  cursor: pointer;
  transition: all 0.3s ease;
}

.retry-button {
  background: #238636;
  color: white;
}

.retry-button:hover {
  background: #2ea043;
}

.back-button {
  background: #21262d;
  color: #c9d1d9;
  border: 1px solid #30363d;
}

.back-button:hover {
  background: #30363d;
}

/* 文章内容 */
.blog-article {
  max-width: 900px;
  margin: 0 auto;
}

.nav-back-button {
  display: inline-flex;
  align-items: center;
  gap: 0.5rem;
  padding: 0.5rem 1rem;
  background: #21262d;
  border: 1px solid #30363d;
  border-radius: 6px;
  color: #8b949e;
  font-size: 0.9rem;
  cursor: pointer;
  margin-bottom: 2rem;
  transition: all 0.3s ease;
}

.nav-back-button:hover {
  background: #30363d;
  color: #58a6ff;
  border-color: #58a6ff;
}

/* 文章头部 */
.article-header {
  margin-bottom: 3rem;
  padding-bottom: 2rem;
  border-bottom: 1px solid #30363d;
}

.article-title {
  font-size: 2.5rem;
  font-weight: 800;
  margin: 0 0 1.5rem 0;
  line-height: 1.2;
  color: #ffffff;
  letter-spacing: -0.02em;
}

.article-meta {
  display: flex;
  flex-wrap: wrap;
  gap: 1.5rem;
  margin-bottom: 1.5rem;
}

.meta-item {
  display: flex;
  align-items: center;
  gap: 0.5rem;
  color: #8b949e;
  font-size: 0.95rem;
}

.meta-item svg {
  color: #58a6ff;
}

.article-tags {
  display: flex;
  flex-wrap: wrap;
  gap: 0.75rem;
}

.tag {
  display: inline-flex;
  align-items: center;
  gap: 0.4rem;
  padding: 0.4rem 0.8rem;
  background: #21262d;
  border: 1px solid #30363d;
  border-radius: 6px;
  color: #58a6ff;
  font-size: 0.85rem;
  transition: all 0.3s ease;
}

.tag:hover {
  background: #30363d;
  border-color: #58a6ff;
}

/* 文章内容 */
.article-content {
  margin-bottom: 3rem;
}

.content-wrapper {
  background: #0d1117;
  border: 1px solid #30363d;
  border-radius: 12px;
  padding: 2.5rem;
  line-height: 1.8;
  font-size: 1.05rem;
}

.content-wrapper :deep(p) {
  margin: 1.5rem 0;
  color: #c9d1d9;
}

.content-wrapper :deep(strong) {
  color: #ffffff;
  font-weight: 600;
}

.content-wrapper :deep(em) {
  color: #7ee787;
  font-style: italic;
}

.content-wrapper :deep(code) {
  background: #161b22;
  border: 1px solid #30363d;
  padding: 0.2rem 0.4rem;
  border-radius: 4px;
  font-family: 'Courier New', monospace;
  font-size: 0.9em;
  color: #ff7b72;
}

.content-wrapper :deep(a) {
  color: #58a6ff;
  text-decoration: none;
  border-bottom: 1px solid transparent;
  transition: border-color 0.3s ease;
}

.content-wrapper :deep(a:hover) {
  border-bottom-color: #58a6ff;
}

/* 文章底部 */
.article-footer {
  padding-top: 2rem;
  border-top: 1px solid #30363d;
}

.footer-actions {
  display: flex;
  gap: 1rem;
  margin-bottom: 2rem;
}

.action-button {
  display: flex;
  align-items: center;
  gap: 0.5rem;
  padding: 0.75rem 1.5rem;
  border: 1px solid #30363d;
  border-radius: 6px;
  background: #21262d;
  color: #c9d1d9;
  font-size: 1rem;
  cursor: pointer;
  transition: all 0.3s ease;
}

.action-button:hover {
  background: #30363d;
  border-color: #58a6ff;
  color: #58a6ff;
  transform: translateY(-2px);
}

.action-button svg {
  transition: all 0.3s ease;
}

.like-button:hover svg {
  fill: #f85149;
  color: #f85149;
}

.author-card {
  display: flex;
  align-items: center;
  gap: 1.5rem;
  padding: 1.5rem;
  background: #0d1117;
  border: 1px solid #30363d;
  border-radius: 12px;
}

.author-avatar {
  width: 64px;
  height: 64px;
  background: #21262d;
  border-radius: 50%;
  display: flex;
  align-items: center;
  justify-content: center;
}

.author-avatar svg {
  color: #58a6ff;
}

.author-info h4 {
  margin: 0 0 0.5rem 0;
  color: #8b949e;
  font-size: 0.9rem;
  font-weight: 500;
}

.author-info p {
  margin: 0;
  color: #c9d1d9;
  font-size: 1.1rem;
  font-weight: 600;
}

/* 响应式设计 */
@media (max-width: 768px) {
  .blog-detail-view {
    padding: 1rem;
  }

  .article-title {
    font-size: 1.8rem;
  }

  .content-wrapper {
    padding: 1.5rem;
  }

  .footer-actions {
    flex-direction: column;
  }

  .action-button {
    width: 100%;
    justify-content: center;
  }
}
</style>
