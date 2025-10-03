import { defineStore } from "pinia";

export const useAuthStore = defineStore("auth", {
  state: () => ({
    isLoggedIn: false,
    userEmail: '',
    userName: '',
    userId: '',
    registerTime: ''
  }),
  
  actions: {
    login(email?: string, name?: string, id?: string, registerTime?: string) {
      this.isLoggedIn = true;
      if (email) {
        this.userEmail = email;
        localStorage.setItem('userEmail', email);
      }
      if (name) {
        this.userName = name;
        localStorage.setItem('userName', name);
      }
      if (id) {
        localStorage.setItem('userId', id);
      }
      if (registerTime) {
        localStorage.setItem('registerTime', registerTime);
      }
      localStorage.setItem("isLoggedIn", "true");
    },
    
    logout() {
      this.isLoggedIn = false;
      this.userEmail = '';
      this.userName = '';
      this.userId = '';
      this.registerTime = '';

      localStorage.clear();
      localStorage.setItem("isLoggedIn", "false");
    },
    
    // 从localStorage初始化状态
    initFromStorage() {
      const isLoggedIn = localStorage.getItem('isLoggedIn') === 'true';
      const userEmail = localStorage.getItem('userEmail') || '';
      const userName = localStorage.getItem('userName') || '';
      const userId = localStorage.getItem('userId') || '';
      const registerTime = localStorage.getItem('registerTime') || '';

      if (isLoggedIn) {
        this.isLoggedIn = true;
        this.userEmail = userEmail;
        this.userName = userName;
        this.userId = userId;
        this.registerTime = registerTime;
      }
    }
  },
});
