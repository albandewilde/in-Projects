<template>
  <div class="home">
    <img alt="In'Project logo" src="../assets/the_logo-ellipse.png" width="200">
    <HelloWorld msg="Welcome to In'Project"/>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator"

@Component({
  components: {
  },
})
export default class Home extends Vue {}
</script>