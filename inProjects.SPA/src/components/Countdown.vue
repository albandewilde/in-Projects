<template>
  <div>
	Prochain forum PI dans : {{result}}
  </div>
</template>

<script lang="ts">
import { Component, Vue, Emit } from "vue-property-decorator"

@Component
export default class Countdown extends Vue {
    nextForum: Date = new Date('July 05, 2019 13:15:00')
    days: number = 0
    hours: number = 0
    minutes: number = 0
    seconds: number = 0
    result: string = ""
    
    async mounted() {
        var vm = this
        var x = setInterval(vm.countDown(), 1000)
    }

    countDown() {
        var vm = this
		let timeToNextForum: number = vm.nextForum.getTime() - new Date().getTime()

		vm.days = Math.floor(timeToNextForum / (1000 * 60 * 60 * 24))
		vm.hours = Math.floor((timeToNextForum % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60))
		vm.minutes = Math.floor((timeToNextForum % (1000 * 60 * 60)) / (1000 * 60))
        vm.seconds = Math.floor((timeToNextForum % (1000 * 60)) / 1000)
        
        vm.result = vm.days + "d " + vm.hours + "h " + vm.minutes + "m " + vm.seconds + "s"
        console.log(vm.result)
        return vm.result
	}
}

</script>