# Vue单文件组件(SFC)概述

## 📋 什么是SFC

Vue单文件组件(Single File Component)是Vue.js的核心特性，允许在一个`.vue`文件中组织HTML、JavaScript和CSS。

## 🏗️ 基本结构

```vue
<template>
  <!-- HTML模板 -->
</template>

<script>
  // JavaScript逻辑
</script>

<style>
  /* CSS样式 */
</style>
```

## 📚 三大组成部分

### 1. Template块
- **作用**：定义组件的HTML结构
- **特点**：支持Vue指令和数据绑定
- [详细了解Template](./Vue-Template.md)

### 2. Script块
- **作用**：定义组件的逻辑和数据
- **特点**：支持TypeScript，组合式API/选项式API
- [详细了解Script](./Vue-Script.md)

### 3. Style块
- **作用**：定义组件的样式
- **特点**：支持作用域样式，CSS预处理器
- [详细了解Style](./Vue-Style.md)

## 🎯 核心优势

- ✅ **关注点分离**：HTML、JS、CSS逻辑清晰分离
- ✅ **组件化开发**：高内聚的功能模块
- ✅ **热重载支持**：开发时实时预览
- ✅ **工具链支持**：IDE语法高亮、智能提示

## 📖 实例分析

以项目中的HelloWorld.vue为例，展示SFC的实际应用：
- [HelloWorld.vue实例分析](./HelloWorld-Analysis.md)

## 📚 相关文档

- [Vue组件导入指南](./Vue-Component-Import-Guide.md)
- [Vue前端结构指南](./Vue-Frontend-Structure-Guide.md)
