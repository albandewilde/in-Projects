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
        <hr>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator"
import PasswordChange from "@/components/PasswordChange.vue"
import InformationsMyProfil from "@/components/InformationsMyProfil.vue"
import ProjectsUserFav from "@/components/ProjectsUserFav.vue"


@Component({
    components: {
        PasswordChange,
        InformationsMyProfil,
        ProjectsUserFav
    },
})
export default class MyProfil extends Vue {
}
</script>

<style>

</style>