<template>
    <el-aside width="220px" style="background-color: rgb(238, 241, 246);">
        <el-menu 
            default-active="1" 
            class="el-menu-vertical-demo" 
            background-color="#545c64"
            text-color="#fff" 
            active-text-color="#ffd04b"
            @open="handleOpen"
            @close="handleClose"
            :unique-opened="true"
            style="height: 100%;">

            <h4 style="color: white;">Mon Pseudo</h4>

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
import { Component, Vue } from 'vue-property-decorator'

@Component
export default class NavBar extends Vue {

    handleOpen(key, keyPath) {
        console.log(key, keyPath)
    }

    handleClose(key, keyPath) {
        console.log(key, keyPath)
    }
}
</script>

<style>
</style>
