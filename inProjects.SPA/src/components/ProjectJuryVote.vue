<template>
    <div>
        <el-select @change="change()" v-model="options" placeholder="Select">
            <el-option v-for="item in schoolOptions" :key="item.schoolId" :value="item.name"></el-option>
        </el-select>
        <br/>
        <br/>
        <div v-if="projectList.length >0">
            <el-row>
                <el-col class="col-jury-grade" :span="5" v-for="(o, index) in projectList.length" :key="o" :offset="index > 0 ? 1 : 1" >  
                    <el-card class="card-jury-grade">
                     <span class="name-project-jury-grade">{{projectList[index].groupName}}</span><br>          
                        <img :src="projectList[index].logo" class="image"  @click="redirect(projectList[index].projectStudentId)">
                        <div class="my-card-row">
                            <el-select  @change="gradeChange(index,projectList[index].projectStudentId)" class="select-grade" v-model="projectList[index].grade" placeholder="Select">
                                <el-option v-for="(item,index) in selector" :key="index" :value="item"></el-option>
                            </el-select><span class="grade-max">/20</span>
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
import { GetAllProject, GetAllTypeProjectsOfSchool, noteProject, GetEvaluateProject, GetSelectorGrade } from "../api/projectApi"
import { getSchools } from "../api/schoolApi"
import {Project} from "../modules/classes/Project"
import {School} from "../modules/classes/School"
import { TypeTimedUser } from "@/modules/classes/TimedUserEnum"

@Component
export default class ProjectJuryVote extends Vue {
    private projectList: Project[] = []
    private projectListToDisplay: Project[] = []
    private schoolOptions: School[] = []
    private options: string = ""
    private type: string = "I"
    private schoolId: number = 0
    private selector: number[] = []
    private grade: number = 0
    private gradeOrigins: number[] = []


    async created() {
      this.schoolOptions = await getSchools()
      this.selector = await GetSelectorGrade()
    }

    async change() {
        try {
            let idx = this.schoolOptions.find(x => x.name == this.options)
            if (idx == undefined) {
                idx = new School(0, "Unknow")
            }
            this.schoolId = idx.schoolId
            this.projectList  = await GetEvaluateProject(this.schoolId)
            for (let index = 0; index < this.projectList.length; index += 1 ) {
                 this.gradeOrigins.push(this.projectList[index].grade)
            }
        } catch (e) {
            console.log(e.mesage)
        }
    }
   async note(newGrade: number, id: number) {
        try {
            await noteProject(id, newGrade, this.schoolId, TypeTimedUser.Jury)
        } catch (e) {
            console.log(e)
        }

    }
     async  gradeChange(idx: number, idProject: number) {
        this.$confirm("Etes-vous sur de valider cette note " + this.projectList[idx].grade + " ? Vous ne pourrez plus la modifier apres cela. Continuez ?", "Warning", {
          confirmButtonText: "OK",
          cancelButtonText: "Annuler",
          type: "warning"
        }).then(async () => {
          await this.note(this.projectList[idx].grade, idProject)
        }).catch(() => {
          this.projectList[idx].grade = this.gradeOrigins[idx]
        })
        console.log( this.projectList[idx].grade)
        console.log( this.gradeOrigins[idx])
        }
}
</script>

<style>
.select-grade{
    width: 30%;
}

.card-jury-grade{
    padding: 0px;
    display: block;
    margin-left: auto;
    margin-right: auto;
}

.col-jury-grade{
    margin-left: 10%;
}

.name-project-jury-grade{
    font-weight: bold;
}

.grade-max{
    font-size: 120%
}
</style>
