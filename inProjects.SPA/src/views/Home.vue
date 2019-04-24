<template>
  <div class="home">
    <img src="../assets/intechLogo.png" width="30%;"><br>
	Prochain forum PI dans : {{days}}d {{hours}}h {{minutes}}m {{seconds}}s
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator"

export default class Home extends Vue {
	nextForum: Date = new Date('July 05, 2019 13:15:00')
	days: number
	hours: number
	minutes: number
	seconds: number

	async mounted() {
		setInterval(function() { 
			var timeToNextForum: number = this.nextForum.getTime() - new Date().getTime()
		
			this.days = Math.floor(timeToNextForum / (1000 * 60 * 60 * 24));
			this.hours = Math.floor((timeToNextForum % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
			this.minutes = Math.floor((timeToNextForum % (1000 * 60 * 60)) / (1000 * 60));
			this.seconds = Math.floor((timeToNextForum % (1000 * 60)) / 1000);
		 }, 1000)
	}
}
</script>