<template>
<div>
    <ul class="sidenav">
        <div class="in-menu">
            <li><center><el-button id="intech" class="active" @click="redirect('/')">IN'TECH</el-button></center></li>
            <div v-if="authService.authenticationInfo.level != 0">
                <li>
                    <el-button id="buttons"><img src="../assets/deco.png" height="30px" @click="logout()">&nbsp
                    <img src="../assets/profile.png" height="30px" @click="redirect('/MyProfil')"/></el-button>
                </li>
            </div>
            
            <div v-else>
                <li><center><el-button id="buttons"><img src="../assets/co.png" @click="redirect('/connection')"></el-button></center></li>
            </div>
            <li><a @click="redirect('/projectList')">Liste des projets</a></li>
            <li><a @click="redirect(`ProjectUserVote`)">Votez pour les projets</a></li>
            <div v-for="(o, idx) in whatTimed" :key="idx">
                <div v-if="o == 'Administration'">
                    <AdminPanel></AdminPanel>
                </div>
<!--                 <div v-if="o == 'User'">
                    <UserPanel></UserPanel>
                </div> -->
               <!-- <div v-if="o == 'Teacher'">
                    <TeacherPanel :isCollapse="isCollapse"></TeacherPanel>
                </div>        -->
                <div v-if="o == 'Jury'">
                    <JuryPanel :isCollapse="isCollapse"></JuryPanel>
                </div>

                <div v-if="o == 'Student'">
                    <StudentPanel></StudentPanel>
                </div>
            </div> 
        </div>
    </ul>

</div>
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

export default class SideBar extends Vue {
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

<!--<style>
.in-menu {
    display: block;
    margin-top: 116px;
    margin-bottom: 50px;
    float: none;
    text-align: center;
}

ul.sidenav {
  list-style-type: none;
  margin: 0;
  padding: 0;
  width: 15%;
  background-color: #2d3e4f;
  position: fixed;
  height: 100%;
  overflow: auto;
}
#intech {
  background-color: #4CAF50;
  color: white;
  border: none;
  width: 100%;
}
#buttons {
  display: block;
  color: white;
  /* padding: 8px 16px; */
  text-decoration: none;
  background-color: #2d3e4f;
  border: none;
  width: 100%;
}

ul.sidenav li a {
  display: block;
  color: white;
  padding: 8px 16px;
  text-decoration: none;
  cursor: pointer;
}
 
ul.sidenav li a.active {
  background-color: #4CAF50;
  color: white;
}

ul.sidenav li a:hover:not(.active) {
  background-color: #555;
  color: white;
}


@media screen and (max-width: 900px) {
  ul.sidenav {
    width: 100%;
    position: relative;
  }
  
  ul.sidenav li a {
    float: left;
    padding: 15px;
  }
  
  .in-menu{margin-top: 0%}
  div.content {margin-left: 0;}
  div.okok{float: left;}
}

@media screen and (max-width: 400px) {
  ul.sidenav li a {
    text-align: center;
    float: none;
  }
}
</style-->
 