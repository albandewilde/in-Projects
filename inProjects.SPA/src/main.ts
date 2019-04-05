import Vue from 'vue';
import App from './App.vue';
import router from './router';
import store from './store';
import {initializeAuthService, getAuthService} from './modules/authService';

Vue.config.productionTip = false;

initializeAuthService().then( () => {
  console.log(getAuthService());
});
getAuthService().basicLogin( 'toto', 'password' );
new Vue({
  router,
  store,
  render: (h) => h(App),
}).$mount('#app');
