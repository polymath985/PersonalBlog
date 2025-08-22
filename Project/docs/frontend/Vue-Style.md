# Vue Style详解

## 📋 概述

`<style>`块定义Vue组件的CSS样式，支持作用域样式和CSS预处理器。

## 🎯 基本用法

### 1. 普通样式
```vue
<style>
.container {
  max-width: 1200px;
  margin: 0 auto;
  padding: 20px;
}

.title {
  font-size: 24px;
  color: #333;
  margin-bottom: 16px;
}
</style>
```

### 2. 作用域样式(推荐)
```vue
<style scoped>
/* 只影响当前组件 */
.container {
  background-color: #f5f5f5;
}

.title {
  color: #2c3e50;
}
</style>
```

## 🔒 作用域样式

### Scoped原理
```vue
<!-- 编译前 -->
<template>
  <div class="container">
    <h1 class="title">标题</h1>
  </div>
</template>

<style scoped>
.container { background: #fff; }
.title { color: #333; }
</style>

<!-- 编译后 -->
<template>
  <div class="container" data-v-f3f3eg9>
    <h1 class="title" data-v-f3f3eg9>标题</h1>
  </div>
</template>

<style>
.container[data-v-f3f3eg9] { background: #fff; }
.title[data-v-f3f3eg9] { color: #333; }
</style>
```

### 深度选择器
```vue
<style scoped>
/* 影响子组件 */
:deep(.child-class) {
  color: red;
}

/* Vue 2语法 */
/deep/ .child-class {
  color: red;
}

>>> .child-class {
  color: red;
}
</style>
```

## 🎨 CSS预处理器

### 1. SCSS/Sass
```vue
<style lang="scss" scoped>
$primary-color: #42b983;
$border-radius: 8px;

.card {
  background: white;
  border-radius: $border-radius;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  
  &:hover {
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.15);
  }
  
  .header {
    background: $primary-color;
    color: white;
    padding: 16px;
    
    h3 {
      margin: 0;
      font-size: 18px;
    }
  }
  
  .content {
    padding: 16px;
  }
}
</style>
```

### 2. Less
```vue
<style lang="less" scoped>
@primary-color: #42b983;
@border-radius: 8px;

.card {
  background: white;
  border-radius: @border-radius;
  
  &:hover {
    transform: translateY(-2px);
  }
  
  .header {
    background: @primary-color;
    .title {
      font-weight: bold;
    }
  }
}
</style>
```

## 🔧 CSS Modules

### 基本用法
```vue
<template>
  <div :class="$style.container">
    <h1 :class="$style.title">标题</h1>
    <p :class="[$style.text, $style.highlight]">内容</p>
  </div>
</template>

<style module>
.container {
  padding: 20px;
}

.title {
  font-size: 24px;
  color: #333;
}

.text {
  line-height: 1.6;
}

.highlight {
  background-color: yellow;
}
</style>
```

### 自定义模块名
```vue
<template>
  <div :class="classes.wrapper">
    <span :class="classes.text">文本</span>
  </div>
</template>

<style module="classes">
.wrapper {
  display: flex;
  align-items: center;
}

.text {
  color: #666;
}
</style>
```

## 🎯 动态样式

### 1. 动态类名
```vue
<template>
  <div 
    :class="{
      active: isActive,
      disabled: isDisabled,
      'text-center': centered
    }"
  >
    动态类名
  </div>
</template>

<script setup lang="ts">
const isActive = ref(true)
const isDisabled = ref(false)
const centered = ref(true)
</script>
```

### 2. 内联样式
```vue
<template>
  <div 
    :style="{
      color: textColor,
      fontSize: fontSize + 'px',
      backgroundColor: bgColor
    }"
  >
    动态样式
  </div>
  
  <!-- 或使用样式对象 -->
  <div :style="styleObject">
    样式对象
  </div>
</template>

<script setup lang="ts">
const textColor = ref('#333')
const fontSize = ref(16)
const bgColor = ref('#f0f0f0')

const styleObject = computed(() => ({
  color: textColor.value,
  fontSize: fontSize.value + 'px',
  backgroundColor: bgColor.value
}))
</script>
```

## 🌍 CSS变量

### 响应式CSS变量
```vue
<template>
  <div class="container">
    <p class="text">响应式颜色文本</p>
  </div>
</template>

<script setup lang="ts">
const primaryColor = ref('#42b983')

// 动态修改CSS变量
const changeColor = () => {
  primaryColor.value = '#ff6b6b'
}
</script>

<style scoped>
.container {
  --primary-color: v-bind(primaryColor);
}

.text {
  color: var(--primary-color);
  transition: color 0.3s ease;
}
</style>
```

## 📱 响应式设计

### 媒体查询
```vue
<style scoped>
.container {
  padding: 20px;
}

.grid {
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  gap: 20px;
}

/* 平板 */
@media (max-width: 768px) {
  .grid {
    grid-template-columns: repeat(2, 1fr);
  }
}

/* 手机 */
@media (max-width: 480px) {
  .container {
    padding: 10px;
  }
  
  .grid {
    grid-template-columns: 1fr;
  }
}
</style>
```

## ⚠️ 注意事项

### 1. 样式优先级
```vue
<!-- 优先级：内联样式 > scoped样式 > 全局样式 -->
<template>
  <div 
    class="global-class scoped-class" 
    style="color: red"  <!-- 最高优先级 -->
  >
    文本
  </div>
</template>

<style>
/* 全局样式 */
.global-class { color: blue; }
</style>

<style scoped>
/* scoped样式 */
.scoped-class { color: green; }
</style>
```

### 2. 性能考虑
```vue
<style scoped>
/* ✅ 推荐：使用类选择器 */
.button {
  background: #42b983;
}

/* ❌ 避免：深层嵌套 */
.container .content .item .button .text {
  color: red;
}

/* ✅ 推荐：简化选择器 */
.button-text {
  color: red;
}
</style>
```

## 📚 相关文档

- [Vue SFC概述](./Vue-SFC-Overview.md)
- [Vue Template详解](./Vue-Template.md)
- [HelloWorld实例分析](./HelloWorld-Analysis.md)
