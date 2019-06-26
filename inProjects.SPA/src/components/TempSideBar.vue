<template>
  <nav>
    <ul class="sidenav">
        <li>
            <a class="active" @click="redirect('/')">IN'TECH</a>
        </li>
        <div v-if="authService.authenticationInfo.level != 0">
            <li class="btn-group">
                <button class="buttons">
                    <img src="../assets/deco.png" height="30px" @click="logout()">
                </button>
                <button class="buttons">
                    <img src="../assets/profile.png" height="30px" @click="redirect('/MyProfil')"/>
                </button>
            </li>
        </div>
        <div v-else>
            <li class="btn-group">
                <button class="buttons" style="width: 100%;">
                    <img src="../assets/co.png" @click="redirect('/connection')">
                </button>
            </li>
        </div>
        <li>
            <a @click="redirect('/projectList')">Liste des projets</a>
        </li>
        <div v-for="(o, idx) in whatTimed" :key="idx">
            <div v-if="o == 'Administration'">
                <AdminPanel></AdminPanel>
            </div>
            <div v-if="o == 'User'">
                <UserPanel></UserPanel>
            </div>
            <div v-if="o == 'Student'">
                <StudentPanel></StudentPanel>
            </div>
        </div> 
    </ul>
  </nav>
</template>

<script lang="ts">
import { Component, Vue, Watch} from "vue-property-decorator"
import UserInfoBox from "./UserInfoBox.vue"
import AdminPanel from "./AdminPanel.vue"
import TeacherPanel from "./TeacherPanel.vue"
import JuryPanel from "./JuryPanel.vue"
import UserPanel from "./UserPanel.vue"
import StudentPanel from "./StudentPanel.vue"
import { AuthService } from "@signature/webfrontauth"
import { getGroupUserAccessPanel } from "../api/groupApi"
import { getAuthService } from "../modules/authService"
import * as SignalR from "@aspnet/signalr"
import { SignalRGestion } from "../modules/classes/SignalR"

@Component({
  components: {
    UserInfoBox,
    AdminPanel,
    TeacherPanel,
    UserPanel,
    JuryPanel,
    StudentPanel
  },
})

export default class TempSideBar extends Vue {
    isCollapse: boolean = false
    whatTimed: string[] = []
    ZoneId: number = 4
    authService: AuthService = getAuthService()
    private co!: SignalR.HubConnection
    private signalr: SignalRGestion = new SignalRGestion()

@Watch("authService.authenticationInfo.level", { immediate: true, deep: true })
  async onLevelChange() {
        await this.getAuthorizedAccess()

    }

    async mounted() {
        this.co = this.$store.state.connectionStaffMember
        if ( this.co.state == undefined ) {
            await this.signalr.connect()
        }
    }
    handleOpen(key: number, keyPath: number) {
        console.log(key, keyPath)
    }
    handleClose(key: number, keyPath: number) {
        console.log(key, keyPath)
    }
    changeCollapse() {
        this.isCollapse = !this.isCollapse
    }
    redirect(destination: string) {
        this.$router.push(destination)
    }
    async logout() {
        await this.authService.logout(true)
        this.$router.push("/")
    }
    async getAuthorizedAccess() {
        this.whatTimed = await getGroupUserAccessPanel(this.ZoneId)
        console.log("ok" + this.whatTimed)
        this.$store.state.currentUserType = this.whatTimed
        console.log("store :")
        console.log(this.$store.state)
    }
}
</script>

<style>
nav {
  background-color: #2d3e4f;
}
ul.sidenav {
  background-color: #2d3e4f;
  position: fixed;
  height: 100%;
  width: 15%;  
  margin: 0;
  padding: 0;
}
#intech {
  background-color: #4CAF50;
  color: white;
  border: none;
  width: 100%;
}
.in-menu {
    display: block;
    margin-top: 116px;
    margin-bottom: 50px;
    float: none;
    text-align: center;
}
 
ul.sidenav li a.active {
  background-color: #4CAF50;
  color: white;
  line-height: 25px;
  font-size: 24px;
  font-weight: bold;
}

ul.sidenav li a {
  display: block;
  color: white;
  padding: 18px 16px;
  text-decoration: none;
  cursor: pointer;
}
.btn-group .buttons {
  background-color: #2d3e4f;
  border: none;
  padding: 10px 25px;
  font-size: 16px;
  cursor: pointer;
  /* width: 50%; */
  margin-top: 2%;
  margin-bottom: 2%;
}
.btn-group .buttons:hover {
  background-color: #2d4f4f;
}
ul.sidenav li a:hover:not(.active) {
  background-color: #2d4f4f;
}

@media screen and (max-width: 900px) {
  ul.sidenav {
    width: 100%;
    height: 18vh;
    position: relative;
    list-style: none;
  }
  ul.sidenav li a {
    text-align: center;
    float: left;
    padding: 15px;
    height: 18vh;
  }
  ul.sidenav li a.active {
    height: 18vh;
  }
  .btn-group .buttons {
    background-color: #2d3e4f;
    border: none;
    cursor: pointer;
    float: left;
    width: 5%;
    height: 18vh;
    padding-right: 6%;
  }
  div.okok {
    float: left;
  }
}
@media screen and (max-width: 400px) {
  ul.sidenav li a {
    text-align: center;
    float: none;
  }
}
</style>
