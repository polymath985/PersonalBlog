<script setup lang="ts">
import HelloWorld from './components/HelloWorld.vue'
import TheWelcome from './components/TheWelcome.vue'
// 🆕 导入新创建的示例组件
import ExampleComponent from './components/ExampleComponent.vue'

// 处理子组件事件的方法
const handleExampleClick = (count: number) => {
  console.log(`示例组件被点击了，当前次数: ${count}`)
}

const handleExampleToggle = (visible: boolean) => {
  console.log(`详情${visible ? '显示' : '隐藏'}了`)
}
</script>

<template>
  <header>
    <img alt="Vue logo" class="logo" src="./assets/logo.svg" width="125" height="125" />

    <div class="wrapper">
      <HelloWorld msg="You did it!" />
    </div>
  </header>

  <main>
    <TheWelcome />
    
    <!-- 🆕 使用新创建的示例组件 -->
    <!-- 演示 props 传递和事件监听 -->
    <ExampleComponent 
      title="我的第一个自定义组件"
      description="这个组件演示了 Vue.js 组件的基本功能"
      :is-visible="true"
      @click="handleExampleClick"
      @toggle="handleExampleToggle"
    />
  </main>
</template>

<style scoped>
header {
  line-height: 1.5;
}

.logo {
  display: block;
  margin: 0 auto 2rem;
}

@media (min-width: 1024px) {
  header {
    display: flex;
    place-items: center;
    padding-right: calc(var(--section-gap) / 2);
  }

  .logo {
    margin: 0 2rem 0 0;
  }

  header .wrapper {
    display: flex;
    place-items: flex-start;
    flex-wrap: wrap;
  }
}
</style>
