<template>
<div>
    <div style="width: 100%;">
        <font-awesome-icon icon="filter" size="lg" /> <b style="margin-left: 10px; margin-right: 15px;">Trier :</b>
        <select @change="schoolSelect()" v-model="schoolChoice" class="selects">
            <option selected value="all">Tous les projets</option>
            <option v-for="item in schoolOptions" :key="item.schoolId" :value="item.name">
                {{item.name}}
            </option>
        </select>
        <select @change="typeSelect()" v-model="typeChoice" class="selects">
            <option selected value="all">Tous types de projets</option>
            <option value="I">Projets informatiques</option>
            <option value="H">Projets de Formation Humaine</option>
        </select>
        <select v-if="typeChoice != 'H'" @change="semesterSelect()" v-model="semesterChoice" class="selects">
            <option selected value="all">Tous les semestres</option>
            <option v-for="item in semesters" :key="item" :value="item">
                {{item}}
            </option>
        </select>
        <select v-else @change="semesterSelect()" v-model="semesterChoice" class="selects">
            <option selected value="all">Tous les semestres</option>
            <option v-for="item in semestersPfh" :key="item" :value="item">
                {{item}}
            </option>
        </select>
        <datalist id="languages" >
            <option v-for="(o, idx) in projectListToDisplay" :key="idx">{{o.groupName}}  ({{formatDateMonth(o.begDate)}}) </option>
        </datalist>
        Chercher un projet: <input type="text" style="width: 20%; border: solid black;" list="languages" v-model="groupName" @change="getProject(groupName)">
    </div>
    <br><br>
    <div class="sk-cube-grid" v-if="isLoading">
        <div class="sk-cube sk-cube1"></div>
        <div class="sk-cube sk-cube2"></div>
        <div class="sk-cube sk-cube3"></div>
        <div class="sk-cube sk-cube4"></div>
        <div class="sk-cube sk-cube5"></div>
        <div class="sk-cube sk-cube6"></div>
        <div class="sk-cube sk-cube7"></div>
        <div class="sk-cube sk-cube8"></div>
        <div class="sk-cube sk-cube9"></div>
    </div>
     <div class="masonry-layout">
        <div class="masonry-layout-panel masonry-layout-flip--md masonry-layout-flip" v-for="(o, index) in projectListToDisplay.length" :key="o">
            <div class="masonry-layout-panel__content masonry-layout-flip__content">
                <div class="masonry-layout-flip__panel masonry-layout-flip__panel--front">
                    <h2>{{projectListToDisplay[index].groupName}}</h2>
                    {{projectListToDisplay[index].semester}} {{projectListToDisplay[index].sector}}
                    <br>
                    {{formatDate(projectListToDisplay[index].begDate)}} / {{formatDate(projectListToDisplay[index].endDate)}}
                    <img :src="projectListToDisplay[index].logo" class="image">
                </div>
                <br>
                <div class="masonry-layout-flip__panel masonry-layout-flip__panel--back">
                    <br>
                    <u><b style="cursor:pointer;" @click="redirect(projectListToDisplay[index].projectStudentId)">{{projectListToDisplay[index].slogan}}</b></u>
                    <br><br>
                    {{projectListToDisplay[index].pitch}}
                    <br><br>
                    <el-button class="test" icon="el-icon-star-off" circle  @click="FavOrUnfav(projectListToDisplay[index], index)" v-bind:style="{background:isStarColored(projectListToDisplay[index].isFav)}"></el-button>
                </div>
            </div>
        </div>
    </div> 
<!--     <div class="masonry-layout test">
        <div class="card-container">
            <div class="card" v-for="(o, index) in projectListToDisplay.length" :key="o" @click="redirect(projectListToDisplay[index].projectStudentId)">
                <a>
                    <div class="card--display">
                        <h2>{{projectListToDisplay[index].groupName}}</h2>
                        <h3>{{formatDate(projectListToDisplay[index].begDate)}} / {{formatDate(projectListToDisplay[index].endDate)}} </h3>
                        <img :src="projectListToDisplay[index].logo" class="image">
                    </div>
                    <div class="card--hover">
                        <u><b style="cursor:pointer;">{{projectListToDisplay[index].slogan}}</b></u>
                        <p>{{projectListToDisplay[index].pitch}}</p>
                    </div>
                </a>
                <div class="card--border"></div>
            </div>
        </div>
    </div> -->

    </div>
</template>

<script lang="ts">
import Vue from "vue"
import { Component } from "vue-property-decorator"
import { GetAllProject } from "../api/projectApi"
import {Project} from "../modules/classes/Project"
import {verifyProjectFav, favProject } from "../api/submitProjectApi"
import {School} from "../modules/classes/School"
import { getSchools } from "../api/schoolApi"

@Component
export default class ProjectList extends Vue {
    private projectList: Project[] = []
    private projectListToDisplay: Project[] = []
    private border !: string
    private isFav!: boolean    
    private schoolOptions: School[] = []
    private starColor!: string
    private semesters!: string[]
    private semestersPfh!: string[]
    private schoolChoice: string = "all"
    private typeChoice: string = "all"
    private semesterChoice: string = "all"
    private isLoading: boolean = false
    private groupName!: string
    
    async mounted() {
        this.isLoading = true
        this.projectList  = await GetAllProject()
        for (const project of this.projectList) {
            this.projectListToDisplay.push(project)
        }
        this.isLoading = false
        this.schoolOptions = await getSchools()
        this.schoolOptions.splice(0, 1)
        this.semesters = ["Semestre 1", "Semestre 2", "Semestre 3", "Semestre 4", "Semestre 5"]
        this.semestersPfh = ["Semestre 1", "Semestre 2", "Semestre 3", "Semestre 4"]
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
    formatDateMonth(date: Date){
        const newDate = date.toString()
        return newDate.substr(0, 7)
    }
    getType(specificProject: Project) {
        if ( specificProject.type == "I" ) return this.border = "red 1px solid"
        if ( specificProject.type == "H" ) return this.border = "blue 1px solid"
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
    redirect(idProject: number) {
         this.$router.push("/Project/" + idProject)
    }
    typeSelect() {
        if (this.typeChoice == "all") {
            this.projectListToDisplay = this.projectList
        }
        else {
            this.projectListToDisplay = []
            for (const project of this.projectList) {
                if (project.type == this.typeChoice) {
                    this.projectListToDisplay.push(project)
                }
            }
        }
    }
    schoolSelect() {
        if(this.schoolChoice == "all") {
            this.projectListToDisplay = this.projectList
        }
        else {
            let idx = this.schoolOptions.find(x => x.name == this.schoolChoice)
            if (idx == undefined) {
                idx = new School(0, "Unknown")
            }
            let idSchool = idx.schoolId
            this.projectListToDisplay = []
            for(const project of this.projectList) {
                if(project.schoolId == idSchool) {
                    this.projectListToDisplay.push(project)
                }
            }
        }
    }
    semesterSelect() {
        if(this.semesterChoice == "all") {
            this.projectListToDisplay = this.projectList
        }
        else {
            this.projectListToDisplay = []
            let sem = this.semesterChoice.split(" ")
            for(const project of this.projectList) {
                if(project.semester == "S0" + sem[1]) {
                    this.projectListToDisplay.push(project)
                }
            }            
        }
    }
    getProject(groupName: string){
        console.log(groupName)
            for(const project of this.projectListToDisplay) {
                console.log(project.groupName.startsWith(groupName))
                if(groupName.startsWith(project.groupName)) {
                    console.log("ok")
                    this.projectListToDisplay = []
                    this.projectListToDisplay.push(project)
            }
        }
    }
}
</script>

<style <style lang="scss" scoped>

.image{
      width: 250px; 
      height: 250px;
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
    border-radius: 0px;
    border-style: solid;
    overflow: hidden;
    border-color: black;
    width: 70%;
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
  background: linear-gradient(rgb(13, 102, 219), rgb(68, 218, 229))
}
.masonry-layout-flip__panel {
  backface-visibility: hidden;
  border-radius: 0px;
  height: 100%;
  left: 0;
  overflow: auto;
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
.selects {
   -webkit-border-radius: 20px;
   -moz-border-radius: 20px;
   border-radius: 20px;
   -webkit-border-radius: 5px;
   -moz-border-radius: 5px;
   border-radius: 5px;
   background-color: #0099ff;
   color: #fff;
   width: 30%;
   height: 30px;
   font-size: medium;
   border-color: #0099ff
}

.sk-cube-grid {
  width: 40px;
  height: 40px;
  margin: 100px auto;
}

.sk-cube-grid .sk-cube {
  width: 33%;
  height: 33%;
  background-color:#0099ff;
  float: left;
  -webkit-animation: sk-cubeGridScaleDelay 1.3s infinite ease-in-out;
          animation: sk-cubeGridScaleDelay 1.3s infinite ease-in-out; 
}
.sk-cube-grid .sk-cube1 {
  -webkit-animation-delay: 0.2s;
          animation-delay: 0.2s; }
.sk-cube-grid .sk-cube2 {
  -webkit-animation-delay: 0.3s;
          animation-delay: 0.3s; }
.sk-cube-grid .sk-cube3 {
  -webkit-animation-delay: 0.4s;
          animation-delay: 0.4s; }
.sk-cube-grid .sk-cube4 {
  -webkit-animation-delay: 0.1s;
          animation-delay: 0.1s; }
.sk-cube-grid .sk-cube5 {
  -webkit-animation-delay: 0.2s;
          animation-delay: 0.2s; }
.sk-cube-grid .sk-cube6 {
  -webkit-animation-delay: 0.3s;
          animation-delay: 0.3s; }
.sk-cube-grid .sk-cube7 {
  -webkit-animation-delay: 0s;
          animation-delay: 0s; }
.sk-cube-grid .sk-cube8 {
  -webkit-animation-delay: 0.1s;
          animation-delay: 0.1s; }
.sk-cube-grid .sk-cube9 {
  -webkit-animation-delay: 0.2s;
          animation-delay: 0.2s; }

@-webkit-keyframes sk-cubeGridScaleDelay {
  0%, 70%, 100% {
    -webkit-transform: scale3D(1, 1, 1);
            transform: scale3D(1, 1, 1);
  } 35% {
    -webkit-transform: scale3D(0, 0, 1);
            transform: scale3D(0, 0, 1); 
  }
}

@keyframes sk-cubeGridScaleDelay {
  0%, 70%, 100% {
    -webkit-transform: scale3D(1, 1, 1);
            transform: scale3D(1, 1, 1);
  } 35% {
    -webkit-transform: scale3D(0, 0, 1);
            transform: scale3D(0, 0, 1);
  } }
.test{
    margin-top: 3px !important;
    display: inline-block
}

</style>