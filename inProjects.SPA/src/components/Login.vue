<template>
    <div>
        {{test}}
        <el-form :label-position="labelPosition" label-width="100px">
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
                        <el-button type="info" @click="Reset()">Réinitialiser</el-button>
                    </el-form-item>
                </b>
            </center>
        </el-form>
    </div>
</template>

<script lang="ts">
import Vue from "vue"
import { User } from "../modules/classes/User"
import { Component } from 'vue-property-decorator'
import { login as loginRequest } from "../api/accountApi"
import { UserLoginResult } from '../modules/classes/UserLoginResult';
import { AuthService } from "@signature/webfrontauth"

@Component
export default class Login extends Vue{
    private labelPosition: string = "top"
    private user: User = new User()
    private userInfos!: UserLoginResult
    private authService!: AuthService

    test: number = 0

    async Login() {
        this.userInfos = await loginRequest(this.user)
        this.test = this.authService.authenticationInfo.level
    }

    async Reset() {
        this.user.reset()
    }
}
</script>

<style>
input {
    text-align: center
}
</style>
