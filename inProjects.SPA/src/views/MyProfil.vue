<template>
    <div>
            <font size=5><b>Mon Profile :</b></font> 
        <br/>
        <br/>
            <InformationsMyProfil></InformationsMyProfil>
        <br/>
        <br/>
        <hr>
        <div v-if="CheckUserSchemes('Basic')">
            <font size=5><b>Editer votre Mot de passe :</b></font> 
        <br/>
        <br/>
            <PasswordChange></PasswordChange>
        <hr>
        </div>

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
import { getAuthService, AuthService } from "../modules/authService"

@Component({
    components: {
        PasswordChange,
        InformationsMyProfil,
        ProjectsUserFav,
        ProjectStudentOwn
    },
})
export default class MyProfil extends Vue {
    private ZoneId: number = 4
    private whatTimed: string[] = []
    private authService: AuthService = getAuthService()

    async created() {
        this.whatTimed = await getGroupUserAccessPanel(this.ZoneId)
    }

    CheckUserSchemes(schemes: string) {
        const exist = this.authService.authenticationInfo.user.schemes.find(x => x.name == schemes)
        if ( exist == undefined) return false
        return true
    }
}
</script>

<style>

</style>