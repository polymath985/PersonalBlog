<template>
  <div class="login-page">
    <LoginComponent 
      @sign-in="handleSignIn"
      @sign-up="handleSignUp" 
      @social-login="handleSocialLogin"
      @forgot-password="handleForgotPassword"
    />
  </div>
</template>

<script setup lang="ts">
import { useRouter } from 'vue-router'
import LoginComponent from '@/components/LoginComponent.vue'

const router = useRouter()

// 登录相关事件处理
const handleSignIn = (data: { email: string; password: string }) => {
  console.log('Sign in attempt:', data)
  
  // 登录成功后保存状态到 localStorage
  localStorage.setItem('isLoggedIn', 'true')
  localStorage.setItem('userEmail', data.email)
  
  // 路由跳转到主页
  router.push('/home')
  
  console.log('登录成功，跳转到主页')
}

const handleSignUp = (data: { name: string; email: string; password: string }) => {
  console.log('Sign up attempt:', data)
  
  // 注册成功后可以选择是否自动登录
  // 这里我们暂时只显示消息，让用户手动登录
  console.log('注册成功，请登录')
}

const handleSocialLogin = (provider: string) => {
  console.log('Social login:', provider)
  // 这里可以处理第三方登录逻辑
}

const handleForgotPassword = () => {
  console.log('Forgot password clicked')
  // 这里可以跳转到重置密码页面
}
</script>

<style scoped>
.login-page {
  min-height: 100vh;
  height: 100vh;
  width: 100vw;
  display: flex;
  align-items: center;
  justify-content: center;
  padding: 0;
  margin: 0;
  overflow: hidden;
  position: relative;
}
</style>
