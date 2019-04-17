<template>
    <div>
        <div v-if="action == true">
            <Login></Login>
            <el-button type="warning" @click="changeAction()">S'enregistrer</el-button>
        </div>
        <div v-else>
            <Register></Register>
            <el-button type="warning" @click="changeAction()">Se connecter</el-button>
        </div>
    </div>
</template>

<script lang="ts">

import { Vue, Component } from "vue-property-decorator"
import Login from "@/components/Login.vue"
import Register from "@/components/Register.vue"
import { AuthService } from "@signature/webfrontauth"

@Component({
    components: {
        Login,
        Register,
    },
})
export default class Connection extends Vue {
    action: boolean = true
    authService!: AuthService
    test: number = 0

    getAuthService() {
        this.test = this.authService.authenticationInfo.level
    }
    changeAction() {
        this.action = !this.action
    }
}

</script>

<style>

</style>
