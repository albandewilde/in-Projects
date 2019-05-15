<template>
    <el-menu 
        class="el-menu-vertical" 
        background-color=#545c64
        default-active="1"
        text-color="white"
        active-text-color=#ffd04b
        @open="handleOpen"
        @close="handleClose"
        :collapse="isCollapse"
        :unique-opened="true"     
        width="200px"       
        style="height: 100%;">

        <el-button class="collapseBtn" type="info" circle @click="changeCollapse()">
            <font-awesome-icon icon="bars" />
        </el-button>
        <br><br>
                
        <div v-if="authService.authenticationInfo.level != 0">
            <el-button type="warning" size="small" circle>
                <font-awesome-icon icon="bell" size="lg" />
            </el-button>
            <div v-if="this.isCollapse">
                <br>
            </div>
            <el-button type="info" size="small" circle>
                <font-awesome-icon icon="cog" size="lg" />
            </el-button>
            <div v-if="this.isCollapse">
                <br>
            </div>
            <el-button type="info" size="small" circle>
                <font-awesome-icon icon="search" size="lg" />
            </el-button>
            <div v-if="this.isCollapse">
                <br>
            </div>
            <el-menu-item index="6" @click="logout()">
                <font-awesome-icon icon="sign-out-alt" size="lg" />
                <span v-if="!isCollapse"> Se déconnecter</span>
            </el-menu-item>
        </div>
        <div v-else>
            <el-menu-item index="7" @click="redirect(`/connection`)">
                <font-awesome-icon icon="sign-in-alt" size="lg" />
                <span v-if="!isCollapse"> Se connecter</span>
            </el-menu-item>
        </div>

        <el-menu-item index="1" @click="redirect(`/`)">
            <font-awesome-icon icon="home" size="lg" />
            <span> Accueil</span>
        </el-menu-item>

        <el-submenu index="2">
            <template slot="title">
                <font-awesome-icon icon="user-graduate" size="lg" />
                <span> Étudiants</span>
            </template>
            <el-menu-item-group width="100%">
                <el-menu-item index="2-1" @click="redirect('/student')">Liste des étudiants</el-menu-item>
                <el-menu-item index="2-2">Trouver un étudiant</el-menu-item>
            </el-menu-item-group>
        </el-submenu>

        <el-submenu index="3">
            <template slot="title">
                <font-awesome-icon icon="user-tie" size="lg" />
                <span> Professeurs</span>
            </template>
            <el-menu-item-group>
                <el-menu-item index="3-1" @click="redirect('/staffMember')">Liste des professeurs</el-menu-item>
                <el-menu-item index="3-2">Trouver un professeur</el-menu-item>
            </el-menu-item-group>
        </el-submenu>

           
        <el-submenu index="4">
            <template slot="title">
                <font-awesome-icon icon="file-alt" size="lg" />
                <span> Projets</span>
            </template>
        <el-menu-item-group>
            <el-menu-item index="4-1">Liste des projets</el-menu-item>
            <el-menu-item index="4-2">Trouver un projet</el-menu-item>
        </el-menu-item-group>
        </el-submenu>

        <el-menu-item index="5">
            <font-awesome-icon icon="clipboard" size="lg" />
            <span> Forum PI</span>
        </el-menu-item>

        <div v-for="(o,idx) in whatTimed" :key="idx">
            <!-- index Admin 10 to 30 -->
            <div v-if="o == 'Administration'">
                <AdminPanel :isCollapse="isCollapse"></AdminPanel>
            </div>

            <!-- index Teacher 31 to 51 -->
             <div v-if="o == 'Teacher'">
                <TeacherPanel :isCollapse="isCollapse"></TeacherPanel>
            </div>

            <!-- index User 52 to 72 -->
            <div v-if="o == 'User'">
                <UserPanel :isCollapse="isCollapse"></UserPanel>
            </div>

            <!-- index Jury 73 to 93 -->
            <div v-if="o == 'Jury'">
                <JuryPanel :isCollapse="isCollapse"></JuryPanel>
            </div>

            <!-- index Student 94 to 114 -->
            <div v-if="o == 'Student'">
                <StudentPanel :isCollapse="isCollapse"></StudentPanel>
            </div>
        </div>

    </el-menu>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator"
import UserInfoBox from "./UserInfoBox.vue"
import AdminPanel from "./AdminPanel.vue"
import TeacherPanel from "./TeacherPanel.vue"
import JuryPanel from "./JuryPanel.vue"
import UserPanel from "./UserPanel.vue"
import StudentPanel from "./StudentPanel.vue"
import { AuthService } from "@signature/webfrontauth"
import { getGroupUserAccessPanel } from "../api/groupApi"
import { getAuthService } from "../modules/authService"

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
    isCollapse: boolean = true
    whatTimed : string[] = []
    ZoneId : number = 4
    authService: AuthService = getAuthService()

     async mounted(){
       const vm = this;
        await this.getAuthorizedAccess()

        this.$root.$on('refreshSideBar', function () {
               vm.getAuthorizedAccess()
        })
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
        this.$router.replace(destination)
    }
    async logout() {
        await this.authService.logout(true)
        this.$router.replace("/")
        this.getAuthorizedAccess();
    }
    async getAuthorizedAccess(){
        this.whatTimed = await getGroupUserAccessPanel(this.ZoneId);
        console.log(this.whatTimed)
    }
}
</script>

<style>
.el-menu-vertical:not(.el-menu--collapse) {
    width: 200px;
    min-height: 400px;
}
.el-menu-vertical{
    width: 80px;
    min-height: 400px;
    z-index: 10
}
</style>
