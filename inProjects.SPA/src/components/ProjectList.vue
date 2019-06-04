<template>
    <div>{{projectList}}
        <el-table
            :data="projectListToDisplay"
            stripe
            style="width: 100%">
        <el-table-column
            prop="groupName"
            label="nom"
        ></el-table-column>        </el-table>
        <el-row>
            <el-col :span="6" v-for="(o, index) in projectListToDisplay.length" :key="o" :offset="index > 0 ? projectListToDisplay.length : 0">
                <el-card v-bind="{ padding: '0px', border:'1px solid' }">
                    <img :src="projectListToDisplay[index].logo" class="image">
                    <div style="padding: 14px;">
                        <span>{{projectListToDisplay[index].groupName}}</span><br>
                        <span>Slogan : {{projectListToDisplay[index].slogan}}</span><br>
                        <span>Pitch : {{projectListToDisplay[index].pitch}}</span><br>
                        <span>Chef de projet: {{getLeader(projectListToDisplay[index])}} equipe de {{projectListToDisplay[index].firstName.length}} personne(s)</span><br>
                        <span>{{formatDate(projectListToDisplay[index].begDate)}} - {{formatDate(projectListToDisplay[index].endDate)}} </span>
                        <div class="bottom clearfix">
                        <time class="time">{{ currentDate }}</time>
                        <el-button type="text" class="button">Operating</el-button>
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

@Component
export default class ProjectList extends Vue {
    private projectList: Project[] = []
    private projectListToDisplay: Project[] = []
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
        if(specificProject.type == "I") return "yellow"
        else if( specificProject.type == "H" ) return  "blue"
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