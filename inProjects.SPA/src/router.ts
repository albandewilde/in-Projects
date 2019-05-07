import Vue from "vue"
import Router from "vue-router"
import Home from "./views/Home.vue"
import Connection from "./views/Connection.vue"
import Student from "./views/Student.vue"
import StaffMember from "./views/StaffMember.vue"

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
      path: "/student",
      name: "student",
      component: Student,

    },
    {
      path: "/staffMember",
      name: "student",
      component: StaffMember 
    }

  ],
  mode: "history",
})
