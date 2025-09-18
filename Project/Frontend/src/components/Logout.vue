<script setup lang="ts">
import { onMounted } from 'vue';
import router from '@/router';
import { useAuthStore } from '@/stores/auth';

const auth = useAuthStore()

onMounted(() => {
    handleLogout();
})

const handleLogout = () => {
  auth.logout()
  // 路由跳转到登录页
  router.push('/login')
}
</script>