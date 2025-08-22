# Vue Script详解

## 📋 概述

`<script>`块定义Vue组件的JavaScript逻辑，包括数据、方法、生命周期等。

## 🎯 两种API风格

### 1. 组合式API (Composition API) - 推荐
```vue
<script setup lang="ts">
import { ref, computed, onMounted } from 'vue'

// 响应式数据
const count = ref(0)
const message = ref('Hello')

// 计算属性
const doubleCount = computed(() => count.value * 2)

// 方法
const increment = () => {
  count.value++
}

// 生命周期
onMounted(() => {
  console.log('组件已挂载')
})
</script>
```

### 2. 选项式API (Options API)
```vue
<script lang="ts">
import { defineComponent } from 'vue'

export default defineComponent({
  data() {
    return {
      count: 0,
      message: 'Hello'
    }
  },
  computed: {
    doubleCount() {
      return this.count * 2
    }
  },
  methods: {
    increment() {
      this.count++
    }
  },
  mounted() {
    console.log('组件已挂载')
  }
})
</script>
```

## 🔧 核心概念

### 响应式数据
```vue
<script setup lang="ts">
import { ref, reactive } from 'vue'

// 基本类型使用ref
const count = ref(0)
const name = ref('Vue')

// 对象类型使用reactive
const user = reactive({
  id: 1,
  name: '张三',
  email: 'zhangsan@example.com'
})

// 访问ref值需要.value
console.log(count.value) // 0
count.value = 10

// reactive对象直接访问
console.log(user.name) // 张三
user.name = '李四'
</script>
```

### 计算属性
```vue
<script setup lang="ts">
import { ref, computed } from 'vue'

const firstName = ref('张')
const lastName = ref('三')

// 只读计算属性
const fullName = computed(() => `${firstName.value}${lastName.value}`)

// 可写计算属性
const writableFullName = computed({
  get() {
    return `${firstName.value}${lastName.value}`
  },
  set(value) {
    [firstName.value, lastName.value] = value.split(' ')
  }
})
</script>
```

### 监听器
```vue
<script setup lang="ts">
import { ref, watch, watchEffect } from 'vue'

const count = ref(0)
const user = reactive({ name: '张三' })

// 监听单个源
watch(count, (newVal, oldVal) => {
  console.log(`count从${oldVal}变为${newVal}`)
})

// 监听多个源
watch([count, () => user.name], ([newCount, newName]) => {
  console.log('count或name发生了变化')
})

// 立即执行的监听器
watchEffect(() => {
  console.log(`当前count: ${count.value}`)
})
</script>
```

## 🔄 生命周期钩子

### 组合式API生命周期
```vue
<script setup lang="ts">
import { 
  onBeforeMount, 
  onMounted, 
  onBeforeUpdate, 
  onUpdated, 
  onBeforeUnmount, 
  onUnmounted 
} from 'vue'

onBeforeMount(() => {
  console.log('组件挂载前')
})

onMounted(() => {
  console.log('组件已挂载')
  // DOM已就绪，可以访问DOM元素
})

onBeforeUpdate(() => {
  console.log('组件更新前')
})

onUpdated(() => {
  console.log('组件已更新')
})

onBeforeUnmount(() => {
  console.log('组件卸载前')
  // 清理工作
})

onUnmounted(() => {
  console.log('组件已卸载')
})
</script>
```

## 📦 组件通信

### Props和Emits
```vue
<script setup lang="ts">
// 定义Props
interface Props {
  title: string
  count?: number
}

const props = withDefaults(defineProps<Props>(), {
  count: 0
})

// 定义Emits
const emit = defineEmits<{
  update: [value: number]
  change: [data: { id: number, name: string }]
}>()

// 触发事件
const handleClick = () => {
  emit('update', props.count + 1)
  emit('change', { id: 1, name: '新值' })
}
</script>
```

### 模板引用
```vue
<script setup lang="ts">
import { ref, onMounted } from 'vue'

// 获取DOM元素引用
const inputRef = ref<HTMLInputElement>()

// 获取组件实例引用
const childRef = ref<InstanceType<typeof ChildComponent>>()

onMounted(() => {
  // 访问DOM元素
  inputRef.value?.focus()
  
  // 调用子组件方法
  childRef.value?.someMethod()
})
</script>

<template>
  <input ref="inputRef" type="text">
  <ChildComponent ref="childRef" />
</template>
```

## 🎯 最佳实践

### 1. 类型安全
```vue
<script setup lang="ts">
// 定义接口
interface User {
  id: number
  name: string
  email: string
}

// 使用类型
const users = ref<User[]>([])
const currentUser = ref<User | null>(null)

// 类型断言
const apiResponse = await fetch('/api/users')
const userData = await apiResponse.json() as User[]
</script>
```

### 2. 组合函数(Composables)
```vue
<script setup lang="ts">
// 自定义组合函数
function useCounter(initialValue = 0) {
  const count = ref(initialValue)
  const increment = () => count.value++
  const decrement = () => count.value--
  
  return { count, increment, decrement }
}

// 使用组合函数
const { count, increment, decrement } = useCounter(10)
</script>
```

## 📚 相关文档

- [Vue SFC概述](./Vue-SFC-Overview.md)
- [Vue Template详解](./Vue-Template.md)
- [HelloWorld实例分析](./HelloWorld-Analysis.md)
