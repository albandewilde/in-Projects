import Vue from 'vue'
import ElementUI from 'element-ui'
import 'element-ui/lib/theme-chalk/index.css'
import App from './App.vue'
import router from './router'
import store from './store'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'

import {initializeAuthService, getAuthService} from './modules/authService'

Vue.config.productionTip = false;
Vue.use(ElementUI);
Vue.component('font-awesome-icon', FontAwesomeIcon)

initializeAuthService().then( () => {
  new Vue({
    router,
    store,
    render: (h) => h(App),
  }).$mount('#app')
});