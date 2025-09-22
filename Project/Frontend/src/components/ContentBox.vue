<template>
  <div 
    class="content-box" 
    :class="{ 'featured': featured }"
    @click="handleClick"
  >
    <!-- 头部图标和标题 -->
    <div class="box-header">
      <div class="icon-container">
        <svg v-if="icon" class="box-icon" width="24" height="24" viewBox="0 0 16 16">
          <path :d="getIconPath(icon)" fill="currentColor"/>
        </svg>
      </div>
      <div class="header-content">
        <h3 class="box-title">{{ title }}</h3>
        <span v-if="badge" class="box-badge">{{ badge }}</span>
      </div>
    </div>

    <!-- 描述内容 -->
    <p class="box-description">{{ description }}</p>

    <!-- 标签列表 -->
    <div v-if="tags && tags.length" class="box-tags">
      <span 
        v-for="tag in tags" 
        :key="tag" 
        class="tag"
        :style="{ '--tag-color': getTagColor(tag) }"
      >
        {{ tag }}
      </span>
    </div>

    <!-- 底部统计信息 -->
    <div v-if="stats" class="box-stats">
      <div v-if="stats.views" class="stat-item">
        <svg width="16" height="16" viewBox="0 0 16 16">
          <path d="M8 2c1.981 0 3.671.992 4.933 2.078 1.27 1.091 2.187 2.345 2.637 3.023a1.62 1.62 0 0 1 0 1.798c-.45.678-1.367 1.932-2.637 3.023C11.67 13.008 9.981 14 8 14c-1.981 0-3.671-.992-4.933-2.078C1.797 10.83.88 9.576.43 8.898a1.62 1.62 0 0 1 0-1.798c.45-.677 1.367-1.931 2.637-3.022C4.33 2.992 6.019 2 8 2ZM1.679 7.932a.12.12 0 0 0 0 .136c.411.622 1.241 1.75 2.366 2.717C5.176 11.758 6.527 12.5 8 12.5c1.473 0 2.825-.742 3.955-1.715 1.124-.967 1.954-2.096 2.366-2.717a.12.12 0 0 0 0-.136C13.91 7.310 13.08 6.181 11.955 5.215 10.826 4.242 9.473 3.5 8 3.5c-1.473 0-2.825.742-3.955 1.715-1.124.967-1.954 2.096-2.366 2.717ZM8 10a2 2 0 1 1-2-2 2 2 0 0 1 2 2Z" fill="currentColor"/>
        </svg>
        <span>{{ formatNumber(stats.views) }}</span>
      </div>
      <div v-if="stats.likes" class="stat-item">
        <svg width="16" height="16" viewBox="0 0 16 16">
          <path d="M8 14.25l-.345-.666a59.47 59.47 0 0 1-.519-.994c-.3-.611-.570-1.228-.822-1.825C5.619 9.177 5.166 7.867 4.83 6.5H4a2 2 0 1 1 0-4h1.535c.218 0 .363.01.53.056.55.152 1.2.598 2.034 1.4.834-.802 1.483-1.248 2.034-1.4A.881.881 0 0 1 10.663 2H12a2 2 0 1 1 0 4h-.83c-.336 1.367-.789 2.677-1.484 4.265-.252.597-.522 1.214-.822 1.825-.207.39-.41.716-.519.994L8 14.25zm5.856-12.133A.75.75 0 0 1 14 2.5v1.146a.75.75 0 0 1-.35.638l-.971.643a.75.75 0 0 1-.679 0l-.97-.643A.75.75 0 0 1 10.5 3.646V2.5a.75.75 0 0 1 .144-.394z" fill="currentColor"/>
        </svg>
        <span>{{ formatNumber(stats.likes) }}</span>
      </div>
      <div v-if="stats.comments" class="stat-item">
        <svg width="16" height="16" viewBox="0 0 16 16">
          <path d="M1 2.75A.75.75 0 0 1 1.75 2h8.5a.75.75 0 0 1 .75.75v5.5a.75.75 0 0 1-.75.75h-3.5l-3 3v-3h-2a.75.75 0 0 1-.75-.75v-5.5Z" fill="currentColor"/>
        </svg>
        <span>{{ formatNumber(stats.comments) }}</span>
      </div>
      <div v-if="updateTime" class="stat-item update-time">
        <svg width="16" height="16" viewBox="0 0 16 16">
          <path d="M8 0a8 8 0 1 1 0 16A8 8 0 0 1 8 0ZM1.5 8a6.5 6.5 0 1 0 13 0 6.5 6.5 0 0 0-13 0Zm7-3.25v2.992l2.028.812a.75.75 0 0 1-.557 1.392l-2.5-1A.751.751 0 0 1 7 8.25v-3.5a.75.75 0 0 1 1.5 0Z" fill="currentColor"/>
        </svg>
        <span>{{ updateTime }}</span>
      </div>
    </div>

    <!-- 悬停效果的装饰边框 -->
    <div class="hover-border"></div>
  </div>
</template>

<script setup lang="ts">
import { computed } from 'vue'

// 定义 props
interface Props {
  title: string
  description: string
  icon?: string
  badge?: string
  tags?: string[]
  featured?: boolean
  stats?: {
    views?: number
    likes?: number
    comments?: number
  }
  updateTime?: string
  clickAction?: string | (() => void)
}

const props = withDefaults(defineProps<Props>(), {
  featured: false,
  tags: () => [],
})

// 定义事件
const emit = defineEmits<{
  click: [action: string | (() => void) | undefined]
}>()

// 处理点击事件
const handleClick = () => {
  emit('click', props.clickAction)
}

// 获取图标路径
const getIconPath = (iconName: string): string => {
  const icons: Record<string, string> = {
    'code': 'M11.013 1.427a1.75 1.75 0 0 1 2.474 0l1.086 1.086a1.75 1.75 0 0 1 0 2.474l-8.61 8.61c-.21.21-.47.364-.756.445l-3.251.93a.75.75 0 0 1-.927-.928l.929-3.25c.081-.286.235-.547.445-.758l8.61-8.61Zm.176 4.823L9.75 4.81l-6.286 6.287a.253.253 0 0 0-.064.108l-.558 1.953 1.953-.558a.253.253 0 0 0 .108-.064Z',
    'book': 'M0 1.75A.75.75 0 0 1 .75 1h4.253c1.227 0 2.317.59 3 1.501A3.743 3.743 0 0 1 11.006 1h4.245a.75.75 0 0 1 .75.75v10.5a.75.75 0 0 1-.75.75h-4.507a2.25 2.25 0 0 0-1.591.659l-.622.621a.75.75 0 0 1-1.06 0l-.622-.621A2.25 2.25 0 0 0 5.258 13H.75a.75.75 0 0 1-.75-.75V1.75Z',
    'project': 'M1.75 0h12.5C15.216 0 16 .784 16 1.75v9.5A1.75 1.75 0 0 1 14.25 13H8.06l-2.573 2.573A1.458 1.458 0 0 1 3 14.543V13H1.75A1.75 1.75 0 0 1 0 11.25v-9.5C0 .784.784 0 1.75 0ZM1.5 1.75v9.5c0 .138.112.25.25.25h2a.75.75 0 0 1 .75.75v1.543c0 .431.464.645.82.374L7.94 11.5h6.31a.25.25 0 0 0 .25-.25v-9.5a.25.25 0 0 0-.25-.25H1.75a.25.25 0 0 0-.25.25Z',
    'star': 'M8 .25a.75.75 0 0 1 .673.418l1.882 3.815 4.21.612a.75.75 0 0 1 .416 1.279l-3.046 2.97.719 4.192a.75.75 0 0 1-1.088.791L8 12.347l-3.766 1.98a.75.75 0 0 1-1.088-.79l.72-4.194L.818 6.374a.75.75 0 0 1 .416-1.28l4.21-.611L7.327.668A.75.75 0 0 1 8 .25Z',
    'rocket': 'M14.064 0h.186C15.216 0 16 .784 16 1.75v.186a8.752 8.752 0 0 1-2.564 6.186l-.458.459c-.314.314-.641.616-.979.904v3.207c0 .608-.315 1.172-.833 1.49l-2.774 1.707a.749.749 0 0 1-1.11-.418l-.954-3.102a1.214 1.214 0 0 1-.145-.125L3.754 9.816a1.218 1.218 0 0 1-.124-.145L.528 8.717a.749.749 0 0 1-.418-1.11l1.71-2.774A1.748 1.748 0 0 1 3.31 4h3.204c.288-.338.59-.665.904-.979l.459-.458A8.749 8.749 0 0 1 14.064 0ZM8.938 3.623h-.002l-.458.458c-.76.76-1.437 1.598-2.02 2.5l-1.5 2.317 2.143 2.143 2.317-1.5c.902-.583 1.74-1.26 2.499-2.02l.459-.458a7.25 7.25 0 0 0 2.123-5.127V1.75a.25.25 0 0 0-.25-.25h-.186a7.249 7.249 0 0 0-5.125 2.123ZM3.56 14.56c-.732.732-2.334 1.045-3.005.374-.671-.671-.358-2.273.374-3.005.732-.732 2.334-1.045 3.005-.374.671.671.358 2.273-.374 3.005Z',
    'portfolio': 'M7.024 2.31a2.402 2.402 0 0 1 1.952 0C10.498.704 12.246 0 14.5 0c.979 0 1.5.375 1.5 1.25v9.5c0 .847-.452 1.25-1.5 1.25-2.254 0-4.002-.704-5.524-2.31a2.402 2.402 0 0 1-1.952 0C5.502 11.296 3.754 12 1.5 12 .521 12 0 11.625 0 10.75v-9.5C0 .375.521 0 1.5 0c2.254 0 4.002.704 5.524 2.31ZM14.5 1.5c-1.613 0-2.9.585-4 1.69v7.66c1.1-1.105 2.387-1.69 4-1.69.023 0 .044.002.067.002V1.5h-.067ZM1.5 1.5v8.662c.023 0 .044-.002.067-.002 1.613 0 2.9.585 4 1.69V3.19c-1.1-1.105-2.387-1.69-4-1.69H1.5Z'
  }
  return icons[iconName] || icons['code']
}

// 获取标签颜色
const getTagColor = (tag: string): string => {
  const colors = [
    '#1f6feb', '#238636', '#da3633', '#fb8500',
    '#8957e5', '#bf8700', '#e85aad', '#0969da'
  ]
  let hash = 0
  for (let i = 0; i < tag.length; i++) {
    hash = tag.charCodeAt(i) + ((hash << 5) - hash)
  }
  return colors[Math.abs(hash) % colors.length]
}

// 格式化数字
const formatNumber = (num: number): string => {
  if (num >= 1000000) {
    return (num / 1000000).toFixed(1) + 'M'
  } else if (num >= 1000) {
    return (num / 1000).toFixed(1) + 'K'
  }
  return num.toString()
}
</script>

<style scoped>
.content-box {
  position: relative;
  background: linear-gradient(135deg, #0d1117 0%, #161b22 100%);
  border: 1px solid #30363d;
  border-radius: 12px;
  padding: 24px;
  cursor: pointer;
  transition: all 0.3s cubic-bezier(0.4, 0, 0.2, 1);
  overflow: hidden;
  backdrop-filter: blur(10px);
  box-shadow: 
    0 1px 3px rgba(0, 0, 0, 0.12),
    0 1px 2px rgba(0, 0, 0, 0.24),
    inset 0 1px 0 rgba(255, 255, 255, 0.05);
}

.content-box:hover {
  transform: translateY(-4px);
  border-color: #1f6feb;
  box-shadow: 
    0 8px 25px rgba(31, 111, 235, 0.15),
    0 4px 10px rgba(0, 0, 0, 0.3),
    inset 0 1px 0 rgba(255, 255, 255, 0.1);
}

.content-box.featured {
  background: linear-gradient(135deg, #1a1f36 0%, #2d1b69 100%);
  border-color: #8957e5;
}

.content-box.featured:hover {
  border-color: #a970ff;
  box-shadow: 
    0 8px 25px rgba(137, 87, 229, 0.2),
    0 4px 10px rgba(0, 0, 0, 0.3);
}

/* 悬停边框效果 */
.hover-border {
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  border-radius: 12px;
  padding: 1px;
  background: linear-gradient(45deg, transparent, #1f6feb, transparent);
  opacity: 0;
  transition: opacity 0.3s ease;
  pointer-events: none;
}

.content-box:hover .hover-border {
  opacity: 0.6;
}

.hover-border::before {
  content: '';
  position: absolute;
  top: 1px;
  left: 1px;
  right: 1px;
  bottom: 1px;
  background: inherit;
  border-radius: 11px;
}

/* 头部样式 */
.box-header {
  display: flex;
  align-items: flex-start;
  gap: 16px;
  margin-bottom: 16px;
}

.icon-container {
  flex-shrink: 0;
  width: 48px;
  height: 48px;
  background: linear-gradient(135deg, #1f6feb 0%, #0969da 100%);
  border-radius: 12px;
  display: flex;
  align-items: center;
  justify-content: center;
  box-shadow: 0 4px 12px rgba(31, 111, 235, 0.3);
}

.featured .icon-container {
  background: linear-gradient(135deg, #8957e5 0%, #a970ff 100%);
  box-shadow: 0 4px 12px rgba(137, 87, 229, 0.3);
}

.box-icon {
  color: white;
}

.header-content {
  flex: 1;
  min-width: 0;
}

.box-title {
  font-size: 18px;
  font-weight: 600;
  color: #f0f6fc;
  margin: 0 0 4px 0;
  line-height: 1.3;
  display: flex;
  align-items: center;
  gap: 8px;
}

.box-badge {
  display: inline-block;
  background: linear-gradient(135deg, #1f6feb 0%, #0969da 100%);
  color: white;
  padding: 2px 8px;
  border-radius: 12px;
  font-size: 12px;
  font-weight: 500;
  text-transform: uppercase;
}

.featured .box-badge {
  background: linear-gradient(135deg, #8957e5 0%, #a970ff 100%);
}

/* 描述样式 */
.box-description {
  color: #8b949e;
  font-size: 14px;
  line-height: 1.5;
  margin: 0 0 16px 0;
  display: -webkit-box;
  -webkit-line-clamp: 3;
  -webkit-box-orient: vertical;
  overflow: hidden;
}

/* 标签样式 */
.box-tags {
  display: flex;
  flex-wrap: wrap;
  gap: 6px;
  margin-bottom: 16px;
}

.tag {
  padding: 4px 8px;
  background: var(--tag-color);
  color: white;
  border-radius: 16px;
  font-size: 12px;
  font-weight: 500;
  opacity: 0.9;
  transition: opacity 0.2s ease;
}

.tag:hover {
  opacity: 1;
}

/* 统计信息样式 */
.box-stats {
  display: flex;
  align-items: center;
  gap: 16px;
  color: #7d8590;
  font-size: 12px;
  flex-wrap: wrap;
}

.stat-item {
  display: flex;
  align-items: center;
  gap: 4px;
  transition: color 0.2s ease;
}

.stat-item:hover {
  color: #f0f6fc;
}

.stat-item svg {
  opacity: 0.7;
}

.update-time {
  margin-left: auto;
  font-style: italic;
}

/* 响应式设计 */
@media (max-width: 768px) {
  .content-box {
    padding: 20px;
  }
  
  .box-header {
    gap: 12px;
  }
  
  .icon-container {
    width: 40px;
    height: 40px;
  }
  
  .box-title {
    font-size: 16px;
  }
  
  .box-description {
    font-size: 13px;
  }
  
  .box-stats {
    gap: 12px;
  }
  
  .update-time {
    margin-left: 0;
    margin-top: 8px;
    width: 100%;
  }
}

/* 动画效果 */
@keyframes shimmer {
  0% {
    background-position: -200px 0;
  }
  100% {
    background-position: calc(200px + 100%) 0;
  }
}

.content-box::before {
  content: '';
  position: absolute;
  top: 0;
  left: -100%;
  width: 100%;
  height: 100%;
  background: linear-gradient(
    90deg,
    transparent,
    rgba(255, 255, 255, 0.05),
    transparent
  );
  transition: left 0.5s;
  pointer-events: none;
}

.content-box:hover::before {
  left: 100%;
}
</style>