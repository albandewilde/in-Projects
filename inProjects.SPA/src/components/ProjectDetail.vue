<template>
    <div>
        <div>
            <span class="text-project-name"> {{projectDetail.project.name}}  </span> 

            <br>

            <span class="text-project-name">
                Semestre : {{projectDetail.project.semester}}
            </span>

            <br>

            <span class="star" @click="FavOrUnfav()">
                <span class="fa fa-star"></span>
            </span>
        </div>

        <div class="sheet">
            <el-button type="primary" class="generate_sheet" @click="CreateSheet(idProject)">
                Fiche du projet
            </el-button>
        </div>

        <br>

        <hr>

        <br/>
            <img class="image-detail-project" :src="projectDetail.project.logo"/>
        <br/>
        <br/>
            <span class="text-project-slogan"> "{{projectDetail.project.slogan}}" </span>
        <br/>
        <br/>
           <span class="text-project-pitch"> {{projectDetail.project.pitch}} </span>
        <br/>
        <br/>
            <div class="title">Membres du projets : </div>
            <div v-for="(o, idx) in projectDetail.students" :key="idx">
                {{o.firstName}} {{o.lastName}}
            </div>
        <br/>
            <div class="title"> Technologies Utilisées : </div>
            <div v-for="(o, idx) in projectDetail.project.technologies" :key="idx">
                {{o}}
            </div>
            
        <br/>
        <div v-if="projectDetail.project.url != null" class="title">
        <img class="image-github" src="https://github.githubassets.com/images/modules/logos_page/Octocat.png">
         &nbsp;
        <a :href="projectDetail.project.url" target="_blank">GitHub</a>
        </div>

    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator"
import { ProjectDetails } from "../modules/classes/ProjectDetails"
import { getInfosProject, verifyProjectFav, favProject } from "../api/submitProjectApi"
import {GetProject} from "../api/getProject"
import pdfMake from "pdfmake/build/pdfmake"
import pdfFonts from "pdfmake/build/vfs_fonts"
pdfMake.vfs = pdfFonts.pdfMake.vfs

@Component
export default class ProjectDetail extends Vue {
    private idProject!: number
    private projectDetail: ProjectDetails = new ProjectDetails()
    private isFav!: boolean
    private idZone: number = 4

    async mounted() {

        this.idProject = Number(this.$route.params.projectId)
        this.projectDetail = await getInfosProject(this.idProject, this.idZone)
        this.isFav = await verifyProjectFav(this.idProject)

        if (this.isFav) {
            const div = document.getElementsByClassName("fa-star")[0]
            div.setAttribute("style", "color: #F5CC27 !important;")
        }
    }

    async FavOrUnfav() {
        try {
            await favProject(this.idProject)
            this.isFav = !this.isFav
            const div = document.getElementsByClassName("fa-star")[0]
            if (this.isFav) {
                div.setAttribute("style", "color: #F5CC27 !important;")
            } else {
                div.setAttribute("style", "color: #000000 !important;")
            }
        } catch (e) {
            console.error(e)
        }
    }


    GenerateSheet(
        place: Array<string> = ["E07", "26"],
        project_name: string = "ITI'Humain",

        semester: string = "Semestre 1",
        sector: string = "SR",
        technos: Array<string> = ["Rust", "Kotlin", "Python", "Bottle"],
        logo: string = "",
        slogan: string = "Parce qu'on aurait aimé en profiter nous aussi.",
        pitch: string = "Le chat commence par une tête et se termine par une queue qui suis son corps. Elle s'arrête au bout d'un moment.\nLe chat est un animal entouré de poils noir, qui sont parfois gris ou blanc. S'il était rayés, ce serait un petit zèbre.\nIl a deux pattes devant et deux derrière. Il a aussi deux pattes de chaque côté. Les pattes de devant servent a courir, avec les pattes de derrière il freine.\nDe temps en temps le char se dit: \"Tien, je vais faire des petit.\" Quand il les a faits, on dit que c'est une chatte. Les petit s'appellent des chatelots.\nQuand il est dans le jardin, il miaule pour attirer les oiseaux. S'ils ne viennent pas, il grimpe dans les arbres et enlève les œufs dont il nourrit ses petit.",
        team: [string, Array<string>] = ["Julie Agopian",  ["Arthur Cheng", "Dan Chiche", "Melvin Delpierre", "Alban De Wilde"]]
    ) {
        // format data
        team[1] = this.removeNonString(team[1])

        technos.length > 9 ? technos[9] = "..." : null
        technos = technos.slice(0, 10)
        let missing = 11 - technos.length
        for (let idx = 0; idx < missing; idx += 1) {technos.push("")}

        // create the pdf
        const sheet = {
            content: [
                {
                    text: place.join("\n"),
                    style: "place"
                },
            
                {
                    text: project_name,
                    style: "project_name"
                },
            
                {
                    text: semester + (sector ? " - " + sector : ""),
                    style: "semester"
                },
            
                {
                    text: "Technologies",
                    style: "techno"
                },
                
                {
                    image: logo,
                    width: 250,
                    height: 250,
                    style: "logo"
                },
                
                {
                    text: technos.join("\n"),
                    style: "techno_list"
                },

                {
                    text: slogan,
                    style: "slogan"
                },
                
                {
                    text: pitch,
                    style: "pitch"
                },
                
                {
                    text: [
                        {
                            text: team[0] + (team[1].length > 0 && !["", " ", undefined, null].includes(team[0])  ? ", " : ""),
                            style: "leader"
                        },
                        
                        {
                            text: team[1].join(", "),
                            style: "members"
                        }
                    ],
                    style: "team"
                }
            ],
        
            styles: {
                place: {
                    alignment: "right",
                    fontSize: 20,
                    color: "blue",
                    margin: [0, -10, -10, 0]
                },
                
                project_name: {
                    alignment: "center",
                    fontSize: 70
                },
                
                semester: {
                    fontSize: 18,
                    margin: [0, 30, 20, 0],
                    alignment: "right"
                },
                
                techno: {
                    alignment: "right",
                    bolt: true,
                    decoration: "underline",
                    fontSize: 18,
                    margin: [0, 20, 20, 0]
                },
                
                logo: {
                    margin: [60, -50, 0, 0]
                },
                
                techno_list: {
                    alignment: "right",
                    margin: [0, -190, 20, 0],
                    fontSize: 14
                },
                
                slogan: {
                    alignment: "center",
                    color: "blue",
                    fontSize: 20,
                    margin: [0, 50, 0, 0]
                },
                
                pitch: {
                    margin: [0, 50, 0, 0]
                },
                
                leader: {
                    bold: true
                },
                
                team: {
                    alignment: "center",
                    italics: true,
                    margin: [0, 30, 0, 0]
                }
            }
        }
        pdfMake.createPdf(sheet).open()
    }

    async CreateSheet(id: number) {
        // fetch to the server all information we need and formated
        let project = await GetProject(id)

        // generate the pdf
        this.GenerateSheet(
            ["None", "None"],
            project.name,
            "Semestre " + project.semester,
            project.sector,
            project.technos,
            "data:image/jpeg;base64," + project.logo,
            project.slogan,
            project.pitch,
            project.team,
        );
    }

    removeNonString(array: Array<any>) {
        let new_array: Array<string> = []
        for (let idx = 0; idx < array.length; idx += 1) {
            if (typeof(array[idx]) === typeof("string")) {
                new_array.push(array[idx])
            }
        }
        return new_array
    }
}
</script>

<style>
.image-detail-project{
    height: auto;
    width: 20vw;
}

.text-project-name{
    text-align: start;
    font-size: 200%;
    font-style: italic;
    margin-right: 80%;

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

.title{
	font-weight: bold;
    font-size: 150%
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

.image-github{
    height: auto;
    width: 2vw;
}

.sheet {
    float: right;
    margin-top: -4vw
}
</style>