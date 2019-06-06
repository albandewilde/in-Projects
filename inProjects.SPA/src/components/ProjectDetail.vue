<template>
    <div>
       <span class="text-project-name"> {{projectDetail.project.name}}  </span> 
		<br/>
		   <span class="text-project-name"> Semestre : {{projectDetail.project.semester}}</span>

        <div class="star" @click="FavOrUnfav()">
            <span class="fa fa-star"></span>
        </div>

        <hr>
        <br/>
            <img class="image-detail-project" :src="projectDetail.project.logo"/>
        <br/>
        <br/>
            <span class="text-project-slogan"> "{{projectDetail.project.slogan}}" </span>
        <br/>
        <br/>
           <span class="text-project-pitch"> "{{projectDetail.project.pitch}}" </span>
        <br/>
        <br/>
            Membres du projets : 
            <div v-for="(o, idx) in projectDetail.students" :key="idx">
                {{o.firstName}} {{o.lastName}}
            </div>
		<br/>
           	Technologies Utilisées : 
            <div v-for="(o, idx) in projectDetail.project.technologies" :key="idx">
                {{o}}
            </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator"
import { ProjectDetails } from '../modules/classes/ProjectDetails';
import { getInfosProject, verifyProjectFav, favProject } from '../api/submitProjectApi';

@Component
export default class ProjectDetail extends Vue {
    private idProject!: number
    private projectDetail: ProjectDetails = new ProjectDetails()
    private isFav!: boolean;
	private idZone: number = 4

    async mounted(){
        this.idProject = Number(this.$route.params.projectId)
		this.projectDetail = await getInfosProject(this.idProject,this.idZone)
		this.isFav = await verifyProjectFav(this.idProject)

		if(this.isFav){
			 var div = document.getElementsByClassName('fa-star')[0]
			  div.setAttribute("style","color: #F5CC27 !important;")
		}

    }

    async FavOrUnfav(){
		try{
			await favProject(this.idProject)
			this.isFav = !this.isFav
			var div = document.getElementsByClassName('fa-star')[0]
			if(this.isFav){
				div.setAttribute("style","color: #F5CC27 !important;")
			}else{
				div.setAttribute("style","color: #000000 !important;")
			}

		} catch(e) {
			console.error(e)
		} 
    }
}
</script>

<style>
.image-detail-project{
    height: auto;
    width: 20vw;
    /* border: 1px black solid;
    border-radius: 50%; */
}

.text-project-name{
    text-align: start;
    font-size: 200%;
    font-style: italic;
    margin-right: 80%

}

.text-project-slogan{
    font-size: 150%;
	font-weight: bold;
    font-style: italic;
}

.text-project-pitch{
    text-align: center;
	width :40%;
	font-size: 150%;
	display: inline-block;
}


.star{
	margin-right: 80%;
	cursor: pointer;
}
.star:hover {
	opacity: 0.8;
}

.star:active {
	transform: scale(0.93,0.93) translateY(2px)
}

.fa-star{
font-size: 200%;

}




</style>
