<template>
  <div class="blog-create-view">
    <!-- 页面头部 -->
    <header class="create-header">
      <button @click="goBack" class="nav-back-button">
        <svg width="16" height="16" viewBox="0 0 16 16">
          <path d="M9.78 12.78a.75.75 0 0 1-1.06 0L4.47 8.53a.75.75 0 0 1 0-1.06l4.25-4.25a.751.751 0 0 1 1.042.018.751.751 0 0 1 .018 1.042L6.06 8l3.72 3.72a.75.75 0 0 1 0 1.06Z" fill="currentColor"/>
        </svg>
        返回
      </button>

      <h1 class="page-title">
        <span class="gradient-text">{{ editingBlogId ? '编辑文章' : '创作新文章' }}</span>
      </h1>

      <div class="header-actions">
        <button 
          @click="saveDraft" 
          class="action-button secondary-button"
          :disabled="saving"
        >
          <svg width="16" height="16" viewBox="0 0 16 16">
            <path d="M13.78 4.22a.75.75 0 0 1 0 1.06l-7.25 7.25a.75.75 0 0 1-1.06 0L2.22 9.28a.751.751 0 0 1 .018-1.042.751.751 0 0 1 1.042-.018L6 10.94l6.72-6.72a.75.75 0 0 1 1.06 0Z" fill="currentColor"/>
          </svg>
          保存草稿
        </button>
        <button 
          @click="publishBlog" 
          class="action-button primary-button"
          :disabled="!canPublish || saving"
        >
          <svg width="16" height="16" viewBox="0 0 16 16">
            <path d="M3.5 3.75a.25.25 0 0 1 .25-.25h8.5a.25.25 0 0 1 .25.25v8.5a.25.25 0 0 1-.25.25h-8.5a.25.25 0 0 1-.25-.25v-8.5Z" fill="currentColor"/>
            <path d="M3.5 3.75a.25.25 0 0 1 .25-.25h8.5a.25.25 0 0 1 .25.25v8.5a.25.25 0 0 1-.25.25h-8.5a.25.25 0 0 1-.25-.25v-8.5ZM3.75 2A1.75 1.75 0 0 0 2 3.75v8.5c0 .966.784 1.75 1.75 1.75h8.5A1.75 1.75 0 0 0 14 12.25v-8.5A1.75 1.75 0 0 0 12.25 2h-8.5Z" fill="currentColor"/>
          </svg>
          {{ saving ? (editingBlogId ? '更新中...' : '发布中...') : (editingBlogId ? '更新文章' : '发布文章') }}
        </button>
      </div>
    </header>

    <!-- 编辑区域 -->
    <div class="editor-container">
      <div class="editor-main">
        <!-- 标题输入 -->
        <div class="title-section">
          <input 
            v-model="form.title" 
            type="text" 
            placeholder="请输入文章标题..."
            class="title-input"
            @input="validateForm"
          />
          <div v-if="titleError" class="error-message">
            <svg width="14" height="14" viewBox="0 0 16 16">
              <path d="M2.343 13.657A8 8 0 1 1 13.657 2.343 8 8 0 0 1 2.343 13.657ZM6.03 4.97a.751.751 0 0 0-1.042.018.751.751 0 0 0-.018 1.042L6.94 8 4.97 9.97a.749.749 0 0 0 .326 1.275.749.749 0 0 0 .734-.215L8 9.06l1.97 1.97a.749.749 0 0 0 1.275-.326.749.749 0 0 0-.215-.734L9.06 8l1.97-1.97a.749.749 0 0 0-.326-1.275.749.749 0 0 0-.734.215L8 6.94Z" fill="currentColor"/>
            </svg>
            {{ titleError }}
          </div>
        </div>

        <!-- 标签输入 -->
        <div class="tags-section">
          <div class="tags-input-wrapper">
            <svg width="16" height="16" viewBox="0 0 16 16">
              <path d="M1 7.775V2.75C1 1.784 1.784 1 2.75 1h5.025c.464 0 .91.184 1.238.513l6.25 6.25a1.75 1.75 0 0 1 0 2.474l-5.026 5.026a1.75 1.75 0 0 1-2.474 0l-6.25-6.25A1.752 1.752 0 0 1 1 7.775Zm1.5 0c0 .066.026.13.073.177l6.25 6.25a.25.25 0 0 0 .354 0l5.025-5.025a.25.25 0 0 0 0-.354l-6.25-6.25a.25.25 0 0 0-.177-.073H2.75a.25.25 0 0 0-.25.25ZM6 5a1 1 0 1 1 0 2 1 1 0 0 1 0-2Z" fill="currentColor"/>
            </svg>
            <input 
              v-model="tagInput" 
              type="text" 
              placeholder="添加标签 (按 Enter 确认)"
              class="tags-input"
              @keydown.enter.prevent="addTag"
              @keydown.comma.prevent="addTag"
            />
          </div>
          
          <div v-if="form.tags.length > 0" class="tags-list">
            <span v-for="(tag, index) in form.tags" :key="index" class="tag">
              {{ tag }}
              <button @click="removeTag(index)" class="tag-remove">
                <svg width="12" height="12" viewBox="0 0 16 16">
                  <path d="M3.72 3.72a.75.75 0 0 1 1.06 0L8 6.94l3.22-3.22a.749.749 0 0 1 1.275.326.749.749 0 0 1-.215.734L9.06 8l3.22 3.22a.749.749 0 0 1-.326 1.275.749.749 0 0 1-.734-.215L8 9.06l-3.22 3.22a.751.751 0 0 1-1.042-.018.751.751 0 0 1-.018-1.042L6.94 8 3.72 4.78a.75.75 0 0 1 0-1.06Z" fill="currentColor"/>
                </svg>
              </button>
            </span>
          </div>
          <p class="tags-hint">支持按 Enter 或逗号分隔添加标签</p>
        </div>

        <!-- 内容编辑器 -->
        <div class="editor-section">
          <div class="editor-toolbar">
            <button @click="insertFormat('**', '**')" class="toolbar-button" title="粗体">
              <svg width="16" height="16" viewBox="0 0 16 16">
                <path d="M4 2a1 1 0 0 0-1 1v10a1 1 0 0 0 1 1h5.5a3.5 3.5 0 0 0 1.852-6.47A3.5 3.5 0 0 0 8.5 2H4Zm4.5 5a1.5 1.5 0 1 1 0-3H5v3h3.5ZM5 9v3h4.5a1.5 1.5 0 0 1 0 3H5V9Z" fill="currentColor"/>
              </svg>
            </button>
            <button @click="insertFormat('*', '*')" class="toolbar-button" title="斜体">
              <svg width="16" height="16" viewBox="0 0 16 16">
                <path d="M6 2.75A.75.75 0 0 1 6.75 2h6.5a.75.75 0 0 1 0 1.5h-2.505l-3.858 9H9.25a.75.75 0 0 1 0 1.5h-6.5a.75.75 0 0 1 0-1.5h2.505l3.858-9H6.75A.75.75 0 0 1 6 2.75Z" fill="currentColor"/>
              </svg>
            </button>
            <button @click="insertFormat('`', '`')" class="toolbar-button" title="代码">
              <svg width="16" height="16" viewBox="0 0 16 16">
                <path d="m11.28 3.22 4.25 4.25a.75.75 0 0 1 0 1.06l-4.25 4.25a.749.749 0 0 1-1.275-.326.749.749 0 0 1 .215-.734L13.94 8l-3.72-3.72a.749.749 0 0 1 .326-1.275.749.749 0 0 1 .734.215Zm-6.56 0a.751.751 0 0 1 1.042.018.751.751 0 0 1 .018 1.042L2.06 8l3.72 3.72a.749.749 0 0 1-.326 1.275.749.749 0 0 1-.734-.215L.47 8.53a.75.75 0 0 1 0-1.06Z" fill="currentColor"/>
              </svg>
            </button>
            <div class="toolbar-divider"></div>
            <button @click="insertLink" class="toolbar-button" title="插入链接">
              <svg width="16" height="16" viewBox="0 0 16 16">
                <path d="m7.775 3.275 1.25-1.25a3.5 3.5 0 1 1 4.95 4.95l-2.5 2.5a3.5 3.5 0 0 1-4.95 0 .751.751 0 0 1 .018-1.042.751.751 0 0 1 1.042-.018 1.998 1.998 0 0 0 2.83 0l2.5-2.5a2.002 2.002 0 0 0-2.83-2.83l-1.25 1.25a.751.751 0 0 1-1.042-.018.751.751 0 0 1-.018-1.042Zm-4.69 9.64a1.998 1.998 0 0 0 2.83 0l1.25-1.25a.751.751 0 0 1 1.042.018.751.751 0 0 1 .018 1.042l-1.25 1.25a3.5 3.5 0 1 1-4.95-4.95l2.5-2.5a3.5 3.5 0 0 1 4.95 0 .751.751 0 0 1-.018 1.042.751.751 0 0 1-1.042.018 1.998 1.998 0 0 0-2.83 0l-2.5 2.5a1.998 1.998 0 0 0 0 2.83Z" fill="currentColor"/>
              </svg>
            </button>
            <button @click="insertCodeBlock" class="toolbar-button" title="代码块">
              <svg width="16" height="16" viewBox="0 0 16 16">
                <path d="M0 1.75C0 .784.784 0 1.75 0h12.5C15.216 0 16 .784 16 1.75v12.5A1.75 1.75 0 0 1 14.25 16H1.75A1.75 1.75 0 0 1 0 14.25Zm1.75-.25a.25.25 0 0 0-.25.25v12.5c0 .138.112.25.25.25h12.5a.25.25 0 0 0 .25-.25V1.75a.25.25 0 0 0-.25-.25ZM7.5 6.5v1h1v-1h-1Zm-2 0v1h1v-1h-1Zm-2 0v1h1v-1h-1Z" fill="currentColor"/>
              </svg>
            </button>
          </div>

          <textarea 
            v-model="form.content" 
            placeholder="开始书写你的文章内容...&#10;&#10;支持完整的 Markdown 语法:&#10;# 一级标题 (注意 # 后有空格)&#10;## 二级标题&#10;**粗体** *斜体* ~~删除线~~&#10;`行内代码`&#10;[链接文字](链接地址)&#10;- 列表项&#10;> 引用&#10;&#10;让思想自由流淌..."
            class="content-textarea"
            @input="validateForm"
            ref="contentTextarea"
          ></textarea>

          <div v-if="contentError" class="error-message">
            <svg width="14" height="14" viewBox="0 0 16 16">
              <path d="M2.343 13.657A8 8 0 1 1 13.657 2.343 8 8 0 0 1 2.343 13.657ZM6.03 4.97a.751.751 0 0 0-1.042.018.751.751 0 0 0-.018 1.042L6.94 8 4.97 9.97a.749.749 0 0 0 .326 1.275.749.749 0 0 0 .734-.215L8 9.06l1.97 1.97a.749.749 0 0 0 1.275-.326.749.749 0 0 0-.215-.734L9.06 8l1.97-1.97a.749.749 0 0 0-.326-1.275.749.749 0 0 0-.734.215L8 6.94Z" fill="currentColor"/>
            </svg>
            {{ contentError }}
          </div>

          <div class="editor-footer">
            <div class="char-count">
              字数: <span class="count-number">{{ form.content.length }}</span>
            </div>
            <div class="format-hint">
              <svg width="14" height="14" viewBox="0 0 16 16">
                <path d="M0 8a8 8 0 1 1 16 0A8 8 0 0 1 0 8Zm8-6.5a6.5 6.5 0 1 0 0 13 6.5 6.5 0 0 0 0-13ZM6.5 7.75A.75.75 0 0 1 7.25 7h1a.75.75 0 0 1 .75.75v2.75h.25a.75.75 0 0 1 0 1.5h-2a.75.75 0 0 1 0-1.5h.25v-2h-.25a.75.75 0 0 1-.75-.75ZM8 6a1 1 0 1 1 0-2 1 1 0 0 1 0 2Z" fill="currentColor"/>
              </svg>
              支持 Markdown 格式
            </div>
          </div>
        </div>
      </div>

      <!-- 预览面板 -->
      <aside class="preview-panel">
        <div class="preview-header">
          <h3>
            <svg width="16" height="16" viewBox="0 0 16 16">
              <path d="M8 9.5a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3Z" fill="currentColor"/>
              <path d="M1.679 7.932c.412-.621 1.242-1.75 2.366-2.717C5.175 4.242 6.527 3.5 8 3.5c1.473 0 2.824.742 3.955 1.715 1.124.967 1.954 2.096 2.366 2.717a.119.119 0 0 1 0 .136c-.412.621-1.242 1.75-2.366 2.717C10.825 11.758 9.473 12.5 8 12.5c-1.473 0-2.824-.742-3.955-1.715C2.92 9.818 2.09 8.69 1.679 8.068a.119.119 0 0 1 0-.136Zm12.28.599A.752.752 0 0 0 14 8a.752.752 0 0 0-.041-.531C13.556 6.908 12.63 5.67 11.34 4.58 9.97 3.425 8.344 2.75 8 2.75c-.344 0-1.97.675-3.34 1.83-1.29 1.09-2.216 2.328-2.619 2.889A.752.752 0 0 0 2 8c0 .185.07.373.041.531.403.561 1.329 1.799 2.619 2.889C6.03 12.575 7.656 13.25 8 13.25c.344 0 1.97-.675 3.34-1.83 1.29-1.09 2.216-2.328 2.619-2.889Z" fill="currentColor"/>
            </svg>
            实时预览
          </h3>
        </div>
        <div class="preview-content">
          <div v-if="!form.title && !form.content" class="preview-empty">
            <svg width="64" height="64" viewBox="0 0 16 16">
              <path d="M0 1.75C0 .784.784 0 1.75 0h12.5C15.216 0 16 .784 16 1.75v12.5A1.75 1.75 0 0 1 14.25 16H1.75A1.75 1.75 0 0 1 0 14.25Zm1.75-.25a.25.25 0 0 0-.25.25v12.5c0 .138.112.25.25.25h12.5a.25.25 0 0 0 .25-.25V1.75a.25.25 0 0 0-.25-.25Z" fill="currentColor"/>
            </svg>
            <p>开始书写,这里将实时显示预览</p>
          </div>
          <div v-else class="preview-article">
            <h1 v-if="form.title" class="preview-title">{{ form.title }}</h1>
            <div v-if="form.tags.length > 0" class="preview-tags">
              <span v-for="tag in form.tags" :key="tag" class="preview-tag">{{ tag }}</span>
            </div>
            <div v-if="form.content" class="preview-body" v-html="renderContent(form.content)"></div>
          </div>
        </div>
      </aside>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted, onBeforeUnmount } from 'vue'
import { useRouter } from 'vue-router'
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

const router = useRouter()

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
  breaks: true, // 支持 GitHub 风格的换行
  gfm: true // 启用 GitHub Flavored Markdown
})

// 表单数据
const form = ref({
  title: '',
  content: '',
  tags: [] as string[]
})

// 编辑模式：存储正在编辑的博客ID
const editingBlogId = ref<string | null>(null)

// 标签输入
const tagInput = ref('')

// 验证状态
const titleError = ref('')
const contentError = ref('')
const saving = ref(false)

// DOM 引用
const contentTextarea = ref<HTMLTextAreaElement | null>(null)

// 自动保存
let autoSaveTimer: ReturnType<typeof setTimeout> | null = null

// 表单验证
const validateForm = () => {
  titleError.value = ''
  contentError.value = ''
  
  if (form.value.title && form.value.title.length > 200) {
    titleError.value = '标题不能超过 200 个字符'
  }
  
  if (form.value.content && form.value.content.length > 50000) {
    contentError.value = '内容不能超过 50000 个字符'
  }
  
  // 触发自动保存
  if (autoSaveTimer) {
    clearTimeout(autoSaveTimer)
  }
  autoSaveTimer = setTimeout(() => {
    saveDraftToLocal()
  }, 3000)
}

// 是否可以发布
const canPublish = computed(() => {
  return form.value.title.trim() !== '' && 
         form.value.content.trim() !== '' &&
         !titleError.value &&
         !contentError.value
})

// 添加标签
const addTag = () => {
  const tag = tagInput.value.trim()
  if (tag && !form.value.tags.includes(tag)) {
    if (form.value.tags.length >= 10) {
      alert('最多只能添加 10 个标签')
      return
    }
    form.value.tags.push(tag)
    tagInput.value = ''
  }
}

// 移除标签
const removeTag = (index: number) => {
  form.value.tags.splice(index, 1)
}

// 插入格式
const insertFormat = (before: string, after: string) => {
  const textarea = contentTextarea.value
  if (!textarea) return
  
  const start = textarea.selectionStart
  const end = textarea.selectionEnd
  const selectedText = form.value.content.substring(start, end)
  const newText = before + selectedText + after
  
  form.value.content = 
    form.value.content.substring(0, start) +
    newText +
    form.value.content.substring(end)
  
  // 重新设置光标位置
  setTimeout(() => {
    textarea.focus()
    const newPosition = start + before.length + selectedText.length
    textarea.setSelectionRange(newPosition, newPosition)
  }, 0)
}

// 插入链接
const insertLink = () => {
  const url = prompt('请输入链接地址:')
  if (url) {
    const text = prompt('请输入链接文字:', '链接文字')
    if (text) {
      const textarea = contentTextarea.value
      if (!textarea) return
      
      const start = textarea.selectionStart
      const linkText = `[${text}](${url})`
      
      form.value.content = 
        form.value.content.substring(0, start) +
        linkText +
        form.value.content.substring(start)
      
      textarea.focus()
    }
  }
}

// 插入代码块
const insertCodeBlock = () => {
  const textarea = contentTextarea.value
  if (!textarea) return
  
  const start = textarea.selectionStart
  const codeBlock = '\n```\n代码内容\n```\n'
  
  form.value.content = 
    form.value.content.substring(0, start) +
    codeBlock +
    form.value.content.substring(start)
  
  setTimeout(() => {
    textarea.focus()
    const newPosition = start + 5 // 光标定位到代码内容位置
    textarea.setSelectionRange(newPosition, newPosition + 4)
  }, 0)
}

// 渲染内容 (使用 marked 和 DOMPurify)
const renderContent = (content: string): string => {
  if (!content) return ''
  
  try {
    // 使用 marked 解析 Markdown
    const rawHtml = marked.parse(content) as string
    
    // 在开发环境下输出调试信息
    if (import.meta.env.DEV) {
      console.log('原始 Markdown:', content.substring(0, 100))
      console.log('渲染后的 HTML:', rawHtml.substring(0, 200))
    }
    
    // 使用 DOMPurify 净化 HTML，防止 XSS 攻击
    const cleanHtml = DOMPurify.sanitize(rawHtml, {
      ALLOWED_TAGS: ['p', 'br', 'strong', 'em', 'u', 's', 'code', 'pre', 'a', 'ul', 'ol', 'li', 'h1', 'h2', 'h3', 'h4', 'h5', 'h6', 'blockquote', 'hr', 'table', 'thead', 'tbody', 'tr', 'th', 'td', 'img', 'div', 'span'],
      ALLOWED_ATTR: ['href', 'target', 'rel', 'class', 'src', 'alt', 'title']
    })
    
    return cleanHtml
  } catch (error) {
    console.error('Markdown 渲染失败:', error)
    return '<p class="error">预览渲染失败，请检查 Markdown 语法</p>'
  }
}

// 保存草稿到本地
const saveDraftToLocal = () => {
  try {
    localStorage.setItem('blog_draft', JSON.stringify(form.value))
  } catch (error) {
    console.error('保存草稿失败:', error)
  }
}

// 从本地加载草稿
const loadDraftFromLocal = () => {
  try {
    const draft = localStorage.getItem('blog_draft')
    if (draft) {
      const parsed = JSON.parse(draft)
      if (parsed.title || parsed.content || parsed.tags?.length > 0) {
        const confirmed = confirm('检测到未完成的草稿,是否恢复?')
        if (confirmed) {
          form.value = parsed
        }
      }
    }
  } catch (error) {
    console.error('加载草稿失败:', error)
  }
}

// 清除本地草稿
const clearDraftFromLocal = () => {
  try {
    localStorage.removeItem('blog_draft')
  } catch (error) {
    console.error('清除草稿失败:', error)
  }
}

// 保存草稿
const saveDraft = () => {
  saveDraftToLocal()
  alert('草稿已保存到本地!')
}

// 发布/更新文章
const publishBlog = async () => {
  if (!canPublish.value) {
    alert('请填写完整的文章标题和内容')
    return
  }
  
  saving.value = true
  
  try {
    const userId = localStorage.getItem('userId') || '049ac648-0176-4be4-88f1-e14838e15152'
    
    const blogData = {
      title: form.value.title.trim(),
      content: form.value.content.trim(),
      tags: form.value.tags.join(','),
      authorId: userId
    }
    
    let response: Response
    let blogId: string
    
    // 如果是编辑模式，使用 PUT 请求更新
    if (editingBlogId.value) {
      response = await fetch('/api/Blog/Update', {
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({
          id: editingBlogId.value,
          ...blogData
        })
      })
      blogId = editingBlogId.value
    } else {
      // 否则创建新博客
      response = await fetch('/api/Blog/create', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(blogData)
      })
      
      if (response.ok) {
        const result = await response.json()
        blogId = result.id
      } else {
        throw new Error('创建失败')
      }
    }
    
    if (!response.ok) {
      const errorText = await response.text()
      throw new Error(errorText || (editingBlogId.value ? '更新失败' : '发布失败'))
    }
    
    // 清除本地草稿
    clearDraftFromLocal()
    
    // 显示成功提示
    alert(editingBlogId.value ? '文章更新成功!' : '文章发布成功!')
    
    // 跳转到文章详情页
    router.push(`/blog/${blogId}`)
    
  } catch (error) {
    console.error('发布/更新文章失败:', error)
    alert(error instanceof Error ? error.message : '操作失败,请重试')
  } finally {
    saving.value = false
  }
}

// 返回
const goBack = () => {
  if (form.value.title || form.value.content || form.value.tags.length > 0) {
    const confirmed = confirm('有未保存的内容,确定要离开吗?')
    if (!confirmed) return
  }
  router.push('/blogs')
}

// 组件挂载
onMounted(() => {
  // 优先检查是否有要编辑的博客
  const editingBlogJson = localStorage.getItem('editingBlog')
  if (editingBlogJson) {
    try {
      const editingBlog = JSON.parse(editingBlogJson)
      form.value.title = editingBlog.title || ''
      form.value.content = editingBlog.content || ''
      form.value.tags = editingBlog.tags ? editingBlog.tags.split(',').map((t: string) => t.trim()).filter((t: string) => t) : []
      
      // 存储博客ID以便更新时使用
      editingBlogId.value = editingBlog.id
      
      // 清除 localStorage 中的编辑数据
      localStorage.removeItem('editingBlog')
      
      console.log('加载编辑博客:', editingBlog)
    } catch (err) {
      console.error('加载编辑博客失败:', err)
      loadDraftFromLocal()
    }
  } else {
    loadDraftFromLocal()
  }
  
  // 监听页面关闭事件
  window.addEventListener('beforeunload', handleBeforeUnload)
})

// 组件卸载
onBeforeUnmount(() => {
  if (autoSaveTimer) {
    clearTimeout(autoSaveTimer)
  }
  window.removeEventListener('beforeunload', handleBeforeUnload)
})

// 页面关闭前提示
const handleBeforeUnload = (e: BeforeUnloadEvent) => {
  if (form.value.title || form.value.content || form.value.tags.length > 0) {
    e.preventDefault()
    e.returnValue = ''
  }
}
</script>

<style scoped>
.blog-create-view {
  min-height: 100vh;
  background: linear-gradient(135deg, #0d1117 0%, #161b22 100%);
  color: #c9d1d9;
  padding: 2rem;
}

/* 页面头部 */
.create-header {
  max-width: 1600px;
  margin: 0 auto 2rem;
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: 2rem;
}

.nav-back-button {
  display: flex;
  align-items: center;
  gap: 0.5rem;
  padding: 0.75rem 1.25rem;
  background: #21262d;
  border: 1px solid #30363d;
  border-radius: 6px;
  color: #8b949e;
  font-size: 0.95rem;
  cursor: pointer;
  transition: all 0.3s ease;
}

.nav-back-button:hover {
  background: #30363d;
  color: #58a6ff;
  border-color: #58a6ff;
}

.page-title {
  flex: 1;
  font-size: 2rem;
  font-weight: 700;
  margin: 0;
  text-align: center;
}

.gradient-text {
  background: linear-gradient(135deg, #58a6ff 0%, #bc8cff 100%);
  -webkit-background-clip: text;
  -webkit-text-fill-color: transparent;
  background-clip: text;
}

.header-actions {
  display: flex;
  gap: 1rem;
}

.action-button {
  display: flex;
  align-items: center;
  gap: 0.5rem;
  padding: 0.75rem 1.5rem;
  border: none;
  border-radius: 6px;
  font-size: 1rem;
  font-weight: 500;
  cursor: pointer;
  transition: all 0.3s ease;
}

.action-button:disabled {
  opacity: 0.5;
  cursor: not-allowed;
}

.secondary-button {
  background: #21262d;
  color: #c9d1d9;
  border: 1px solid #30363d;
}

.secondary-button:hover:not(:disabled) {
  background: #30363d;
}

.primary-button {
  background: linear-gradient(135deg, #238636 0%, #2ea043 100%);
  color: white;
  box-shadow: 0 4px 12px rgba(35, 134, 54, 0.3);
}

.primary-button:hover:not(:disabled) {
  transform: translateY(-2px);
  box-shadow: 0 6px 16px rgba(35, 134, 54, 0.4);
}

/* 编辑容器 */
.editor-container {
  max-width: 1600px;
  margin: 0 auto;
  display: grid;
  grid-template-columns: 1fr 500px;
  gap: 2rem;
  align-items: start;
}

.editor-main {
  display: flex;
  flex-direction: column;
  gap: 1.5rem;
}

/* 标题输入 */
.title-section {
  background: #0d1117;
  border: 2px solid #30363d;
  border-radius: 12px;
  padding: 1.5rem;
  transition: border-color 0.3s ease;
}

.title-section:focus-within {
  border-color: #58a6ff;
}

.title-input {
  width: 100%;
  background: transparent;
  border: none;
  color: #ffffff;
  font-size: 1.75rem;
  font-weight: 700;
  outline: none;
  padding: 0;
}

.title-input::placeholder {
  color: #484f58;
}

/* 标签输入 */
.tags-section {
  background: #0d1117;
  border: 2px solid #30363d;
  border-radius: 12px;
  padding: 1.5rem;
  transition: border-color 0.3s ease;
}

.tags-section:focus-within {
  border-color: #58a6ff;
}

.tags-input-wrapper {
  display: flex;
  align-items: center;
  gap: 0.75rem;
  margin-bottom: 1rem;
}

.tags-input-wrapper svg {
  color: #58a6ff;
  flex-shrink: 0;
}

.tags-input {
  flex: 1;
  background: transparent;
  border: none;
  color: #c9d1d9;
  font-size: 1rem;
  outline: none;
}

.tags-input::placeholder {
  color: #484f58;
}

.tags-list {
  display: flex;
  flex-wrap: wrap;
  gap: 0.75rem;
  margin-bottom: 0.75rem;
}

.tag {
  display: inline-flex;
  align-items: center;
  gap: 0.5rem;
  padding: 0.5rem 1rem;
  background: #21262d;
  border: 1px solid #30363d;
  border-radius: 6px;
  color: #58a6ff;
  font-size: 0.9rem;
  transition: all 0.3s ease;
}

.tag:hover {
  background: #30363d;
  border-color: #58a6ff;
}

.tag-remove {
  display: flex;
  align-items: center;
  background: transparent;
  border: none;
  color: #8b949e;
  cursor: pointer;
  padding: 0;
  transition: color 0.3s ease;
}

.tag-remove:hover {
  color: #f85149;
}

.tags-hint {
  margin: 0;
  color: #8b949e;
  font-size: 0.85rem;
}

/* 编辑器 */
.editor-section {
  background: #0d1117;
  border: 2px solid #30363d;
  border-radius: 12px;
  overflow: hidden;
  transition: border-color 0.3s ease;
}

.editor-section:focus-within {
  border-color: #58a6ff;
}

.editor-toolbar {
  display: flex;
  align-items: center;
  gap: 0.25rem;
  padding: 0.75rem 1rem;
  background: #161b22;
  border-bottom: 1px solid #30363d;
}

.toolbar-button {
  display: flex;
  align-items: center;
  justify-content: center;
  width: 32px;
  height: 32px;
  background: transparent;
  border: none;
  border-radius: 6px;
  color: #8b949e;
  cursor: pointer;
  transition: all 0.3s ease;
}

.toolbar-button:hover {
  background: #21262d;
  color: #58a6ff;
}

.toolbar-divider {
  width: 1px;
  height: 24px;
  background: #30363d;
  margin: 0 0.5rem;
}

.content-textarea {
  width: 100%;
  min-height: 500px;
  background: transparent;
  border: none;
  color: #c9d1d9;
  font-size: 1rem;
  line-height: 1.8;
  padding: 1.5rem;
  outline: none;
  resize: vertical;
  font-family: 'Segoe UI', system-ui, -apple-system, sans-serif;
}

.content-textarea::placeholder {
  color: #484f58;
}

.editor-footer {
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 0.75rem 1.5rem;
  background: #161b22;
  border-top: 1px solid #30363d;
}

.char-count {
  color: #8b949e;
  font-size: 0.85rem;
}

.count-number {
  color: #58a6ff;
  font-weight: 600;
}

.format-hint {
  display: flex;
  align-items: center;
  gap: 0.5rem;
  color: #8b949e;
  font-size: 0.85rem;
}

.format-hint svg {
  color: #58a6ff;
}

/* 预览面板 */
.preview-panel {
  position: sticky;
  top: 2rem;
  background: #0d1117;
  border: 2px solid #30363d;
  border-radius: 12px;
  overflow: hidden;
  max-height: calc(100vh - 4rem);
  display: flex;
  flex-direction: column;
}

.preview-header {
  padding: 1rem 1.5rem;
  background: #161b22;
  border-bottom: 1px solid #30363d;
}

.preview-header h3 {
  margin: 0;
  display: flex;
  align-items: center;
  gap: 0.5rem;
  color: #c9d1d9;
  font-size: 1rem;
  font-weight: 600;
}

.preview-header svg {
  color: #58a6ff;
}

.preview-content {
  flex: 1;
  overflow-y: auto;
  padding: 1.5rem;
}

.preview-empty {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  min-height: 300px;
  color: #484f58;
  text-align: center;
}

.preview-empty svg {
  margin-bottom: 1rem;
  opacity: 0.5;
}

.preview-article {
  animation: fadeIn 0.3s ease;
}

@keyframes fadeIn {
  from {
    opacity: 0;
    transform: translateY(10px);
  }
  to {
    opacity: 1;
    transform: translateY(0);
  }
}

.preview-title {
  font-size: 1.75rem;
  font-weight: 700;
  color: #ffffff;
  margin: 0 0 1rem 0;
  line-height: 1.3;
}

.preview-tags {
  display: flex;
  flex-wrap: wrap;
  gap: 0.5rem;
  margin-bottom: 1.5rem;
}

.preview-tag {
  padding: 0.35rem 0.75rem;
  background: #21262d;
  border: 1px solid #30363d;
  border-radius: 6px;
  color: #58a6ff;
  font-size: 0.85rem;
}

.preview-body {
  color: #c9d1d9;
  line-height: 1.8;
  word-wrap: break-word;
}

/* 段落 */
.preview-body :deep(p) {
  margin: 1rem 0;
  line-height: 1.8;
}

.preview-body :deep(p:first-child) {
  margin-top: 0;
}

.preview-body :deep(p:last-child) {
  margin-bottom: 0;
}

/* 标题 */
.preview-body :deep(h1),
.preview-body :deep(h2),
.preview-body :deep(h3),
.preview-body :deep(h4),
.preview-body :deep(h5),
.preview-body :deep(h6) {
  color: #ffffff;
  font-weight: 600;
  margin: 1.5rem 0 1rem 0;
  line-height: 1.3;
}

.preview-body :deep(h1) {
  font-size: 2rem;
  padding-bottom: 0.5rem;
  border-bottom: 1px solid #30363d;
}

.preview-body :deep(h2) {
  font-size: 1.5rem;
  padding-bottom: 0.5rem;
  border-bottom: 1px solid #30363d;
}

.preview-body :deep(h3) {
  font-size: 1.25rem;
}

.preview-body :deep(h4) {
  font-size: 1.1rem;
}

.preview-body :deep(h5),
.preview-body :deep(h6) {
  font-size: 1rem;
}

/* 文字样式 */
.preview-body :deep(strong) {
  color: #ffffff;
  font-weight: 600;
}

.preview-body :deep(em) {
  color: #7ee787;
  font-style: italic;
}

.preview-body :deep(s),
.preview-body :deep(del) {
  text-decoration: line-through;
  opacity: 0.7;
}

/* 行内代码 */
.preview-body :deep(code) {
  background: #161b22;
  border: 1px solid #30363d;
  padding: 0.2rem 0.4rem;
  border-radius: 4px;
  font-family: 'Courier New', 'Consolas', monospace;
  font-size: 0.9em;
  color: #ff7b72;
}

/* 代码块 */
.preview-body :deep(pre) {
  background: #161b22;
  border: 1px solid #30363d;
  border-radius: 6px;
  padding: 1rem;
  overflow-x: auto;
  margin: 1rem 0;
}

.preview-body :deep(pre code) {
  background: transparent;
  border: none;
  padding: 0;
  color: #c9d1d9;
  font-size: 0.9rem;
}

/* 链接 */
.preview-body :deep(a) {
  color: #58a6ff;
  text-decoration: none;
  border-bottom: 1px solid transparent;
  transition: border-color 0.3s ease;
}

.preview-body :deep(a:hover) {
  border-bottom-color: #58a6ff;
}

/* 列表 */
.preview-body :deep(ul),
.preview-body :deep(ol) {
  margin: 1rem 0;
  padding-left: 2rem;
}

.preview-body :deep(li) {
  margin: 0.5rem 0;
}

.preview-body :deep(ul) {
  list-style-type: disc;
}

.preview-body :deep(ol) {
  list-style-type: decimal;
}

.preview-body :deep(li > ul),
.preview-body :deep(li > ol) {
  margin: 0.25rem 0;
}

/* 引用 */
.preview-body :deep(blockquote) {
  margin: 1rem 0;
  padding: 0.5rem 1rem;
  border-left: 4px solid #58a6ff;
  background: #161b22;
  color: #8b949e;
}

.preview-body :deep(blockquote p) {
  margin: 0.5rem 0;
}

/* 水平线 */
.preview-body :deep(hr) {
  border: none;
  border-top: 1px solid #30363d;
  margin: 1.5rem 0;
}

/* 表格 */
.preview-body :deep(table) {
  width: 100%;
  border-collapse: collapse;
  margin: 1rem 0;
  overflow: hidden;
  border-radius: 6px;
  border: 1px solid #30363d;
}

.preview-body :deep(thead) {
  background: #161b22;
}

.preview-body :deep(th) {
  padding: 0.75rem;
  text-align: left;
  font-weight: 600;
  color: #ffffff;
  border-bottom: 2px solid #30363d;
}

.preview-body :deep(td) {
  padding: 0.75rem;
  border-bottom: 1px solid #30363d;
}

.preview-body :deep(tr:last-child td) {
  border-bottom: none;
}

.preview-body :deep(tr:hover) {
  background: #161b22;
}

/* 错误提示 */
.error-message {
  display: flex;
  align-items: center;
  gap: 0.5rem;
  margin-top: 0.75rem;
  padding: 0.75rem;
  background: rgba(248, 81, 73, 0.1);
  border: 1px solid #f85149;
  border-radius: 6px;
  color: #f85149;
  font-size: 0.9rem;
}

.error-message svg {
  flex-shrink: 0;
}

/* 响应式设计 */
@media (max-width: 1200px) {
  .editor-container {
    grid-template-columns: 1fr;
  }
  
  .preview-panel {
    position: relative;
    top: 0;
    max-height: 600px;
  }
}

@media (max-width: 768px) {
  .blog-create-view {
    padding: 1rem;
  }
  
  .create-header {
    flex-direction: column;
    align-items: stretch;
  }
  
  .page-title {
    font-size: 1.5rem;
    text-align: left;
  }
  
  .header-actions {
    flex-direction: column;
  }
  
  .action-button {
    width: 100%;
    justify-content: center;
  }
  
  .content-textarea {
    min-height: 400px;
  }
}

/* 滚动条样式 */
.preview-content::-webkit-scrollbar,
.content-textarea::-webkit-scrollbar {
  width: 8px;
}

.preview-content::-webkit-scrollbar-track,
.content-textarea::-webkit-scrollbar-track {
  background: #161b22;
}

.preview-content::-webkit-scrollbar-thumb,
.content-textarea::-webkit-scrollbar-thumb {
  background: #30363d;
  border-radius: 4px;
}

.preview-content::-webkit-scrollbar-thumb:hover,
.content-textarea::-webkit-scrollbar-thumb:hover {
  background: #484f58;
}
</style>
