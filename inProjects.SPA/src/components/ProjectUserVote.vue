<template>
    <div>
        <el-select @change="change()" v-model="options" placeholder="Select">
            <el-option v-for="item in schoolOptions" :key="item.schoolId" :value="item.name">
            </el-option>
        </el-select>
        <br/>

        <div v-if="projectList.length >0">
            <el-row>
                <el-col :span="5" v-for="(o, index) in projectList.length" :key="o" :offset="index > 0 ? 1 : 1" >            
                    <el-card v-bind:body-style="{ padding: '0px'}">
                        <img :src="projectList[index].logo" class="image"  @click="redirect(projectList[index].projectStudentId)">
                        <div class="my-card-row">
                            <span>{{projectList[index].groupName}}</span><br>
                            <div class="block">
                                <el-rate @change="note(projectList[index].grade,projectList[index].projectStudentId)" v-model="projectList[index].grade"></el-rate>
                            </div>
                        </div>
                    </el-card>
                    <br/>
                </el-col>
            </el-row>
        </div>
        
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator"
import { GetAllProject, GetAllTypeProjectsOfSchool, noteProject } from "../api/projectApi"
import { getSchools } from "../api/schoolApi"
import {Project} from "../modules/classes/Project"
import {School} from "../modules/classes/School"
import { TypeTimedUser } from "@/modules/classes/TimedUserEnum"

@Component
export default class ProjectUserVote extends Vue {
    private projectList: Project[] = []
    private projectListToDisplay: Project[] = []
    private schoolOptions: School[] = []
    private options: string = ""
    private type: string ='I'
    private schoolId: number = 0
    private numberMax = 4

    async created() {
      this.schoolOptions = await getSchools()
    }

    async change(){
        try{
            let idx = this.schoolOptions.find(x => x.name == this.options)
            if(idx == undefined){
                idx = new School(0,"Unknow")
            }
            this.schoolId = idx.schoolId;
            this.projectList  = await GetAllTypeProjectsOfSchool(this.schoolId, this.type)
           for (let index = 0; index < this.projectList.length; index++) {
               this.projectList[index].grade = Math.ceil(this.projectList[index].grade/4);
           }
        } catch(e){

        }
    }

    getStars(grade: number){
        const idx = Math.ceil(grade/4);

        return idx
    }

   async note(newGrade: number, id:number){
        try{
            const grade = newGrade * this.numberMax
            console.log(grade)
            await noteProject(id, grade,this.schoolId,TypeTimedUser.Anon)
        } catch (e) {
            console.log(e)
        }
    }
}
</script>

<style>

</style>
