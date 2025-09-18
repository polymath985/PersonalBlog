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
          <a href="/posts" class="nav-link">文章</a>
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
            placeholder="搜索文章..." 
            class="search-input"
            v-model="searchQuery"
            @keyup.enter="handleSearch"
          />
        </div>
      </div>

      <!-- 右侧：用户操作 -->
      <div class="nav-right">
        <a href="/notifications" class="nav-icon" title="通知">
          <svg width="16" height="16" viewBox="0 0 16 16">
            <path fill="currentColor" d="M8 16a2 2 0 0 0 1.985-1.75c.017-.137-.097-.25-.235-.25h-3.5c-.138 0-.252.113-.235.25A2 2 0 0 0 8 16zM3 5a5 5 0 0 1 10 0v2.947c0 .05.015.098.042.139l1.703 2.555A.822.822 0 0 1 14.053 12H1.947a.822.822 0 0 1-.692-1.359l1.703-2.555A.265.265 0 0 0 3 7.947V5z"/>
          </svg>
        </a>
        
        <a href="/create" class="nav-icon" title="创建">
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
                <strong>用户名</strong>
                <div class="user-email">user@example.com</div>
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

// 搜索功能
const handleSearch = () => {
  if (searchQuery.value.trim()) {
    // 这里可以实现搜索逻辑，比如路由跳转或API调用
    console.log('搜索:', searchQuery.value)
    // 示例：跳转到搜索结果页
    // router.push(`/search?q=${encodeURIComponent(searchQuery.value)}`)
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

