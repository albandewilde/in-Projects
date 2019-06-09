<template>
    <div>
        <el-row>
            <el-col :span="6" v-for="(o, index) in projectListToDisplay.length" :key="o" :offset="index > 0 ? projectListToDisplay.length : 0">            
                <el-card v-bind:body-style="{ padding: '0px', border:getType(projectListToDisplay[index]) }">
                    <img :src="projectListToDisplay[index].logo" class="image">
                    <div style="padding: 14px;">
                        <span>{{projectListToDisplay[index].groupName}}</span><br>
                        <span>Slogan : {{projectListToDisplay[index].slogan}}</span><br>
                        <span>Pitch : {{projectListToDisplay[index].pitch}}</span><br>
                        <span>Chef de projet: {{getLeader(projectListToDisplay[index])}} equipe de {{projectListToDisplay[index].firstName.length}} personne(s)</span><br>
                        <span>{{formatDate(projectListToDisplay[index].begDate)}} - {{formatDate(projectListToDisplay[index].endDate)}} </span>
                        <div class="bottom clearfix">
                            <el-button class="test" icon="el-icon-star-off" circle  @click="FavOrUnfav(projectListToDisplay[index], index)" v-bind:style="{background:isStarColored(projectListToDisplay[index].isFav)}"></el-button>
                           <!--  <div class="star" @click="FavOrUnfav(projectListToDisplay[index].projectStudentId)">
                                <span class="fa fa-star" v-bind:style="{color:isProjectFav(projectListToDisplay[index].projectStudentId)}"></span>
                                {{isProjectFav(projectListToDisplay[index].projectStudentId)}}
                            </div> -->
                        </div>
                    </div>
                </el-card>
            </el-col>
        </el-row>
    </div>
    
</template>

<script lang="ts">
import Vue from 'vue'
import { Component } from 'vue-property-decorator'
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
    async mounted(){
        this.projectList  = await GetAllProject()
        console.log(this.projectList)
        for(const project of this.projectList){
            this.projectListToDisplay.push(project);
        }

    }

    getLeader(specificProject: Project){
        for(var i = 0; i < specificProject.userId.length; i++){
            if(specificProject.userId[i] == specificProject.leaderId){
                return specificProject.firstName[i] + " " + specificProject.lastName[i]
            }
        }
    }

    formatDate(date: Date){
        var newDate = date.toString()
        return newDate.substr(0,10)
    }

    getType(specificProject: Project){
        if(specificProject.type == "I") return this.border = "red 1px solid"
        else if( specificProject.type == "H" ) return this.border =  "blue 1px solid"
        else return this.border = "white 1px solid"
    }

    async FavOrUnfav(specificProject: Project, index: number) {
        try {
            var div  = document.getElementsByClassName("test")[index];
            console.log(div)
            this.isFav = await verifyProjectFav(specificProject.projectStudentId)
            await favProject(specificProject.projectStudentId)
            this.isFav = !this.isFav
            console.log(this.isFav)
            if (this.isFav) {
                div.setAttribute("style", "background: #F5CC27 !important;")
            } else {
                div.setAttribute("style", "background: #fffcfc !important;")
            }        } catch (e) {
            console.error(e)
        }
    }

    isStarColored(i: number){
        if (i != 0) {
            return this.starColor = "#F5CC27 !important"
        } else {
            return this.starColor = "#000000 !important;"
        }   
    }
}
</script>

<style>
.image{
    width: 100%;
    display: block; 
    height: 300px; 
}

.el-card{
    border:darkred 1px solid ;
    border: 100pc;
}
</style>