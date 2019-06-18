import Vue from "vue"
import Router from "vue-router"
import Home from "./views/Home.vue"
import Connection from "./components/Connection.vue"
import SubmitProject from "./components/SubmitProject.vue"
import StudentList from "./components/StudentList.vue"
import CreatePeriod from "./components/CreatePeriod.vue"
import ListPeriod from "./components/ListPeriod.vue"
import StaffMemberList from "./components/StaffMemberList.vue"
import ProjectDetail from "./components/ProjectDetail.vue"
import MyProfil from "./components/MyProfil.vue"
import Plan from "./components/Plan.vue"
import ProjectList from "./components/ProjectList.vue"
import AddJury from "./components/AddJury.vue"
import ForumPlan from "./components/ForumPlan.vue"
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
      component: StudentList
    },
    {
     path: "/createPeriod",
     name: "createPeriod",
     component: CreatePeriod
    },
    {
      path: "/staffMember",
      name: "staffMember",
      component: StaffMemberList
    },
    {
      path: "/plan",
      name: "plan",
      component: ForumPlan
    },
    {
      path: "/test",
      name: "test",
      component: ForumPlan
    },
    {
      path: "/listPeriod",
      name: "listPeriod",
      component: ListPeriod
    },
    {
      path: "/projectList",
      name: "projectList",
      component: ProjectList
     },
     {
       path: "/addJury",
       name: "addJury",
       component: AddJury
     },
     {
      path: "/MyProfil",
      name: "MyProfil",
      component: MyProfil
     },
     {
      path: "/Project/:projectId",
      name: "Project",
      component: ProjectDetail
    }

  ],
  mode: "history"
})
