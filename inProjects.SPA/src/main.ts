import Vue from 'vue';
import App from './App.vue';
import router from './router';
import store from './store';
import axios from 'axios';
import {initializeAuthService} from './modules/authService';

Vue.config.productionTip = false;
const axiosConst =  axios.create();

initializeAuthService(axiosConst).then( () => {
  new Vue({
    router,
    store,
    render: (h) => h(App),
  }).$mount('#app');
});