<template>
    <div>
         <div v-for="(o, idx) in projects" :key="idx">
            <el-card class="box-card">
                <div slot="header" class="clearfix">
                    <span>{{o.name}}</span>
                    &nbsp;
                    <el-button type="warning">Bloquer les notes</el-button>
                </div>
                <div v-for="(a,idx2) in o.individualGrade" :key="idx2" class="text item">
                        {{idx2}}
                        <div v-if="projects[idx].individualGrade[idx2] > 0">
                            <el-select @change="gradeChange(idx,idx2)" style="width: 5%" v-model="projects[idx].individualGrade[idx2]" placeholder="Select">
                                        <el-option v-for="(item,index) in 21" :key="index" :value="index"></el-option>
                            </el-select><span class="grade-max">/20</span>
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

@Component
export default class ForumResult extends Vue {
    private projects: ProjectForumResult[] = []

    async created(){
       this.projects = await getAllGradeProjects()
    }

     async gradeChange(idx: number, idx2: number) {
         console.log(idx)
         console.log(idx2)
         console.log(this.projects[idx].individualGrade[idx2])
     }

    async DownloadExcel(){
         await downloadExcel()
     }
}
</script>

<style>

</style>
