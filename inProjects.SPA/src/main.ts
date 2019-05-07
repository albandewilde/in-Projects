import Vue from "vue"
import VeeValidate from "vee-validate"
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
    faUser, faCog, faSearch, faPlusSquare
} from "@fortawesome/free-solid-svg-icons"

library.add(
    faHome, faUserGraduate, faUserTie,
    faFileAlt, faClipboard, faBars,
    faSignInAlt, faSignOutAlt, faBell,
    faUser, faCog, faSearch, faPlusSquare
)

Vue.component("font-awesome-icon", FontAwesomeIcon)

Vue.config.productionTip = false
const axiosConst = axios.create()
Vue.use(ElementUI)
Vue.use(VeeValidate)

initializeAuthService(axiosConst).then( () => {
    new Vue({
        router,
        store,
        render: (h) => h(App),
    }).$mount("#app")
})