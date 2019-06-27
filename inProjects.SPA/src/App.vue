<template>
    <div id="app">
      <TempSideBar></TempSideBar>
      <div class="content">
        <main class="vue">
            <router-view></router-view>
        </main>
      </div>

    </div>
</template>

<script>
import { Component, Vue } from "vue-property-decorator"
import SideBar from "@/components/SideBar.vue"
import TempSideBar from "@/components/TempSideBar.vue"

@Component({
  components: {
    // SideBar,
    TempSideBar
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
  margin-left: 17%;
  height: 100vh;
}
@media screen and (max-width: 900px) {
  .content{
    margin-left: 4%;
    margin-top: 2%;
    margin-right: 2%;
  }
}
</style>