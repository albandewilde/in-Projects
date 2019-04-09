import Vue from "vue"
import ElementUI from "element-ui"
import "element-ui/lib/theme-chalk/index.css"
import App from "./App.vue"
import router from "./router"
import store from "./store"
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome"
import { library } from "@fortawesome/fontawesome-svg-core"
import { faHome, faUserGraduate, faUserTie, faFileAlt, faClipboard, faBars, faSignInAlt, faSignOutAlt, faBell, faUser, faCog } from "@fortawesome/free-solid-svg-icons"
import {initializeAuthService, /*getAuthService*/} from "./modules/authService"

library.add(faHome, faUserGraduate, faUserTie, faFileAlt, faClipboard, faBars, faSignInAlt, faSignOutAlt, faBell, faUser, faCog)

Vue.component("font-awesome-icon", FontAwesomeIcon)

Vue.config.productionTip = false

Vue.use(ElementUI)

initializeAuthService().then( () => {
  new Vue({
    router,
    store,
    render: (h) => h(App),
  }).$mount("#app")
})