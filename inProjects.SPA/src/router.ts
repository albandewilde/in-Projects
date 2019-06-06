import Vue from "vue"
import Router from "vue-router"
import Home from "./views/Home.vue"
import Connection from "./views/Connection.vue"
import SubmitProject from "./components/SubmitProject.vue"
import Student from "./views/Student.vue"
import CreatePeriod from "./components/CreatePeriod.vue"
import ListPeriod from "./components/ListPeriod.vue"
import StaffMember from "./views/StaffMember.vue"
import Plan from "./components/Plan.vue"
Vue.use(Router)

export default new Router({
  routes: [
    {
      path: "/",
      name: "home",
      component: Home
    },
    {
      path: "/connection",
      name: "connection",
      component: Connection,
    },
    {
      path: "/submit_project",
      name: "submit_project",
      component: SubmitProject
    },
    {
      path: "/student",
      name: "student",
      component: Student
    },
    {
     path: "/createPeriod",
     name: "createPeriod",
     component: CreatePeriod
    },
    {
      path: "/staffMember",
      name: "staffMember",
      component: StaffMember
    },
    {
      path: "/plan",
      name: "plan",
      component: Plan
    },
    {
      path: "/listPeriod",
      name: "listPeriod",
      component: ListPeriod
     }
  ],
  mode: "history"
})
