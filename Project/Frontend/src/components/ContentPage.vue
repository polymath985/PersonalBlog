<template>
  <div class="content-page">
    <!-- 内容网格 -->
    <div class="content-grid" :style="{ gridTemplateColumns: `repeat(${itemsPerRow}, 1fr)` }">
      <ContentBox
        v-for="item in paginatedItems"
        :key="item.id"
        :title="item.title"
        :description="item.description"
        :icon="item.icon"
        :badge="item.badge"
        :tags="item.tags"
        :featured="item.featured"
        :stats="item.stats"
        :update-time="item.updateTime"
<<<<<<< HEAD
=======
        :author="item.author"
>>>>>>> dev
        @click="handleItemClick(item)"
      />
    </div>

    <!-- 分页控件 -->
    <div v-if="totalPages > 1" class="pagination">
      <button 
        class="page-btn"
        :disabled="currentPage === 1"
        @click="changePage(currentPage - 1)"
      >
        <svg width="16" height="16" viewBox="0 0 16 16">
          <path d="M9.78 12.78a.75.75 0 0 1-1.06 0L4.47 8.53a.75.75 0 0 1 0-1.06l4.25-4.25a.751.751 0 0 1 1.042.018.751.751 0 0 1 .018 1.042L5.81 8l3.97 3.97a.75.75 0 0 1 0 1.06Z" fill="currentColor"/>
        </svg>
        上一页
      </button>

      <div class="page-numbers">
        <button
          v-for="page in visiblePages"
          :key="page"
          class="page-number"
          :class="{ active: page === currentPage, ellipsis: page === '...' }"
          :disabled="page === '...'"
          @click="page !== '...' && changePage(page as number)"
        >
          {{ page }}
        </button>
      </div>

      <button 
        class="page-btn"
        :disabled="currentPage === totalPages"
        @click="changePage(currentPage + 1)"
      >
        下一页
        <svg width="16" height="16" viewBox="0 0 16 16">
          <path d="M6.22 3.22a.75.75 0 0 1 1.06 0l4.25 4.25a.75.75 0 0 1 0 1.06l-4.25 4.25a.751.751 0 0 1-1.042-.018.751.751 0 0 1-.018-1.042L10.19 8 6.22 4.03a.75.75 0 0 1 0-1.06Z" fill="currentColor"/>
        </svg>
      </button>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed } from 'vue'
import ContentBox from './ContentBox.vue'

<<<<<<< HEAD
=======
interface Author {
  id: string
  name: string
  avatar?: string
}

>>>>>>> dev
interface ContentItem {
  id: string
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
<<<<<<< HEAD
}

=======
  author?: Author
}

type SortBy = 'time-desc' | 'time-asc' | 'views-desc' | 'views-asc' | 'likes-desc' | 'likes-asc' | 'comments-desc' | 'comments-asc' | 'none'

>>>>>>> dev
interface Props {
  items: ContentItem[]
  itemsPerRow?: number
  itemsPerPage?: number
<<<<<<< HEAD
=======
  sortBy?: SortBy
>>>>>>> dev
}

const props = withDefaults(defineProps<Props>(), {
  itemsPerRow: 3,
<<<<<<< HEAD
  itemsPerPage: 10
=======
  itemsPerPage: 10,
  sortBy: 'none'
>>>>>>> dev
})

const emit = defineEmits<{
  'item-click': [item: ContentItem]
}>()

// 当前页码
const currentPage = ref(1)

<<<<<<< HEAD
// 计算总页数
const totalPages = computed(() => {
  return Math.ceil(props.items.length / props.itemsPerPage)
=======
// 排序后的数据
const sortedItems = computed(() => {
  if (props.sortBy === 'none') {
    return props.items
  }

  const itemsCopy = [...props.items]
  
  switch (props.sortBy) {
    case 'time-desc':
      // 按时间降序（最新的在前）
      return itemsCopy.sort((a, b) => {
        const timeA = a.updateTime ? new Date(a.updateTime).getTime() : 0
        const timeB = b.updateTime ? new Date(b.updateTime).getTime() : 0
        return timeB - timeA
      })
      
    case 'time-asc':
      // 按时间升序（最旧的在前）
      return itemsCopy.sort((a, b) => {
        const timeA = a.updateTime ? new Date(a.updateTime).getTime() : 0
        const timeB = b.updateTime ? new Date(b.updateTime).getTime() : 0
        return timeA - timeB
      })
      
    case 'views-desc':
      // 按浏览量降序（热度高的在前）
      return itemsCopy.sort((a, b) => {
        const viewsA = a.stats?.views || 0
        const viewsB = b.stats?.views || 0
        return viewsB - viewsA
      })
      
    case 'views-asc':
      // 按浏览量升序
      return itemsCopy.sort((a, b) => {
        const viewsA = a.stats?.views || 0
        const viewsB = b.stats?.views || 0
        return viewsA - viewsB
      })
      
    case 'likes-desc':
      // 按点赞数降序（最受欢迎的在前）
      return itemsCopy.sort((a, b) => {
        const likesA = a.stats?.likes || 0
        const likesB = b.stats?.likes || 0
        return likesB - likesA
      })
      
    case 'likes-asc':
      // 按点赞数升序
      return itemsCopy.sort((a, b) => {
        const likesA = a.stats?.likes || 0
        const likesB = b.stats?.likes || 0
        return likesA - likesB
      })
      
    case 'comments-desc':
      // 按评论数降序（讨论最多的在前）
      return itemsCopy.sort((a, b) => {
        const commentsA = a.stats?.comments || 0
        const commentsB = b.stats?.comments || 0
        return commentsB - commentsA
      })
      
    case 'comments-asc':
      // 按评论数升序
      return itemsCopy.sort((a, b) => {
        const commentsA = a.stats?.comments || 0
        const commentsB = b.stats?.comments || 0
        return commentsA - commentsB
      })
      
    default:
      return itemsCopy
  }
})

// 计算总页数
const totalPages = computed(() => {
  return Math.ceil(sortedItems.value.length / props.itemsPerPage)
>>>>>>> dev
})

// 当前页显示的数据
const paginatedItems = computed(() => {
  const start = (currentPage.value - 1) * props.itemsPerPage
  const end = start + props.itemsPerPage
<<<<<<< HEAD
  return props.items.slice(start, end)
=======
  return sortedItems.value.slice(start, end)
>>>>>>> dev
})

// 计算可见的页码
const visiblePages = computed(() => {
  const pages: (number | string)[] = []
  const total = totalPages.value
  const current = currentPage.value
  
  if (total <= 7) {
    // 如果总页数 <= 7，显示所有页码
    for (let i = 1; i <= total; i++) {
      pages.push(i)
    }
  } else {
    // 总是显示第一页
    pages.push(1)
    
    if (current > 3) {
      pages.push('...')
    }
    
    // 显示当前页及其前后页
    const start = Math.max(2, current - 1)
    const end = Math.min(total - 1, current + 1)
    
    for (let i = start; i <= end; i++) {
      pages.push(i)
    }
    
    if (current < total - 2) {
      pages.push('...')
    }
    
    // 总是显示最后一页
    pages.push(total)
  }
  
  return pages
})

// 切换页码
const changePage = (page: number) => {
  if (page >= 1 && page <= totalPages.value && page !== currentPage.value) {
    currentPage.value = page
    // 滚动到页面顶部
    window.scrollTo({ top: 0, behavior: 'smooth' })
  }
}

// 处理项目点击
const handleItemClick = (item: ContentItem) => {
  emit('item-click', item)
}
</script>

<style scoped>
.content-page {
  width: 100%;
}

.content-grid {
  display: grid;
  gap: 2rem;
  margin-bottom: 3rem;
}

/* 响应式布局 */
@media (max-width: 1400px) {
  .content-grid {
    grid-template-columns: repeat(2, 1fr) !important;
  }
}

@media (max-width: 768px) {
  .content-grid {
    grid-template-columns: 1fr !important;
    gap: 1.5rem;
  }
}

/* 分页样式 */
.pagination {
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 1rem;
  padding: 2rem 0;
}

.page-btn {
  display: flex;
  align-items: center;
  gap: 0.5rem;
  padding: 0.75rem 1.25rem;
  background: linear-gradient(135deg, #161b22 0%, #1f242f 100%);
  border: 1px solid #30363d;
  border-radius: 8px;
  color: #c9d1d9;
  font-size: 0.95rem;
  font-weight: 500;
  cursor: pointer;
  transition: all 0.3s ease;
}

.page-btn:hover:not(:disabled) {
  background: linear-gradient(135deg, #1f242f 0%, #2d3748 100%);
  border-color: #58a6ff;
  color: #58a6ff;
  transform: translateY(-2px);
  box-shadow: 0 4px 12px rgba(88, 166, 255, 0.2);
}

.page-btn:disabled {
  opacity: 0.3;
  cursor: not-allowed;
}

.page-btn svg {
  flex-shrink: 0;
}

.page-numbers {
  display: flex;
  gap: 0.5rem;
}

.page-number {
  min-width: 40px;
  height: 40px;
  padding: 0 0.75rem;
  background: #161b22;
  border: 1px solid #30363d;
  border-radius: 8px;
  color: #c9d1d9;
  font-size: 0.95rem;
  font-weight: 500;
  cursor: pointer;
  transition: all 0.2s ease;
}

.page-number:hover:not(:disabled):not(.active) {
  background: #1f242f;
  border-color: #58a6ff;
  color: #58a6ff;
}

.page-number.active {
  background: linear-gradient(135deg, #1f6feb 0%, #0969da 100%);
  border-color: #1f6feb;
  color: white;
  box-shadow: 0 4px 12px rgba(31, 111, 235, 0.3);
}

.page-number.ellipsis {
  background: transparent;
  border: none;
  cursor: default;
}

.page-number.ellipsis:hover {
  background: transparent;
  border: none;
  color: #c9d1d9;
}

/* 加载动画 */
@keyframes fadeIn {
  from {
    opacity: 0;
    transform: translateY(20px);
  }
  to {
    opacity: 1;
    transform: translateY(0);
  }
}

.content-grid > * {
  animation: fadeIn 0.5s ease forwards;
}

.content-grid > *:nth-child(1) { animation-delay: 0.05s; }
.content-grid > *:nth-child(2) { animation-delay: 0.1s; }
.content-grid > *:nth-child(3) { animation-delay: 0.15s; }
.content-grid > *:nth-child(4) { animation-delay: 0.2s; }
.content-grid > *:nth-child(5) { animation-delay: 0.25s; }
.content-grid > *:nth-child(6) { animation-delay: 0.3s; }
.content-grid > *:nth-child(7) { animation-delay: 0.35s; }
.content-grid > *:nth-child(8) { animation-delay: 0.4s; }
.content-grid > *:nth-child(9) { animation-delay: 0.45s; }
.content-grid > *:nth-child(10) { animation-delay: 0.5s; }
</style>
