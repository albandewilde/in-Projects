<template>
<div>
    <div class="masonry-layout">
        <div class="masonry-layout-panel masonry-layout-flip--md masonry-layout-flip" v-for="(o, index) in projectListToDisplay.length" :key="o">
            <div class="masonry-layout-panel__content masonry-layout-flip__content">
                <div class="masonry-layout-flip__panel masonry-layout-flip__panel--front">
                    <h2>{{projectListToDisplay[index].groupName}}</h2>
                    <h3>{{formatDate(projectListToDisplay[index].begDate)}} / {{formatDate(projectListToDisplay[index].endDate)}} </h3>
                    <img :src="projectListToDisplay[index].logo" class="image">
                </div>
                <br>
                <div class="masonry-layout-flip__panel masonry-layout-flip__panel--back">
                    <br>
                    <u><b @click="redirect('Project/' + projectListToDisplay[index].projectStudentId)">{{projectListToDisplay[index].slogan}}</b></u>
                    <br><br>
                    {{projectListToDisplay[index].pitch}}
                    <br><br>
                    <el-button class="test" icon="el-icon-star-off" circle  @click="FavOrUnfav(projectListToDisplay[index], index)" v-bind:style="{background:isStarColored(projectListToDisplay[index].isFav)}"></el-button>
                </div>
            </div>
        </div>
    </div>
</div>
</template>

<script lang="ts">
import Vue from "vue"
import { Component } from "vue-property-decorator"
import { GetAllProject } from "../api/projectApi"
import {Project} from "../modules/classes/Project"
import {verifyProjectFav, favProject } from "../api/submitProjectApi"

@Component
export default class ProjectList extends Vue {
    private projectList: Project[] = []
    private projectListToDisplay: Project[] = []
    private border !: string
    private isFav!: boolean
    private starColor!: string
    async mounted() {
        this.projectList  = await GetAllProject()
        console.log(this.projectList)
        for (const project of this.projectList) {
            this.projectListToDisplay.push(project)
        }
    }

    getLeader(specificProject: Project) {
        for (let i = 0; i < specificProject.userId.length; i += 1) {
            if (specificProject.userId[i] == specificProject.leaderId) {
                return specificProject.firstName[i] + " " + specificProject.lastName[i]
            }
        }
    }
    formatDate(date: Date) {
        const newDate = date.toString()
        return newDate.substr(0, 10)
    }
    getType(specificProject: Project) {
        if ( specificProject.type == "I" ) return this.border = "red 1px solid"
        if ( specificProject.type == "H" ) return this.border =  "blue 1px solid"
        return this.border = "white 1px solid"
    }
    async FavOrUnfav(specificProject: Project, index: number) {
        try {
            const div  = document.getElementsByClassName("test")[index]
            this.isFav = await verifyProjectFav(specificProject.projectStudentId)
            await favProject(specificProject.projectStudentId)
            this.isFav = !this.isFav
            console.log(this.isFav)
            if (this.isFav) {
                div.setAttribute("style", "background: #F5CC27 !important;")
            } else {
                div.setAttribute("style", "background: #fffcfc !important;")
            }
        } catch (e) {
            console.error(e)
        }
    }
    isStarColored(i: number) {
        if (i != 0) {
            return this.starColor = "#F5CC27 !important"
        }
        return this.starColor = "#000000 !important;"
    }

    redirect(destination: string) {
        this.$router.replace(destination)
    }
}
</script>

<style>

.image{
      width: 100%; 
      height: 300px;
    cursor: pointer;
}

.projectList{
    position: relative;
}

.masonry-layout {
    column-count: 3;
    column-gap: 0;
}
.masonry-layout-panel {
    break-inside: avoid;
    padding: 5px;
}
.masonry-layout-panel__content {
    padding: 10px;
    border-radius: 10px;
    border-style: solid;
    overflow: hidden;
    border-color: #167BEB;
}
    
.masonry-layout-flip {
    perspective: 1000;
}
.masonry-layout-flip:hover .masonry-layout-flip__content {
  transform: rotateY(180deg);
}

.masonry-layout-flip__panel--front {
  transform: rotateY(0deg);
  z-index: 2;
}
.masonry-layout-flip__panel--back {
  transform: rotateY(180deg);
  background: linear-gradient(rgb(13, 102, 219, 1), rgb(68, 218, 229))
}

.masonry-layout-flip__panel {
  backface-visibility: hidden;
  border-radius: 10px;
  height: 100%;
  left: 0;
  overflow: hidden;
  position: absolute;
  top: 0;
  width: 100%;
}

.masonry-layout-flip__content {
  overflow: visible;
  position: relative;
  transform-style: preserve-3d;
  transition: 0.25s;
  height: 400px; 
}
</style>