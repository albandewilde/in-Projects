import Vue from "vue"
import ElementUI from "element-ui"
import "element-ui/lib/theme-chalk/index.css"
import App from "./App.vue"
import router from "./router"
import store from "./store"
import axios from "axios"
import {initializeAuthService} from "./modules/authService"
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome"
import { library } from "@fortawesome/fontawesome-svg-core"
import {
    faHome, faUserGraduate, faUserTie,
    faFileAlt, faClipboard, faBars,
    faSignInAlt, faSignOutAlt, faBell,
    faUser, faCog
} from "@fortawesome/free-solid-svg-icons"

library.add(
    faHome, faUserGraduate, faUserTie,
    faFileAlt, faClipboard, faBars,
    faSignInAlt, faSignOutAlt, faBell,
    faUser, faCog
)

Vue.component("font-awesome-icon", FontAwesomeIcon)

Vue.config.productionTip = false
const axiosConst = axios.create()
Vue.use(ElementUI)

initializeAuthService(axiosConst).then( () => {
    new Vue({
        router,
        store,
        render: (h) => h(App),
    }).$mount("#app")
})