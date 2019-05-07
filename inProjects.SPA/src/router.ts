import Vue from "vue"
import Router from "vue-router"
import Home from "./views/Home.vue"
import Connection from "./views/Connection.vue"
import CreatePeriod from "./components/CreatePeriod.vue"
Vue.use(Router)

export default new Router({
  routes: [
    {
      path: "/",
      name: "home",
      component: Home,
    },
    {
      path: "/connection",
      name: "connection",
      component: Connection,
    },
    {
      path: "/createPeriod",
      name: "createPeriod",
      component: CreatePeriod,
    }
  ],
  mode: "history",
})
