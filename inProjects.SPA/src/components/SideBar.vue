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

        <el-submenu index="6">
            <template slot="title">
                <font-awesome-icon icon="user-tie" size="lg" />
                <span> Periode </span>
            </template>
            <el-menu-item-group>
                <el-menu-item index="3-1" @click="redirect('/createPeriod')" >Creer Periode </el-menu-item>
            </el-menu-item-group>
        </el-submenu>
    </el-menu>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator"
import UserInfoBox from "./UserInfoBox.vue"
import { AuthService } from "@signature/webfrontauth"
import { getAuthService } from "../modules/authService"

@Component({
  components: {
    UserInfoBox
  },
})
export default class SideBar extends Vue {
    isCollapse: boolean = true
    authService: AuthService = getAuthService()
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
