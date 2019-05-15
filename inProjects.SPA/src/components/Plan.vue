<template>
    <div>
        <img v-for="project in projects" :key="project" 
            v-on:dragstart="setDrag(project)"
            v-bind:src="project" 
            draggable="true" style="height: 200px; width: auto;"/>
        <div
            v-for="box in boxes.length"
            :key="box"
            v-on:dragenter="allowDrop(box - 1)"
            v-on:dragleave="leave(box - 1)"
            v-on:drop="drop(box - 1)"
            v-bind:style="{background: boxesColors[box - 1]}"
            style="height: 200px; width: 200px"> 
                Drop Zone {{box}}
        </div>
        <!-- <div v-on:dragenter="allowDrop(1)" v-on:dragleave="leave(1)" v-bind:style="{background: boxesColors[1]}" style="height: 200px; width: 200px"> Drop Zone </div> -->
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator"

@Component

export default class Plan extends Vue {
    private projects: string[] = []
    private color: string = "green"
    private boxes: string[] = []
    private boxesColors: string[] = []
    private dragged: string = ""

    async mounted() {
        let i!: number
        const max: number = 4
        for (i = 0; i < max; i += 1) {
            Vue.set(this.boxesColors, i, "green")
            Vue.set(this.boxes, i, null)
        }
        this.projects[0] = "/BlueSnail.png"
        this.projects[1] = "/JeanTurtle.png"
    }

    async setDrag(name: string) {
        this.dragged = name
    }

    async allowDrop(idx: number): Promise<void> {
        if (this.dragged != "") {
            Vue.set(this.boxesColors, idx, "blue")
            console.debug("y u do dis")
        } else
            console.debug("wat ze wtf")
        console.debug(this.dragged)
        console.debug("______________________")
    }

    async leave(idx: number): Promise<void> {
        Vue.set(this.boxesColors, idx, "green")
        console.debug("o noo")
        console.debug(this.dragged)
        console.debug("______________________")
    }

    // a bit useless
    async reset() {
        this.dragged = ""
        console.debug("dont loos faiss")
        console.debug(this.dragged)
        console.debug("______________________")
    }

    async drop(idx: number) {
        this.boxes[idx] = this.dragged
        Vue.set(this.boxesColors, idx, "red")
        console.debug(this.boxesColors[idx])
        this.reset()

        console.debug("your doin great")
        console.debug("______________________")
    }
}
</script>

<style>

</style>
