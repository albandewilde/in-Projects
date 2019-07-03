<template>
    <div class="addJury">
        <div><br>
            <span>Attribuer un n° aux projets</span>
            <CsvImport type="projectNumber"></CsvImport>
        </div>
        <br>
        {{countProjetWNumber()}} Projets se sont vus attribuer un numéro pour cette période      
        <br>
        <div>  
            <span><h3>Attribuer les projets aux jurys</h3></span>
            <CsvImport type="jury"></CsvImport>
        </div>
    </div>
</template>


<script lang="ts">
import { Component, Vue } from "vue-property-decorator"
import CsvImport from "@/components/CsvImport.vue"
import { getProjects } from "../api/forumApi"
import { ForumProject } from '../modules/classes/ForumProject';
import { Project } from '../modules/classes/Project';

@Component({
    components: {
     CsvImport
    }
})
export default class AddJury extends Vue {
    private projects: Project[] = new Array()

    async mounted(){
        this.projects = await getProjects()
        console.log(this.projects)
    }

    countProjetWNumber(){
        if(this.projects.length != 0 ) return this.projects.length 
        return 0
    }
}
</script>
<style>
.addJury {
    height: 100vh;
}
</style>
