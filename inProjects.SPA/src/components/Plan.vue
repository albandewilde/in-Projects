<template>
    <div>
        <div v-for="project in projects" :key="project.name">
            <img v-if="!isPresent(project)"
                v-on:dragstart="setDrag(project)"
                v-bind:src="project.logo" 
                :draggable="true"
                style="height: 200px; width: auto;"/>
        </div>

        <div >
            <table class="plan">
                <tr
                    v-for="boxRow in boxes.length"
                    :key="boxRow">
                    <td
                        v-for="box in boxes[boxRow - 1].length"
                        :key="box">
                        kjk
                        <div
                            v-on:dragover.prevent="allowDrop(boxRow - 1, box - 1)"
                            v-on:dragleave.prevent="leave(boxRow - 1, box - 1)"
                            v-on:drop.prevent="drop(boxRow - 1, box - 1)"
                            v-bind:style="{background: boxesColors[boxRow - 1][box - 1]}"
                            style="height: 200px; width: 200px; opacity: 0.33">

                            <img
                                v-if="boxes[boxRow - 1][box - 1]"
                                v-bind:src="boxes[boxRow - 1][box - 1].logo"
                                :draggable="true"
                                v-on:dragstart="setDrag(boxes[boxRow - 1][box - 1])"
                                style="height: 150px; width: auto; opacity: 1;" />
                        </div>
                    </td>
                </tr>
            </table>
            <!-- <img :src="plan" style="width: 100%"/> -->
        </div>

        <div
            v-for="box in boxes.length"
            :key="box"
            v-on:dragover.prevent="allowDrop(box - 1)"
            v-on:dragleave.prevent="leave(box - 1)"
            v-on:drop.prevent="drop(box - 1)"
            v-bind:style="{background: boxesColors[box - 1]}"
            style="height: 200px; width: 200px; opacity: 0.33;"> 
                Drop Zone {{box}}

                <img :draggable="true" v-on:dragstart="setDrag(boxes[box - 1])" v-if="boxes[box - 1]" v-bind:src="boxes[box - 1].logo" style="height: 150px; width: auto; opacity: 1;"/>
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
    private boxes: Project[][] = []
    private boxesColors: string[][] = []
    private dragged!: Project
    private plan: string = "/plan.png"

    async mounted() {
        let i!: number
        let j!: number
        // max is the number of stands, will need some tweaks for the real plan
        const maxWidth: number = 7
        const maxHeight: number = 4
        let rowBoxes: Project[] = []
        let rowColors: string[] = []
        for (i = 0; i < maxHeight; i += 1) {
            rowBoxes = this.boxes[i]
            rowColors = this.boxesColors[i]
            for (j = 0; j < maxWidth; j += 1) {

                Vue.set(rowColors, j, "green")
                Vue.set(rowBoxes, j, null)
            }
            Vue.set(this.boxes, i, rowBoxes)
            Vue.set(this.boxesColors, i, rowColors)
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

    async allowDrop(idxRow: number, idxBox: number): Promise<void> {
        if (this.dragged != undefined && this.dragged != this.boxes[idxRow][idxBox]) {
            const color: string[] = this.boxesColors[idxRow]
            color[idxBox] = "blue"
            Vue.set(this.boxesColors, idxRow, color)
        }
    }

    async leave(idxRow: number, idxBox: number): Promise<void> {
        const color: string[] = this.boxesColors[idxRow]
        if (this.boxes[idxRow][idxBox]) {
            color[idxBox] = "red"
            Vue.set(this.boxesColors, idxRow, color)
        } else {
            color[idxBox] = "green"
            Vue.set(this.boxesColors, idxRow, color)
        }
    }

    async drop(idxRow: number, idxBox: number) {
        // const idxPresent = this.boxes.findIndex(box => box == this.dragged)
        let rowPresent: number
        let idxPresent!: number
        for (rowPresent = 0; rowPresent < this.boxes.length; rowPresent += 1) {
            idxPresent = this.boxes[rowPresent].findIndex(box => box == this.dragged)

            if (idxPresent >= 0)
                break
        }

        // Keep the content of the destination
        const tempBox = this.boxes[idxRow][idxBox]
        const tempBoxColor = this.boxesColors[idxRow][idxBox]

        // Copy the target into the destination
        Vue.set(this.boxes[idxRow], idxBox, this.dragged)
        Vue.set(this.boxesColors[idxRow], idxBox, "red")

        // Put the content of the destination into the target origin
        Vue.set(this.boxesColors[rowPresent], idxPresent, tempBoxColor)
        Vue.set(this.boxes[rowPresent], idxPresent, tempBox)
        this.leave(rowPresent, idxPresent)
    }

    isPresent(project: Project): boolean {
        // const idxPresent = this.boxes.findIndex(box => box == project)
        let rowPresent: number
        let idxPresent!: number
        for (rowPresent = 0; rowPresent < this.boxes.length; rowPresent += 1) {
            idxPresent = this.boxes[rowPresent].findIndex(box => box == project)
            if (idxPresent >= 0)
                break
        }

        // console.debug(this.boxes)
        // console.debug(idxPresent)
        if (idxPresent >= 0) {
            // console.debug("is true")
            return true
        }
        // console.debug("is false")
        return false
    }
}
</script>

<style>
.plan {
    background-image: url("/plan.jpg");
    height: 200px;
    width: 200px;
}

</style>
