<template>
  <div id="app">

    <el-container style="height: 100vh">
      <SideBar></SideBar>

          <TopBar style="width: 100%; height: 50px;"></TopBar>
          <div><el-main class="vue">
            <router-view></router-view>
          </el-main></div>

    </el-container>


      
  </div>
</template>

<script>
import { Component, Vue } from "vue-property-decorator"
import SideBar from "@/components/SideBar.vue"
import TopBar from "@/components/TopBar.vue"

@Component({
  components: {
    SideBar,
    TopBar,
  },
})

export default class App extends Vue {}
</script>

<style lang="scss">
@import "./styles/global.scss";

#app {
  font-family: "Avenir", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
</style>
