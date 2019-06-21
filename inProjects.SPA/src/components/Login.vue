<template>
    <div>
        <br>
        <el-form ref="user" :model="user" :label-position="labelPosition" label-width="100px" >
        <form v-on:submit.prevent.stop="Login" >
            <center>
                <b>
                    <u style="color: red;">{{error}}</u>
                    <el-form-item label="Identifiant" style="width:60%">
                        <el-input name="email" v-validate="'required|email'" placeholder="Insérez votre identifiant" v-model="user.email"></el-input>
                        <i v-show="errors.has('email')" class="fa fa-warning" style="color:orange;"></i>
                        <span v-show="errors.has('email')" class="errorStyle">{{ errors.first('email') }}</span>
                    </el-form-item>
                    <el-form-item label="Mot de passe" style="width:60%" >
                        <el-input name="password" v-validate="'required'" placeholder="Insérez votre mot de passe"
                            v-model="user.password" show-password ></el-input>
                        <i v-show="errors.has('password')" class="fa fa-warning" style="color:orange;"></i>
                        <span v-show="errors.has('password')" class="errorStyle">{{ errors.first('password') }}</span>
                    </el-form-item>
                    <el-form-item>
                        <el-button type="success" @click="Login">Valider</el-button>
                        <el-button type="info" @click="resetForm()">Réinitialiser</el-button>
                    </el-form-item>
                </b>
            </center>
            <input type="submit" hidden/>
        </form>
        </el-form>
        <div>
            <el-button type="primary" @click="Outlook()">Connexion avec Outlook</el-button>
        </div>
    </div>
</template>

<script lang="ts">
import Vue from "vue"
import { User } from "../modules/classes/User"
import { Component } from "vue-property-decorator"
import { getUserName } from "../api/accountApi"
import { AuthService } from "@signature/webfrontauth"
import { UserInfo } from "../modules/classes/UserInfo"
import { getAuthService } from "../modules/authService"
import { sha256 } from "js-sha256"

@Component

export default class Login extends Vue {
    private labelPosition: string = "top"
    private user: User = new User()
    private authService: AuthService = getAuthService()
    private error: string = ""

    async Login() {
        const userInfos: UserInfo = await getUserName(this.user)
        const password: string = sha256(this.user.password)

        if (await this.$validator.validateAll()) {
            await this.authService.basicLogin(userInfos.userName, password)
            if (this.authService.authenticationInfo.level == 0) {
                this.error = "La connexion a échouée ! Réessayez !"
            } else {
                this.error = ""
                this.$router.replace("/")
            }
        }
    }

    async Outlook() {
        await this.authService.startPopupLogin("Oidc")
        this.$router.replace("/")
    }

    resetForm() {
        this.user.email = ""
        this.user.password = ""
    }
}
</script>

<style>
input {
    text-align: center
}
.errorStyle {
    color: red
}
</style>
