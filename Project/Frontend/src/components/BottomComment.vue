<template>
  <div class="bottom-comment" :class="{ 'replying': replyTarget }">
    <!-- 回复提示栏 -->
    <div v-if="replyTarget" class="reply-hint">
      <span class="reply-info">
        <svg width="14" height="14" viewBox="0 0 16 16">
          <path d="M6.78 1.97a.75.75 0 0 1 0 1.06L3.81 6h6.44A4.75 4.75 0 0 1 15 10.75v2.5a.75.75 0 0 1-1.5 0v-2.5a3.25 3.25 0 0 0-3.25-3.25H3.81l2.97 2.97a.749.749 0 0 1-.326 1.275.749.749 0 0 1-.734-.215L1.47 7.28a.75.75 0 0 1 0-1.06l4.25-4.25a.75.75 0 0 1 1.06 0Z" fill="currentColor"/>
        </svg>
        回复 <strong>{{ replyTarget.userName }}</strong>
      </span>
      <button class="cancel-reply-btn" @click="cancelReply">
        <svg width="12" height="12" viewBox="0 0 16 16">
          <path d="M3.72 3.72a.75.75 0 0 1 1.06 0L8 6.94l3.22-3.22a.749.749 0 0 1 1.275.326.749.749 0 0 1-.215.734L9.06 8l3.22 3.22a.749.749 0 0 1-.326 1.275.749.749 0 0 1-.734-.215L8 9.06l-3.22 3.22a.751.751 0 0 1-1.042-.018.751.751 0 0 1-.018-1.042L6.94 8 3.72 4.78a.75.75 0 0 1 0-1.06Z" fill="currentColor"/>
        </svg>
      </button>
    </div>

    <!-- 输入区域 -->
    <div class="comment-input-wrapper">
      <!-- 用户头像 -->
      <div class="input-avatar">
        <svg width="32" height="32" viewBox="0 0 16 16">
          <path d="M10.561 8.073a6.005 6.005 0 0 1 3.432 5.142.75.75 0 1 1-1.498.07 4.5 4.5 0 0 0-8.99 0 .75.75 0 0 1-1.498-.07 6.004 6.004 0 0 1 3.431-5.142 3.999 3.999 0 1 1 5.123 0ZM10.5 5a2.5 2.5 0 1 0-5 0 2.5 2.5 0 0 0 5 0Z" fill="currentColor"/>
        </svg>
      </div>

      <!-- 输入框 -->
      <div class="input-container">
        <textarea
          ref="textareaRef"
          v-model="commentContent"
          :placeholder="placeholder"
          class="comment-textarea"
          rows="1"
          maxlength="1000"
          @input="handleInput"
          @keydown.ctrl.enter="handleSend"
          @keydown.meta.enter="handleSend"
        ></textarea>
        
        <!-- 工具栏 -->
        <div class="comment-toolbar">
          <div class="toolbar-left">
            <span class="char-count" :class="{ 'warning': commentContent.length > 900 }">
              {{ commentContent.length }} / 1000
            </span>
          </div>
          
          <div class="toolbar-right">
            <!-- 发送按钮 -->
            <button 
              class="send-btn" 
              :disabled="!canSend"
              @click="handleSend"
            >
              <svg width="16" height="16" viewBox="0 0 16 16">
                <path d="M.989 8 .064 2.68a1.342 1.342 0 0 1 1.85-1.462l13.402 5.744a1.13 1.13 0 0 1 0 2.076L1.913 14.782a1.343 1.343 0 0 1-1.85-1.463L.99 8Zm.603-5.288L2.38 7.25h4.87a.75.75 0 0 1 0 1.5H2.38l-.788 4.538L13.929 8Z" fill="currentColor"/>
              </svg>
              <span>发送</span>
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, watch, nextTick } from 'vue'

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
  replyTarget?: Comment | null
}

const props = withDefaults(defineProps<Props>(), {
  replyTarget: null
})

const emit = defineEmits<{
  send: [content: string, parentCommentId?: string]
  'cancel-reply': []
}>()

const textareaRef = ref<HTMLTextAreaElement>()
const commentContent = ref('')

// 占位符文本
const placeholder = computed(() => {
  if (props.replyTarget) {
    return `回复 @${props.replyTarget.userName}...`
  }
  return '说说你的想法... (Ctrl+Enter 发送)'
})

// 是否可以发送
const canSend = computed(() => {
  return commentContent.value.trim().length > 0
})

// 监听回复目标变化，自动聚焦
watch(() => props.replyTarget, (newTarget) => {
  if (newTarget) {
    nextTick(() => {
      textareaRef.value?.focus()
    })
  }
})

// 处理输入，自动调整高度
function handleInput() {
  if (!textareaRef.value) return
  
  // 重置高度以正确计算 scrollHeight
  textareaRef.value.style.height = 'auto'
  
  // 设置新高度（最小 40px，最大 200px）
  const scrollHeight = textareaRef.value.scrollHeight
  const newHeight = Math.min(Math.max(scrollHeight, 40), 200)
  textareaRef.value.style.height = `${newHeight}px`
}

// 取消回复
function cancelReply() {
  emit('cancel-reply')
}

// 发送评论
function handleSend() {
  if (!canSend.value) return
  
  const content = commentContent.value.trim()
  const parentCommentId = props.replyTarget?.id
  
  emit('send', content, parentCommentId)
  
  // 清空输入框
  commentContent.value = ''
  
  // 重置高度
  if (textareaRef.value) {
    textareaRef.value.style.height = 'auto'
  }
}

// 暴露方法供父组件调用
defineExpose({
  focus: () => textareaRef.value?.focus()
})
</script>

<style scoped>
.bottom-comment {
  position: fixed;
  bottom: 0;
  left: 0;
  right: 0;
  background: #0d1117;
  border-top: 1px solid #30363d;
  z-index: 100;
  box-shadow: 0 -2px 8px rgba(0, 0, 0, 0.3);
}

.bottom-comment.replying {
  border-top: 2px solid #58a6ff;
}

.reply-hint {
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 8px 16px;
  background: rgba(88, 166, 255, 0.1);
  border-bottom: 1px solid #30363d;
}

.reply-info {
  display: flex;
  align-items: center;
  gap: 8px;
  font-size: 13px;
  color: #58a6ff;
}

.reply-info svg {
  flex-shrink: 0;
}

.reply-info strong {
  font-weight: 600;
  color: #58a6ff;
}

.cancel-reply-btn {
  padding: 4px;
  background: transparent;
  border: none;
  border-radius: 4px;
  color: #8b949e;
  cursor: pointer;
  transition: all 0.2s ease;
}

.cancel-reply-btn:hover {
  background: rgba(248, 81, 73, 0.2);
  color: #f85149;
}

.comment-input-wrapper {
  display: flex;
  gap: 12px;
  padding: 16px;
  max-width: 1200px;
  margin: 0 auto;
}

.input-avatar {
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

.input-container {
  flex: 1;
  display: flex;
  flex-direction: column;
  gap: 8px;
}

.comment-textarea {
  width: 100%;
  min-height: 40px;
  max-height: 200px;
  padding: 10px 12px;
  background: #0d1117;
  border: 1px solid #30363d;
  border-radius: 6px;
  color: #c9d1d9;
  font-size: 14px;
  font-family: inherit;
  line-height: 1.5;
  resize: none;
  outline: none;
  transition: all 0.2s ease;
}

.comment-textarea:focus {
  border-color: #58a6ff;
  box-shadow: 0 0 0 2px rgba(88, 166, 255, 0.1);
}

.comment-textarea::placeholder {
  color: #6e7681;
}

.comment-toolbar {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.toolbar-left {
  display: flex;
  gap: 12px;
}

.char-count {
  font-size: 12px;
  color: #6e7681;
}

.char-count.warning {
  color: #f85149;
}

.toolbar-right {
  display: flex;
  gap: 8px;
}

.send-btn {
  display: flex;
  align-items: center;
  gap: 6px;
  padding: 6px 16px;
  background: #238636;
  border: none;
  border-radius: 6px;
  color: #ffffff;
  font-size: 14px;
  font-weight: 500;
  cursor: pointer;
  transition: all 0.2s ease;
}

.send-btn:hover:not(:disabled) {
  background: #2ea043;
}

.send-btn:disabled {
  background: #21262d;
  color: #6e7681;
  cursor: not-allowed;
}

/* 响应式设计 */
@media (max-width: 768px) {
  .comment-input-wrapper {
    padding: 12px;
  }
  
  .input-avatar {
    width: 32px;
    height: 32px;
  }
  
  .input-avatar svg {
    width: 20px;
    height: 20px;
  }
  
  .send-btn span {
    display: none;
  }
}
</style>
