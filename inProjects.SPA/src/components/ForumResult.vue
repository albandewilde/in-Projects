<template>
    <div>
        {{projects}}
         <div v-for="(o, idx) in projects" :key="idx">
            <el-card class="box-card">
                <div slot="header" class="clearfix">
                    <span>{{o.name}}</span>
                    &nbsp;
                    <el-button v-if="!projects[idx].isBlocked" @click="blockedGrade(idx)" type="warning">Bloquer les notes</el-button>
                </div>
                <div v-for="(a,value,idx2) in o.individualGrade" :key="idx2" class="text item">
                        {{value}}
                        <div v-if="projects[idx].individualGrade[value] > 0">
                            <div v-if="!projects[idx].isBlocked">
                                <el-select @change="gradeChange(idx,idx2,value)" style="width: 5%" v-model="projects[idx].individualGrade[value]" placeholder="Select">
                                            <el-option v-for="(item,index) in selector" :key="index" :value="item"></el-option>
                                </el-select><span class="grade-max">/20</span>
                            </div>
                            <div v-else>
                                 <span class="grade-max">{{projects[idx].individualGrade[value]}}/20</span>
                            </div>
                        </div>
                        <div v-else>
                            Ce Jury n'a pas encore voté ce projet
                        </div>
                        <br/>
                </div>
                Moyenne : {{o.average}}
            </el-card>
            <br/>
        </div>
        <el-button @click="DownloadExcel()"> Telecharger les résultats </el-button>
    </div>
</template>

<script lang="ts">
import { Component,Vue } from 'vue-property-decorator';
import { getAllGradeProjects, downloadExcel } from '../api/forumApi';
import { ProjectForumResult } from '@/modules/classes/ProjectForumResult';
import { GetSelectorGrade, changeNoteProject, blockedNoteProject } from '../api/projectApi';
import { TypeTimedUser } from '../modules/classes/TimedUserEnum';
import{saveAs} from "file-saver"

@Component
export default class ForumResult extends Vue {
    private projects: ProjectForumResult[] = []
    private projectsOrigin: ProjectForumResult[] = []
    private selector: number[]= []

    async created(){
       this.projects = await getAllGradeProjects()
    //    this.projects.forEach(element => {
    //        this.projectsOrigin.push(element)
    //    });
    //    console.log(this.projectsOrigin)
       this.selector = await GetSelectorGrade()
    }

     async gradeChange(idx: number, idx2: number, key: string) {
        try {
            await changeNoteProject(this.projects[idx].projectId, this.projects[idx].individualGrade[key], this.projects[idx].jurysId[idx2], TypeTimedUser.StaffMember)
        } catch (e) {
            console.log(e)
        }
     }

     async blockedGrade(idx: number){
        try {
            await blockedNoteProject(this.projects[idx].projectId,this.projects[idx].jurysId,this.projects[idx].individualGrade)
            this.projects[idx].isBlocked = true
        } catch (e) {
            console.log(e)
        }
     }

    async DownloadExcel(){
         let excel = await downloadExcel()
         let blob = new Blob([excel], {type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'})
         saveAs(blob,'ResultatFP.xlsx')
     }
}
</script>

<style>

</style>
