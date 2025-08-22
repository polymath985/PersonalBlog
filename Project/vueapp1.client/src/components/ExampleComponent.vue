<script setup lang="ts">
// 定义 props 接口
interface Props {
  title: string
  description?: string
  isVisible?: boolean
}

// 接收 props 并设置默认值
const props = withDefaults(defineProps<Props>(), {
  description: '这是一个示例组件',
  isVisible: true
})

// 定义响应式数据
import { ref } from 'vue'
const count = ref(0)
const showDetails = ref(false)

// 定义 emits
const emit = defineEmits<{
  click: [value: number]
  toggle: [visible: boolean]
}>()

// 定义方法
const handleClick = () => {
  count.value++
  emit('click', count.value)
}

const toggleDetails = () => {
  showDetails.value = !showDetails.value
  emit('toggle', showDetails.value)
}
</script>

<template>
  <div class="example-component" v-if="props.isVisible">
    <!-- 标题部分 -->
    <header class="header">
      <h3>{{ props.title }}</h3>
      <p class="description">{{ props.description }}</p>
    </header>

    <!-- 交互部分 -->
    <div class="content">
      <div class="counter">
        <span class="label">点击次数:</span>
        <span class="count">{{ count }}</span>
      </div>
      
      <div class="actions">
        <button 
          @click="handleClick" 
          class="primary-button"
        >
          点击我 (+1)
        </button>
        
        <button 
          @click="toggleDetails" 
          class="secondary-button"
        >
          {{ showDetails ? '隐藏' : '显示' }} 详情
        </button>
      </div>

      <!-- 可切换的详情部分 -->
      <div 
        v-if="showDetails" 
        class="details"
      >
        <h4>组件详情</h4>
        <ul>
          <li>这是一个 Vue 3 + TypeScript 组件</li>
          <li>使用了 Composition API 和 &lt;script setup&gt;</li>
          <li>包含了 props、响应式数据和事件</li>
          <li>演示了父子组件通信机制</li>
        </ul>
      </div>
    </div>
  </div>
</template>

<style scoped>
.example-component {
  padding: 1.5rem;
  border: 2px solid #42b883;
  border-radius: 12px;
  margin: 1rem 0;
  background: linear-gradient(135deg, #f5f7fa 0%, #c3cfe2 100%);
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
}

.header {
  margin-bottom: 1rem;
  padding-bottom: 0.5rem;
  border-bottom: 1px solid #e0e0e0;
}

.header h3 {
  color: #2c3e50;
  margin: 0 0 0.5rem 0;
  font-size: 1.5rem;
  font-weight: 600;
}

.description {
  color: #666;
  margin: 0;
  font-style: italic;
}

.content {
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

.counter {
  display: flex;
  align-items: center;
  gap: 0.5rem;
  font-size: 1.2rem;
}

.label {
  font-weight: 500;
  color: #2c3e50;
}

.count {
  background: #42b883;
  color: white;
  padding: 0.2rem 0.8rem;
  border-radius: 20px;
  font-weight: bold;
  min-width: 2rem;
  text-align: center;
}

.actions {
  display: flex;
  gap: 0.5rem;
  flex-wrap: wrap;
}

.primary-button {
  background: #42b883;
  color: white;
  border: none;
  padding: 0.8rem 1.5rem;
  border-radius: 8px;
  cursor: pointer;
  font-size: 1rem;
  font-weight: 500;
  transition: all 0.3s ease;
}

.primary-button:hover {
  background: #369870;
  transform: translateY(-2px);
}

.secondary-button {
  background: transparent;
  color: #42b883;
  border: 2px solid #42b883;
  padding: 0.8rem 1.5rem;
  border-radius: 8px;
  cursor: pointer;
  font-size: 1rem;
  font-weight: 500;
  transition: all 0.3s ease;
}

.secondary-button:hover {
  background: #42b883;
  color: white;
}

.details {
  background: rgba(255, 255, 255, 0.8);
  padding: 1rem;
  border-radius: 8px;
  border-left: 4px solid #42b883;
}

.details h4 {
  margin: 0 0 0.8rem 0;
  color: #2c3e50;
  font-size: 1.1rem;
}

.details ul {
  margin: 0;
  padding-left: 1.5rem;
  color: #555;
}

.details li {
  margin-bottom: 0.3rem;
}

/* 响应式设计 */
@media (max-width: 768px) {
  .example-component {
    padding: 1rem;
    margin: 0.5rem 0;
  }

  .actions {
    flex-direction: column;
  }

  .primary-button,
  .secondary-button {
    width: 100%;
    text-align: center;
  }
}
</style>
