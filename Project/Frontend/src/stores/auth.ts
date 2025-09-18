import { defineStore } from "pinia";

export const useAuthStore = defineStore("auth", {
  state: () => ({
    isLoggedIn: false,
    userEmail: '',
    userName: ''
  }),
  
  actions: {
    login(email?: string, name?: string) {
      this.isLoggedIn = true;
      if (email) {
        this.userEmail = email;
        localStorage.setItem('userEmail', email);
      }
      if (name) {
        this.userName = name;
        localStorage.setItem('userName', name);
      }
      localStorage.setItem("isLoggedIn", "true");
    },
    
    logout() {
      this.isLoggedIn = false;
      this.userEmail = '';
      this.userName = '';
      
      // 清除所有localStorage数据
      localStorage.removeItem("isLoggedIn");
      localStorage.removeItem("userEmail");
      localStorage.removeItem("userName");
    },
    
    // 从localStorage初始化状态
    initFromStorage() {
      const isLoggedIn = localStorage.getItem('isLoggedIn') === 'true';
      const userEmail = localStorage.getItem('userEmail') || '';
      const userName = localStorage.getItem('userName') || '';
      
      if (isLoggedIn) {
        this.isLoggedIn = true;
        this.userEmail = userEmail;
        this.userName = userName;
      }
    }
  },
});
