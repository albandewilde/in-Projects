import Vue from "vue"
import Router from "vue-router"
import Home from "./views/Home.vue"
import Login from "./components/Login.vue"
import Register from "./components/Register.vue"
import Connection from "./views/Connection.vue"

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
    }
  ],
  mode: "history",
})
