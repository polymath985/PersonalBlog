<template>
  <nav class="top-navigation">
    <div class="nav-container">
      <!-- 左侧：Logo和主导航 -->
      <div class="nav-left">
        <a href="/" class="logo">
          <svg height="32" viewBox="0 0 16 16" width="32" class="logo-icon">
            <path fill="currentColor" d="M8 0C3.58 0 0 3.58 0 8c0 3.54 2.29 6.53 5.47 7.59.4.07.55-.17.55-.38 0-.19-.01-.82-.01-1.49-2.01.37-2.53-.49-2.69-.94-.09-.23-.48-.94-.82-1.13-.28-.15-.68-.52-.01-.53.63-.01 1.08.58 1.23.82.72 1.21 1.87.87 2.33.66.07-.52.28-.87.51-1.07-1.78-.2-3.64-.89-3.64-3.95 0-.87.31-1.59.82-2.15-.08-.2-.36-1.02.08-2.12 0 0 .67-.21 2.2.82.64-.18 1.32-.27 2-.27.68 0 1.36.09 2 .27 1.53-1.04 2.2-.82 2.2-.82.44 1.1.16 1.92.08 2.12.51.56.82 1.27.82 2.15 0 3.07-1.87 3.75-3.65 3.95.29.25.54.73.54 1.48 0 1.07-.01 1.93-.01 2.2 0 .21.15.46.55.38A8.013 8.013 0 0016 8c0-4.42-3.58-8-8-8z"/>
          </svg>
          <span class="logo-text">PersonalBlog</span>
        </a>
        
        <div class="nav-links">
          <a href="/" class="nav-link">首页</a>
          <a href="/blogs" class="nav-link">文章</a>
          <a href="/projects" class="nav-link">项目</a>
          <a href="/about" class="nav-link">关于</a>
        </div>
      </div>

      <!-- 中间：搜索框 -->
      <div class="nav-center">
        <div class="search-container">
          <svg class="search-icon" width="16" height="16" viewBox="0 0 16 16">
            <path fill="currentColor" d="M11.742 10.344a6.5 6.5 0 1 0-1.397 1.398h-.001c.03.04.062.078.098.115l3.85 3.85a1 1 0 0 0 1.415-1.414l-3.85-3.85a1.007 1.007 0 0 0-.115-.1zM12 6.5a5.5 5.5 0 1 1-11 0 5.5 5.5 0 0 1 11 0z"/>
          </svg>
          <input 
            type="text" 
            placeholder="搜索文章、@用户、#标签..." 
            class="search-input"
            v-model="searchQuery"
            @input="handleSearchInput"
            @keyup.enter="handleSearch"
            @keydown.down.prevent="navigateSuggestions(1)"
            @keydown.up.prevent="navigateSuggestions(-1)"
            @blur="handleSearchBlur"
            @focus="handleSearchFocus"
            title="提示: 搜索用户使用 @用户名, 搜索标签使用 #标签名"
          />
          
          <!-- 搜索建议下拉列表 -->
          <div v-if="showSuggestions && searchSuggestions.length > 0" class="search-suggestions">
            <!-- 用户建议 -->
            <template v-if="currentSearchType === 'user'">
              <div class="suggestions-header">
                <svg width="12" height="12" viewBox="0 0 16 16">
                  <path fill="currentColor" d="M10.561 8.073a6.005 6.005 0 0 1 3.432 5.142.75.75 0 1 1-1.498.07 4.5 4.5 0 0 0-8.99 0 .75.75 0 0 1-1.498-.07 6.004 6.004 0 0 1 3.431-5.142 3.999 3.999 0 1 1 5.123 0zM10.5 5a2.5 2.5 0 1 0-5 0 2.5 2.5 0 0 0 5 0z"/>
                </svg>
                <span>用户建议</span>
              </div>
              <div 
                v-for="(user, index) in searchSuggestions" 
                :key="user.id"
                :class="['suggestion-item', { active: selectedSuggestionIndex === index }]"
                @mousedown.prevent="selectUser(user)"
                @mouseenter="selectedSuggestionIndex = index"
              >
                <img 
                  :src="user.avatar || '/default-avatar.png'" 
                  :alt="user.name"
                  class="suggestion-avatar"
                />
                <div class="suggestion-info">
                  <div class="suggestion-name">{{ user.name }}</div>
                  <div v-if="user.bio" class="suggestion-bio">{{ user.bio }}</div>
                </div>
              </div>
            </template>

            <!-- 标签建议 -->
            <template v-else-if="currentSearchType === 'tag'">
              <div class="suggestions-header">
                <svg width="12" height="12" viewBox="0 0 16 16">
                  <path fill="currentColor" d="M1 7.775V2.75C1 1.784 1.784 1 2.75 1h5.025c.464 0 .91.184 1.238.513l6.25 6.25a1.75 1.75 0 0 1 0 2.474l-5.026 5.026a1.75 1.75 0 0 1-2.474 0l-6.25-6.25A1.752 1.752 0 0 1 1 7.775Zm1.5 0c0 .066.026.13.073.177l6.25 6.25a.25.25 0 0 0 .354 0l5.025-5.025a.25.25 0 0 0 0-.354l-6.25-6.25a.25.25 0 0 0-.177-.073H2.75a.25.25 0 0 0-.25.25ZM6 5a1 1 0 1 1 0 2 1 1 0 0 1 0-2Z"/>
                </svg>
                <span>标签建议</span>
              </div>
              <div 
                v-for="(tag, index) in searchSuggestions" 
                :key="tag"
                :class="['suggestion-item', 'tag-item', { active: selectedSuggestionIndex === index }]"
                @mousedown.prevent="selectTag(tag)"
                @mouseenter="selectedSuggestionIndex = index"
              >
                <div class="tag-icon">#</div>
                <div class="suggestion-info">
                  <div class="suggestion-name">{{ tag }}</div>
                </div>
              </div>
            </template>

            <!-- 博客建议 -->
            <template v-else-if="currentSearchType === 'blog'">
              <div class="suggestions-header">
                <svg width="12" height="12" viewBox="0 0 16 16">
                  <path fill="currentColor" d="M0 1.75A.75.75 0 0 1 .75 1h4.253c1.227 0 2.317.59 3 1.501A3.743 3.743 0 0 1 11.006 1h4.245a.75.75 0 0 1 .75.75v10.5a.75.75 0 0 1-.75.75h-4.507a2.25 2.25 0 0 0-1.591.659l-.622.621a.75.75 0 0 1-1.06 0l-.622-.621A2.25 2.25 0 0 0 5.258 13H.75a.75.75 0 0 1-.75-.75V1.75Z"/>
                </svg>
                <span>文章建议</span>
              </div>
              <div 
                v-for="(blog, index) in searchSuggestions" 
                :key="blog.id"
                :class="['suggestion-item', { active: selectedSuggestionIndex === index }]"
                @mousedown.prevent="selectBlog(blog)"
                @mouseenter="selectedSuggestionIndex = index"
              >
                <svg class="blog-icon" width="20" height="20" viewBox="0 0 16 16">
                  <path fill="currentColor" d="M0 1.75A.75.75 0 0 1 .75 1h4.253c1.227 0 2.317.59 3 1.501A3.743 3.743 0 0 1 11.006 1h4.245a.75.75 0 0 1 .75.75v10.5a.75.75 0 0 1-.75.75h-4.507a2.25 2.25 0 0 0-1.591.659l-.622.621a.75.75 0 0 1-1.06 0l-.622-.621A2.25 2.25 0 0 0 5.258 13H.75a.75.75 0 0 1-.75-.75V1.75Z"/>
                </svg>
                <div class="suggestion-info">
                  <div class="suggestion-name">{{ blog.title }}</div>
                  <div class="suggestion-meta">
                    <span class="author">{{ blog.authorName }}</span>
                    <span class="views">👁️ {{ blog.views }}</span>
                  </div>
                </div>
              </div>
            </template>
          </div>
          
          <!-- 无结果提示 -->
          <div v-if="showSuggestions && searchSuggestions.length === 0 && searchQuery.trim()" class="search-no-results">
            <svg width="16" height="16" viewBox="0 0 16 16">
              <path fill="currentColor" d="M3.72 3.72a.75.75 0 0 1 1.06 0L8 6.94l3.22-3.22a.749.749 0 0 1 1.275.326.749.749 0 0 1-.215.734L9.06 8l3.22 3.22a.749.749 0 0 1-.326 1.275.749.749 0 0 1-.734-.215L8 9.06l-3.22 3.22a.751.751 0 0 1-1.042-.018.751.751 0 0 1-.018-1.042L6.94 8 3.72 4.78a.75.75 0 0 1 0-1.06Z"/>
            </svg>
            <span>{{ getNoResultsMessage() }}</span>
          </div>
        </div>
      </div>

      <!-- 右侧：用户操作 -->
      <div class="nav-right">
        <a href="/notifications" class="nav-icon" title="通知">
          <svg width="16" height="16" viewBox="0 0 16 16">
            <path fill="currentColor" d="M8 16a2 2 0 0 0 1.985-1.75c.017-.137-.097-.25-.235-.25h-3.5c-.138 0-.252.113-.235.25A2 2 0 0 0 8 16zM3 5a5 5 0 0 1 10 0v2.947c0 .05.015.098.042.139l1.703 2.555A.822.822 0 0 1 14.053 12H1.947a.822.822 0 0 1-.692-1.359l1.703-2.555A.265.265 0 0 0 3 7.947V5z"/>
          </svg>
        </a>
        
        <a href="/blog/create" class="nav-icon" title="创建">
          <svg width="16" height="16" viewBox="0 0 16 16">
            <path fill="currentColor" d="M7.75 2a.75.75 0 0 1 .75.75V7h4.25a.75.75 0 0 1 0 1.5H8.5v4.25a.75.75 0 0 1-1.5 0V8.5H2.75a.75.75 0 0 1 0-1.5H7V2.75A.75.75 0 0 1 7.75 2z"/>
          </svg>
        </a>

        <div class="user-menu">
          <button class="user-avatar" @click="toggleUserMenu">
            <svg class="avatar-icon" width="20" height="20" viewBox="0 0 16 16">
              <path fill="currentColor" d="M10.561 8.073a6.005 6.005 0 0 1 3.432 5.142.75.75 0 1 1-1.498.07 4.5 4.5 0 0 0-8.99 0 .75.75 0 0 1-1.498-.07 6.004 6.004 0 0 1 3.431-5.142 3.999 3.999 0 1 1 5.123 0zM10.5 5a2.5 2.5 0 1 0-5 0 2.5 2.5 0 0 0 5 0z"/>
            </svg>
          </button>
          
          <div v-if="showUserMenu" class="user-dropdown">
            <div class="dropdown-header">
              <div class="user-info">
                <strong>用户名:{{ userName }}</strong>
                <div class="user-email">{{ userEmail }}</div>
              </div>
            </div>
            <div class="dropdown-divider"></div>
            <a href="/profile" class="dropdown-item">个人资料</a>
            <a href="/settings" class="dropdown-item">设置</a>
            <div class="dropdown-divider"></div>
            <a href="/logout" class="dropdown-item">退出登录</a>
          </div>
        </div>
      </div>
    </div>
  </nav>
</template>

<script setup lang="ts">
import { ref, onMounted, onUnmounted } from 'vue'
import router from '@/router'


// 响应式数据
const searchQuery = ref('')
const showUserMenu = ref(false)
const showSuggestions = ref(false)
const searchSuggestions = ref<any[]>([])
const selectedSuggestionIndex = ref(-1)
const searchTimeout = ref<number | null>(null)
const currentSearchType = ref<'user' | 'tag' | 'blog' | null>(null)

//获取localStorage中的用户信息
const userName = localStorage.getItem('userName') || '用户名'
const userEmail = localStorage.getItem('userEmail') || '用户邮箱'

// 处理搜索输入变化
const handleSearchInput = () => {
  const query = searchQuery.value.trim()
  
  // 清除之前的定时器
  if (searchTimeout.value) {
    clearTimeout(searchTimeout.value)
  }
  
  if (query.length === 0) {
    // 输入为空,隐藏建议
    showSuggestions.value = false
    searchSuggestions.value = []
    currentSearchType.value = null
    return
  }
  
  // 根据输入判断搜索类型
  if (query.startsWith('@')) {
    // 用户搜索
    const username = query.substring(1).trim()
    currentSearchType.value = 'user'
    
    if (username.length > 0) {
      searchTimeout.value = window.setTimeout(() => {
        fetchUserSuggestions(username)
      }, 300)
    } else {
      searchSuggestions.value = []
      showSuggestions.value = true
    }
  } else if (query.startsWith('#')) {
    // 标签搜索
    const tag = query.substring(1).trim()
    currentSearchType.value = 'tag'
    
    if (tag.length > 0) {
      searchTimeout.value = window.setTimeout(() => {
        fetchTagSuggestions(tag)
      }, 300)
    } else {
      searchSuggestions.value = []
      showSuggestions.value = true
    }
  } else {
    // 博客搜索
    currentSearchType.value = 'blog'
    
    searchTimeout.value = window.setTimeout(() => {
      fetchBlogSuggestions(query)
    }, 300)
  }
}

// 获取用户搜索建议
const fetchUserSuggestions = async (username: string) => {
  try {
    const response = await fetch(`/api/UserData/search?name=${encodeURIComponent(username)}&limit=5`)
    
    if (response.ok) {
      const users = await response.json()
      searchSuggestions.value = users
      showSuggestions.value = true
      selectedSuggestionIndex.value = -1
    } else {
      searchSuggestions.value = []
      showSuggestions.value = true
    }
  } catch (error) {
    console.error('获取用户建议失败:', error)
    searchSuggestions.value = []
    showSuggestions.value = false
  }
}

// 获取博客搜索建议
const fetchBlogSuggestions = async (query: string) => {
  try {
    const response = await fetch(`/api/Blog/search/suggestions?query=${encodeURIComponent(query)}&limit=5`)
    
    if (response.ok) {
      const blogs = await response.json()
      searchSuggestions.value = blogs
      showSuggestions.value = true
      selectedSuggestionIndex.value = -1
    } else {
      searchSuggestions.value = []
      showSuggestions.value = true
    }
  } catch (error) {
    console.error('获取博客建议失败:', error)
    searchSuggestions.value = []
    showSuggestions.value = false
  }
}

// 获取标签搜索建议
const fetchTagSuggestions = async (tag: string) => {
  try {
    const response = await fetch(`/api/Blog/tags/suggestions?query=${encodeURIComponent(tag)}&limit=5`)
    
    if (response.ok) {
      const tags = await response.json()
      searchSuggestions.value = tags
      showSuggestions.value = true
      selectedSuggestionIndex.value = -1
    } else {
      searchSuggestions.value = []
      showSuggestions.value = true
    }
  } catch (error) {
    console.error('获取标签建议失败:', error)
    searchSuggestions.value = []
    showSuggestions.value = false
  }
}

// 键盘导航搜索建议
const navigateSuggestions = (direction: number) => {
  if (searchSuggestions.value.length === 0) return
  
  selectedSuggestionIndex.value += direction
  
  // 循环导航
  if (selectedSuggestionIndex.value < 0) {
    selectedSuggestionIndex.value = searchSuggestions.value.length - 1
  } else if (selectedSuggestionIndex.value >= searchSuggestions.value.length) {
    selectedSuggestionIndex.value = 0
  }
}

// 选择用户
const selectUser = (user: any) => {
  router.push(`/profile/${user.id}`)
  searchQuery.value = ''
  showSuggestions.value = false
  searchSuggestions.value = []
  selectedSuggestionIndex.value = -1
  currentSearchType.value = null
}

// 选择博客
const selectBlog = (blog: any) => {
  router.push(`/blog/${blog.id}`)
  searchQuery.value = ''
  showSuggestions.value = false
  searchSuggestions.value = []
  selectedSuggestionIndex.value = -1
  currentSearchType.value = null
}

// 选择标签
const selectTag = (tag: string) => {
  router.push(`/blogs?tag=${encodeURIComponent(tag)}`)
  searchQuery.value = ''
  showSuggestions.value = false
  searchSuggestions.value = []
  selectedSuggestionIndex.value = -1
  currentSearchType.value = null
}

// 获取无结果提示消息
const getNoResultsMessage = () => {
  if (currentSearchType.value === 'user') {
    return '未找到匹配的用户'
  } else if (currentSearchType.value === 'tag') {
    return '未找到匹配的标签'
  } else if (currentSearchType.value === 'blog') {
    return '未找到匹配的文章'
  }
  return '未找到结果'
}

// 搜索框失去焦点
const handleSearchBlur = () => {
  // 延迟隐藏,以便点击建议项能够触发
  setTimeout(() => {
    showSuggestions.value = false
  }, 200)
}

// 搜索框获得焦点
const handleSearchFocus = () => {
  const query = searchQuery.value.trim()
  // 如果已有搜索内容,重新显示建议
  if (query.length > 0 && searchSuggestions.value.length > 0) {
    showSuggestions.value = true
  }
}

// 搜索功能
const handleSearch = async () => {
  const query = searchQuery.value.trim()
  if (!query) return

  // 如果正在显示建议列表且有选中项,根据类型选择对应的项
  if (showSuggestions.value && selectedSuggestionIndex.value >= 0 && searchSuggestions.value.length > 0) {
    const selectedItem = searchSuggestions.value[selectedSuggestionIndex.value]
    
    if (currentSearchType.value === 'user') {
      selectUser(selectedItem)
    } else if (currentSearchType.value === 'blog') {
      selectBlog(selectedItem)
    } else if (currentSearchType.value === 'tag') {
      selectTag(selectedItem)
    }
    return
  }

  // 隐藏建议列表
  showSuggestions.value = false

  // 根据搜索内容判断搜索类型
  const searchType = detectSearchType(query)
  
  if (searchType === 'user') {
    // 搜索用户 (格式: @username 或 user:username)
    const username = query.replace(/^(@|user:)/i, '').trim()
    await searchUser(username)
  } else if (searchType === 'tag') {
    // 搜索标签 (格式: #tag 或 tag:tagname)
    const tag = query.replace(/^(#|tag:)/i, '').trim()
    router.push({
      path: '/blogs',
      query: { tag: tag }
    })
    searchQuery.value = '' // 清空搜索框
  } else {
    // 默认搜索博客标题和内容
    router.push({
      path: '/blogs',
      query: { search: query }
    })
    searchQuery.value = '' // 清空搜索框
  }
}

// 检测搜索类型
const detectSearchType = (query: string): 'user' | 'tag' | 'blog' => {
  if (query.startsWith('@') || query.toLowerCase().startsWith('user:')) {
    return 'user'
  }
  if (query.startsWith('#') || query.toLowerCase().startsWith('tag:')) {
    return 'tag'
  }
  return 'blog'
}

// 搜索用户并跳转到用户主页
const searchUser = async (username: string) => {
  try {
    // 调用后端API搜索用户
    const response = await fetch(`/api/UserData/search?name=${encodeURIComponent(username)}`)
    
    if (response.ok) {
      const users = await response.json()
      
      if (users && users.length > 0) {
        // 如果只找到一个用户,直接跳转到该用户的主页
        if (users.length === 1) {
          router.push(`/profile/${users[0].id}`)
        } else {
          // 如果找到多个用户,跳转到第一个,但也可以在博客页面按作者名筛选
          router.push(`/profile/${users[0].id}`)
        }
        searchQuery.value = '' // 清空搜索框
      } else {
        // 如果没找到用户，跳转到博客页面并搜索该用户名
        // 这样可以搜索到包含该用户名的博客标题或内容
        console.log(`未找到用户 "${username}",在博客中搜索...`)
        router.push({
          path: '/blogs',
          query: { search: username }
        })
        searchQuery.value = ''
      }
    } else {
      // API调用失败，回退到博客搜索
      console.error('搜索用户API失败')
      router.push({
        path: '/blogs',
        query: { search: username }
      })
      searchQuery.value = ''
    }
  } catch (error) {
    console.error('搜索用户失败:', error)
    // 出错时回退到博客搜索
    router.push({
      path: '/blogs',
      query: { search: username }
    })
    searchQuery.value = ''
  }
}

// 用户菜单切换
const toggleUserMenu = () => {
  showUserMenu.value = !showUserMenu.value
}

// 点击外部关闭用户菜单
const handleClickOutside = (event: Event) => {
  const target = event.target as Element
  if (!target.closest('.user-menu')) {
    showUserMenu.value = false
  }
}

onMounted(() => {
  document.addEventListener('click', handleClickOutside)
})

onUnmounted(() => {
  document.removeEventListener('click', handleClickOutside)
})
</script>

<style scoped>
.top-navigation {
  background-color: #24292f;
  border-bottom: 1px solid #30363d;
  position: sticky;
  top: 0;
  z-index: 100;
  height: 64px;
}

.nav-container {
  max-width: 1280px;
  margin: 0 auto;
  padding: 0 16px;
  height: 100%;
  display: flex;
  align-items: center;
  justify-content: space-between;
}

/* 左侧区域 */
.nav-left {
  display: flex;
  align-items: center;
  gap: 16px;
}

.logo {
  display: flex;
  align-items: center;
  gap: 8px;
  text-decoration: none;
  color: #f0f6fc;
  font-weight: 600;
  font-size: 18px;
}

.logo-icon {
  color: #f0f6fc;
}

.logo-text {
  white-space: nowrap;
}

.nav-links {
  display: flex;
  align-items: center;
  gap: 16px;
  margin-left: 16px;
}

.nav-link {
  color: #f0f6fc;
  text-decoration: none;
  padding: 8px 12px;
  border-radius: 6px;
  font-weight: 500;
  transition: background-color 0.2s ease;
}

.nav-link:hover {
  background-color: #30363d;
}

/* 中间搜索区域 */
.nav-center {
  flex: 1;
  max-width: 400px;
  margin: 0 24px;
}

.search-container {
  position: relative;
  width: 100%;
  /* 确保容器不会因为绝对定位的子元素而改变大小 */
  min-height: 40px;
}

.search-icon {
  position: absolute;
  left: 12px;
  top: 50%;
  transform: translateY(-50%);
  color: #7d8590;
  pointer-events: none;
}

.search-input {
  width: 100%;
  background-color: #21262d;
  border: 1px solid #30363d;
  border-radius: 6px;
  padding: 8px 12px 8px 40px;
  color: #f0f6fc;
  font-size: 14px;
  transition: all 0.2s ease;
}

.search-input::placeholder {
  color: #7d8590;
}

.search-input:focus {
  outline: none;
  border-color: #1f6feb;
  box-shadow: 0 0 0 3px rgba(31, 111, 235, 0.3);
  background-color: #0d1117;
}

/* 搜索建议下拉列表 */
.search-suggestions {
  position: absolute;
  top: calc(100% + 8px);
  left: 0;
  right: 0;
  background-color: #21262d;
  border: 1px solid #30363d;
  border-radius: 8px;
  box-shadow: 0 8px 24px rgba(0, 0, 0, 0.3);
  z-index: 1000;
  max-height: 320px;
  overflow-y: auto;
  animation: slideDown 0.2s ease;
}

@keyframes slideDown {
  from {
    opacity: 0;
    transform: translateY(-10px);
  }
  to {
    opacity: 1;
    transform: translateY(0);
  }
}

.suggestions-header {
  padding: 8px 12px;
  border-bottom: 1px solid #30363d;
  display: flex;
  align-items: center;
  gap: 6px;
  color: #7d8590;
  font-size: 12px;
  font-weight: 600;
  text-transform: uppercase;
}

.suggestions-header svg {
  opacity: 0.7;
}

.suggestion-item {
  padding: 10px 12px;
  display: flex;
  align-items: center;
  gap: 12px;
  cursor: pointer;
  transition: all 0.2s ease;
  border-bottom: 1px solid #30363d;
}

.suggestion-item:last-child {
  border-bottom: none;
}

.suggestion-item:hover,
.suggestion-item.active {
  background-color: #30363d;
}

.suggestion-avatar {
  width: 32px;
  height: 32px;
  border-radius: 50%;
  object-fit: cover;
  border: 2px solid #30363d;
  flex-shrink: 0;
}

.suggestion-info {
  flex: 1;
  min-width: 0;
}

.suggestion-name {
  color: #f0f6fc;
  font-size: 14px;
  font-weight: 500;
  margin-bottom: 2px;
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
}

.suggestion-bio {
  color: #7d8590;
  font-size: 12px;
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
}

/* 标签建议样式 */
.suggestion-item.tag-item {
  gap: 8px;
}

.tag-icon {
  width: 32px;
  height: 32px;
  display: flex;
  align-items: center;
  justify-content: center;
  background: linear-gradient(135deg, #58a6ff 0%, #1f6feb 100%);
  color: white;
  font-size: 18px;
  font-weight: 600;
  border-radius: 6px;
  flex-shrink: 0;
}

/* 博客建议样式 */
.blog-icon {
  flex-shrink: 0;
  color: #58a6ff;
  opacity: 0.8;
}

.suggestion-meta {
  display: flex;
  align-items: center;
  gap: 12px;
  color: #7d8590;
  font-size: 12px;
  margin-top: 4px;
}

.suggestion-meta .author {
  display: flex;
  align-items: center;
  gap: 4px;
}

.suggestion-meta .author::before {
  content: '👤';
  font-size: 11px;
}

.suggestion-meta .views {
  display: flex;
  align-items: center;
  gap: 4px;
}

/* 无结果提示 */
.search-no-results {
  position: absolute;
  top: calc(100% + 8px);
  left: 0;
  right: 0;
  padding: 16px 12px;
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 8px;
  color: #7d8590;
  font-size: 13px;
  background-color: #21262d;
  border: 1px solid #30363d;
  border-radius: 8px;
  box-shadow: 0 8px 24px rgba(0, 0, 0, 0.3);
  z-index: 1000;
  animation: slideDown 0.2s ease;
}

.search-no-results svg {
  opacity: 0.5;
}

/* 右侧区域 */
.nav-right {
  display: flex;
  align-items: center;
  gap: 16px;
}

.nav-icon {
  display: flex;
  align-items: center;
  justify-content: center;
  width: 32px;
  height: 32px;
  color: #f0f6fc;
  text-decoration: none;
  border-radius: 6px;
  transition: background-color 0.2s ease;
}

.nav-icon:hover {
  background-color: #30363d;
}

/* 用户菜单 */
.user-menu {
  position: relative;
}

.user-avatar {
  background: none;
  border: 1px solid #30363d;
  cursor: pointer;
  border-radius: 50%;
  padding: 6px;
  width: 32px;
  height: 32px;
  display: flex;
  align-items: center;
  justify-content: center;
  transition: all 0.2s ease;
  background-color: #21262d;
}

.user-avatar:hover {
  background-color: #30363d;
  border-color: #484f58;
}

.avatar-icon {
  color: #f0f6fc;
}

.user-dropdown {
  position: absolute;
  top: 100%;
  right: 0;
  margin-top: 8px;
  background-color: #21262d;
  border: 1px solid #30363d;
  border-radius: 8px;
  box-shadow: 0 8px 24px rgba(0, 0, 0, 0.3);
  min-width: 200px;
  z-index: 1000;
}

.dropdown-header {
  padding: 12px 16px;
}

.user-info strong {
  color: #f0f6fc;
  font-size: 14px;
  display: block;
}

.user-email {
  color: #7d8590;
  font-size: 12px;
  margin-top: 2px;
}

.dropdown-divider {
  height: 1px;
  background-color: #30363d;
  margin: 8px 0;
}

.dropdown-item {
  display: block;
  padding: 8px 16px;
  color: #f0f6fc;
  text-decoration: none;
  font-size: 14px;
  transition: background-color 0.2s ease;
}

.dropdown-item:hover {
  background-color: #30363d;
}

/* 响应式设计 */
@media (max-width: 768px) {
  .nav-container {
    padding: 0 12px;
  }
  
  .nav-links {
    display: none;
  }
  
  .nav-center {
    margin: 0 16px;
    max-width: none;
  }
  
  .search-input {
    font-size: 16px; /* 防止iOS缩放 */
  }
  
  .logo-text {
    display: none;
  }
}

@media (max-width: 480px) {
  .nav-center {
    display: none;
  }
  
  .nav-right {
    gap: 8px;
  }
}
</style>

