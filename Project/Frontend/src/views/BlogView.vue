<template>
  <div class="blog-view">
    <!-- 页面头部 -->
    <header class="blog-header">
      <div class="header-content">
        <div class="header-left">
        </div>
        <div class="header-actions">
          <!-- 排序选择器 -->
          <CustomSelect 
            v-model="sortBy"
            :options="sortOptions"
          />
          <button @click="createBlog" class="create-blog-button">
            <svg width="16" height="16" viewBox="0 0 16 16">
              <path d="M7.75 2a.75.75 0 0 1 .75.75V7h4.25a.75.75 0 0 1 0 1.5H8.5v4.25a.75.75 0 0 1-1.5 0V8.5H2.75a.75.75 0 0 1 0-1.5H7V2.75A.75.75 0 0 1 7.75 2Z" fill="currentColor"/>
            </svg>
            创作文章
          </button>
        </div>
      </div>
      
      <!-- 搜索和筛选 -->
      <div class="filter-section">
        <div class="search-box">
          <svg class="search-icon" width="16" height="16" viewBox="0 0 16 16">
            <path d="M10.68 11.74a6 6 0 0 1-7.922-8.982 6 6 0 0 1 8.982 7.922l3.04 3.04a.749.749 0 0 1-.326 1.275.749.749 0 0 1-.734-.215ZM11.5 7a4.5 4.5 0 1 0-8.997.01A4.5 4.5 0 0 0 11.5 7Z" fill="currentColor"/>
          </svg>
          <input 
            type="text" 
            v-model="searchQuery" 
            placeholder="搜索文章..."
            @input="handleSearch"
          />
        </div>
        
        <div class="filter-tags">
          <button 
            v-for="tag in allTags" 
            :key="tag"
            :class="['tag-filter', { active: selectedTag === tag }]"
            @click="filterByTag(tag)"
          >
            {{ tag }}
          </button>
        </div>
      </div>
    </header>

    <!-- 文章统计 -->
    <div class="blog-stats">
      <div class="stat-card">
        <div class="stat-icon">📝</div>
        <div class="stat-info">
          <span class="stat-number">{{ blogs.length }}</span>
          <span class="stat-label">总文章</span>
        </div>
      </div>
      <div class="stat-card">
        <div class="stat-icon">🏷️</div>
        <div class="stat-info">
          <span class="stat-number">{{ allTags.length }}</span>
          <span class="stat-label">标签</span>
        </div>
      </div>
      <div class="stat-card">
        <div class="stat-icon">👁️</div>
        <div class="stat-info">
          <span class="stat-number">{{ totalViews }}</span>
          <span class="stat-label">总阅读</span>
        </div>
      </div>
    </div>

    <!-- 加载状态 -->
    <div v-if="loading" class="loading-container">
      <div class="loading-spinner"></div>
      <p>加载中...</p>
    </div>

    <!-- 错误状态 -->
    <div v-else-if="error" class="error-container">
      <svg width="48" height="48" viewBox="0 0 16 16">
        <path d="M8 0a8 8 0 1 1 0 16A8 8 0 0 1 8 0ZM1.5 8a6.5 6.5 0 1 0 13 0 6.5 6.5 0 0 0-13 0Zm9.78-2.22a.751.751 0 0 0-1.042-.018.751.751 0 0 0-.018 1.042L9.94 8.5l-1.72 1.696a.75.75 0 1 0 1.06 1.06L11 9.56l1.72 1.696a.75.75 0 1 0 1.06-1.06L12.06 8.5l1.72-1.696a.75.75 0 1 0-1.06-1.06L11 7.44 9.28 5.744Z" fill="currentColor"/>
      </svg>
      <p>{{ error }}</p>
      <button @click="loadBlogs(null)" class="retry-button">重试</button>
    </div>

    <!-- 文章列表 -->
    <main v-else class="blog-list">
      <TransitionGroup name="blog-list" tag="div" class="blog-grid">
        <ContentBox
          v-for="blog in filteredBlogs"
          :key="blog.id"
          :title="blog.title"
          :description="getExcerpt(blog.content)"
          :icon="'book'"
          :tags="parseTags(blog.tags)"
          :stats="{
            views: blog.views || 0,
            likes: blog.likes || 0,
            comments: blog.commentsCount || 0
          }"
          :updateTime="formatDate(blog.updatedAt || blog.createdAt)"
          :featured="false"
          @click="() => viewBlog(blog.id)"
        />
      </TransitionGroup>

      <!-- 空状态 -->
      <div v-if="filteredBlogs.length === 0" class="empty-state">
        <svg width="64" height="64" viewBox="0 0 16 16">
          <path d="M1.75 1A1.75 1.75 0 0 0 0 2.75v10.5C0 14.216.784 15 1.75 15h12.5A1.75 1.75 0 0 0 16 13.25v-8.5A1.75 1.75 0 0 0 14.25 3H7.5L5.625 1.8a.75.75 0 0 0-.375-.1H1.75Z" fill="currentColor" opacity="0.3"/>
        </svg>
        <h3>暂无文章</h3>
        <p>{{ searchQuery || selectedTag !== 'All' ? '没有找到符合条件的文章' : '还没有发布任何文章' }}</p>
      </div>
<<<<<<< Updated upstream
=======

      <!-- 使用 ContentPage 组件展示文章 -->
      <ContentPage
        v-else
        :items="contentItems"
        :items-per-row="3"
        :items-per-page="9"
        :sort-by="sortBy"
        @item-click="handleItemClick"
      />
>>>>>>> Stashed changes
    </main>
  </div>
</template>

<script setup lang="ts">
<<<<<<< Updated upstream
import { ref, computed, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import ContentBox from '@/components/ContentBox.vue'
=======
import { ref, computed, watch, onMounted } from 'vue'
import { useRouter, useRoute } from 'vue-router'
import ContentPage from '@/components/ContentPage.vue'
import CustomSelect from '@/components/CustomSelect.vue'
>>>>>>> Stashed changes

const router = useRouter()
const route = useRoute()

interface Props {
  userId?: string | null
}

const props = defineProps<Props>()

// 响应式数据
const blogs = ref<any[]>([])
const loading = ref(true)
const error = ref('')
const searchQuery = ref('')
const selectedTag = ref('All')
const allTags = ref(['All'])
const totalViews = ref(0)
const sortBy = ref<'time-desc' | 'time-asc' | 'views-desc' | 'views-asc' | 'likes-desc' | 'likes-asc' | 'comments-desc' | 'comments-asc' | 'none'>('time-desc')

// 排序选项
const sortOptions = [
  { label: '最新发布', value: 'time-desc' },
  { label: '最早发布', value: 'time-asc' },
  { label: '最多浏览', value: 'views-desc' },
  { label: '最多点赞', value: 'likes-desc' },
  { label: '最多评论', value: 'comments-desc' }
]

// 加载博客列表
const loadBlogs = async (userId: string | null) => {
  loading.value = true
  error.value = ''
  let response;


  try {
    // 获取所有博客（不传 userId 参数）
    // 如果需要获取特定用户的博客，使用: `/api/Blog/all?userId=${userId}`
    if(userId){
       response = await fetch(`/api/Blog/all?userId=${userId}`)
    }else{
       response = await fetch('/api/Blog/all')
    }

    if (!response.ok) {
      throw new Error('获取文章列表失败')
    }
    
    const data = await response.json()
    blogs.value = data
    
    // 提取所有标签
    const tagsSet = new Set<string>(['All'])
    data.forEach((blog: any) => {
      if (blog.tags) {
        const tags = blog.tags.split(',').map((t: string) => t.trim())
        tags.forEach((tag: string) => tagsSet.add(tag))
      }
    })
    allTags.value = Array.from(tagsSet)
    
    // 计算总浏览量
    totalViews.value = data.reduce((sum: number, blog: any) => sum + (blog.views || 0), 0)
    
  } catch (err) {
    error.value = err instanceof Error ? err.message : '加载失败,请重试'
    console.error('加载博客失败:', err)
  } finally {
    loading.value = false
  }
}

// 过滤后的博客列表
const filteredBlogs = computed(() => {
  let filtered = blogs.value

  // 按标签筛选
  if (selectedTag.value !== 'All') {
    filtered = filtered.filter(blog => {
      const tags = parseTags(blog.tags)
      return tags.includes(selectedTag.value)
    })
  }

  // 按搜索关键词筛选 (搜索标题、内容、作者名)
  if (searchQuery.value) {
    const query = searchQuery.value.toLowerCase()
    filtered = filtered.filter(blog => {
      const titleMatch = blog.title.toLowerCase().includes(query)
      const contentMatch = blog.content.toLowerCase().includes(query)
      const authorMatch = blog.authorName && blog.authorName.toLowerCase().includes(query)
      const tagsMatch = blog.tags && blog.tags.toLowerCase().includes(query)
      
      return titleMatch || contentMatch || authorMatch || tagsMatch
    })
  }

  return filtered
})

<<<<<<< Updated upstream
=======
// 转换为 ContentPage 需要的格式
const contentItems = computed(() => {
  return filteredBlogs.value.map(blog => ({
    id: blog.id,
    title: blog.title,
    description: getExcerpt(blog.content),
    icon: 'book',
    tags: parseTags(blog.tags),
    stats: {
      views: blog.views || 0,
      likes: blog.likes || 0,
      comments: blog.commentsCount || 0
    },
    updateTime: blog.updatedAt || blog.createdAt, // 保留原始日期字符串用于排序
    author: blog.authorId ? {
      id: blog.authorId,
      name: blog.authorName || '未知作者',
      avatar: blog.authorAvatar
    } : undefined
  }))
})

>>>>>>> Stashed changes
// 解析标签
const parseTags = (tags: string): string[] => {
  if (!tags) return []
  return tags.split(',').map(t => t.trim()).filter(t => t)
}

// 获取文章摘要
const getExcerpt = (content: string, maxLength: number = 150): string => {
  if (!content) return '暂无内容...'
  if (content.length <= maxLength) return content
  return content.substring(0, maxLength) + '...'
}

// 格式化日期
const formatDate = (dateString: string): string => {
  const date = new Date(dateString)
  const now = new Date()
  const diff = now.getTime() - date.getTime()
  
  const minutes = Math.floor(diff / 60000)
  const hours = Math.floor(diff / 3600000)
  const days = Math.floor(diff / 86400000)
  
  if (minutes < 60) return `${minutes}分钟前`
  if (hours < 24) return `${hours}小时前`
  if (days < 7) return `${days}天前`
  
  return date.toLocaleDateString('zh-CN', {
    year: 'numeric',
    month: '2-digit',
    day: '2-digit'
  })
}

// 搜索处理
const handleSearch = () => {
  // 搜索逻辑已在 computed 中处理
}

// 按标签筛选
const filterByTag = (tag: string) => {
  selectedTag.value = tag
}

// 查看博客详情
const viewBlog = async (blogId: string) => {
  router.push(`/blog/${blogId}`)
}

// 创建博客
const createBlog = () => {
  router.push('/blog/create')
}

// 从 URL 查询参数初始化搜索条件
const initializeFromQuery = () => {
  const query = route.query.search as string
  const tag = route.query.tag as string
  
  if (query) {
    searchQuery.value = query
  }
  
  if (tag) {
    // 等待 allTags 加载后再设置选中的标签
    setTimeout(() => {
      if (allTags.value.includes(tag)) {
        selectedTag.value = tag
      }
    }, 500)
  }
}

// 监听路由查询参数变化
watch(() => route.query, (newQuery) => {
  if (newQuery.search) {
    searchQuery.value = newQuery.search as string
  } else if (searchQuery.value && !newQuery.search) {
    searchQuery.value = ''
  }
  
  if (newQuery.tag) {
    const tag = newQuery.tag as string
    if (allTags.value.includes(tag)) {
      selectedTag.value = tag
    }
  } else if (!newQuery.tag && selectedTag.value !== 'All') {
    selectedTag.value = 'All'
  }
})

// 组件挂载时加载数据
onMounted(() => {
  loadBlogs(props.userId || null)
  initializeFromQuery()
})
</script>

<style scoped>
.blog-view {
  min-height: 100vh;
  background: linear-gradient(135deg, #0d1117 0%, #161b22 100%);
  color: #c9d1d9;
  padding: 2rem;
}

/* 页面头部 */
.blog-header {
  max-width: 1200px;
  margin: 0 auto 3rem;
}

.header-content {
  display: flex;
  align-items: center;
  justify-content: space-between;
  margin-bottom: 2rem;
}

.header-left {
  flex: 1;
  text-align: center;
}

.header-actions {
  display: flex;
  align-items: center;
  gap: 1rem;
}

.page-title {
  font-size: 3rem;
  font-weight: 800;
  margin: 0 0 1rem 0;
  letter-spacing: -0.02em;
}

.create-blog-button {
  display: flex;
  align-items: center;
  gap: 0.5rem;
  padding: 0.875rem 1.5rem;
  background: linear-gradient(135deg, #238636 0%, #2ea043 100%);
  border: none;
  border-radius: 8px;
  color: white;
  font-size: 1rem;
  font-weight: 600;
  cursor: pointer;
  transition: all 0.3s ease;
  box-shadow: 0 4px 12px rgba(35, 134, 54, 0.3);
}

.create-blog-button:hover {
  transform: translateY(-2px);
  box-shadow: 0 6px 16px rgba(35, 134, 54, 0.4);
}

.create-blog-button svg {
  transition: transform 0.3s ease;
}

.create-blog-button:hover svg {
  transform: rotate(90deg);
}

.gradient-text {
  background: linear-gradient(135deg, #58a6ff 0%, #7ee787 100%);
  -webkit-background-clip: text;
  -webkit-text-fill-color: transparent;
  background-clip: text;
}

.page-subtitle {
  font-size: 1.1rem;
  color: #8b949e;
  margin: 0;
}

/* 搜索和筛选 */
.filter-section {
  max-width: 800px;
  margin: 0 auto;
}

.search-box {
  position: relative;
  margin-bottom: 1.5rem;
}

.search-icon {
  position: absolute;
  left: 1rem;
  top: 50%;
  transform: translateY(-50%);
  color: #8b949e;
}

.search-box input {
  width: 100%;
  padding: 0.75rem 1rem 0.75rem 3rem;
  background: #161b22;
  border: 1px solid #30363d;
  border-radius: 8px;
  color: #c9d1d9;
  font-size: 1rem;
  transition: all 0.3s ease;
}

.search-box input:focus {
  outline: none;
  border-color: #58a6ff;
  box-shadow: 0 0 0 3px rgba(88, 166, 255, 0.1);
}

.filter-tags {
  display: flex;
  flex-wrap: wrap;
  gap: 0.75rem;
  justify-content: center;
}

.tag-filter {
  padding: 0.5rem 1rem;
  background: #21262d;
  border: 1px solid #30363d;
  border-radius: 6px;
  color: #8b949e;
  font-size: 0.9rem;
  cursor: pointer;
  transition: all 0.3s ease;
}

.tag-filter:hover {
  background: #30363d;
  border-color: #58a6ff;
  color: #58a6ff;
}

.tag-filter.active {
  background: #58a6ff;
  border-color: #58a6ff;
  color: #ffffff;
}

/* 统计卡片 */
.blog-stats {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
  gap: 1.5rem;
  max-width: 1200px;
  margin: 0 auto 3rem;
}

.stat-card {
  background: #161b22;
  border: 1px solid #30363d;
  border-radius: 12px;
  padding: 1.5rem;
  display: flex;
  align-items: center;
  gap: 1rem;
  transition: all 0.3s ease;
}

.stat-card:hover {
  transform: translateY(-2px);
  border-color: #58a6ff;
  box-shadow: 0 8px 24px rgba(88, 166, 255, 0.1);
}

.stat-icon {
  font-size: 2rem;
  width: 3rem;
  height: 3rem;
  display: flex;
  align-items: center;
  justify-content: center;
  background: #21262d;
  border-radius: 10px;
}

.stat-info {
  display: flex;
  flex-direction: column;
}

.stat-number {
  font-size: 1.8rem;
  font-weight: 700;
  color: #58a6ff;
  line-height: 1;
  margin-bottom: 0.25rem;
}

.stat-label {
  font-size: 0.9rem;
  color: #8b949e;
}

/* 加载和错误状态 */
.loading-container,
.error-container {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  padding: 4rem 2rem;
  text-align: center;
}

.loading-spinner {
  width: 48px;
  height: 48px;
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
  margin-bottom: 1rem;
}

.retry-button {
  margin-top: 1rem;
  padding: 0.75rem 1.5rem;
  background: #238636;
  border: none;
  border-radius: 6px;
  color: white;
  font-size: 1rem;
  cursor: pointer;
  transition: all 0.3s ease;
}

.retry-button:hover {
  background: #2ea043;
}

/* 文章列表 */
.blog-list {
  max-width: 1200px;
  margin: 0 auto;
}

.blog-grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(350px, 1fr));
  gap: 1.5rem;
}

/* 空状态 */
.empty-state {
  text-align: center;
  padding: 4rem 2rem;
  color: #8b949e;
}

.empty-state svg {
  margin-bottom: 1.5rem;
  opacity: 0.5;
}

.empty-state h3 {
  font-size: 1.5rem;
  margin: 0 0 0.5rem 0;
  color: #c9d1d9;
}

.empty-state p {
  font-size: 1rem;
  margin: 0;
}

/* 动画 */
.blog-list-enter-active,
.blog-list-leave-active {
  transition: all 0.3s ease;
}

.blog-list-enter-from {
  opacity: 0;
  transform: translateY(20px);
}

.blog-list-leave-to {
  opacity: 0;
  transform: scale(0.9);
}

/* 响应式设计 */
@media (max-width: 768px) {
  .blog-view {
    padding: 1rem;
  }

  .page-title {
    font-size: 2rem;
  }

  .blog-grid {
    grid-template-columns: 1fr;
  }

  .blog-stats {
    grid-template-columns: 1fr;
  }
}
</style>
