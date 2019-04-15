<template>
    <div>
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
import { Component } from 'vue-property-decorator';

@Component
export default class Login extends Vue{
    private labelPosition: string = "top"
    private user: User = new User()

    async Login() {
        throw new Error()
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
