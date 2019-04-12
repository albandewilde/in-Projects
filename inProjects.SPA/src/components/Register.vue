<template>
    <el-form :label-position="labelPosition" label-width="100px" :model="user">
        <center>
            <b>
                <el-form-item label="Nom" style="width:60%;">
                    <el-input placeholder="Insérez votre nom" v-model="lastName"></el-input>
                </el-form-item>
                <el-form-item label="Prénom" style="width:60%">
                    <el-input placeholder="Insérez votre prénom" v-model="firstName"></el-input>
                </el-form-item>
                <el-form-item label="Email" style="width:60%">
                    <el-input placeholder="Insérez votre email" v-model="email"></el-input>
                </el-form-item>
                <el-form-item label="Mot de passe" style="width:60%">
                    <el-input placeholder="Insérez votre mot de passe" v-model="password" show-password></el-input>
                </el-form-item>
                <el-form-item>
                    <el-button type="primary" @click="Register()">Valider</el-button>
                    <el-button type="info" @click="Reset()">Réinitialiser</el-button>
                </el-form-item>
            </b>
        </center>
    </el-form>
</template>

<script lang="ts">

import { Component, Vue, Prop } from "vue-property-decorator"
import { User } from "../modules/classes/User"
import { register as registerRequest } from "../api/accountApi"

@Component
export default class Register extends Vue {
    labelPosition: string = "top"
    firstName: string = ""
    lastName: string = ""
    email: string = ""
    password: string = ""

    async Register() {
        let user: User = new User(this.firstName, this.lastName, this.email, this.password)
        await registerRequest(user)
    }
    async Reset() {
        this.firstName = "".toString()
        this.lastName = "".toString()
        this.email = "".toString()
        this.password = "".toString()
    }
}

</script>

<style>
input {
    text-align: center
}
</style>
