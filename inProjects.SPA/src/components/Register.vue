<template>
    <el-form ref="user" :label-position="labelPosition" :model="user" label-width="100px">
        <center>
            <b>
                <el-form-item label="Nom" style="width:60%;" prop="lastName">
                    <el-input name="lastName" placeholder="Insérez votre nom" v-model="user.lastName" v-validate="'required|alpha'"></el-input>
                    <i v-show="errors.has('lastName')" class="fa fa-warning" style="color:orange;"></i>
                    <span v-show="errors.has('lastName')" class="errorStyle">{{ errors.first('lastName') }}</span>
                </el-form-item>
                <el-form-item label="Prénom" style="width:60%" prop="firstName">
                    <el-input name="firstName" placeholder="Insérez votre prénom" v-model="user.firstName" v-validate="'required|alpha'"></el-input>                    
                    <i v-show="errors.has('firstName')" class="fa fa-warning" style="color:orange;"></i>
                    <span v-show="errors.has('firstName')" class="errorStyle">{{ errors.first('firstName') }}</span>
                </el-form-item>
                <el-form-item label="Email" style="width:60%" prop="email">
                    <el-input name="email" placeholder="Insérez votre email" v-model="user.email" v-validate="'required|email'"></el-input>
                    <i v-show="errors.has('email')" class="fa fa-warning" style="color:orange;"></i>
                    <span v-show="errors.has('email')" class="errorStyle">{{ errors.first('email') }}</span>
                </el-form-item>
                <el-form-item label="Mot de passe" style="width:60%" prop="password">
                    <el-input name="password" ref="password" v-validate="'required|min:6'" placeholder="Insérez votre mot de passe" v-model="user.password" show-password></el-input>
                    <i v-show="errors.has('password')" class="fa fa-warning" style="color:orange;"></i>
                    <span v-show="errors.has('password')" class="errorStyle">{{ errors.first('password') }}</span>
                </el-form-item>
                <el-form-item label="Confirmer le mot de passe" style="width:60%" prop="verifiedPassword">
                    <el-input name="verifiedPassword" v-validate="'required|min:6|confirmed:password'" placeholder="Insérez votre mot de passe" v-model="verifiedPassword" show-password></el-input>
                    <i v-show="errors.has('verifiedPassword')" class="fa fa-warning" style="color:orange;"></i>
                    <span v-show="errors.has('verifiedPassword')" class="errorStyle">{{ errors.first('verifiedPassword') }}</span>
                </el-form-item>
                <el-form-item>
                    <el-button type="primary" @click="Register()">Valider</el-button>
                    <el-button type="info" @click="resetForm()">Réinitialiser</el-button>
                </el-form-item>
            </b>
        </center>
    </el-form>
</template>

<script lang="ts">

import { Component, Vue } from "vue-property-decorator"
import { User } from "../modules/classes/User"
import { register as registerRequest } from "../api/accountApi"
import { sha256 } from "js-sha256"
import { AuthService } from "@signature/webfrontauth"
import { getAuthService } from "../modules/authService"
import { Form as ElForm } from "element-ui"

@Component
export default class Register extends Vue {

    private labelPosition: string = "top"
    private user: User = new User()
    private loginResult!: string
    private authService: AuthService = getAuthService()
    private verifiedPassword: string = ""

    async Register() {
        const userHashed: User = new User()
        userHashed.firstName = this.user.firstName
        userHashed.lastName = this.user.lastName
        userHashed.email = this.user.email

        if (await this.$validator.validateAll()) {
            userHashed.password = sha256(this.user.password)
            this.loginResult = await registerRequest(userHashed)
            this.Login(this.loginResult, userHashed.password)
        }
    }

    resetForm() {
        const ref = this.$refs.user as ElForm
        this.verifiedPassword = ""
        ref.resetFields()
    }

    async Login(userId: string, pw: string) {
        await this.authService.basicLogin(userId, pw)
        this.$router.replace("/")
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
