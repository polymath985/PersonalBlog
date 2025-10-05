<template>
  <div class="image-upload">
    <div class="upload-area" @click="selectFile" :class="{ 'has-image': imageUrl }">
      <input 
        ref="fileInput" 
        type="file" 
        accept="image/*" 
        @change="handleFileChange"
        style="display: none"
      />
      
      <div v-if="!imageUrl" class="upload-placeholder">
        <svg width="48" height="48" viewBox="0 0 16 16">
          <path d="M2.75 14A1.75 1.75 0 0 1 1 12.25v-2.5a.75.75 0 0 1 1.5 0v2.5c0 .138.112.25.25.25h10.5a.25.25 0 0 0 .25-.25v-2.5a.75.75 0 0 1 1.5 0v2.5A1.75 1.75 0 0 1 13.25 14Z" fill="currentColor"/>
          <path d="M7.25 7.689V2a.75.75 0 0 1 1.5 0v5.689l1.97-1.969a.749.749 0 1 1 1.06 1.06l-3.25 3.25a.749.749 0 0 1-1.06 0L4.22 6.78a.749.749 0 1 1 1.06-1.06l1.97 1.969Z" fill="currentColor"/>
        </svg>
        <p>点击上传图片</p>
        <span>支持 JPG、PNG、GIF、WebP，最大 5MB</span>
      </div>
      
      <div v-else class="image-preview">
        <img :src="getImageUrl(imageUrl)" :alt="imageAlt" />
        <button @click.stop="removeImage" class="remove-btn" title="移除图片">
          <svg width="16" height="16" viewBox="0 0 16 16">
            <path d="M3.72 3.72a.75.75 0 0 1 1.06 0L8 6.94l3.22-3.22a.749.749 0 0 1 1.275.326.749.749 0 0 1-.215.734L9.06 8l3.22 3.22a.749.749 0 0 1-.326 1.275.749.749 0 0 1-.734-.215L8 9.06l-3.22 3.22a.751.751 0 0 1-1.042-.018.751.751 0 0 1-.018-1.042L6.94 8 3.72 4.78a.75.75 0 0 1 0-1.06Z" fill="currentColor"/>
          </svg>
        </button>
      </div>
    </div>
    
    <div v-if="uploading" class="upload-progress">
      <div class="progress-bar" :style="{ width: progress + '%' }"></div>
      <span class="progress-text">{{ progress }}%</span>
    </div>
    
    <div v-if="error" class="error-message">
      <svg width="16" height="16" viewBox="0 0 16 16">
        <path d="M2.343 13.657A8 8 0 1 1 13.657 2.343 8 8 0 0 1 2.343 13.657ZM6.03 4.97a.751.751 0 0 0-1.042.018.751.751 0 0 0-.018 1.042L6.94 8 4.97 9.97a.749.749 0 0 0 .326 1.275.749.749 0 0 0 .734-.215L8 9.06l2.03 2.03a.751.751 0 0 0 1.042-.018.751.751 0 0 0 .018-1.042L9.06 8l2.03-2.03a.749.749 0 0 0-.326-1.275.749.749 0 0 0-.734.215L8 6.94Z" fill="currentColor"/>
      </svg>
      {{ error }}
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'

interface Props {
  modelValue?: string
  imageAlt?: string
}

const props = withDefaults(defineProps<Props>(), {
  modelValue: '',
  imageAlt: '预览图片'
})

const emit = defineEmits<{
  'update:modelValue': [url: string]
  'file-selected': [file: File, blobUrl: string]
}>()

const fileInput = ref<HTMLInputElement>()
const imageUrl = ref(props.modelValue)
const uploading = ref(false)
const progress = ref(0)
const error = ref('')
const pendingFile = ref<File | null>(null)

const selectFile = () => {
  error.value = ''
  fileInput.value?.click()
}

const handleFileChange = (e: Event) => {
  const target = e.target as HTMLInputElement
  const file = target.files?.[0]
  
  if (!file) return
  
  // 验证文件大小 (5MB)
  if (file.size > 5 * 1024 * 1024) {
    error.value = '文件大小不能超过 5MB'
    return
  }
  
  // 验证文件类型
  if (!file.type.startsWith('image/')) {
    error.value = '请选择图片文件'
    return
  }
  
  // 保存待上传的文件
  pendingFile.value = file
  
  // 创建 blob URL 用于预览
  const blobUrl = URL.createObjectURL(file)
  imageUrl.value = blobUrl
  emit('update:modelValue', blobUrl)
  emit('file-selected', file, blobUrl)
}

// 暴露给父组件调用的上传方法
const uploadFile = async (category: string = 'other'): Promise<string> => {
  if (!pendingFile.value) {
    throw new Error('没有待上传的文件')
  }
  
  uploading.value = true
  progress.value = 0
  error.value = ''
  
  return new Promise((resolve, reject) => {
    const formData = new FormData()
    formData.append('file', pendingFile.value!)
    
    const xhr = new XMLHttpRequest()
    
    // 监听上传进度
    xhr.upload.onprogress = (e) => {
      if (e.lengthComputable) {
        progress.value = Math.round((e.loaded / e.total) * 100)
      }
    }
    
    // 上传完成
    xhr.onload = () => {
      uploading.value = false
      
      if (xhr.status === 200) {
        const response = JSON.parse(xhr.responseText)
        // 将相对路径转换为完整 URL
        const fullUrl = `https://localhost:7005${response.url}`
        
        // 释放 blob URL
        if (imageUrl.value.startsWith('blob:')) {
          URL.revokeObjectURL(imageUrl.value)
        }
        
        imageUrl.value = fullUrl
        emit('update:modelValue', fullUrl)
        pendingFile.value = null
        resolve(fullUrl)
      } else {
        const errorResponse = JSON.parse(xhr.responseText)
        error.value = errorResponse.error || '上传失败'
        reject(new Error(error.value))
      }
    }
    
    xhr.onerror = () => {
      error.value = '上传失败，请检查网络连接'
      uploading.value = false
      reject(new Error(error.value))
    }
    
    // 添加 category 参数到 URL
    xhr.open('POST', `/api/File/upload?category=${encodeURIComponent(category)}`)
    xhr.send(formData)
  })
}

const removeImage = () => {
  // 释放 blob URL
  if (imageUrl.value.startsWith('blob:')) {
    URL.revokeObjectURL(imageUrl.value)
  }
  
  imageUrl.value = ''
  pendingFile.value = null
  emit('update:modelValue', '')
  if (fileInput.value) {
    fileInput.value.value = ''
  }
  error.value = ''
}

// 暴露方法给父组件
defineExpose({
  uploadFile,
  hasPendingFile: () => !!pendingFile.value
})

const getImageUrl = (url: string) => {
  // 如果是相对路径，确保加上正确的前缀
  if (url.startsWith('/uploads/')) {
    return `https://localhost:7005${url}`
  }
  return url
}
</script>

<style scoped>
.image-upload {
  width: 100%;
}

.upload-area {
  border: 2px dashed #30363d;
  border-radius: 8px;
  padding: 2rem;
  text-align: center;
  cursor: pointer;
  transition: all 0.3s ease;
  background: #0d1117;
}

.upload-area:hover {
  border-color: #58a6ff;
  background: rgba(88, 166, 255, 0.05);
}

.upload-area.has-image {
  padding: 0;
  border-style: solid;
}

.upload-placeholder {
  color: #8b949e;
}

.upload-placeholder svg {
  margin-bottom: 1rem;
  color: #58a6ff;
}

.upload-placeholder p {
  margin: 0.5rem 0;
  font-size: 1.1rem;
  color: #c9d1d9;
  font-weight: 500;
}

.upload-placeholder span {
  font-size: 0.85rem;
  color: #8b949e;
}

.image-preview {
  position: relative;
  width: 100%;
  min-height: 200px;
}

.image-preview img {
  width: 100%;
  height: auto;
  max-height: 400px;
  object-fit: contain;
  border-radius: 8px;
  display: block;
}

.remove-btn {
  position: absolute;
  top: 0.5rem;
  right: 0.5rem;
  padding: 0.5rem;
  background: rgba(248, 81, 73, 0.9);
  border: none;
  border-radius: 6px;
  color: white;
  cursor: pointer;
  transition: all 0.3s ease;
  display: flex;
  align-items: center;
  justify-content: center;
}

.remove-btn:hover {
  background: #f85149;
  transform: scale(1.05);
}

.upload-progress {
  margin-top: 1rem;
  position: relative;
}

.progress-bar {
  height: 4px;
  background: linear-gradient(90deg, #58a6ff 0%, #7ee787 100%);
  border-radius: 2px;
  transition: width 0.3s ease;
}

.progress-text {
  position: absolute;
  top: -1.5rem;
  right: 0;
  font-size: 0.85rem;
  color: #58a6ff;
  font-weight: 600;
}

.error-message {
  margin-top: 0.75rem;
  padding: 0.75rem 1rem;
  background: rgba(248, 81, 73, 0.1);
  border: 1px solid rgba(248, 81, 73, 0.3);
  border-radius: 6px;
  color: #ff7b72;
  font-size: 0.9rem;
  display: flex;
  align-items: center;
  gap: 0.5rem;
}

.error-message svg {
  flex-shrink: 0;
}
</style>
