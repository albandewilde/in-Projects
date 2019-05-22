<template>
    <div>
        <div class="projectsList">
            <div 
                class="project"
                v-for="project in projects" :key="project.name">
                <img
                    class="projectImage"
                    v-if="!isPresent(project)"
                    v-on:dragstart="setDrag(project)"
                    v-bind:src="project.logo" 
                    :draggable="true"/>
            </div>
        </div>

        <div class="plan" >
            <table class="table">
                <tr
                    v-for="boxRow in boxes.length"
                    :key="boxRow">
                    <td
                        v-for="box in boxes[boxRow - 1].length"
                        :key="box" 
                        :style="{width: boxSizeW, height: boxSizeH, maxWidth: boxSizeW, maxHeight: boxSizeH}">
                        <div
                            v-on:dragenter.prevent="dragEnter(boxRow - 1, box - 1)"
                            v-on:dragover.prevent="allowDrop(boxRow - 1, box - 1)"
                            v-on:dragleave.prevent="leave(boxRow - 1, box - 1)"
                            v-on:drop.prevent="drop(boxRow - 1, box - 1)"
                            v-bind:style="{background: boxesColors[boxRow - 1][box - 1]}"
                            class="dropZones">

                            <img
                                class="logos"
                                v-if="boxes[boxRow - 1][box - 1] != defaultProject"
                                v-bind:src="boxes[boxRow - 1][box - 1].logo"
                                :draggable="true"
                                v-on:dragstart="setDrag(boxes[boxRow - 1][box - 1])" />
                        </div>
                    </td>
                </tr>
            </table>
        </div>
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
    private defaultProject = new Project()
    private maxWidth: number = 14
    private maxHeight: number = 12
    private boxSizeH: string = 100 / this.maxWidth + "vh"
    private boxSizeW: string = 100 / this.maxWidth + "vw"

    async mounted() {
        let i!: number
        let j!: number
        // max is the number of stands, will need some tweaks for the real plan
        let rowBoxes: Project[] = []
        let rowColors: string[] = []

        for (i = 0; i < this.maxHeight; i += 1) {
            for (j = 0; j < this.maxWidth; j += 1) {
                rowColors.push("green")
                rowBoxes.push(this.defaultProject)
            }

            Vue.set(this.boxes, i, rowBoxes)
            Vue.set(this.boxesColors, i, rowColors)
            rowColors = []
            rowBoxes = []
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

    async dragEnter(idxRow: number, idxBox: number): Promise<void> {
        if (this.dragged != undefined && this.dragged != this.boxes[idxRow][idxBox]) {
            const color: string[] = this.boxesColors[idxRow]
            color[idxBox] = "blue"
            Vue.set(this.boxesColors, idxRow, color)
        }
    }

    async allowDrop(idxRow: number, idxBox: number): Promise<void> {
        // Does nothing
    }

    async leave(idxRow: number, idxBox: number): Promise<void> {
        const color: string[] = this.boxesColors[idxRow]
        if (this.boxes[idxRow][idxBox] != this.defaultProject) {
            color[idxBox] = "red"
            Vue.set(this.boxesColors, idxRow, color)
        } else {
            color[idxBox] = "green"
            Vue.set(this.boxesColors, idxRow, color)
        }
    }

    async drop(idxRow: number, idxBox: number) {
        let rowPresent: number
        let idxPresent!: number
        for (rowPresent = 0; rowPresent < this.boxes.length; rowPresent += 1) {
            idxPresent = this.boxes[rowPresent].findIndex(box => box == this.dragged)

            if (idxPresent >= 0)
                break
        }

        if (this.boxes[idxRow][idxBox] != this.defaultProject) {
            // Move the project
            Vue.set(this.boxes[idxRow], idxBox, this.dragged)
            Vue.set(this.boxesColors[idxRow], idxBox, "red")

            // Reset the box
            Vue.set(this.boxes[rowPresent], idxPresent, this.defaultProject)
            Vue.set(this.boxesColors[rowPresent], idxPresent, "green")
        } else {
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

    }

    // Need to be optimized
    isPresent(project: Project): boolean {
        let rowPresent: number
        let idxPresent!: number
        for (rowPresent = 0; rowPresent < this.boxes.length; rowPresent += 1) {
            idxPresent = this.boxes[rowPresent].findIndex(box => box == project)
            if (idxPresent >= 0)
                break
        }

        if (idxPresent >= 0) {
            return true
        }
        return false
    }

    checkBoxes() {
        console.debug("_________________")
        console.debug("BOXES")
        console.debug(this.boxes)
        console.debug("_________________")
    }
}
</script>

<style>
.plan {
    background-image: url("/plan.png");
    background-size: 100% 100%;
    background-repeat: no-repeat;
    width: 65vw;
    height: 90vh;
}

.table {
    border-width: 0px;  
    border-spacing: 0;
    display: grid;
    height: 100%;
    width: 100%;
}

.dropZones {
    height: 100%;
    width: 100%;
    /*  */
    max-width: inherit;
    max-height: inherit;
    opacity: 0.2;
}

.logos {
    /* width: inherit;
    height: inherit; */
    max-width: 100%;
    max-height: 100%;
    opacity: 1;
}

.projectsList {
    position: absolute;
    margin-left: 72vw;
}

.project {
    height: auto;
    width: auto;
}

.projectImage {
    height: 200px;
    width: auto;
}
</style>
