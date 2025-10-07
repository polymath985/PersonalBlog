<template>
  <div class="blog-detail-view">
    <!-- 加载状态 -->
    <div v-if="loading" class="loading-container">
      <div class="loading-spinner"></div>
      <p>加载文章中...</p>
    </div>

    <!-- 错误状态 -->
    <div v-else-if="error" class="error-container">
      <svg width="64" height="64" viewBox="0 0 16 16">
        <path d="M8 0a8 8 0 1 1 0 16A8 8 0 0 1 8 0ZM1.5 8a6.5 6.5 0 1 0 13 0 6.5 6.5 0 0 0-13 0Zm9.78-2.22a.751.751 0 0 0-1.042-.018.751.751 0 0 0-.018 1.042L9.94 8.5l-1.72 1.696a.75.75 0 1 0 1.06 1.06L11 9.56l1.72 1.696a.75.75 0 1 0 1.06-1.06L12.06 8.5l1.72-1.696a.75.75 0 1 0-1.06-1.06L11 7.44 9.28 5.744Z" fill="currentColor"/>
      </svg>
      <h2>{{ error }}</h2>
      <button @click="loadBlog" class="retry-button">重试</button>
      <button @click="goBack" class="back-button">返回</button>
    </div>

    <!-- 文章内容 -->
    <article v-else-if="blog" class="blog-article">
      <!-- 返回按钮 -->
      <button @click="goBack" class="nav-back-button">
        <svg width="16" height="16" viewBox="0 0 16 16">
          <path d="M9.78 12.78a.75.75 0 0 1-1.06 0L4.47 8.53a.75.75 0 0 1 0-1.06l4.25-4.25a.751.751 0 0 1 1.042.018.751.751 0 0 1 .018 1.042L6.06 8l3.72 3.72a.75.75 0 0 1 0 1.06Z" fill="currentColor"/>
        </svg>
        返回
      </button>

      <!-- 文章头部 -->
      <header class="article-header">
        <!-- 标题和编辑按钮 -->
        <div class="title-container">
          <h1 class="article-title">{{ blog.title }}</h1>
          
          <!-- 编辑和删除按钮（仅作者可见） -->
          <div v-if="isAuthor" class="author-actions">
            <button 
              class="edit-blog-button"
              @click="editBlog"
              title="编辑文章"
            >
              <svg width="16" height="16" viewBox="0 0 16 16">
                <path d="M11.013 1.427a1.75 1.75 0 0 1 2.474 0l1.086 1.086a1.75 1.75 0 0 1 0 2.474l-8.61 8.61c-.21.21-.47.364-.756.445l-3.251.93a.75.75 0 0 1-.927-.928l.929-3.25c.081-.286.235-.547.445-.758l8.61-8.61Zm.176 4.823L9.75 4.81l-6.286 6.287a.253.253 0 0 0-.064.108l-.558 1.953 1.953-.558a.253.253 0 0 0 .108-.064Zm1.238-3.763a.25.25 0 0 0-.354 0L10.811 3.75l1.439 1.44 1.263-1.263a.25.25 0 0 0 0-.354Z" fill="currentColor"/>
              </svg>
              <span>编辑</span>
            </button>
            <button 
              class="delete-blog-button"
              @click="deleteBlog"
              title="删除文章"
            >
              <svg width="16" height="16" viewBox="0 0 16 16">
                <path d="M11 1.75V3h2.25a.75.75 0 0 1 0 1.5H2.75a.75.75 0 0 1 0-1.5H5V1.75C5 .784 5.784 0 6.75 0h2.5C10.216 0 11 .784 11 1.75ZM4.496 6.675l.66 6.6a.25.25 0 0 0 .249.225h5.19a.25.25 0 0 0 .249-.225l.66-6.6a.75.75 0 0 1 1.492.149l-.66 6.6A1.748 1.748 0 0 1 10.595 15h-5.19a1.75 1.75 0 0 1-1.741-1.575l-.66-6.6a.75.75 0 1 1 1.492-.15ZM6.5 1.75V3h3V1.75a.25.25 0 0 0-.25-.25h-2.5a.25.25 0 0 0-.25.25Z" fill="currentColor"/>
              </svg>
              <span>删除</span>
            </button>
          </div>
        </div>
        
        <div class="article-meta">
          <div class="meta-item">
            <svg width="16" height="16" viewBox="0 0 16 16">
              <path d="M8 0a8 8 0 1 1 0 16A8 8 0 0 1 8 0ZM1.5 8a6.5 6.5 0 1 0 13 0 6.5 6.5 0 0 0-13 0Zm7-3.25v2.992l2.028.812a.75.75 0 0 1-.557 1.392l-2.5-1A.751.751 0 0 1 7 8.25v-3.5a.75.75 0 0 1 1.5 0Z" fill="currentColor"/>
            </svg>
            <span>{{ formatDate(blog.createdAt) }}</span>
          </div>
          
          <div v-if="blog.updatedAt && blog.updatedAt !== blog.createdAt" class="meta-item">
            <svg width="16" height="16" viewBox="0 0 16 16">
              <path d="M1.705 8.005a.75.75 0 0 1 .834.656 5.5 5.5 0 0 0 9.592 2.97l-1.204-1.204a.25.25 0 0 1 .177-.427h3.646a.25.25 0 0 1 .25.25v3.646a.25.25 0 0 1-.427.177l-1.38-1.38A7.002 7.002 0 0 1 1.05 8.84a.75.75 0 0 1 .656-.834ZM8 2.5a5.487 5.487 0 0 0-4.131 1.869l1.204 1.204A.25.25 0 0 1 4.896 6H1.25A.25.25 0 0 1 1 5.75V2.104a.25.25 0 0 1 .427-.177l1.38 1.38A7.002 7.002 0 0 1 14.95 7.16a.75.75 0 0 1-1.49.178A5.5 5.5 0 0 0 8 2.5Z" fill="currentColor"/>
            </svg>
            <span>更新于 {{ formatDate(blog.updatedAt) }}</span>
          </div>
        </div>

        <!-- 统计数据 -->
        <div class="article-stats">
          <div class="stat-item">
            <svg width="16" height="16" viewBox="0 0 16 16">
              <path d="M8 16c4.418 0 8-3.582 8-8s-3.582-8-8-8-8 3.582-8 8 3.582 8 8 8ZM1.5 8a6.5 6.5 0 1 0 13 0 6.5 6.5 0 0 0-13 0Zm4.879-2.773 4.264 2.559a.25.25 0 0 1 0 .428l-4.264 2.559A.25.25 0 0 1 6 10.559V5.442a.25.25 0 0 1 .379-.215Z" fill="currentColor"/>
            </svg>
            <span>{{ blog.views || 0 }} 浏览</span>
          </div>
          
          <div class="stat-item">
            <svg width="16" height="16" viewBox="0 0 16 16">
              <path d="m8 14.25.345.666a.75.75 0 0 1-.69 0l-.008-.004-.018-.01a7.152 7.152 0 0 1-.31-.17 22.055 22.055 0 0 1-3.434-2.414C2.045 10.731 0 8.35 0 5.5 0 2.836 2.086 1 4.25 1 5.797 1 7.153 1.802 8 3.02 8.847 1.802 10.203 1 11.75 1 13.914 1 16 2.836 16 5.5c0 2.85-2.045 5.231-3.885 6.818a22.066 22.066 0 0 1-3.744 2.584l-.018.01-.006.003h-.002ZM4.25 2.5c-1.336 0-2.75 1.164-2.75 3 0 2.15 1.58 4.144 3.365 5.682A20.58 20.58 0 0 0 8 13.393a20.58 20.58 0 0 0 3.135-2.211C12.92 9.644 14.5 7.65 14.5 5.5c0-1.836-1.414-3-2.75-3-1.373 0-2.609.986-3.029 2.456a.749.749 0 0 1-1.442 0C6.859 3.486 5.623 2.5 4.25 2.5Z" fill="currentColor"/>
            </svg>
            <span>{{ blog.likes || 0 }} 点赞</span>
          </div>
          
          <div class="stat-item">
            <svg width="16" height="16" viewBox="0 0 16 16">
              <path d="M1.75 1h8.5c.966 0 1.75.784 1.75 1.75v5.5A1.75 1.75 0 0 1 10.25 10H7.061l-2.574 2.573A1.458 1.458 0 0 1 2 11.543V10h-.25A1.75 1.75 0 0 1 0 8.25v-5.5C0 1.784.784 1 1.75 1ZM1.5 2.75v5.5c0 .138.112.25.25.25h1a.75.75 0 0 1 .75.75v2.19l2.72-2.72a.749.749 0 0 1 .53-.22h3.5a.25.25 0 0 0 .25-.25v-5.5a.25.25 0 0 0-.25-.25h-8.5a.25.25 0 0 0-.25.25Zm13 2a.25.25 0 0 0-.25-.25h-.5a.75.75 0 0 1 0-1.5h.5c.966 0 1.75.784 1.75 1.75v5.5A1.75 1.75 0 0 1 14.25 12H14v1.543a1.458 1.458 0 0 1-2.487 1.03L9.22 12.28a.749.749 0 0 1 .326-1.275.749.749 0 0 1 .734.215l2.22 2.22v-2.19a.75.75 0 0 1 .75-.75h1a.25.25 0 0 0 .25-.25Z" fill="currentColor"/>
            </svg>
            <span>{{ blog.commentsCount || 0 }} 评论</span>
          </div>
        </div>

        <!-- 标签 -->
        <div v-if="blog.tags" class="article-tags">
          <span 
            v-for="tag in parseTags(blog.tags)" 
            :key="tag" 
            class="tag"
          >
            <svg width="16" height="16" viewBox="0 0 16 16">
              <path d="M1 7.775V2.75C1 1.784 1.784 1 2.75 1h5.025c.464 0 .91.184 1.238.513l6.25 6.25a1.75 1.75 0 0 1 0 2.474l-5.026 5.026a1.75 1.75 0 0 1-2.474 0l-6.25-6.25A1.752 1.752 0 0 1 1 7.775Zm1.5 0c0 .066.026.13.073.177l6.25 6.25a.25.25 0 0 0 .354 0l5.025-5.025a.25.25 0 0 0 0-.354l-6.25-6.25a.25.25 0 0 0-.177-.073H2.75a.25.25 0 0 0-.25.25ZM6 5a1 1 0 1 1 0 2 1 1 0 0 1 0-2Z" fill="currentColor"/>
            </svg>
            {{ tag }}
          </span>
        </div>
      </header>

      <!-- 文章内容 -->
      <div class="article-content">
        <div class="content-wrapper" v-html="renderContent(blog.content)"></div>
      </div>

      <!-- 文章底部 -->
      <footer class="article-footer">
        <div class="footer-actions">
          <button 
            class="action-button like-button" 
            :class="{ 'liked': liked }"
            @click="toggleLike"
          >
            <svg width="20" height="20" viewBox="0 0 16 16">
              <!-- 已点赞时显示实心图标 -->
              <path v-if="liked" d="M7.655 14.916v-.001h-.002l-.006-.003-.018-.01a22.066 22.066 0 0 1-3.744-2.584C2.045 10.731 0 8.35 0 5.5 0 2.836 2.086 1 4.25 1 5.797 1 7.153 1.802 8 3.02 8.847 1.802 10.203 1 11.75 1 13.914 1 16 2.836 16 5.5c0 2.85-2.045 5.231-3.885 6.818a22.066 22.066 0 0 1-3.744 2.584l-.018.01-.006.003h-.002ZM4.25 2.5c-1.336 0-2.75 1.164-2.75 3 0 2.15 1.58 4.144 3.365 5.682A20.58 20.58 0 0 0 8 13.393a20.58 20.58 0 0 0 3.135-2.211C12.92 9.644 14.5 7.65 14.5 5.5c0-1.836-1.414-3-2.75-3-1.373 0-2.609.986-3.029 2.456a.749.749 0 0 1-1.442 0C6.859 3.486 5.623 2.5 4.25 2.5Z" fill="currentColor"/>
              <!-- 未点赞时显示空心图标 -->
              <path v-else d="m8 14.25.345.666a.75.75 0 0 1-.69 0l-.008-.004-.018-.01a7.152 7.152 0 0 1-.31-.17 22.055 22.055 0 0 1-3.434-2.414C2.045 10.731 0 8.35 0 5.5 0 2.836 2.086 1 4.25 1 5.797 1 7.153 1.802 8 3.02 8.847 1.802 10.203 1 11.75 1 13.914 1 16 2.836 16 5.5c0 2.85-2.045 5.231-3.885 6.818a22.066 22.066 0 0 1-3.744 2.584l-.018.01-.006.003h-.002ZM4.25 2.5c-1.336 0-2.75 1.164-2.75 3 0 2.15 1.58 4.144 3.365 5.682A20.58 20.58 0 0 0 8 13.393a20.58 20.58 0 0 0 3.135-2.211C12.92 9.644 14.5 7.65 14.5 5.5c0-1.836-1.414-3-2.75-3-1.373 0-2.609.986-3.029 2.456a.749.749 0 0 1-1.442 0C6.859 3.486 5.623 2.5 4.25 2.5Z" fill="currentColor"/>
            </svg>
            <span>{{ liked ? '取消点赞' : '点赞' }}</span>
          </button>
          
          <button class="action-button share-button" @click="shareArticle">
            <svg width="20" height="20" viewBox="0 0 16 16">
              <path d="M7.775 3.275a.75.75 0 0 0 1.06 1.06l1.25-1.25a2 2 0 1 1 2.83 2.83l-2.5 2.5a2 2 0 0 1-2.83 0 .75.75 0 0 0-1.06 1.06 3.5 3.5 0 0 0 4.95 0l2.5-2.5a3.5 3.5 0 0 0-4.95-4.95l-1.25 1.25Zm-4.69 9.64a2 2 0 0 1 0-2.83l2.5-2.5a2 2 0 0 1 2.83 0 .75.75 0 0 0 1.06-1.06 3.5 3.5 0 0 0-4.95 0l-2.5 2.5a3.5 3.5 0 0 0 4.95 4.95l1.25-1.25a.75.75 0 0 0-1.06-1.06l-1.25 1.25a2 2 0 0 1-2.83 0Z" fill="currentColor"/>
            </svg>
            <span>分享</span>
          </button>
        </div>

        <div class="author-card" @click="goToAuthorProfile" :class="{ 'clickable': blog.authorId }">
          <div class="author-avatar">
            <img 
              v-if="blog.authorAvatar" 
              :src="blog.authorAvatar" 
              :alt="blog.authorName"
              class="avatar-image"
            />
            <svg v-else width="48" height="48" viewBox="0 0 16 16">
              <path d="M10.561 8.073a6.005 6.005 0 0 1 3.432 5.142.75.75 0 1 1-1.498.07 4.5 4.5 0 0 0-8.99 0 .75.75 0 0 1-1.498-.07 6.004 6.004 0 0 1 3.431-5.142 3.999 3.999 0 1 1 5.123 0ZM10.5 5a2.5 2.5 0 1 0-5 0 2.5 2.5 0 0 0 5 0Z" fill="currentColor"/>
            </svg>
          </div>
          <div class="author-info">
            <h4>作者</h4>
            <p>{{ blog.authorName || '未知作者' }}</p>
          </div>
          <svg class="arrow-icon" width="16" height="16" viewBox="0 0 16 16">
            <path d="M6.22 3.22a.75.75 0 0 1 1.06 0l4.25 4.25a.75.75 0 0 1 0 1.06l-4.25 4.25a.751.751 0 0 1-1.042-.018.751.751 0 0 1-.018-1.042L9.94 8 6.22 4.28a.75.75 0 0 1 0-1.06Z" fill="currentColor"/>
          </svg>
        </div>
      </footer>

      <!-- 评论区 -->
      <section class="comments-section">
        <h3 class="comments-title">
          <svg width="20" height="20" viewBox="0 0 16 16">
            <path d="M1.75 1h8.5c.966 0 1.75.784 1.75 1.75v5.5A1.75 1.75 0 0 1 10.25 10H7.061l-2.574 2.573A1.458 1.458 0 0 1 2 11.543V10h-.25A1.75 1.75 0 0 1 0 8.25v-5.5C0 1.784.784 1 1.75 1ZM1.5 2.75v5.5c0 .138.112.25.25.25h1a.75.75 0 0 1 .75.75v2.19l2.72-2.72a.749.749 0 0 1 .53-.22h3.5a.25.25 0 0 0 .25-.25v-5.5a.25.25 0 0 0-.25-.25h-8.5a.25.25 0 0 0-.25.25Zm13 2a.25.25 0 0 0-.25-.25h-.5a.75.75 0 0 1 0-1.5h.5c.966 0 1.75.784 1.75 1.75v5.5A1.75 1.75 0 0 1 14.25 12H14v1.543a1.458 1.458 0 0 1-2.487 1.03L9.22 12.28a.749.749 0 0 1 .326-1.275.749.749 0 0 1 .734.215l2.22 2.22v-2.19a.75.75 0 0 1 .75-.75h1a.25.25 0 0 0 .25-.25Z" fill="currentColor"/>
          </svg>
          评论 {{ comments.length }}
        </h3>

        <!-- 评论加载状态 -->
        <div v-if="commentsLoading" class="comments-loading">
          <div class="loading-spinner-small"></div>
          <span>加载评论中...</span>
        </div>

        <!-- 评论列表 -->
        <div v-else-if="comments.length > 0" class="comments-list">
          <CommentBox
            v-for="comment in comments"
            :key="comment.id"
            :comment="comment"
            :current-user-id="currentUserId"
            @delete="handleDeleteComment"
            @like="handleLikeComment"
            @reply="handleReplyComment"
          />
        </div>

        <!-- 无评论提示 -->
        <div v-else class="no-comments">
          <svg width="48" height="48" viewBox="0 0 16 16">
            <path d="M1.75 1h8.5c.966 0 1.75.784 1.75 1.75v5.5A1.75 1.75 0 0 1 10.25 10H7.061l-2.574 2.573A1.458 1.458 0 0 1 2 11.543V10h-.25A1.75 1.75 0 0 1 0 8.25v-5.5C0 1.784.784 1 1.75 1ZM1.5 2.75v5.5c0 .138.112.25.25.25h1a.75.75 0 0 1 .75.75v2.19l2.72-2.72a.749.749 0 0 1 .53-.22h3.5a.25.25 0 0 0 .25-.25v-5.5a.25.25 0 0 0-.25-.25h-8.5a.25.25 0 0 0-.25.25Z" fill="currentColor"/>
          </svg>
          <p>还没有评论，快来抢沙发吧！</p>
        </div>
      </section>

      <!-- 底部留出空间给 BottomComment 组件 -->
      <div class="bottom-spacer"></div>
    </article>

    <!-- 底部评论输入框（固定在屏幕底部） -->
    <BottomComment
      v-if="blog"
      :reply-target="replyTarget"
      @send="handleSendComment"
      @cancel-reply="handleCancelReply"
    />
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, computed } from 'vue'
import { useRouter, useRoute } from 'vue-router'
import { marked } from 'marked'
import DOMPurify from 'dompurify'
import hljs from 'highlight.js/lib/core'
// 导入常用语言支持
import javascript from 'highlight.js/lib/languages/javascript'
import typescript from 'highlight.js/lib/languages/typescript'
import python from 'highlight.js/lib/languages/python'
import java from 'highlight.js/lib/languages/java'
import cpp from 'highlight.js/lib/languages/cpp'
import csharp from 'highlight.js/lib/languages/csharp'
import css from 'highlight.js/lib/languages/css'
import xml from 'highlight.js/lib/languages/xml' // HTML
import json from 'highlight.js/lib/languages/json'
import sql from 'highlight.js/lib/languages/sql'
import bash from 'highlight.js/lib/languages/bash'
import markdown from 'highlight.js/lib/languages/markdown'
// 导入 GitHub Dark 主题样式
import 'highlight.js/styles/github-dark.css'

// 注册语言
hljs.registerLanguage('javascript', javascript)
hljs.registerLanguage('typescript', typescript)
hljs.registerLanguage('python', python)
hljs.registerLanguage('java', java)
hljs.registerLanguage('cpp', cpp)
hljs.registerLanguage('csharp', csharp)
hljs.registerLanguage('css', css)
hljs.registerLanguage('html', xml)
hljs.registerLanguage('xml', xml)
hljs.registerLanguage('json', json)
hljs.registerLanguage('sql', sql)
hljs.registerLanguage('bash', bash)
hljs.registerLanguage('sh', bash)
hljs.registerLanguage('markdown', markdown)

import CommentBox from '@/components/CommentBox.vue'
import BottomComment from '@/components/BottomComment.vue'

const router = useRouter()
const route = useRoute()

// 创建自定义渲染器处理代码高亮
const renderer = new marked.Renderer()

renderer.code = function({ text, lang }: { text: string; lang?: string }) {
  // 如果指定了语言且支持该语言，使用 highlight.js 高亮
  if (lang && hljs.getLanguage(lang)) {
    try {
      const highlighted = hljs.highlight(text, { language: lang }).value
      return `<pre><code class="hljs language-${lang}">${highlighted}</code></pre>`
    } catch (err) {
      console.error('代码高亮失败:', err)
    }
  }
  // 否则使用默认渲染（转义 HTML）
  const escaped = text
    .replace(/&/g, '&amp;')
    .replace(/</g, '&lt;')
    .replace(/>/g, '&gt;')
    .replace(/"/g, '&quot;')
    .replace(/'/g, '&#39;')
  return `<pre><code>${escaped}</code></pre>`
}

// 配置 marked 选项
marked.setOptions({
  renderer: renderer,
  breaks: true,
  gfm: true
})

// 接口定义
interface Comment {
  id: string
  userName: string
  content: string
  createdAt: string
  likes: number
  userId: string
  replies?: Comment[]
}

interface UserData {
  id: string
  name: string
  email: string
  password: string
}

// 响应式数据
const blog = ref<any>(null)
const loading = ref(true)
const error = ref('')
const liked = ref(false)

// 评论相关
const comments = ref<Comment[]>([])
const commentsLoading = ref(false)
const replyTarget = ref<Comment | null>(null)
const currentUserId = ref<string>('')

// LocalStorage 键名
const LIKED_BLOGS_KEY = 'likedBlogs'

// 组件挂载时加载数据
onMounted(async () => {
  // 获取当前用户ID
  const user = getCurrentUser()
  if (user) {
    currentUserId.value = user.id
  }
  
  // 加载博客
  await loadBlog()
  
  // 加载评论
  await loadComments()
})

// 判断当前用户是否是文章作者
const isAuthor = computed(() => {
  if (!blog.value || !currentUserId.value) return false
  return blog.value.authorId === currentUserId.value
})

// 检查用户是否已点赞该博客
const checkIfLiked = (blogId: string): boolean => {
  try {
    const likedBlogs = localStorage.getItem(LIKED_BLOGS_KEY)
    if (!likedBlogs) return false
    const likedList: string[] = JSON.parse(likedBlogs)
    return likedList.includes(blogId)
  } catch (err) {
    console.error('检查点赞状态失败:', err)
    return false
  }
}

// 添加博客到已点赞列表
const addToLikedBlogs = (blogId: string): void => {
  try {
    const likedBlogs = localStorage.getItem(LIKED_BLOGS_KEY)
    const likedList: string[] = likedBlogs ? JSON.parse(likedBlogs) : []
    if (!likedList.includes(blogId)) {
      likedList.push(blogId)
      localStorage.setItem(LIKED_BLOGS_KEY, JSON.stringify(likedList))
    }
  } catch (err) {
    console.error('保存点赞状态失败:', err)
  }
}

// 从已点赞列表中移除博客
const removeFromLikedBlogs = (blogId: string): void => {
  try {
    const likedBlogs = localStorage.getItem(LIKED_BLOGS_KEY)
    if (!likedBlogs) return
    const likedList: string[] = JSON.parse(likedBlogs)
    const filteredList = likedList.filter(id => id !== blogId)
    localStorage.setItem(LIKED_BLOGS_KEY, JSON.stringify(filteredList))
  } catch (err) {
    console.error('移除点赞状态失败:', err)
  }
}

// 加载文章详情
const loadBlog = async () => {
  loading.value = true
  error.value = ''
  
  try {
    const blogId = route.params.id as string
    
    if (!blogId) {
      throw new Error('文章ID无效')
    }

    // 调用后端API获取单篇文章(包含作者名称)
    const response = await fetch(`/api/Blog?id=${blogId}`)
    
    if (!response.ok) {
      if (response.status === 404) {
        throw new Error('文章不存在')
      }
      throw new Error('获取文章失败')
    }
    
    const data = await response.json()
    blog.value = data
    
    console.log('加载的博客数据:', data)
    
    // 检查用户是否已点赞
    liked.value = checkIfLiked(blogId)
    
    // 增加浏览量
    await incrementViews(blogId)
    
  } catch (err) {
    error.value = err instanceof Error ? err.message : '加载失败,请重试'
    console.error('加载文章失败:', err)
  } finally {
    loading.value = false
  }
}

// 增加浏览量
const incrementViews = async (blogId: string) => {
  try {
    const response = await fetch(`/api/Blog/${blogId}/view`, {
      method: 'POST'
    })
    if (response.ok) {
      const data = await response.json()
      if (blog.value) {
        blog.value.views = data.views
      }
    }
  } catch (err) {
    console.error('增加浏览量失败:', err)
  }
}

// 解析标签
const parseTags = (tags: string): string[] => {
  if (!tags) return []
  return tags.split(',').map(t => t.trim()).filter(t => t)
}

// 格式化日期
const formatDate = (dateString: string): string => {
  const date = new Date(dateString)
  return date.toLocaleDateString('zh-CN', {
    year: 'numeric',
    month: 'long',
    day: 'numeric',
    hour: '2-digit',
    minute: '2-digit'
  })
}

// 渲染文章内容(使用 marked 和 DOMPurify)
const renderContent = (content: string): string => {
  if (!content) return ''
  
  try {
    // 使用 marked 解析 Markdown
    const rawHtml = marked.parse(content) as string
    
    // 使用 DOMPurify 净化 HTML，防止 XSS 攻击
    // 对于代码高亮，我们需要保留所有 span 标签和 class 属性
    const cleanHtml = DOMPurify.sanitize(rawHtml, {
      ALLOWED_TAGS: ['p', 'br', 'strong', 'em', 'u', 's', 'code', 'pre', 'a', 'ul', 'ol', 'li', 'h1', 'h2', 'h3', 'h4', 'h5', 'h6', 'blockquote', 'hr', 'table', 'thead', 'tbody', 'tr', 'th', 'td', 'img', 'div', 'span'],
      ALLOWED_ATTR: ['href', 'target', 'rel', 'class', 'src', 'alt', 'title', 'style'],
      // 允许所有以 hljs- 或 language- 开头的 class
      ALLOW_DATA_ATTR: false
    })
    
    return cleanHtml
  } catch (error) {
    console.error('Markdown 渲染失败:', error)
    return `<p class="error">内容渲染失败</p>`
  }
}

// 点赞/取消点赞功能
const toggleLike = async () => {
  if (!blog.value) return
  
  const newLikedState = !liked.value
  
  try {
    const response = await fetch(`/api/Blog/${blog.value.id}/like`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({
        isLiked: newLikedState
      })
    })
    
    if (response.ok) {
      const data = await response.json()
      blog.value.likes = data.likes
      liked.value = newLikedState
      
      // 更新 localStorage
      if (newLikedState) {
        addToLikedBlogs(blog.value.id)
      } else {
        removeFromLikedBlogs(blog.value.id)
      }
    }
  } catch (err) {
    console.error('点赞操作失败:', err)
    alert('操作失败，请稍后重试')
  }
}

// 分享文章
const shareArticle = async () => {
  if (navigator.share) {
    try {
      await navigator.share({
        title: blog.value?.title,
        text: blog.value?.content?.substring(0, 100),
        url: window.location.href
      })
    } catch (err) {
      console.log('分享失败', err)
    }
  } else {
    // 复制链接到剪贴板
    await navigator.clipboard.writeText(window.location.href)
    alert('链接已复制到剪贴板!')
  }
}

// 返回上一页
const goBack = () => {
  // 如果有历史记录，返回上一页；否则返回博客列表
  if (window.history.length > 1) {
    router.back()
  } else {
    router.push('/blogs')
  }
}

// 编辑博客
const editBlog = () => {
  if (!blog.value) return
  
  // 将博客数据存储到 localStorage 以便在编辑页面使用
  const blogToEdit = {
    id: blog.value.id,
    title: blog.value.title,
    content: blog.value.content,
    tags: blog.value.tags,
    authorId: blog.value.authorId,
    authorName: blog.value.authorName
  }
  
  localStorage.setItem('editingBlog', JSON.stringify(blogToEdit))
  
  // 跳转到创建/编辑页面
  router.push('/blog/create')
}

// 删除博客
const deleteBlog = async () => {
  if (!blog.value) return
  
  const confirmed = confirm('确定要删除这篇文章吗？此操作无法撤销！')
  if (!confirmed) return
  
  try {
    const response = await fetch(`/api/Blog/${blog.value.id}`, {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json'
      }
    })
    
    if (!response.ok) {
      const errorText = await response.text()
      throw new Error(errorText || '删除失败')
    }
    
    alert('文章已删除')
    router.push('/blogs')
  } catch (err) {
    console.error('删除博客失败:', err)
    alert(`删除失败: ${err instanceof Error ? err.message : '未知错误'}`)
  }
}

// 跳转到作者个人资料页面
const goToAuthorProfile = () => {
  if (!blog.value || !blog.value.authorId) return
  
  const currentUserId = localStorage.getItem('userId')
  
  // 如果是当前用户,跳转到自己的 profile
  if (blog.value.authorId === currentUserId) {
    router.push('/profile')
  } else {
    // 否则跳转到指定用户的 profile
    router.push(`/profile/${blog.value.authorId}`)
  }
}

// 获取当前用户信息
const getCurrentUser = (): UserData | null => {
  try {
    const isLoggedIn = localStorage.getItem('isLoggedIn') === 'true'
    if (!isLoggedIn) return null
    
    const userId = localStorage.getItem('userId')
    const userName = localStorage.getItem('userName')
    const userEmail = localStorage.getItem('userEmail')
    
    if (!userId || !userName) return null
    
    return {
      id: userId,
      name: userName,
      email: userEmail || '',
      password: '' // 密码不从localStorage读取
    }
  } catch {
    return null
  }
}

// 加载评论
const loadComments = async () => {
  if (!blog.value) return
  
  commentsLoading.value = true
  
  try {
    const response = await fetch(`/api/Comment/blog/${blog.value.id}`)
    
    if (response.ok) {
      const data = await response.json()
      comments.value = data
      console.log('加载的评论:', data)
    } else {
      console.error('加载评论失败:', response.statusText)
    }
  } catch (err) {
    console.error('加载评论失败:', err)
  } finally {
    commentsLoading.value = false
  }
}

// 发送评论
const handleSendComment = async (content: string, parentCommentId?: string) => {
  const user = getCurrentUser()
  
  if (!user) {
    alert('请先登录后再评论')
    return
  }
  
  if (!blog.value) return
  
  try {
    const response = await fetch('/api/Comment/create', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({
        content,
        blogId: blog.value.id,
        userId: user.id,
        parentCommentId: parentCommentId || null
      })
    })
    
    if (response.ok) {
      // 重新加载评论
      await loadComments()
      
      // 取消回复状态
      replyTarget.value = null
      
      // 更新博客评论数
      if (blog.value) {
        blog.value.commentsCount = (blog.value.commentsCount || 0) + 1
      }
    } else {
      const errorData = await response.json()
      alert(`发送评论失败: ${errorData.message || '未知错误'}`)
    }
  } catch (err) {
    console.error('发送评论失败:', err)
    alert('发送评论失败，请稍后重试')
  }
}

// 删除评论
const handleDeleteComment = async (commentId: string) => {
  const user = getCurrentUser()
  
  if (!user) {
    alert('请先登录')
    return
  }
  
  try {
    const response = await fetch(`/api/Comment/${commentId}?userId=${user.id}`, {
      method: 'DELETE'
    })
    
    if (response.ok) {
      // 重新加载评论
      await loadComments()
      
      // 更新博客评论数
      if (blog.value && blog.value.commentsCount > 0) {
        blog.value.commentsCount -= 1
      }
    } else {
      const errorData = await response.json()
      alert(`删除评论失败: ${errorData.message || '未知错误'}`)
    }
  } catch (err) {
    console.error('删除评论失败:', err)
    alert('删除评论失败，请稍后重试')
  }
}

// 点赞评论
const handleLikeComment = async (commentId: string) => {
  try {
    const response = await fetch(`/api/Comment/${commentId}/like`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      }
    })
    
    if (response.ok) {
      // 重新加载评论以更新点赞数
      await loadComments()
    } else {
      console.error('点赞评论失败')
    }
  } catch (err) {
    console.error('点赞评论失败:', err)
  }
}

// 回复评论
const handleReplyComment = (comment: Comment) => {
  replyTarget.value = comment
}

// 取消回复
const handleCancelReply = () => {
  replyTarget.value = null
}
</script>

<style scoped>
.blog-detail-view {
  min-height: 100vh;
  background: linear-gradient(135deg, #0d1117 0%, #161b22 100%);
  color: #c9d1d9;
  padding: 2rem;
}

/* 加载和错误状态 */
.loading-container,
.error-container {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  min-height: 60vh;
  text-align: center;
}

.loading-spinner {
  width: 64px;
  height: 64px;
  border: 4px solid #30363d;
  border-top-color: #58a6ff;
  border-radius: 50%;
  animation: spin 1s linear infinite;
  margin-bottom: 1rem;
}

@keyframes spin {
  to { transform: rotate(360deg); }
}

.error-container svg {
  color: #f85149;
  margin-bottom: 1.5rem;
}

.error-container h2 {
  margin: 0 0 1.5rem 0;
  color: #c9d1d9;
}

.retry-button,
.back-button {
  margin: 0.5rem;
  padding: 0.75rem 1.5rem;
  border: none;
  border-radius: 6px;
  font-size: 1rem;
  cursor: pointer;
  transition: all 0.3s ease;
}

.retry-button {
  background: #238636;
  color: white;
}

.retry-button:hover {
  background: #2ea043;
}

.back-button {
  background: #21262d;
  color: #c9d1d9;
  border: 1px solid #30363d;
}

.back-button:hover {
  background: #30363d;
}

/* 文章内容 */
.blog-article {
  max-width: 900px;
  margin: 0 auto;
}

.nav-back-button {
  display: inline-flex;
  align-items: center;
  gap: 0.5rem;
  padding: 0.5rem 1rem;
  background: #21262d;
  border: 1px solid #30363d;
  border-radius: 6px;
  color: #8b949e;
  font-size: 0.9rem;
  cursor: pointer;
  margin-bottom: 2rem;
  transition: all 0.3s ease;
}

.nav-back-button:hover {
  background: #30363d;
  color: #58a6ff;
  border-color: #58a6ff;
}

/* 文章头部 */
.article-header {
  margin-bottom: 3rem;
  padding-bottom: 2rem;
  border-bottom: 1px solid #30363d;
}

.title-container {
  display: flex;
  align-items: flex-start;
  justify-content: space-between;
  gap: 1rem;
  margin-bottom: 1.5rem;
}

.article-title {
  flex: 1;
  font-size: 2.5rem;
  font-weight: 800;
  margin: 0;
  line-height: 1.2;
  color: #ffffff;
  letter-spacing: -0.02em;
}

.author-actions {
  display: flex;
  align-items: center;
  gap: 0.75rem;
  flex-shrink: 0;
}

.edit-blog-button,
.delete-blog-button {
  flex-shrink: 0;
  display: inline-flex;
  align-items: center;
  gap: 0.5rem;
  padding: 0.5rem 1rem;
  border: none;
  border-radius: 6px;
  color: #ffffff;
  font-size: 0.9rem;
  font-weight: 500;
  cursor: pointer;
  transition: all 0.3s ease;
}

.edit-blog-button {
  background: #238636;
}

.edit-blog-button:hover {
  background: #2ea043;
  transform: translateY(-2px);
  box-shadow: 0 4px 8px rgba(35, 134, 54, 0.3);
}

.delete-blog-button {
  background: #da3633;
}

.delete-blog-button:hover {
  background: #f85149;
  transform: translateY(-2px);
  box-shadow: 0 4px 8px rgba(218, 54, 51, 0.3);
}

.edit-blog-button svg,
.delete-blog-button svg {
  flex-shrink: 0;
}

.article-meta {
  display: flex;
  flex-wrap: wrap;
  gap: 1.5rem;
  margin-bottom: 1.5rem;
}

.meta-item {
  display: flex;
  align-items: center;
  gap: 0.5rem;
  color: #8b949e;
  font-size: 0.95rem;
}

.meta-item svg {
  color: #58a6ff;
}

/* 统计数据 */
.article-stats {
  display: flex;
  flex-wrap: wrap;
  gap: 1.5rem;
  margin-top: 1rem;
  padding: 1rem;
  background: #0d1117;
  border-radius: 6px;
  border: 1px solid #21262d;
}

.stat-item {
  display: flex;
  align-items: center;
  gap: 0.5rem;
  color: #8b949e;
  font-size: 0.95rem;
}

.stat-item svg {
  color: #58a6ff;
}

.article-tags {
  display: flex;
  flex-wrap: wrap;
  gap: 0.75rem;
}

.tag {
  display: inline-flex;
  align-items: center;
  gap: 0.4rem;
  padding: 0.4rem 0.8rem;
  background: #21262d;
  border: 1px solid #30363d;
  border-radius: 6px;
  color: #58a6ff;
  font-size: 0.85rem;
  transition: all 0.3s ease;
}

.tag:hover {
  background: #30363d;
  border-color: #58a6ff;
}

/* 文章内容 */
.article-content {
  margin-bottom: 3rem;
}

.content-wrapper {
  background: #0d1117;
  border: 1px solid #30363d;
  border-radius: 12px;
  padding: 2.5rem;
  line-height: 1.8;
  font-size: 1.05rem;
}

.content-wrapper :deep(p) {
  margin: 1.5rem 0;
  color: #c9d1d9;
}

.content-wrapper :deep(strong) {
  color: #ffffff;
  font-weight: 600;
}

.content-wrapper :deep(em) {
  color: #7ee787;
  font-style: italic;
}

/* 内联代码（非代码块中的） */
.content-wrapper :deep(code:not(pre code)) {
  background: #161b22;
  border: 1px solid #30363d;
  padding: 0.2rem 0.4rem;
  border-radius: 4px;
  font-family: 'Courier New', monospace;
  font-size: 0.9em;
  color: #ff7b72;
}

/* 代码块 */
.content-wrapper :deep(pre) {
  background: #161b22 !important;
  border: 1px solid #30363d;
  border-radius: 6px;
  padding: 1rem;
  overflow-x: auto;
  margin: 1.5rem 0;
}

/* 代码块中的代码 - 保留 highlight.js 的样式 */
.content-wrapper :deep(pre code) {
  background: transparent !important;
  border: none;
  padding: 0;
  font-size: 0.9rem;
  line-height: 1.6;
  /* 不设置 color，让 highlight.js 的样式生效 */
}

/* 确保 highlight.js 的 span 标签样式不被覆盖 */
.content-wrapper :deep(pre code .hljs-keyword),
.content-wrapper :deep(pre code .hljs-selector-tag),
.content-wrapper :deep(pre code .hljs-literal),
.content-wrapper :deep(pre code .hljs-section),
.content-wrapper :deep(pre code .hljs-link) {
  font-weight: inherit;
}

/* 标题 */
.content-wrapper :deep(h1) {
  font-size: 2rem;
  font-weight: 700;
  margin: 2rem 0 1rem 0;
  padding-bottom: 0.5rem;
  border-bottom: 1px solid #30363d;
  color: #ffffff;
}

.content-wrapper :deep(h2) {
  font-size: 1.6rem;
  font-weight: 700;
  margin: 1.8rem 0 0.8rem 0;
  padding-bottom: 0.3rem;
  border-bottom: 1px solid #30363d;
  color: #ffffff;
}

.content-wrapper :deep(h3) {
  font-size: 1.4rem;
  font-weight: 600;
  margin: 1.5rem 0 0.8rem 0;
  color: #ffffff;
}

.content-wrapper :deep(h4) {
  font-size: 1.2rem;
  font-weight: 600;
  margin: 1.2rem 0 0.6rem 0;
  color: #c9d1d9;
}

.content-wrapper :deep(h5),
.content-wrapper :deep(h6) {
  font-size: 1rem;
  font-weight: 600;
  margin: 1rem 0 0.5rem 0;
  color: #c9d1d9;
}

/* 链接 */
.content-wrapper :deep(a) {
  color: #58a6ff;
  text-decoration: none;
  border-bottom: 1px solid transparent;
  transition: border-color 0.3s ease;
}

.content-wrapper :deep(a:hover) {
  border-bottom-color: #58a6ff;
}

/* 列表 */
.content-wrapper :deep(ul),
.content-wrapper :deep(ol) {
  margin: 1rem 0;
  padding-left: 2rem;
}

.content-wrapper :deep(li) {
  margin: 0.5rem 0;
  line-height: 1.8;
}

.content-wrapper :deep(ul) {
  list-style-type: disc;
}

.content-wrapper :deep(ol) {
  list-style-type: decimal;
}

.content-wrapper :deep(li > ul),
.content-wrapper :deep(li > ol) {
  margin: 0.25rem 0;
}

/* 引用 */
.content-wrapper :deep(blockquote) {
  margin: 1.5rem 0;
  padding: 0.5rem 1rem;
  border-left: 4px solid #58a6ff;
  background: #161b22;
  color: #8b949e;
}

.content-wrapper :deep(blockquote p) {
  margin: 0.5rem 0;
}

/* 水平线 */
.content-wrapper :deep(hr) {
  border: none;
  border-top: 1px solid #30363d;
  margin: 2rem 0;
}

/* 表格 */
.content-wrapper :deep(table) {
  width: 100%;
  border-collapse: collapse;
  margin: 1.5rem 0;
  background: #0d1117;
  border: 1px solid #30363d;
}

.content-wrapper :deep(th),
.content-wrapper :deep(td) {
  padding: 0.75rem;
  text-align: left;
  border: 1px solid #30363d;
}

.content-wrapper :deep(th) {
  background: #161b22;
  font-weight: 600;
  color: #ffffff;
}

.content-wrapper :deep(tr:hover) {
  background: rgba(48, 54, 61, 0.3);
}

/* 图片 */
.content-wrapper :deep(img) {
  max-width: 100%;
  height: auto;
  border-radius: 8px;
  margin: 1.5rem 0;
  border: 1px solid #30363d;
  display: block;
  transition: all 0.3s ease;
}

.content-wrapper :deep(img:hover) {
  border-color: #58a6ff;
  box-shadow: 0 4px 12px rgba(88, 166, 255, 0.2);
  transform: scale(1.02);
}

/* 文章底部 */
.article-footer {
  padding-top: 2rem;
  border-top: 1px solid #30363d;
}

.footer-actions {
  display: flex;
  gap: 1rem;
  margin-bottom: 2rem;
}

.action-button {
  display: flex;
  align-items: center;
  gap: 0.5rem;
  padding: 0.75rem 1.5rem;
  border: 1px solid #30363d;
  border-radius: 6px;
  background: #21262d;
  color: #c9d1d9;
  font-size: 1rem;
  cursor: pointer;
  transition: all 0.3s ease;
}

.action-button:hover {
  background: #30363d;
  border-color: #58a6ff;
  color: #58a6ff;
  transform: translateY(-2px);
}

.action-button svg {
  transition: all 0.3s ease;
}

.like-button:hover svg {
  fill: #f85149;
  color: #f85149;
}

/* 已点赞状态样式 */
.like-button.liked {
  background: linear-gradient(135deg, #f85149 0%, #da3633 100%);
  border-color: #f85149;
  color: white;
}

.like-button.liked svg {
  fill: white;
  color: white;
}

.like-button.liked:hover {
  background: linear-gradient(135deg, #da3633 0%, #c93230 100%);
  border-color: #c93230;
  color: white;
  transform: translateY(-2px);
}

.author-card {
  display: flex;
  align-items: center;
  gap: 1.5rem;
  padding: 1.5rem;
  background: #0d1117;
  border: 1px solid #30363d;
  border-radius: 12px;
  position: relative;
  transition: all 0.3s ease;
}

.author-card.clickable {
  cursor: pointer;
}

.author-card.clickable:hover {
  background: #161b22;
  border-color: #58a6ff;
  transform: translateY(-2px);
  box-shadow: 0 4px 12px rgba(88, 166, 255, 0.2);
}

.author-avatar {
  width: 64px;
  height: 64px;
  background: #21262d;
  border-radius: 50%;
  display: flex;
  align-items: center;
  justify-content: center;
  flex-shrink: 0;
  overflow: hidden;
  border: 2px solid #30363d;
  transition: all 0.3s ease;
}

.author-card.clickable:hover .author-avatar {
  border-color: #58a6ff;
  transform: scale(1.05);
}

.author-avatar .avatar-image {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.author-avatar svg {
  color: #58a6ff;
}

.author-info {
  flex: 1;
}

.author-info h4 {
  margin: 0 0 0.5rem 0;
  color: #8b949e;
  font-size: 0.9rem;
  font-weight: 500;
}

.author-info p {
  margin: 0;
  color: #c9d1d9;
  font-size: 1.1rem;
  font-weight: 600;
  transition: color 0.3s ease;
}

.author-card.clickable:hover .author-info p {
  color: #58a6ff;
}

.arrow-icon {
  flex-shrink: 0;
  color: #8b949e;
  transition: all 0.3s ease;
  opacity: 0;
}

.author-card.clickable:hover .arrow-icon {
  opacity: 1;
  color: #58a6ff;
  transform: translateX(4px);
}

/* 评论区样式 */
.comments-section {
  margin-top: 3rem;
  padding-top: 2rem;
  border-top: 2px solid #30363d;
}

.comments-title {
  display: flex;
  align-items: center;
  gap: 0.75rem;
  margin: 0 0 2rem 0;
  font-size: 1.5rem;
  font-weight: 700;
  color: #c9d1d9;
}

.comments-title svg {
  color: #58a6ff;
}

.comments-loading {
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 1rem;
  padding: 3rem;
  color: #8b949e;
}

.loading-spinner-small {
  width: 24px;
  height: 24px;
  border: 3px solid #30363d;
  border-top-color: #58a6ff;
  border-radius: 50%;
  animation: spin 1s linear infinite;
}

.comments-list {
  display: flex;
  flex-direction: column;
}

.no-comments {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  padding: 4rem 2rem;
  color: #8b949e;
  text-align: center;
}

.no-comments svg {
  margin-bottom: 1rem;
  opacity: 0.5;
}

.no-comments p {
  margin: 0;
  font-size: 1.1rem;
}

.bottom-spacer {
  height: 120px;
}

/* 响应式设计 */
@media (max-width: 768px) {
  .blog-detail-view {
    padding: 1rem;
  }

  .title-container {
    flex-direction: column;
    align-items: stretch;
  }

  .article-title {
    font-size: 1.8rem;
  }

  .edit-blog-button {
    align-self: flex-end;
    font-size: 0.85rem;
    padding: 0.4rem 0.8rem;
  }

  .content-wrapper {
    padding: 1.5rem;
  }

  .footer-actions {
    flex-direction: column;
  }

  .action-button {
    width: 100%;
    justify-content: center;
  }
  
  .bottom-spacer {
    height: 140px;
  }
}
</style>
