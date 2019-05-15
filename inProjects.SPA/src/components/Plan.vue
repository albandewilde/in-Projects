<template>
    <div>
        <div v-for="project in projects" :key="project.name">
            <img v-if="!isPresent(project)"
                v-on:dragstart="setDrag(project)"
                v-bind:src="project.logo" 
                :draggable="true"
                style="height: 200px; width: auto;"/>
        </div>

        <div
            v-for="box in boxes.length"
            :key="box"
            v-on:dragover.prevent="allowDrop(box - 1)"
            v-on:dragleave.prevent="leave(box - 1)"
            v-on:drop.prevent="drop(box - 1)"
            v-bind:style="{background: boxesColors[box - 1]}"
            style="height: 200px; width: 200px"> 
                Drop Zone {{box}}

                <img :draggable="true" v-on:dragstart="setDrag(boxes[box - 1])" v-if="boxes[box - 1]" v-bind:src="boxes[box - 1].logo" style="height: 150px; width: auto;"/>
                <br/>
                <span v-if="boxes[box - 1] && boxes[box - 1].name"> {{boxes[box - 1].name}}</span>

        </div>

        <button @click="checkBoxes()">Boites</button>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator"
import { Project } from "../modules/classes/Project"

@Component

export default class Plan extends Vue {
    private projects: Project[] = []
    private color: string = "green"
    private boxes: Project[] = []
    private boxesColors: string[] = []
    private dragged!: Project

    async mounted() {
        let i!: number
        // max is the number of stands, will need some tweaks for the real plan
        const max: number = 4
        for (i = 0; i < max; i += 1) {
            Vue.set(this.boxesColors, i, "green")
            Vue.set(this.boxes, i, null)
        }

        // FOR TESTING NEEDS
        this.projects[0] = new Project()
        this.projects[0].logo = "/BlueSnail.png"
        this.projects[0].name = "Snail"
        this.projects[1] = new Project()
        this.projects[1].logo = "/JeanTurtle.png"
        this.projects[1].name = "Turtle"
        this.projects[2] = new Project()
        this.projects[2].logo = "/pickel.png"
        this.projects[2].name = "pickel"
        this.projects[3] = new Project()
        this.projects[3].logo = "/bart.jpg"
        this.projects[3].name = "bart"
        this.projects[4] = new Project()
        this.projects[4].logo = "/kappa.png"
        this.projects[4].name = "kappa"
    }

    async setDrag(project: Project) {
        this.dragged = project
    }

    async allowDrop(idx: number): Promise<void> {
        if (this.dragged != undefined && this.dragged != this.boxes[idx]) {
            Vue.set(this.boxesColors, idx, "blue")
        }
    }

    async leave(idx: number): Promise<void> {
        if (this.boxes[idx]) {
            Vue.set(this.boxesColors, idx, "red")
        } else {
            Vue.set(this.boxesColors, idx, "green")
        }
    }

    async drop(idx: number) {
        const idxPresent = this.boxes.findIndex(box => box == this.dragged)

        // Keep the content of the destination
        const tempBox = this.boxes[idx]
        const tempBoxColor = this.boxesColors[idx]

        // Copy the target into the destination
        Vue.set(this.boxes, idx, this.dragged)
        Vue.set(this.boxesColors, idx, "red")

        // Put the content of the destination into the target origin
        Vue.set(this.boxesColors, idxPresent, tempBoxColor)
        Vue.set(this.boxes, idxPresent, tempBox)
        this.leave(idxPresent)
    }

    isPresent(project: Project): boolean {
        const idxPresent = this.boxes.findIndex(box => box == project)
        console.debug(this.boxes)
        console.debug(idxPresent)
        if (idxPresent >= 0) {
            console.debug("is true")
            return true
        }
        console.debug("is false")
        return false
    }
}
</script>

<style>

</style>
