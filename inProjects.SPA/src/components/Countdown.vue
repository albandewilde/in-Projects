<template>
  <div>
	<h2>Prochain forum PI dans : {{days}}d {{hours}}h {{minutes}}m {{seconds}}s</h2>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator"

@Component
export default class Countdown extends Vue {
    nextForum: Date = new Date("July 05, 2019 8:45:00")
    days: number = 0
    hours: number = 0
    minutes: number = 0
    seconds: number = 0
    vm = this

    async mounted() {
        const vm = this
        const x = setInterval(() => {
            const timeToNextForum: number = vm.nextForum.getTime() - new Date().getTime()

            vm.days = Math.floor(timeToNextForum / (1000 * 60 * 60 * 24))
            vm.hours = Math.floor((timeToNextForum % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60))
            vm.minutes = Math.floor((timeToNextForum % (1000 * 60 * 60)) / (1000 * 60))
            vm.seconds = Math.floor((timeToNextForum % (1000 * 60)) / 1000)
        }, 1000)
    }
}

</script>