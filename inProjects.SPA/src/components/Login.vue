<template>
    <div>
        <center><div style="color: red;"><u>{{error}}</u></div></center><br>
        <el-form :label-position="labelPosition" ref="form" label-width="100px">
            <center>
                <b>
                    <el-form-item label="Identifiant" style="width:60%">
                        <el-input placeholder="Insérez votre identifiant" v-model="user.email"></el-input>
                    </el-form-item>
                    <el-form-item label="Mot de passe" style="width:60%">
                        <el-input placeholder="Insérez votre mot de passe" v-model="user.password" show-password></el-input>
                    </el-form-item>
                    <el-form-item>
                        <el-button type="primary" @click="Login()">Valider</el-button>
                        <el-button type="info" @click="resetForm()">Réinitialiser</el-button>
                    </el-form-item>
                </b>
            </center>
        </el-form>
    </div>
</template>

<script lang="ts">
import Vue from "vue"
import { User } from "../modules/classes/User"
import { Component } from "vue-property-decorator"
import { getUserName } from "../api/accountApi"
import { AuthService } from "@signature/webfrontauth"
import { UserInfo } from "../modules/classes/UserInfo"
import { UserLoginResult } from "../modules/classes/UserLoginResult"
import { getAuthService } from "../modules/authService"
import { sha256 } from "js-sha256"
import { Form as ElForm } from "element-ui"

@Component
export default class Login extends Vue {
    private labelPosition: string = "top"
    private user: User = new User()
    private authService: AuthService = getAuthService()
    private error: string = ""

    async Login() {
        const userInfos: UserInfo = await getUserName(this.user)

        if(userInfos.userName != null) {
            const password: string = sha256(this.user.password) 

            await this.authService.basicLogin(userInfos.userName, password)

            if(this.authService.authenticationInfo.level == 0) {
                this.error = "La connexion a échouée ! Réessayez !"
            }            
            else {
                this.error = ""
                this.$router.replace("/")
            }
        }
        else {
            this.error = "Identifiant invalide !"
        }
    }

    resetForm() {
        const ref = <ElForm>this.$refs.form
        ref.resetFields()
    }
}
</script>

<style>
input {
    text-align: center
}
</style>
