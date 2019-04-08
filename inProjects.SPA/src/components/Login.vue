<template>
    <div>
        {{login.id}}
        <el-form :label-position="labelPosition" label-width="100px" :model="login">
            <center>
                <b>
                    <el-form-item label="Identifiant" style="width:60%">
                        <el-input placeholder="Insérez votre identifiant" v-model="login.id"></el-input>
                    </el-form-item>
                    <el-form-item label="Mot de passe" style="width:60%">
                        <el-input placeholder="Insérez votre mot de passe" v-model="login.pw" show-password></el-input>
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
import Vue from 'vue'
export default Vue.extend({
    data() {
        return {
            labelPosition: 'top',
            login: {
                id: "".toString(),
                pw: "".toString()
            }
        }
    },

    methods: {
        async Login() {

        },

        async Reset() {
            this.login.id = "".toString()
            this.login.pw = "".toString()
        }
    }
})
</script>

<style>
input {
    text-align: center
}
</style>
