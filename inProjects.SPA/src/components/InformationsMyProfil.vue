<template>
    <div>
        <Error :error="error"/>
        <div v-if="mode=='normal'">
            <div class="panel panel-info">
                <center>
                    <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6 col-xs-offset-0 col-sm-offset-0 col-md-offset-3 col-lg-offset-3 toppad">
                        <div class="panel-body">
                            <div class="row">
                                <div class=" col-md-9 col-lg-9 "> 
                                    <table class="table table-striped table-hover table-bordered">
                                        <tbody>
                                            <tr>
                                                <td><b>Nom : </b></td>
                                                <td>{{this.infosUser.userData.firstName}}</td>
                                            <tr>
                                                <td><b>Prénom: </b></td>
                                                <td>{{this.infosUser.userData.lastName}}</td>
                                            <tr>
                                                <td><b>Email : </b></td>
                                                <td>{{this.infosUser.userData.email}}</td>
                                            <tr>
                                                <td><b>Email Secondaire : </b></td>
                                                <td>{{this.infosUser.userData.emailSecondary}}<span v-if="this.infosUser.userData.emailSecondary == null"> Auncun email secondaire</span></td>
                                             <tr>
                                                <td><b>Groupe : </b></td>
                                                <td>{{this.infosUser.group}} <span v-if="this.infosUser.isActual == false">(Anciennement)</span></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </center>
            </div>
        <el-button type="primary" @click="ChangeMode('edit')">Editer Profile</el-button>
        </div>
        <!-- Mode edit -->
        <div v-else>
            <el-form ref="infosUserTemp" label-suffix=" :" size="medium">

                            <i v-show="errors.has('Nom')" class="fa fa-warning" style="color:orange;"></i>
                            <span v-show="errors.has('Nom')" class="errorStyle">{{ errors.first('Nom') }}</span>
                    <el-form-item>
                        Nom :
                            <el-input class="text-input-my-profil" name="Nom" v-validate="'required|alpha'" v-model="infosUser.userData.firstName"></el-input>
                    </el-form-item>


                            <i v-show="errors.has('Prenom')" class="fa fa-warning" style="color:orange;"></i>
                            <span v-show="errors.has('Prenom')" class="errorStyle">{{ errors.first('Prenom') }}</span>
                    <el-form-item>
                        Prénom :
                            <el-input class="text-input-my-profil" name="Prenom" v-validate="'required|alpha'" v-model="infosUser.userData.lastName"></el-input>
                        </el-form-item>

                            <i v-show="errors.has('Email')" class="fa fa-warning" style="color:orange;"></i>
                            <span v-show="errors.has('Email')" class="errorStyle">{{ errors.first('Email') }}</span>
                        <el-form-item v-if="CheckUserSchemes('Basic')">
                            Email :
                            <el-input class="text-input-my-profil" name="Email" v-validate="'required|email'" v-model="infosUser.userData.email"></el-input>
                        </el-form-item>


                            <i v-show="errors.has('Email Secondaire')" class="fa fa-warning" style="color:orange;"></i>
                            <span v-show="errors.has('Email Secondaire')" class="errorStyle">{{ errors.first('Email Secondaire') }}</span>
                        <el-form-item>
                            Email Secondaire :
                            <el-input name="Email Secondaire" class="text-input-my-profil" v-validate="'email'" v-model="infosUser.userData.emailSecondary"></el-input>
                        </el-form-item>
    
                <el-button type="success" @click="ChangeInformation()">Changer ses infos</el-button>
                <el-button type="danger" @click="Cancel('normal')">Annuler</el-button>
            </el-form>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator"
import { getAccountInfos, changeAccountInfos} from "../api/accountApi"
import { InfosAccount } from "@/modules/classes/InfosAccount"
import { getAuthService, AuthService } from "../modules/authService"
import Error from "./Erreur.vue"

@Component({
    components: {
        Error
    }
})
export default class InformationsMyProfil extends Vue {
    public  error: string[] = []
    private idZone: number = 4
    private infosUser: InfosAccount = new InfosAccount()
    private infosUserTemp: InfosAccount = new InfosAccount()
    private mode: string = "normal"
    private authService: AuthService = getAuthService()

    async created() {
       this.infosUser = await getAccountInfos(this.idZone)
    }

    async ChangeInformation() {
         if (await this.$validator.validateAll()) {
             try {
                this.error = []

                await changeAccountInfos(this.infosUser)

                this.$message({
                    message: "Information changer",
                    type: "success"
                })

                this.ChangeMode("normal")
             } catch (e) {
                 this.error.push(e.response.data)
             }
         }
    }

    ChangeMode(mode: string) {
        this.error = []
        this.mode = mode
        console.log(this.infosUser.clone)
        if (this.mode == "edit") {this.infosUserTemp = this.infosUser.clone()}
    }

    Cancel(mode: string) {
        this.infosUser = this.infosUserTemp.clone()
        this.ChangeMode(mode)
    }

    CheckUserSchemes(schemes: string) {
        const exist = this.authService.authenticationInfo.user.schemes.find(x => x.name == schemes)
        if ( exist == undefined) return false
        return true
    }
}
</script>

<style>
.text-input-my-profil{
    width: 20%;
    padding: 0%;
}

.panel-body{
   margin-left: 40%;
}

</style>