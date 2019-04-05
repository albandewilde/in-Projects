import Vue from 'vue';
import App from './App.vue';
import router from './router';
import store from './store';
import {initializeAuthService, getAuthService} from './modules/authService';

Vue.config.productionTip = false;

initializeAuthService().then( () => {
  new Vue({
    router,
    store,
    render: (h) => h(App),
  }).$mount('#app');
});