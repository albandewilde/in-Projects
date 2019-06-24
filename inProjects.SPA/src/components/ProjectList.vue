<template>
    <div>
        <div v-if="CheckedAuthorize('Administration')">
            <el-button type="primary" class="generate_sheet" @click="download()">
                Télechargez les fiches projets
            </el-button>
        </div>
        <br/>
        <br/>
        <el-row>
            <el-col :span="5" v-for="(o, index) in projectListToDisplay.length" :key="o" :offset="index > 0 ? 1 : 1" >            
                <el-card v-bind:body-style="{ padding: '0px', border:getType(projectListToDisplay[index]) }">
                    <img :src="projectListToDisplay[index].logo" class="image"  @click="redirect(projectListToDisplay[index].projectStudentId)">
                    <div class="my-card-row">
                        <span>{{projectListToDisplay[index].groupName}}</span><br>
                        <span>Slogan : {{projectListToDisplay[index].slogan}}</span><br>
                        <span>Pitch : {{projectListToDisplay[index].pitch}}</span><br>
                        <span>Chef de projet: {{getLeader(projectListToDisplay[index])}} equipe de {{projectListToDisplay[index].firstName.length}} personne(s)</span><br>
                        <span>{{formatDate(projectListToDisplay[index].begDate)}} - {{formatDate(projectListToDisplay[index].endDate)}} </span>
                        <div class="bottom clearfix">
                            <el-button class="test" icon="el-icon-star-off" circle  @click="FavOrUnfav(projectListToDisplay[index], index)" v-bind:style="{background:isStarColored(projectListToDisplay[index].isFav)}"></el-button>
                        </div>
                    </div>
                </el-card>
            </el-col>
        </el-row>
    </div>
    
</template>

<script lang="ts">
import Vue from "vue"
import { Component } from "vue-property-decorator"
import { GetAllProject } from "../api/projectApi"
import {Project} from "../modules/classes/Project"
import {verifyProjectFav, favProject } from "../api/submitProjectApi"
import JSZip from "jszip"
import {ProjectSheet} from "../modules/classes/ProjectSheet"
import {GeneratePiSheet, GeneratePfhSheet} from "../modules/functions/GenerateSheet"
import {GetAllSheet} from "../api/projectApi"
import pdfMake from "pdfmake/build/pdfmake"
import { saveAs } from "file-saver"


@Component
export default class ProjectList extends Vue {
    private projectList: Project[] = []
    private projects : ProjectSheet[] = []
    private projectListToDisplay: Project[] = []
    private border !: string
    private isFav!: boolean
    private starColor!: string
    private zip : JSZip = new JSZip()

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

    async GetAllProjectSheet(index : number) {
            const sheet =  pdfMake.createPdf(
                    GeneratePiSheet(
                        [
                            "None",
                            "None"
                        ],
                        this.projects[index].name,
                        this.projects[index].semester,
                        this.projects[index].sector,
                        this.projects[index].technos,
                        this.projects[index].logo,
                        this.projects[index].slogan,
                        this.projects[index].pitch,
                        this.projects[index].team
                    )
                )
                sheet.getBlob(async (blob :Blob) => {
                    this.zip.file("fiches/"+this.projects[index].name + ".pdf",blob)
                    if(this.projects.length -1 != index){
                        await this.GetAllProjectSheet(++index)
                    }else{
                     this.zip.generateAsync({type:"blob"})
                        .then(function(content) {
                        saveAs(content, "fiches.zip");
                    });
                    }
                 });
    }

   async download(){
       let index = 0 
       this.zip = new JSZip()
       this.zip.folder("fiches")
       this.projects = await GetAllSheet()
       await this.GetAllProjectSheet(index)

    }
    CheckedAuthorize(needToBe: string){
        return this.$store.state.currentUserType.find(x => x == needToBe) != null ? true : false
    }

     redirect(idProject: number) {
         this.$router.replace("/Project/" + idProject)
    }
}
</script>

<style>
.image{
    width: 100%;
    display: inline-block; 
    height: 300px; 
    cursor: pointer;
}

.el-card{
    border:darkred 1px solid ;
    border: 100pc;

}
.el-col-6 {
    margin-left: 1% !important
}

.my-card-row{
    height: 480 !important;
}
</style>