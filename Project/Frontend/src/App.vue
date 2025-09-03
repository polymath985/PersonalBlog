<template>
  <!-- 导航栏 -->
  <nav v-if="currentPage === 'home'" class="navigation">
    <button @click="showLogin" class="nav-button">Go to Login</button>
  </nav>

  <!-- 登录页面 -->
  <div v-if="currentPage === 'login'" class="login-page">
    <button @click="showHome" class="back-button">← Back to Home</button>
    <LoginComponent 
      @sign-in="handleSignIn"
      @sign-up="handleSignUp" 
      @social-login="handleSocialLogin"
      @forgot-password="handleForgotPassword"
    />
  </div>

  <!-- 主页 -->
  <div v-else class="home-page">
    <header>
      <img alt="Vue logo" class="logo" src="./assets/logo.svg" width="125" height="125" />

      <div class="wrapper">
        <HelloWorld msg="You did it!" />
      </div>
    </header>

     <TheWelcome />
    <main>
     
    </main>  
      <!-- 🆕 使用新创建的示例组件 -->
      <!-- 演示 props 传递和事件监听 -->
      <!-- <ExampleComponent 
        title="我的第一个自定义组件"
        description="这个组件演示了 Vue.js 组件的基本功能"
        :is-visible="true"
        @click="handleExampleClick"
        @toggle="handleExampleToggle"
      /> -->

  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import HelloWorld from './components/HelloWorld.vue'
import TheWelcome from './components/TheWelcome.vue'
import LoginComponent from './components/LoginComponent.vue'
// 🆕 导入新创建的示例组件
import ExampleComponent from './components/ExampleComponent.vue'

// 当前页面状态
const currentPage = ref<'home' | 'login'>('home')

// 处理子组件事件的方法
const handleExampleClick = (count: number) => {
  console.log(`示例组件被点击了，当前次数: ${count}`)
}

const handleExampleToggle = (visible: boolean) => {
  console.log(`详情${visible ? '显示' : '隐藏'}了`)
}

// 登录相关事件处理
const handleSignIn = (data: { email: string; password: string }) => {
  console.log('Sign in attempt:', data)
  // 这里可以调用API进行登录验证
  alert(`Attempting to sign in with: ${data.email}`)
}

const handleSignUp = (data: { name: string; email: string; password: string }) => {
  console.log('Sign up attempt:', data)
  // 这里可以调用API进行注册
  alert(`Attempting to sign up: ${data.name} (${data.email})`)
}

const handleSocialLogin = (provider: string) => {
  console.log('Social login:', provider)
  alert(`${provider} login clicked`)
}

const handleForgotPassword = () => {
  console.log('Forgot password clicked')
  alert('Forgot password feature - redirect to reset page')
}

// 页面切换
const showLogin = () => {
  currentPage.value = 'login'
}

const showHome = () => {
  currentPage.value = 'home'
}
</script>

<style scoped>
/* 导航样式 */
.navigation {
  position: fixed;
  top: 20px;
  right: 20px;
  z-index: 1000;
}

.nav-button {
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  color: white;
  border: none;
  padding: 12px 24px;
  border-radius: 25px;
  font-size: 14px;
  font-weight: 600;
  cursor: pointer;
  box-shadow: 0 4px 15px rgba(0, 0, 0, 0.2);
  transition: all 0.3s ease;
}

.nav-button:hover {
  transform: translateY(-2px);
  box-shadow: 0 6px 20px rgba(0, 0, 0, 0.3);
}

/* 返回按钮样式 */
.back-button {
  position: fixed;
  top: 20px;
  left: 20px;
  z-index: 1001;
  background: rgba(255, 255, 255, 0.9);
  color: #333;
  border: 2px solid #ddd;
  padding: 10px 20px;
  border-radius: 20px;
  font-size: 14px;
  font-weight: 500;
  cursor: pointer;
  backdrop-filter: blur(10px);
  transition: all 0.3s ease;
}

.back-button:hover {
  background: white;
  border-color: #999;
  transform: translateY(-1px);
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
}

/* 页面容器 */
.login-page {
  min-height: 100vh;
}

.home-page {
  min-height: 100vh;
}

/* 原有样式保持不变 */
header {
  line-height: 1.5;
}

.logo {
  display: block;
  margin: 0 auto 2rem;
}

@media (min-width: 1024px) {
  header {
    display: flex;
    place-items: center;
    padding-right: calc(var(--section-gap) / 2);
  }

  .logo {
    margin: 0 2rem 0 0;
  }

  header .wrapper {
    display: flex;
    place-items: flex-start;
    flex-wrap: wrap;
  }
}
</style>
