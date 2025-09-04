<template>
  <div class="login-container">
    <div class="container" :class="{ active: isActive }" id="container">
      <!-- 注册表单 -->
      <div class="form-container sign-up">
        <form @submit.prevent="handleSignUp">
          <h1 class="logintitle">Create Account</h1>
          <div class="social-icons">
            <a href="#" class="icon" @click.prevent="socialLogin('google')">
              <i class="fa-brands fa-google-plus-g"></i>
            </a>
            <a href="#" class="icon" @click.prevent="socialLogin('facebook')">
              <i class="fa-brands fa-facebook-f"></i>
            </a>
            <a href="#" class="icon" @click.prevent="socialLogin('github')">
              <i class="fa-brands fa-github"></i>
            </a>
            <a href="#" class="icon" @click.prevent="socialLogin('linkedin')">
              <i class="fa-brands fa-linkedin-in"></i>
            </a>
          </div>
          <span>or use your email for registration</span>
          <input 
            type="text" 
            placeholder="Name" 
            v-model="signUpForm.name"
            required 
          />
          <input 
            type="email" 
            placeholder="Email" 
            v-model="signUpForm.email"
            required 
          />
          <input 
            type="password" 
            placeholder="Password" 
            v-model="signUpForm.password"
            required 
          />
          <button type="submit" :disabled="isLoading">
            {{ isLoading ? 'Creating...' : 'Sign Up' }}
          </button>
        </form>
      </div>

      <!-- 登录表单 -->
      <div class="form-container sign-in">
        <form @submit.prevent="handleSignIn">
          <h1 class="logintitle">Sign In</h1>
          <div class="social-icons">
            <a href="#" class="icon" @click.prevent="socialLogin('google')">
              <i class="fa-brands fa-google-plus-g"></i>
            </a>
            <a href="#" class="icon" @click.prevent="socialLogin('facebook')">
              <i class="fa-brands fa-facebook-f"></i>
            </a>
            <a href="#" class="icon" @click.prevent="socialLogin('github')">
              <i class="fa-brands fa-github"></i>
            </a>
            <a href="#" class="icon" @click.prevent="socialLogin('linkedin')">
              <i class="fa-brands fa-linkedin-in"></i>
            </a>
          </div>
          <span>or use your email password</span>
          <input 
            type="email" 
            placeholder="Email" 
            v-model="signInForm.email"
            required 
          />
          <input 
            type="password" 
            placeholder="Password" 
            v-model="signInForm.password"
            required 
          />
          <a href="#" @click.prevent="forgotPassword">Forgot your password?</a>
          <button type="submit" :disabled="isLoading">
            {{ isLoading ? 'Signing In...' : 'Sign In' }}
          </button>
        </form>
      </div>

      <!-- 切换面板 -->
      <div class="toggle-container">
        <div class="toggle">
          <div class="toggle-panel toggle-left">
            <h1>Welcome Back!</h1>
            <p>Enter your personal details to use all of site features.</p>
            <button class="hidden" @click="switchToSignIn">Sign In</button>
          </div>
          <div class="toggle-panel toggle-right">
            <h1>Hello Friend!</h1>
            <p>Register your personal details to use all of site features.</p>
            <button class="hidden" @click="switchToSignUp">Sign Up</button>
          </div>
        </div>
      </div>
    </div>

    <!-- 错误/成功消息 -->
    <div v-if="message" :class="['message', messageType]">
      {{ message }}
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive } from 'vue'

// 响应式数据
const isActive = ref(false)
const isLoading = ref(false)
const message = ref('')
const messageType = ref<'success' | 'error'>('success')

// 表单数据
const signUpForm = reactive({
  name: '',
  email: '',
  password: ''
})

const signInForm = reactive({
  email: '',
  password: ''
})

// 定义事件
const emit = defineEmits<{
  signIn: [data: { email: string; password: string }]
  signUp: [data: { name: string; email: string; password: string }]
  socialLogin: [provider: string]
  forgotPassword: []
}>()

// 方法
const switchToSignUp = () => {
  isActive.value = true
}

const switchToSignIn = () => {
  isActive.value = false
}

const handleSignIn = async () => {
  if (!signInForm.email || !signInForm.password) {
    showMessage('Please fill in all fields', 'error')
    return
  }

  isLoading.value = true

  try {
    const response = await fetch('/UserData/SignIn', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        email: signInForm.email,
        password: signInForm.password
      })
    })

    if (!response.ok) {
      const errorText = await response.text()
      if (response.status === 401) {
        showMessage('邮箱或密码错误', 'error')
      } else {
        showMessage('登录失败，请重试', 'error')
      }
      return
    }

    const userData = await response.json()
    console.log('用户登录成功:', userData)

    // 触发事件传递给父组件
    emit('signIn', { ...signInForm })
    showMessage('Sign in successful!', 'success')
  } catch (error) {
    showMessage('Sign in failed. Please try again.', 'error')
  } finally {
    isLoading.value = false
  }
}

const handleSignUp = async () => {
  if (!signUpForm.name || !signUpForm.email || !signUpForm.password) {
    showMessage('Please fill in all fields', 'error')
    return
  }

  if (signUpForm.password.length < 6) {
    showMessage('Password must be at least 6 characters long', 'error')
    return
  }

  isLoading.value = true
  try {
    // 调用后端API注册用户
    const response = await fetch('/UserData/register', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        name: signUpForm.name,
        email: signUpForm.email,
        password: signUpForm.password
      })
    })

    if (!response.ok) {
      const errorText = await response.text()
      if (response.status === 409) {
        showMessage('该邮箱已被注册', 'error')
      } else if (response.status === 400) {
        showMessage('请填写完整信息', 'error')
      }else if(response.status == 500){
        showMessage("后端接收到信息，但写入错误","error")
      }
       else {
        showMessage('注册失败，请重试', 'error')
      }
      return
    }

    const userData = await response.json()
    console.log('用户注册成功:', userData)
    
    // 触发事件传递给父组件
    emit('signUp', { ...signUpForm })
    showMessage('账号创建成功！', 'success')
    
    // 清空表单
    signUpForm.name = ''
    signUpForm.email = ''
    signUpForm.password = ''
    
    // 可选：自动切换到登录表单
    setTimeout(() => {
      switchToSignIn()
    }, 1500)
    
  } catch (error) {
    console.error('注册请求失败:', error)
    showMessage('网络错误，请检查连接后重试', 'error')
  } finally {
    isLoading.value = false
  }
}

const socialLogin = (provider: string) => {
  if (provider === 'google') {
    // 直接跳转到谷歌官网
    window.open('https://www.google.com', '_blank')
    showMessage('Redirecting to Google...', 'success')
  } else {
    emit('socialLogin', provider)
    showMessage(`${provider} login clicked`, 'success')
  }

}

const forgotPassword = () => {
  emit('forgotPassword')
  showMessage('Forgot password feature coming soon', 'success')
}

const showMessage = (text: string, type: 'success' | 'error') => {
  message.value = text
  messageType.value = type
  setTimeout(() => {
    message.value = ''
  }, 3000)
}
</script>

<style scoped>
.logintitle{
  color: #333;
}

.login-container {
  display: flex;
  align-items: center;
  justify-content: center;
  width: 100vw;
  height: 100vh;
  min-height: 100vh;
  background: linear-gradient(to right, #c9d6ff, #e2e2e2);
  font-family: Arial, Helvetica, sans-serif;
  padding: 20px;
  box-sizing: border-box;
}

.container {
  background-color: #fff;
  border-radius: 30px;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.35);
  position: relative;
  overflow: hidden;
  width: 768px;
  max-width: calc(100vw - 40px);
  min-height: 480px;
  max-height: calc(100vh - 40px);
}

.container p {
  font-size: 14px;
  line-height: 20px;
  letter-spacing: 0.3px;
  margin: 20px 0;
}

.container span {
  font-size: 12px;
}

.container a {
  color: #333;
  font-size: 13px;
  text-decoration: none;
  margin: 15px 0 10px;
}

.container button {
  background-color: #512da8;
  color: #fff;
  font-size: 12px;
  padding: 10px 45px;
  border: 1px solid transparent;
  border-radius: 8px;
  font-weight: 600;
  letter-spacing: 0.5px;
  text-transform: uppercase;
  margin-top: 10px;
  cursor: pointer;
  transition: all 0.3s ease;
}

.container button:hover:not(:disabled) {
  background-color: #4527a0;
}

.container button:disabled {
  background-color: #9e9e9e;
  cursor: not-allowed;
}

.container button.hidden {
  background-color: transparent;
  border-color: #fff;
}

.container form {
  background-color: #fff;
  display: flex;
  align-items: center;
  justify-content: center;
  flex-direction: column;
  padding: 0 40px;
  height: 100%;
}

.container input {
  background-color: #eee;
  border: none;
  margin: 8px 0;
  padding: 10px 15px;
  font-size: 13px;
  border-radius: 8px;
  width: 100%;
  outline: none;
  transition: background-color 0.3s ease;
}

.container input:focus {
  background-color: #e0e0e0;
}

.form-container {
  position: absolute;
  top: 0;
  height: 100%;
  transition: all 0.6s ease-in-out;
}

.sign-in {
  left: 0;
  width: 50%;
  z-index: 2;
}

.container.active .sign-in {
  transform: translateX(100%);
}

.sign-up {
  left: 0;
  width: 50%;
  opacity: 0;
  z-index: 1;
}

.container.active .sign-up {
  transform: translateX(100%);
  opacity: 1;
  z-index: 5;
  animation: show 0.6s;
}

@keyframes show {
  0%,
  49.99% {
    opacity: 0;
    z-index: 1;
  }
  50%,
  100% {
    opacity: 1;
    z-index: 5;
  }
}

.social-icons {
  margin: 20px 0;
}

.social-icons a {
  border: 1px solid #ccc;
  border-radius: 20%;
  display: inline-flex;
  justify-content: center;
  align-items: center;
  margin: 0 3px;
  width: 40px;
  height: 40px;
  transition: all 0.3s ease;
}

.social-icons a:hover {
  background-color: #f5f5f5;
  transform: scale(1.1);
}

.toggle-container {
  position: absolute;
  top: 0;
  left: 50%;
  width: 50%;
  height: 100%;
  overflow: hidden;
  transition: all 0.6s ease-in-out;
  border-radius: 150px 0 0 100px;
  z-index: 1000;
}

.container.active .toggle-container {
  transform: translateX(-100%);
  border-radius: 0 150px 100px 0;
}

.toggle {
  background-color: #512da8;
  height: 100%;
  background: linear-gradient(to right, #5c6bc0, #512da8);
  color: #fff;
  position: relative;
  left: -100%;
  height: 100%;
  width: 200%;
  transform: translateX(0);
  transition: all 0.6s ease-in-out;
}

.container.active .toggle {
  transform: translateX(50%);
}

.toggle-panel {
  position: absolute;
  width: 50%;
  height: 100%;
  display: flex;
  align-items: center;
  justify-content: center;
  flex-direction: column;
  padding: 0 30px;
  text-align: center;
  top: 0;
  transform: translateX(0);
  transition: all 0.6s ease-in-out;
}

.toggle-left {
  transform: translateX(-200%);
}

.container.active .toggle-left {
  transform: translateX(0);
}

.toggle-right {
  right: 0;
  transform: translateX(0);
}

.container.active .toggle-right {
  transform: translateX(200%);
}

/* 消息提示样式 */
.message {
  position: fixed;
  top: 20px;
  right: 20px;
  padding: 12px 20px;
  border-radius: 8px;
  font-size: 14px;
  font-weight: 500;
  z-index: 9999;
  animation: slideIn 0.3s ease-out;
}

.message.success {
  background-color: #4caf50;
  color: white;
}

.message.error {
  background-color: #f44336;
  color: white;
}

@keyframes slideIn {
  from {
    transform: translateX(100%);
    opacity: 0;
  }
  to {
    transform: translateX(0);
    opacity: 1;
  }
}

/* 响应式设计 */
@media (max-width: 768px) {
  .login-container {
    padding: 10px;
  }

  .container {
    width: calc(100vw - 20px);
    max-width: none;
    min-height: calc(100vh - 20px);
    max-height: calc(100vh - 20px);
    border-radius: 20px;
  }

  .form-container {
    width: 100% !important;
    padding: 20px;
  }

  .toggle-container {
    display: none;
  }

  .sign-up {
    opacity: 1;
    z-index: 1;
  }

  .container.active .sign-up {
    transform: translateX(0);
    z-index: 2;
  }

  .container.active .sign-in {
    transform: translateX(0);
    z-index: 1;
  }
}
</style>
