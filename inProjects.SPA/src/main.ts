import Vue from 'vue'
import ElementUI from 'element-ui'
import 'element-ui/lib/theme-chalk/index.css'
import App from './App.vue'
import router from './router'
import store from './store'
import { library } from '@fortawesome/fontawesome-svg-core'
import { faCoffee, faHome, faUserGraduate, faUserTie, faFileAlt, faClipboard } from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
import {initializeAuthService, getAuthService} from './modules/authService'

library.add(faHome, faUserGraduate, faUserTie, faFileAlt, faClipboard)

Vue.component('font-awesome-icon', FontAwesomeIcon)

Vue.config.productionTip = false
Vue.use(ElementUI)

initializeAuthService().then( () => {
  new Vue({
    router,
    store,
    render: (h) => h(App),
  }).$mount('#app')
});