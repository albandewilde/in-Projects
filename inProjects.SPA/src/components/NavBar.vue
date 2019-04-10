<template>
    <el-aside style="width: 201px;">
        <el-menu 
            default-active="1" 
            class="el-menu-vertical-demo" 
            background-color="#545c64"
            text-color="#fff" 
            active-text-color="#ffd04b"
            @open="handleOpen"
            @close="handleClose"
            :collapse="isCollapse"
            :unique-opened="true"
            style="height: 100%;">

            <el-button class="collapseBtn" type="info" circle @click="changeCollapse()">
                <font-awesome-icon icon="bars" />
            </el-button>

            <!-- if user is connected -->
            <h4 style="color: white;">Mon Pseudo</h4>
            <el-button type="warning" size="small" circle>
                <font-awesome-icon icon="bell" size="lg" />
            </el-button>
            <el-button type="warning" size="small" circle>
                <font-awesome-icon icon="user" size="lg" />
            </el-button>
            <el-button type="info" size="small" circle>
                <font-awesome-icon icon="cog" size="lg" />
            </el-button>
            <el-menu-item index="6">
                <font-awesome-icon icon="sign-out-alt" size="lg" />
                <span> Se déconnecter</span>
            </el-menu-item>
            <!--  -->

            <!-- if user not connected -->
            <el-menu-item index="6">
                <font-awesome-icon icon="sign-in-alt" size="lg" />
                <span> Se connecter</span>
            </el-menu-item>
            <!--  -->

            <el-menu-item index="1">
                <font-awesome-icon icon="home" size="lg" />
                <span> Accueil</span>
            </el-menu-item>
            <el-submenu index="2">
                <template slot="title">
                    <font-awesome-icon icon="user-graduate" size="lg" />
                    <span> Étudiants</span>
                </template>
                <el-menu-item-group>
                    <el-menu-item index="2-1">Liste des étudiants</el-menu-item>
                    <el-menu-item index="2-2">Trouver un étudiant</el-menu-item>
                </el-menu-item-group>
            </el-submenu>
            <el-submenu index="3">
                <template slot="title">
                    <font-awesome-icon icon="user-tie" size="lg" />
                    <span> Professeurs</span>
                </template>
                <el-menu-item-group>
                    <el-menu-item index="3-1">Liste des professeurs</el-menu-item>
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
        </el-menu>
    </el-aside>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator"

@Component
export default class NavBar extends Vue {
    
    isCollapse: boolean = true

    handleOpen(key: number, keyPath: number) {
        console.log(key, keyPath)
    }

    handleClose(key: number, keyPath: number) {
        console.log(key, keyPath)
    }

    changeCollapse() {
        this.isCollapse = !this.isCollapse
    }
}
</script>

<style>
    .el-menu-vertical-demo:not(.el-menu--collapse) {
        width: 200px;
        min-height: 400px;
    }
    .collapseBtn {
        margin-top: 10px;   
    }
    .settingsBtn {
        
    }
</style>
