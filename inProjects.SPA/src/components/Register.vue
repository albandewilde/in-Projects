<template>
    <el-form :label-position="labelPosition" label-width="100px">
        <center>
            <b>
                <el-form-item label="Nom" style="width:60%;">
                    <el-input placeholder="Insérez votre nom" v-model="user.lastName"></el-input>
                </el-form-item>
                <el-form-item label="Prénom" style="width:60%">
                    <el-input placeholder="Insérez votre prénom" v-model="user.firstName"></el-input>
                </el-form-item>
                <el-form-item label="Email" style="width:60%">
                    <el-input placeholder="Insérez votre email" v-model="user.email"></el-input>
                </el-form-item>
                <el-form-item label="Mot de passe" style="width:60%">
                    <el-input placeholder="Insérez votre mot de passe" v-model="user.password" show-password></el-input>
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

import { Component, Vue, Prop, Watch } from "vue-property-decorator"
import { User } from "../modules/classes/User"
import { register as registerRequest } from "../api/accountApi"

@Component
export default class Register extends Vue {

    private labelPosition: string = "top"
    private user: User = new User()

    async Register() {
        await registerRequest(this.user)
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
