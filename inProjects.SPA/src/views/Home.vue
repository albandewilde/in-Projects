<template>
  <div class="home">
    <img src="../assets/intechLogo.png" width="300em;"><br>
    
    <Countdown></Countdown>
    <b><u>NOUS PENDANT CE PROJET :</u></b><br><br>
    <img src="/nous.gif">

  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator"
import Countdown from "@/components/Countdown.vue"

@Component({
    components: {
        Countdown
    }
})
export default class Home extends Vue {

}
</script>