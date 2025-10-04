<template>
  <div class="comment-box" :class="{ 'is-reply': isReply }">
    <!-- 评论主体 -->
    <div class="comment-main">
      <!-- 用户头像 -->
      <div class="comment-avatar">
        <svg width="40" height="40" viewBox="0 0 16 16">
          <path d="M10.561 8.073a6.005 6.005 0 0 1 3.432 5.142.75.75 0 1 1-1.498.07 4.5 4.5 0 0 0-8.99 0 .75.75 0 0 1-1.498-.07 6.004 6.004 0 0 1 3.431-5.142 3.999 3.999 0 1 1 5.123 0ZM10.5 5a2.5 2.5 0 1 0-5 0 2.5 2.5 0 0 0 5 0Z" fill="currentColor"/>
        </svg>
      </div>

      <!-- 评论内容区 -->
      <div class="comment-content">
        <!-- 头部：用户名和删除按钮 -->
        <div class="comment-header">
          <div class="comment-user-info">
            <span class="comment-username">{{ comment.userName }}</span>
            <span class="comment-time">{{ formatTime(comment.createdAt) }}</span>
          </div>
          
          <!-- 删除按钮（仅自己的评论可见） -->
          <button 
            v-if="isOwnComment" 
            class="comment-delete-btn"
            @click="handleDelete"
            title="删除评论"
          >
            <svg width="16" height="16" viewBox="0 0 16 16">
              <path d="M11 1.75V3h2.25a.75.75 0 0 1 0 1.5H2.75a.75.75 0 0 1 0-1.5H5V1.75C5 .784 5.784 0 6.75 0h2.5C10.216 0 11 .784 11 1.75ZM4.496 6.675l.66 6.6a.25.25 0 0 0 .249.225h5.19a.25.25 0 0 0 .249-.225l.66-6.6a.75.75 0 0 1 1.492.149l-.66 6.6A1.748 1.748 0 0 1 10.595 15h-5.19a1.75 1.75 0 0 1-1.741-1.575l-.66-6.6a.75.75 0 1 1 1.492-.15ZM6.5 1.75V3h3V1.75a.25.25 0 0 0-.25-.25h-2.5a.25.25 0 0 0-.25.25Z" fill="currentColor"/>
            </svg>
          </button>
        </div>

        <!-- 评论文本 -->
        <div class="comment-text">{{ comment.content }}</div>

        <!-- 底部操作栏 -->
        <div class="comment-actions">
          <!-- 点赞 -->
          <button 
            class="action-btn like-btn" 
            :class="{ 'liked': isLiked }"
            @click="handleLike"
          >
            <svg width="16" height="16" viewBox="0 0 16 16">
              <path v-if="isLiked" d="M7.655 14.916v-.001h-.002l-.006-.003-.018-.01a22.066 22.066 0 0 1-3.744-2.584C2.045 10.731 0 8.35 0 5.5 0 2.836 2.086 1 4.25 1 5.797 1 7.153 1.802 8 3.02 8.847 1.802 10.203 1 11.75 1 13.914 1 16 2.836 16 5.5c0 2.85-2.045 5.231-3.885 6.818a22.066 22.066 0 0 1-3.744 2.584l-.018.01-.006.003h-.002Z" fill="currentColor"/>
              <path v-else d="m8 14.25.345.666a.75.75 0 0 1-.69 0l-.008-.004-.018-.01a7.152 7.152 0 0 1-.31-.17 22.055 22.055 0 0 1-3.434-2.414C2.045 10.731 0 8.35 0 5.5 0 2.836 2.086 1 4.25 1 5.797 1 7.153 1.802 8 3.02 8.847 1.802 10.203 1 11.75 1 13.914 1 16 2.836 16 5.5c0 2.85-2.045 5.231-3.885 6.818a22.066 22.066 0 0 1-3.744 2.584l-.018.01-.006.003h-.002ZM4.25 2.5c-1.336 0-2.75 1.164-2.75 3 0 2.15 1.58 4.144 3.365 5.682A20.58 20.58 0 0 0 8 13.393a20.58 20.58 0 0 0 3.135-2.211C12.92 9.644 14.5 7.65 14.5 5.5c0-1.836-1.414-3-2.75-3-1.373 0-2.609.986-3.029 2.456a.749.749 0 0 1-1.442 0C6.859 3.486 5.623 2.5 4.25 2.5Z" fill="currentColor"/>
            </svg>
            <span>{{ comment.likes || 0 }}</span>
          </button>

          <!-- 回复 -->
          <button class="action-btn reply-btn" @click="handleReply">
            <svg width="16" height="16" viewBox="0 0 16 16">
              <path d="M6.78 1.97a.75.75 0 0 1 0 1.06L3.81 6h6.44A4.75 4.75 0 0 1 15 10.75v2.5a.75.75 0 0 1-1.5 0v-2.5a3.25 3.25 0 0 0-3.25-3.25H3.81l2.97 2.97a.749.749 0 0 1-.326 1.275.749.749 0 0 1-.734-.215L1.47 7.28a.75.75 0 0 1 0-1.06l4.25-4.25a.75.75 0 0 1 1.06 0Z" fill="currentColor"/>
            </svg>
            <span>回复</span>
          </button>
        </div>
      </div>
    </div>

    <!-- 子评论列表 -->
    <div v-if="comment.replies && comment.replies.length > 0" class="comment-replies">
      <CommentBox
        v-for="reply in comment.replies"
        :key="reply.id"
        :comment="reply"
        :current-user-id="currentUserId"
        :is-reply="true"
        @delete="$emit('delete', $event)"
        @like="$emit('like', $event)"
        @reply="$emit('reply', $event)"
      />
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed } from 'vue'

interface Comment {
  id: string
  userName: string
  content: string
  createdAt: string
  likes: number
  userId: string
  replies?: Comment[]
}

interface Props {
  comment: Comment
  currentUserId?: string
  isReply?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  isReply: false
})

const emit = defineEmits<{
  delete: [commentId: string]
  like: [commentId: string]
  reply: [comment: Comment]
}>()

// 点赞状态（从 localStorage 读取）
const LIKED_COMMENTS_KEY = 'likedComments'
const isLiked = ref(checkIfLiked())

// 是否是当前用户的评论
const isOwnComment = computed(() => {
  return props.currentUserId && props.comment.userId === props.currentUserId
})

// 检查是否已点赞
function checkIfLiked(): boolean {
  try {
    const likedComments = localStorage.getItem(LIKED_COMMENTS_KEY)
    if (!likedComments) return false
    const likedList: string[] = JSON.parse(likedComments)
    return likedList.includes(props.comment.id)
  } catch {
    return false
  }
}

// 格式化时间
function formatTime(dateString: string): string {
  const date = new Date(dateString)
  const now = new Date()
  const diff = now.getTime() - date.getTime()
  
  const minutes = Math.floor(diff / 60000)
  const hours = Math.floor(diff / 3600000)
  const days = Math.floor(diff / 86400000)
  
  if (minutes < 1) return '刚刚'
  if (minutes < 60) return `${minutes}分钟前`
  if (hours < 24) return `${hours}小时前`
  if (days < 7) return `${days}天前`
  
  return date.toLocaleDateString('zh-CN', {
    year: 'numeric',
    month: '2-digit',
    day: '2-digit'
  })
}

// 处理删除
function handleDelete() {
  if (confirm('确定要删除这条评论吗？')) {
    emit('delete', props.comment.id)
  }
}

// 处理点赞
function handleLike() {
  isLiked.value = !isLiked.value
  emit('like', props.comment.id)
}

// 处理回复
function handleReply() {
  emit('reply', props.comment)
}
</script>

<style scoped>
.comment-box {
  padding: 16px 0;
  border-bottom: 1px solid #30363d;
  transition: background 0.2s ease;
}

.comment-box:hover {
  background: rgba(48, 54, 61, 0.2);
}

.comment-box.is-reply {
  margin-left: 48px;
  padding: 12px 0;
}

.comment-main {
  display: flex;
  gap: 12px;
}

.comment-avatar {
  flex-shrink: 0;
  width: 40px;
  height: 40px;
  background: #21262d;
  border-radius: 50%;
  display: flex;
  align-items: center;
  justify-content: center;
  color: #58a6ff;
}

.comment-content {
  flex: 1;
  min-width: 0;
}

.comment-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 8px;
}

.comment-user-info {
  display: flex;
  align-items: center;
  gap: 12px;
}

.comment-username {
  font-size: 14px;
  font-weight: 600;
  color: #c9d1d9;
}

.comment-time {
  font-size: 12px;
  color: #8b949e;
}

.comment-delete-btn {
  padding: 4px 8px;
  background: transparent;
  border: none;
  border-radius: 6px;
  color: #8b949e;
  cursor: pointer;
  transition: all 0.2s ease;
}

.comment-delete-btn:hover {
  background: #f8514933;
  color: #f85149;
}

.comment-text {
  font-size: 14px;
  line-height: 1.6;
  color: #c9d1d9;
  margin-bottom: 12px;
  word-wrap: break-word;
}

.comment-actions {
  display: flex;
  gap: 16px;
}

.action-btn {
  display: flex;
  align-items: center;
  gap: 4px;
  padding: 4px 8px;
  background: transparent;
  border: none;
  border-radius: 6px;
  color: #8b949e;
  font-size: 13px;
  cursor: pointer;
  transition: all 0.2s ease;
}

.action-btn:hover {
  background: rgba(48, 54, 61, 0.5);
  color: #c9d1d9;
}

.like-btn.liked {
  color: #f85149;
}

.like-btn.liked svg {
  fill: #f85149;
}

.comment-replies {
  margin-top: 8px;
}

/* 响应式设计 */
@media (max-width: 768px) {
  .comment-box.is-reply {
    margin-left: 32px;
  }
  
  .comment-avatar {
    width: 32px;
    height: 32px;
  }
  
  .comment-avatar svg {
    width: 20px;
    height: 20px;
  }
}
</style>
