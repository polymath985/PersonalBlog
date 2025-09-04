<template>
  <div class="home-view">
    <!-- 顶部导航栏 -->
    <nav class="top-nav">
      <div class="nav-content">
        <h2>欢迎，{{ userEmail }}</h2>
        <button class="logout-button" @click="handleLogout">
          退出登录
        </button>
      </div>
    </nav>

    <!-- 主要内容区域 -->
    <main class="main-content">
      <header class="welcome-header">
        <img alt="Vue logo" class="logo" src="@/assets/logo.svg" width="125" height="125" />
        <div class="wrapper">
          <HelloWorld msg="欢迎来到主页!" />
        </div>
      </header>

      <section class="welcome-section">
        <TheWelcome />
      </section>
    </main>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import HelloWorld from '@/components/HelloWorld.vue'
import TheWelcome from '@/components/TheWelcome.vue'

const router = useRouter()
const userEmail = ref('')

onMounted(() => {
  // 从本地存储获取用户信息
  userEmail.value = localStorage.getItem('userEmail') || '用户'
})

const handleLogout = () => {
  // 清除登录状态
  localStorage.removeItem('isLoggedIn')
  localStorage.removeItem('userEmail')
  localStorage.removeItem('userName')
  
  // 路由跳转到登录页
  router.push('/login')
}
</script>

<style scoped>
.home-view {
  min-height: 100vh;
  background: var(--color-background);
}

.top-nav {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  background: rgba(255, 255, 255, 0.95);
  backdrop-filter: blur(10px);
  border-bottom: 1px solid var(--color-border);
  z-index: 1000;
  padding: 0 2rem;
}

.nav-content {
  display: flex;
  justify-content: space-between;
  align-items: center;
  max-width: 1200px;
  margin: 0 auto;
  padding: 1rem 0;
}

.nav-content h2 {
  color: var(--color-heading);
  font-size: 1.2rem;
  font-weight: 600;
}

.logout-button {
  background: linear-gradient(135deg, #f44336 0%, #d32f2f 100%);
  color: white;
  border: none;
  padding: 8px 20px;
  border-radius: 20px;
  font-size: 14px;
  font-weight: 600;
  cursor: pointer;
  box-shadow: 0 2px 10px rgba(244, 67, 54, 0.3);
  transition: all 0.3s ease;
}

.logout-button:hover {
  background: linear-gradient(135deg, #e53935 0%, #c62828 100%);
  transform: translateY(-1px);
  box-shadow: 0 4px 15px rgba(244, 67, 54, 0.4);
}

.main-content {
  padding-top: 80px;
  max-width: 1200px;
  margin: 0 auto;
  padding-left: 2rem;
  padding-right: 2rem;
}

.welcome-header {
  text-align: center;
  margin-bottom: 3rem;
  padding: 2rem 0;
}

.logo {
  margin-bottom: 2rem;
}

.welcome-section {
  margin-top: 2rem;
}

/* 响应式设计 */
@media (max-width: 768px) {
  .nav-content {
    padding: 0.8rem 0;
  }
  
  .nav-content h2 {
    font-size: 1rem;
  }
  
  .logout-button {
    padding: 6px 16px;
    font-size: 12px;
  }
  
  .main-content {
    padding-top: 70px;
    padding-left: 1rem;
    padding-right: 1rem;
  }
}
</style>
