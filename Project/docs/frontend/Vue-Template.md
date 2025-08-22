# Vue Template详解

## 📋 概述

`<template>`块定义Vue组件的HTML结构，是用户看到的界面部分。

## 🎯 基本语法

### 1. HTML基础结构
```vue
<template>
  <div class="container">
    <h1>标题</h1>
    <p>段落内容</p>
  </div>
</template>
```

### 2. 数据绑定
```vue
<template>
  <div>
    <!-- 文本插值 -->
    <h1>{{ title }}</h1>
    
    <!-- 属性绑定 -->
    <img :src="imageUrl" :alt="imageAlt">
    
    <!-- 双向绑定 -->
    <input v-model="message">
  </div>
</template>
```

## 🔧 常用指令

### 条件渲染
```vue
<template>
  <div>
    <!-- v-if条件渲染 -->
    <p v-if="loading">加载中...</p>
    <div v-else-if="error">出错了</div>
    <div v-else>内容显示</div>
    
    <!-- v-show显示隐藏 -->
    <div v-show="isVisible">可见内容</div>
  </div>
</template>
```

### 列表渲染
```vue
<template>
  <div>
    <!-- 遍历数组 -->
    <ul>
      <li v-for="item in items" :key="item.id">
        {{ item.name }}
      </li>
    </ul>
    
    <!-- 遍历对象 -->
    <div v-for="(value, key) in user" :key="key">
      {{ key }}: {{ value }}
    </div>
  </div>
</template>
```

### 事件处理
```vue
<template>
  <div>
    <!-- 事件绑定 -->
    <button @click="handleClick">点击我</button>
    <button @click="handleClickWithParam('参数')">带参数</button>
    
    <!-- 修饰符 -->
    <form @submit.prevent="handleSubmit">
      <input @keyup.enter="handleEnter">
    </form>
  </div>
</template>
```

## 🎨 模板特性

### 1. 插槽(Slots)
```vue
<template>
  <div class="card">
    <header>
      <slot name="header"></slot>
    </header>
    <main>
      <slot></slot>  <!-- 默认插槽 -->
    </main>
    <footer>
      <slot name="footer"></slot>
    </footer>
  </div>
</template>
```

### 2. 动态组件
```vue
<template>
  <div>
    <component :is="currentComponent"></component>
  </div>
</template>
```

## ⚠️ 注意事项

### 单根节点
```vue
<!-- Vue 2 需要单根节点 -->
<template>
  <div>  <!-- 根节点 -->
    <h1>标题</h1>
    <p>内容</p>
  </div>
</template>

<!-- Vue 3 支持多根节点 -->
<template>
  <h1>标题</h1>
  <p>内容</p>
</template>
```

### 模板语法限制
```vue
<template>
  <!-- ❌ 不能使用JavaScript语句 -->
  <div>{{ var a = 1 }}</div>
  
  <!-- ✅ 只能使用表达式 -->
  <div>{{ message.split('').reverse().join('') }}</div>
</template>
```

## 📚 相关文档

- [Vue SFC概述](./Vue-SFC-Overview.md)
- [Vue Script详解](./Vue-Script.md)
- [HelloWorld实例分析](./HelloWorld-Analysis.md)
