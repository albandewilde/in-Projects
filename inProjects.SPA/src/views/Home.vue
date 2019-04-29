<template>
  <div class="home">
    <img src="../assets/intechLogo.png" width="300em;"><br>
    
    <Countdown></Countdown>

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