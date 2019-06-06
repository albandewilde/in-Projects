<template>
    <div>
            <font size=5><b>Mon Profile :</b></font> 
        <br/>
        <br/>
            <InformationsMyProfil></InformationsMyProfil>
        <br/>
        <br/>
        <hr>
            <font size=5><b>Editer votre Mot de passe :</b></font> 
        <br/>
        <br/>
            <PasswordChange></PasswordChange>
        <hr>
             <font size=5><b>Les Projets que vous aimez :</b></font> 
             <ProjectsUserFav></ProjectsUserFav>
        <div v-if="this.whatTimed.find(x => x == 'Student')">
            <hr>
            <font size=5><b>Vos Projets :</b></font> 
            <ProjectStudentOwn></ProjectStudentOwn>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator"
import PasswordChange from "@/components/PasswordChange.vue"
import InformationsMyProfil from "@/components/InformationsMyProfil.vue"
import ProjectsUserFav from "@/components/ProjectsUserFav.vue"
import ProjectStudentOwn from "@/components/ProjectStudentOwn.vue"
import { getGroupUserAccessPanel } from "../api/groupApi"

@Component({
    components: {
        PasswordChange,
        InformationsMyProfil,
        ProjectsUserFav,
        ProjectStudentOwn
    },
})
export default class MyProfil extends Vue {
    private ZoneId: number = 4;
    private whatTimed: string[] = []

    async created(){
        this.whatTimed = await getGroupUserAccessPanel(this.ZoneId)
    }
}
</script>

<style>

</style>