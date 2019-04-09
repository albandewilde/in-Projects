<template>
  <div id="app">
    <el-container style="height: 100vh">
      <NavBar></NavBar>
      <el-container>
        <el-main class="vue">
          <router-view></router-view>
        </el-main>
      </el-container>
    </el-container>

  </div>
</template>

<script>
import { Component, Vue } from "vue-property-decorator"
import NavBar from "@/components/NavBar.vue"

@Component({
  components: {
    NavBar,
  },
})

export default class App extends Vue {}

</script>

<style>
#app {
  font-family: "Avenir", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
body {
  margin: 0px;
}
</style>
