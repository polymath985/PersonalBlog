<template>
  <div id="app">
    <!-- 路由视图容器 -->
    <router-view />
  </div>
</template>

<script setup lang="ts">
// 移除所有页面切换逻辑，现在由路由管理
import TopNavigation from './components/TopNavigation.vue'
</script>

<style scoped>
#app {
  width: 100vw;
  height: 100vh;
  margin: 0;
  padding: 0;
  font-weight: normal;
  overflow-x: hidden;
}
</style>
