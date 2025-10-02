import { createRouter, createWebHistory } from 'vue-router'
import LoginView from '@/views/LoginView.vue'
import HomeView from '@/views/HomeView.vue'
import BlogView from '@/views/BlogView.vue'
import BlogDetailView from '@/views/BlogDetailView.vue'
import BlogCreateView from '@/views/BlogCreateView.vue'
import Logout from '@/components/Logout.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      redirect: '/login'
    },
    {
      path: '/login',
      name: 'login',
      component: LoginView
    },
    {
      path: '/home',
      name: 'home',
      component: HomeView,
      meta: { requiresAuth: true }
    },
    {
      path: '/blogs',
      name: 'blogs',
      component: BlogView,
      meta: { requiresAuth: true }
    },
    {
      path: '/blog/create',
      name: 'blogCreate',
      component: BlogCreateView,
      meta: { requiresAuth: true }
    },
    {
      path: '/blog/:id',
      name: 'blogDetail',
      component: BlogDetailView,
      meta: { requiresAuth: true }
    },
    {
      path: '/logout',
      component: Logout
    }
  ]
})

// 路由守卫 - 检查是否需要登录
router.beforeEach(async (to, from, next) => {
  // 动态导入store，确保Pinia已经初始化
  const { useAuthStore } = await import('@/stores/auth')
  const authStore = useAuthStore()
  
  const isLoggedIn = authStore.isLoggedIn
  
  if (to.meta.requiresAuth && !isLoggedIn) {
    // 需要登录但未登录，重定向到登录页
    next('/login')
  } else if (to.name === 'login' && isLoggedIn) {
    // 已登录但访问登录页，重定向到首页
    next('/home')
  } else {
    next()
  }
})

export default router
