<template>
  <div id="app">
    <el-container>
      <el-aside width="220px" style="background-color: rgb(238, 241, 246);">
        <el-menu 
          default-active="1" 
          class="el-menu-vertical-demo" 
          background-color="#545c64"
          text-color="#fff" 
          active-text-color="#ffd04b"
          @open="handleOpen"
          @close="handleClose"
          :unique-opened="true">
          
          <h4 style="color: white;">Mon Pseudo</h4>

          <el-menu-item index="1">
            <span><i class="fas fa-home"></i>Accueil</span>
          </el-menu-item>
          <el-submenu index="2">
            <template slot="title">
              <span>Étudiants</span>
            </template>
            <el-menu-item-group>
                <el-menu-item index="2-1">Liste des étudiants</el-menu-item>
                <el-menu-item index="2-2">Trouver un étudiant</el-menu-item>
            </el-menu-item-group>
          </el-submenu>
          <el-submenu index="3">
            <template slot="title">
              <span>Professeurs</span>
            </template>
            <el-menu-item-group>
                <el-menu-item index="3-1">Liste des professeurs</el-menu-item>
                <el-menu-item index="3-2">Trouver un professeur</el-menu-item>
            </el-menu-item-group>
          </el-submenu>
          <el-submenu index="4">
            <template slot="title">
              <span>Projets</span>
            </template>
            <el-menu-item-group>
                <el-menu-item index="4-1">Liste des projets</el-menu-item>
                <el-menu-item index="4-2">Trouver un projet</el-menu-item>
            </el-menu-item-group>
          </el-submenu>
          <el-menu-item index="5">
            <span>Forum PI</span>
          </el-menu-item>
        </el-menu>
      </el-aside>

      <el-container>
        <el-main class="vue">
          <router-view></router-view>
        </el-main>
      </el-container>
    </el-container>

  </div>
</template>

<script>
import Vue from 'vue'

export default Vue.extend({
  data() {
      return {

      }
  },

  methods: {
    handleOpen(key, keyPath) {
      console.log(key, keyPath);
    },
    handleClose(key, keyPath) {
      console.log(key, keyPath);
    }
  }
})
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
body {
  margin: 0px;
}
</style>
