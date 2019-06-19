<template>
    <div id="app">

      <SideBar></SideBar>
      <div class="content">
        <el-main class="vue">
            <router-view></router-view>
        </el-main>
      </div>

    </div>
</template>

<script>
import { Component, Vue } from "vue-property-decorator"
import SideBar from "@/components/SideBar.vue"

@Component({
  components: {
    SideBar,
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
.content{
  margin-left: 25%;
  padding: 1px 16px;
  height: 1000px;
}
</style>