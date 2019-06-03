<template>
    <div>
        <Error :error="error"/>
        <el-form ref="password">
                            <i v-show="errors.has('ancien mot de passe')" class="fa fa-warning" style="color:orange;"></i>
                            <span v-show="errors.has('ancien mot de passe')" class="errorStyle">{{ errors.first('ancien mot de passe') }}</span>
                            <br/>
        Ancien mot de passe : <el-form-item>
                                    <el-input class="text-input-my-profil" name="ancien mot de passe" ref="password" v-validate="'min:6'" placeholder="Insérez votre ancien mot de passe" v-model="password.oldPassword" show-password></el-input>
                             </el-form-item>
                              

                                <i v-show="errors.has('nouveau mot de passe')" class="fa fa-warning" style="color:orange;"></i>
                                <span v-show="errors.has('nouveau mot de passe')" class="errorStyle">{{ errors.first('nouveau mot de passe') }}</span>
                                <br/>
        Nouveau mot de passe : <el-form-item>
                                    <el-input class="text-input-my-profil" name="nouveau mot de passe" ref="nouveau mot de passe" v-validate="'min:6'" placeholder="Insérez votre nouveau mot de passe" v-model="password.newPassword" show-password></el-input>
                             </el-form-item>

                                            <i v-show="errors.has('confirmation')" class="fa fa-warning" style="color:orange;"></i>
                                            <span v-show="errors.has('confirmation')" class="errorStyle">{{ errors.first('confirmation') }}</span>
                                            <br/>
        Confirmer le nouveau mot de passe : <el-form-item>
                                                <el-input class="text-input-my-profil" name="confirmation" v-validate="'required|min:6|confirmed:nouveau mot de passe'" placeholder="Confimer votre mot de passe" v-model="password.confirmNewPassword" show-password></el-input>
                                            </el-form-item>

        <el-button type="success" @click="ChangePassword()">Valider</el-button>
        </el-form>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator"
import { PasswordChangeInfo } from "@/modules/classes/PasswordChangeInfo"
import { sha256 } from "js-sha256"
import { changePassword} from "../api/accountApi"
import Error from "./Erreur.vue"

@Component({
    components: {
        Error
    }
})
export default class PasswordChange extends Vue {
    private password: PasswordChangeInfo = new PasswordChangeInfo()
    public error: string[] = []

    async ChangePassword(){
        if (await this.$validator.validateAll()) {
            this.error =[]
            const passwordHashed = new PasswordChangeInfo()

            passwordHashed.oldPassword = sha256(this.password.oldPassword)
            passwordHashed.newPassword = sha256(this.password.newPassword)

            try {
                const changepassword = await changePassword(passwordHashed)
                 this.$message({
                    message: "mot de passe changer avec succés",
                    type: "success"
                })
            } catch(e) {
                this.error.push(e.response.data)
            }
        }
    }
}
</script>

<style>

</style>
