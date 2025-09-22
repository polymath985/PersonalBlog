<template>
  <div class="home-view">
    <!-- 英雄区域 -->
    <section class="hero-section">
      <div class="hero-content">
        <div class="hero-text">
          <h1 class="hero-title">
            <span class="gradient-text">Welcome to</span>
            <br>
            <span class="highlight">Personal Blog</span>
          </h1>
          <p class="hero-description">
            探索技术的边界，分享创新的思考。在这里记录我的编程之旅、项目经验和技术洞察。
          </p>
          <div class="hero-stats">
            <div class="stat-item">
              <span class="stat-number">{{ totalPosts }}</span>
              <span class="stat-label">文章</span>
            </div>
            <div class="stat-item">
              <span class="stat-number">{{ totalProjects }}</span>
              <span class="stat-label">项目</span>
            </div>
            <div class="stat-item">
              <span class="stat-number">{{ totalViews }}</span>
              <span class="stat-label">访问</span>
            </div>
          </div>
        </div>
        <div class="hero-visual">
          <div class="floating-elements">
            <div class="floating-card" v-for="n in 6" :key="n" :style="getFloatingStyle(n)">
              <div class="card-content"></div>
            </div>
          </div>
        </div>
      </div>
    </section>

    <!-- 主要内容区域 -->
    <main class="main-content">
      <!-- 内容卡片网格 -->
      <section class="content-grid">
        <ContentBox
          v-for="(item, index) in contentItems"
          :key="index"
          :title="item.title"
          :description="item.description"
          :icon="item.icon"
          :badge="item.badge"
          :tags="item.tags"
          :featured="item.featured"
          :stats="item.stats"
          :updateTime="item.updateTime"
          :clickAction="item.clickAction"
          @click="handleContentClick"
        />
      </section>

      <!-- 快速操作区域 -->
      <section class="quick-actions">
        <h2 class="section-title">快速操作</h2>
        <div class="action-grid">
          <div 
            v-for="action in quickActions" 
            :key="action.title"
            class="action-card"
            @click="handleActionClick(action.action)"
          >
            <div class="action-icon">
              <svg width="24" height="24" viewBox="0 0 16 16">
                <path :d="getActionIcon(action.icon)" fill="currentColor"/>
              </svg>
            </div>
            <h3>{{ action.title }}</h3>
            <p>{{ action.description }}</p>
          </div>
        </div>
      </section>
    </main>
  </div>
</template>

<script setup lang="ts">
import { ref, computed } from 'vue'
import { useRouter } from 'vue-router'
import ContentBox from '@/components/ContentBox.vue'

const router = useRouter()

// 统计数据
const totalPosts = ref(42)
const totalProjects = ref(15)
const totalViews = ref(12800)

const WhenLogIn = () => {
  fetch("/Hot")
}

// 内容项目数据
const contentItems = ref([
  {
    title: "Vue 3 进阶指南",
    description: "深入探索Vue 3的Composition API、响应式系统和性能优化技巧，构建现代化的前端应用。",
    icon: "code",
    badge: "热门",
    tags: ["Vue3", "JavaScript", "前端"],
    featured: true,
    stats: { views: 1250, likes: 89, comments: 23 },
    updateTime: "2天前",
    clickAction: () => router.push('/posts/vue3-advanced')
  },
  {
    title: "全栈博客系统",
    description: "使用Vue 3 + ASP.NET Core构建的现代化个人博客系统，包含完整的前后端实现。",
    icon: "project",
    badge: "项目",
    tags: ["Vue3", "ASP.NET", "全栈"],
    featured: false,
    stats: { views: 890, likes: 45, comments: 12 },
    updateTime: "5天前",
    clickAction: () => router.push('/projects/fullstack-blog')
  },
  {
    title: "TypeScript 最佳实践",
    description: "从基础语法到高级类型，掌握TypeScript在大型项目中的应用和最佳实践模式。",
    icon: "book",
    badge: "教程",
    tags: ["TypeScript", "JavaScript", "最佳实践"],
    featured: false,
    stats: { views: 756, likes: 67, comments: 18 },
    updateTime: "1周前",
    clickAction: () => router.push('/posts/typescript-best-practices')
  },
  {
    title: "开源组件库",
    description: "基于Vue 3开发的企业级组件库，提供丰富的UI组件和开箱即用的解决方案。",
    icon: "star",
    badge: "开源",
    tags: ["Vue3", "组件库", "开源"],
    featured: true,
    stats: { views: 2100, likes: 156, comments: 34 },
    updateTime: "3天前",
    clickAction: () => window.open('https://github.com/username/component-library')
  },
  {
    title: "微前端架构实践",
    description: "探索微前端架构的设计原理和实施策略，在大型团队中实现前端应用的模块化开发。",
    icon: "rocket",
    badge: "架构",
    tags: ["微前端", "架构", "大型项目"],
    featured: false,
    stats: { views: 543, likes: 38, comments: 15 },
    updateTime: "2周前",
    clickAction: () => router.push('/posts/micro-frontend')
  },
  {
    title: "个人作品集",
    description: "展示我的设计作品、开发项目和技术文章，记录我的成长历程和技术栈演进。",
    icon: "portfolio",
    badge: "作品",
    tags: ["作品集", "设计", "开发"],
    featured: false,
    stats: { views: 1890, likes: 98, comments: 27 },
    updateTime: "4天前",
    clickAction: () => router.push('/portfolio')
  }
])

// 快速操作数据
const quickActions = ref([
  {
    title: "写新文章",
    description: "分享你的技术见解",
    icon: "edit",
    action: () => router.push('/write')
  },
  {
    title: "管理项目",
    description: "查看和编辑项目",
    icon: "folder",
    action: () => router.push('/projects')
  },
  {
    title: "查看统计",
    description: "分析网站数据",
    icon: "chart",
    action: () => router.push('/analytics')
  },
  {
    title: "设置中心",
    description: "个性化你的博客",
    icon: "settings",
    action: () => router.push('/settings')
  }
])

// 浮动元素样式生成
const getFloatingStyle = (index: number) => {
  const positions = [
    { top: '10%', left: '80%', delay: '0s' },
    { top: '60%', left: '85%', delay: '2s' },
    { top: '20%', left: '75%', delay: '4s' },
    { top: '80%', left: '70%', delay: '1s' },
    { top: '40%', left: '90%', delay: '3s' },
    { top: '70%', left: '78%', delay: '5s' }
  ]
  
  const pos = positions[index - 1] || positions[0]
  return {
    position: 'absolute' as const,
    top: pos.top,
    left: pos.left,
    animationDelay: pos.delay
  }
}

// 处理内容点击
const handleContentClick = (action: string | (() => void) | undefined) => {
  console.log('Content clicked:', action)
  if (typeof action === 'function') {
    action()
  } else if (typeof action === 'string') {
    router.push(action)
  }
}

// 处理快速操作点击
const handleActionClick = (action: () => void) => {
  console.log('Action clicked')
  action()
}

// 获取操作图标
const getActionIcon = (iconName: string): string => {
  const icons: Record<string, string> = {
    'edit': 'M11.013 1.427a1.75 1.75 0 0 1 2.474 0l1.086 1.086a1.75 1.75 0 0 1 0 2.474l-8.61 8.61c-.21.21-.47.364-.756.445l-3.251.93a.75.75 0 0 1-.927-.928l.929-3.25c.081-.286.235-.547.445-.758l8.61-8.61Z',
    'folder': 'M1.75 0A1.75 1.75 0 0 0 0 1.75v3c0 .414.336.75.75.75h4.5A.75.75 0 0 0 6 4.75v-.5h7.25A1.75 1.75 0 0 1 15 5.75v6.5A1.75 1.75 0 0 1 13.25 14H1.75A1.75 1.75 0 0 1 0 12.25V1.75Z',
    'chart': 'M1.5 1.75V13.5h13.75a.75.75 0 0 1 0 1.5H.75a.75.75 0 0 1-.75-.75V1.75a.75.75 0 0 1 1.5 0Zm14.28 2.53-5.25 5.25a.75.75 0 0 1-1.06 0L7 7.06 4.28 9.78a.751.751 0 0 1-1.042-.018.751.751 0 0 1-.018-1.042l3.25-3.25a.75.75 0 0 1 1.06 0L10 7.94l4.72-4.72a.751.751 0 0 1 1.042.018.751.751 0 0 1 .018 1.042Z',
    'settings': 'M8 0a8.2 8.2 0 0 1 .701.031C9.444.095 9.99.645 10.16 1.29l.288 1.107c.018.066.079.158.212.224.231.114.454.243.668.386.123.082.233.09.299.071l1.103-.303c.644-.176 1.392.021 1.82.63.27.385.506.792.704 1.218.315.675.111 1.422-.364 1.891l-.814.806c-.049.048-.098.147-.088.294.016.257.016.515 0 .772-.01.147.039.246.088.294l.814.806c.475.469.679 1.216.364 1.891a7.977 7.977 0 0 1-.704 1.218c-.428.609-1.176.806-1.82.63l-1.103-.303c-.066-.019-.176-.011-.299.071a4.845 4.845 0 0 1-.668.386c-.133.066-.194.158-.212.224l-.288 1.107c-.17.645-.716 1.195-1.459 1.26a8.006 8.006 0 0 1-1.402 0c-.743-.065-1.289-.615-1.459-1.26L5.54 11.717c-.018-.066-.079-.158-.212-.224a4.845 4.845 0 0 1-.668-.386c-.123-.082-.233-.09-.299-.071L3.258 11.34c-.644.176-1.392-.021-1.82-.63a7.977 7.977 0 0 1-.704-1.218c-.315-.675-.111-1.422.363-1.891l.815-.806c.05-.048.098-.147.088-.294a6.214 6.214 0 0 1 0-.772c.01-.147-.038-.246-.088-.294l-.815-.806C.635 6.045.431 5.298.746 4.623a7.92 7.92 0 0 1 .704-1.217c.428-.61 1.176-.807 1.82-.631l1.103.303c.066.019.176.011.299-.071.214-.143.437-.272.668-.386.133-.066.194-.158.212-.224L5.84 1.29C6.009.645 6.556.095 7.299.03 7.53.01 7.764 0 8 0Zm-.571 1.525c-.036.003-.108.036-.137.146l-.289 1.105c-.147.561-.549.967-.998 1.189-.173.086-.34.183-.5.29-.417.278-.97.423-1.529.27l-1.103-.303c-.109-.03-.175.016-.195.045-.22.312-.412.644-.573.99-.014.031-.021.11.059.19l.815.806c.411.406.562.957.53 1.456a4.709 4.709 0 0 0 0 .582c.032.499-.119 1.05-.53 1.456l-.815.806c-.081.08-.073.159-.059.19.161.346.353.678.573.989.02.03.085.076.195.046l1.103-.303c.559-.153 1.112-.008 1.529.27.16.107.327.204.5.29.449.222.851.628.998 1.189l.289 1.105c.029.109.101.143.137.146a6.6 6.6 0 0 0 1.142 0c.036-.003.108-.036.137-.146l.289-1.105c.147-.561.549-.967.998-1.189.173-.086.34-.183.5-.29.417-.278.97-.423 1.529-.27l1.103.303c.109.029.175-.016.195-.045.22-.313.411-.644.573-.990.014-.031.021-.11-.059-.19l-.815-.806c-.411-.406-.562-.957-.53-1.456a4.709 4.709 0 0 0 0-.582c-.032-.499.119-1.05.53-1.456l.815-.806c.081-.08.073-.159.059-.19a6.464 6.464 0 0 0-.573-.989c-.02-.03-.085-.076-.195-.046l-1.103.303c-.559.153-1.112.008-1.529-.27a4.44 4.44 0 0 0-.5-.29c-.449-.222-.851-.628-.998-1.189L8.708 1.671c-.029-.109-.101-.143-.137-.146a6.6 6.6 0 0 0-1.142 0ZM8 11a3 3 0 1 1 0-6 3 3 0 0 1 0 6Zm0-1.5a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3Z'
  }
  return icons[iconName] || icons['edit']
}
</script>

<style scoped>
.home-view {
  min-height: 100vh;
  background: linear-gradient(135deg, #0d1117 0%, #161b22 50%, #21262d 100%);
  color: #f0f6fc;
  overflow-x: hidden;
}

/* 英雄区域样式 */
.hero-section {
  position: relative;
  min-height: 60vh;
  display: flex;
  align-items: center;
  background: 
    radial-gradient(circle at 20% 80%, rgba(31, 111, 235, 0.1) 0%, transparent 50%),
    radial-gradient(circle at 80% 20%, rgba(137, 87, 229, 0.1) 0%, transparent 50%),
    linear-gradient(135deg, #0d1117 0%, #161b22 100%);
  overflow: hidden;
}

.hero-content {
  max-width: 1200px;
  margin: 0 auto;
  padding: 0 2rem;
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 4rem;
  align-items: center;
  width: 100%;
}

.hero-text {
  z-index: 2;
}

.hero-title {
  font-size: clamp(2.5rem, 5vw, 4rem);
  font-weight: 700;
  line-height: 1.1;
  margin-bottom: 1.5rem;
}

.gradient-text {
  background: linear-gradient(135deg, #1f6feb 0%, #a970ff 100%);
  background-clip: text;
  -webkit-background-clip: text;
  -webkit-text-fill-color: transparent;
  display: inline-block;
}

.highlight {
  color: #f0f6fc;
  position: relative;
}

.highlight::after {
  content: '';
  position: absolute;
  bottom: -4px;
  left: 0;
  right: 0;
  height: 3px;
  background: linear-gradient(90deg, #1f6feb, #a970ff);
  border-radius: 2px;
}

.hero-description {
  font-size: 1.25rem;
  line-height: 1.6;
  color: #8b949e;
  margin-bottom: 2rem;
  max-width: 500px;
}

.hero-stats {
  display: flex;
  gap: 2rem;
  margin-top: 2rem;
}

.stat-item {
  display: flex;
  flex-direction: column;
  align-items: center;
  padding: 1rem;
  background: rgba(255, 255, 255, 0.05);
  border-radius: 12px;
  border: 1px solid rgba(255, 255, 255, 0.1);
  backdrop-filter: blur(10px);
  transition: all 0.3s ease;
}

.stat-item:hover {
  transform: translateY(-2px);
  background: rgba(255, 255, 255, 0.08);
  border-color: #1f6feb;
}

.stat-number {
  font-size: 1.5rem;
  font-weight: 700;
  color: #1f6feb;
}

.stat-label {
  font-size: 0.875rem;
  color: #8b949e;
  margin-top: 0.25rem;
}

/* 视觉元素 */
.hero-visual {
  position: relative;
  height: 400px;
}

.floating-elements {
  position: relative;
  width: 100%;
  height: 100%;
}

.floating-card {
  width: 60px;
  height: 60px;
  background: linear-gradient(135deg, rgba(31, 111, 235, 0.2), rgba(137, 87, 229, 0.2));
  border: 1px solid rgba(255, 255, 255, 0.1);
  border-radius: 12px;
  animation: float 6s ease-in-out infinite;
  backdrop-filter: blur(10px);
}

.floating-card:nth-child(even) {
  animation-direction: reverse;
}

.card-content {
  width: 100%;
  height: 100%;
  background: linear-gradient(45deg, transparent, rgba(255, 255, 255, 0.1), transparent);
  border-radius: 12px;
}

@keyframes float {
  0%, 100% { transform: translateY(0px) rotate(0deg); }
  25% { transform: translateY(-20px) rotate(5deg); }
  50% { transform: translateY(-10px) rotate(-5deg); }
  75% { transform: translateY(-15px) rotate(3deg); }
}

/* 主要内容区域 */
.main-content {
  max-width: 1200px;
  margin: 0 auto;
  padding: 4rem 2rem;
}

/* 内容网格 */
.content-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(400px, 1fr));
  gap: 2rem;
  margin-bottom: 4rem;
}

/* 快速操作区域 */
.quick-actions {
  margin-top: 4rem;
}

.section-title {
  font-size: 2rem;
  font-weight: 600;
  color: #f0f6fc;
  margin-bottom: 2rem;
  text-align: center;
  position: relative;
}

.section-title::after {
  content: '';
  position: absolute;
  bottom: -8px;
  left: 50%;
  transform: translateX(-50%);
  width: 60px;
  height: 3px;
  background: linear-gradient(90deg, #1f6feb, #a970ff);
  border-radius: 2px;
}

.action-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
  gap: 1.5rem;
}

.action-card {
  background: linear-gradient(135deg, rgba(255, 255, 255, 0.05) 0%, rgba(255, 255, 255, 0.02) 100%);
  border: 1px solid rgba(255, 255, 255, 0.1);
  border-radius: 12px;
  padding: 1.5rem;
  text-align: center;
  cursor: pointer;
  transition: all 0.3s cubic-bezier(0.4, 0, 0.2, 1);
  backdrop-filter: blur(10px);
}

.action-card:hover {
  transform: translateY(-4px);
  background: linear-gradient(135deg, rgba(255, 255, 255, 0.08) 0%, rgba(255, 255, 255, 0.04) 100%);
  border-color: #1f6feb;
  box-shadow: 0 8px 25px rgba(31, 111, 235, 0.15);
}

.action-icon {
  width: 48px;
  height: 48px;
  background: linear-gradient(135deg, #1f6feb 0%, #0969da 100%);
  border-radius: 12px;
  display: flex;
  align-items: center;
  justify-content: center;
  margin: 0 auto 1rem;
  transition: all 0.3s ease;
}

.action-card:hover .action-icon {
  transform: scale(1.1);
  box-shadow: 0 4px 12px rgba(31, 111, 235, 0.3);
}

.action-icon svg {
  color: white;
}

.action-card h3 {
  font-size: 1.125rem;
  font-weight: 600;
  color: #f0f6fc;
  margin-bottom: 0.5rem;
}

.action-card p {
  font-size: 0.875rem;
  color: #8b949e;
  margin: 0;
}

/* 响应式设计 */
@media (max-width: 1024px) {
  .hero-content {
    grid-template-columns: 1fr;
    gap: 2rem;
  }
  
  .hero-visual {
    height: 200px;
  }
  
  .content-grid {
    grid-template-columns: 1fr;
  }
}

@media (max-width: 768px) {
  .hero-content {
    padding: 0 1rem;
  }
  
  .main-content {
    padding: 2rem 1rem;
  }
  
  .hero-stats {
    flex-direction: column;
    gap: 1rem;
  }
  
  .stat-item {
    flex-direction: row;
    gap: 1rem;
  }
  
  .floating-card {
    width: 40px;
    height: 40px;
  }
  
  .content-grid {
    gap: 1.5rem;
  }
  
  .action-grid {
    grid-template-columns: 1fr;
    gap: 1rem;
  }
}

@media (max-width: 480px) {
  .hero-title {
    font-size: 2rem;
  }
  
  .hero-description {
    font-size: 1rem;
  }
  
  .section-title {
    font-size: 1.5rem;
  }
}

/* 滚动动画 */
@media (prefers-reduced-motion: no-preference) {
  .content-grid > :nth-child(odd) {
    animation: slideInLeft 0.6s ease-out;
  }
  
  .content-grid > :nth-child(even) {
    animation: slideInRight 0.6s ease-out;
  }
  
  .action-card {
    animation: fadeInUp 0.6s ease-out;
  }
  
  .action-card:nth-child(2) { animation-delay: 0.1s; }
  .action-card:nth-child(3) { animation-delay: 0.2s; }
  .action-card:nth-child(4) { animation-delay: 0.3s; }
}

@keyframes slideInLeft {
  from {
    opacity: 0;
    transform: translateX(-50px);
  }
  to {
    opacity: 1;
    transform: translateX(0);
  }
}

@keyframes slideInRight {
  from {
    opacity: 0;
    transform: translateX(50px);
  }
  to {
    opacity: 1;
    transform: translateX(0);
  }
}

@keyframes fadeInUp {
  from {
    opacity: 0;
    transform: translateY(30px);
  }
  to {
    opacity: 1;
    transform: translateY(0);
  }
}
</style>

