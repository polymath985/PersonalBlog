<template>
  <div class="custom-select-wrapper" v-click-outside="closeDropdown">
    <div 
      class="custom-select" 
      :class="{ 'is-open': isOpen }"
      @click="toggleDropdown"
    >
      <div class="select-trigger">
        <svg class="select-icon" width="16" height="16" viewBox="0 0 16 16">
          <path d="M2.75 2.5a.75.75 0 0 0 0 1.5h10.5a.75.75 0 0 0 0-1.5H2.75Zm0 5a.75.75 0 0 0 0 1.5h6.5a.75.75 0 0 0 0-1.5h-6.5Zm0 5a.75.75 0 0 0 0 1.5h3.5a.75.75 0 0 0 0-1.5h-3.5Z" fill="currentColor"/>
        </svg>
        <span class="select-text">{{ selectedLabel }}</span>
        <svg 
          class="select-arrow" 
          :class="{ 'is-rotated': isOpen }"
          width="12" 
          height="12" 
          viewBox="0 0 12 12"
        >
          <path d="M6 9L1.5 4.5L2.5 3.5L6 7L9.5 3.5L10.5 4.5L6 9Z" fill="currentColor"/>
        </svg>
      </div>
    </div>
    
    <transition name="dropdown">
      <div v-if="isOpen" class="select-dropdown">
        <div 
          v-for="option in options" 
          :key="option.value"
          class="select-option"
          :class="{ 'is-selected': modelValue === option.value }"
          @click="selectOption(option.value)"
        >
          <svg 
            v-if="modelValue === option.value" 
            class="option-check" 
            width="16" 
            height="16" 
            viewBox="0 0 16 16"
          >
            <path d="M13.78 4.22a.75.75 0 0 1 0 1.06l-7.25 7.25a.75.75 0 0 1-1.06 0L2.22 9.28a.751.751 0 0 1 .018-1.042.751.751 0 0 1 1.042-.018L6 10.94l6.72-6.72a.75.75 0 0 1 1.06 0Z" fill="currentColor"/>
          </svg>
          <span>{{ option.label }}</span>
        </div>
      </div>
    </transition>
  </div>
</template>

<script setup lang="ts">
import { ref, computed } from 'vue'

interface SelectOption {
  label: string
  value: string
}

interface Props {
  modelValue: string
  options: SelectOption[]
}

const props = defineProps<Props>()
const emit = defineEmits<{
  'update:modelValue': [value: string]
}>()

const isOpen = ref(false)

const selectedLabel = computed(() => {
  const option = props.options.find(opt => opt.value === props.modelValue)
  return option ? option.label : '请选择'
})

const toggleDropdown = () => {
  isOpen.value = !isOpen.value
}

const closeDropdown = () => {
  isOpen.value = false
}

const selectOption = (value: string) => {
  emit('update:modelValue', value)
  closeDropdown()
}

// 自定义指令: 点击外部关闭
const vClickOutside = {
  mounted(el: any, binding: any) {
    el.clickOutsideEvent = (event: Event) => {
      if (!(el === event.target || el.contains(event.target))) {
        binding.value()
      }
    }
    document.addEventListener('click', el.clickOutsideEvent)
  },
  unmounted(el: any) {
    document.removeEventListener('click', el.clickOutsideEvent)
  }
}
</script>

<style scoped>
.custom-select-wrapper {
  position: relative;
  display: inline-block;
}

.custom-select {
  position: relative;
  min-width: 160px;
  cursor: pointer;
}

.select-trigger {
  display: flex;
  align-items: center;
  gap: 0.5rem;
  padding: 0.6rem 1rem;
  background: linear-gradient(135deg, #161b22 0%, #1c2128 100%);
  border: 1px solid #30363d;
  border-radius: 8px;
  transition: all 0.3s ease;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.2);
}

.custom-select:hover .select-trigger {
  border-color: #58a6ff;
  box-shadow: 0 4px 12px rgba(88, 166, 255, 0.15);
  transform: translateY(-1px);
}

.custom-select.is-open .select-trigger {
  border-color: #58a6ff;
  box-shadow: 0 4px 12px rgba(88, 166, 255, 0.2);
}

.select-icon {
  color: #58a6ff;
  flex-shrink: 0;
  transition: color 0.3s ease;
}

.custom-select:hover .select-icon {
  color: #7ee787;
}

.select-text {
  flex: 1;
  color: #f0f6fc;
  font-size: 0.9rem;
  font-weight: 500;
  white-space: nowrap;
}

.select-arrow {
  color: #8b949e;
  flex-shrink: 0;
  transition: all 0.3s ease;
}

.select-arrow.is-rotated {
  transform: rotate(180deg);
  color: #58a6ff;
}

.custom-select:hover .select-arrow {
  color: #58a6ff;
}

/* 下拉列表 */
.select-dropdown {
  position: absolute;
  top: calc(100% + 0.5rem);
  left: 0;
  right: 0;
  background: linear-gradient(135deg, #1c2128 0%, #22272e 100%);
  border: 1px solid #30363d;
  border-radius: 8px;
  box-shadow: 0 8px 24px rgba(0, 0, 0, 0.4);
  overflow: hidden;
  z-index: 1000;
  backdrop-filter: blur(10px);
}

.select-option {
  display: flex;
  align-items: center;
  gap: 0.5rem;
  padding: 0.75rem 1rem;
  color: #c9d1d9;
  font-size: 0.9rem;
  font-weight: 500;
  cursor: pointer;
  transition: all 0.2s ease;
  position: relative;
}

.select-option::before {
  content: '';
  position: absolute;
  left: 0;
  top: 0;
  bottom: 0;
  width: 3px;
  background: #58a6ff;
  transform: scaleY(0);
  transition: transform 0.2s ease;
}

.select-option:hover {
  background: rgba(88, 166, 255, 0.1);
  color: #f0f6fc;
  padding-left: 1.25rem;
}

.select-option:hover::before {
  transform: scaleY(1);
}

.select-option.is-selected {
  background: rgba(88, 166, 255, 0.15);
  color: #58a6ff;
}

.select-option.is-selected::before {
  transform: scaleY(1);
  background: linear-gradient(180deg, #58a6ff 0%, #7ee787 100%);
}

.option-check {
  color: #7ee787;
  flex-shrink: 0;
  animation: checkIn 0.3s ease;
}

@keyframes checkIn {
  0% {
    opacity: 0;
    transform: scale(0.5);
  }
  100% {
    opacity: 1;
    transform: scale(1);
  }
}

/* 下拉动画 */
.dropdown-enter-active {
  animation: dropdownIn 0.3s ease;
}

.dropdown-leave-active {
  animation: dropdownOut 0.2s ease;
}

@keyframes dropdownIn {
  0% {
    opacity: 0;
    transform: translateY(-10px) scale(0.95);
  }
  100% {
    opacity: 1;
    transform: translateY(0) scale(1);
  }
}

@keyframes dropdownOut {
  0% {
    opacity: 1;
    transform: translateY(0) scale(1);
  }
  100% {
    opacity: 0;
    transform: translateY(-10px) scale(0.95);
  }
}

/* 选项之间的分割线 */
.select-option:not(:last-child) {
  border-bottom: 1px solid rgba(48, 54, 61, 0.5);
}
</style>
