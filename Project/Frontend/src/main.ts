import './assets/main.css'

import { createApp } from 'vue'
import { createPinia } from 'pinia'
import App from './App.vue'
import router from './router'

const app = createApp(App)
const pinia = createPinia()

// 正确的顺序：先注册Pinia，再使用router
app.use(pinia)
app.use(router)

// 初始化认证状态（在Pinia注册后）
import { useAuthStore } from './stores/auth'
const authStore = useAuthStore()
authStore.initFromStorage()

app.mount('#app')
