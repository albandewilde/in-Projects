import Vue from 'vue';
import App from './App.vue';
import router from './router';
import store from './store';
import {initialize, getAuthService} from './modules/authService';

Vue.config.productionTip = false;

initialize();
console.log(getAuthService());
new Vue({
  router,
  store,
  render: (h) => h(App),
}).$mount('#app');
