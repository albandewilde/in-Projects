<template>
    <div>
        <Error :error="error"/>
        <el-form ref="password">
        Ancien mot de passe : <el-form-item>
                                    <el-input name="ancien mot de passe" ref="password" v-validate="'required|min:6'" placeholder="Insérez votre ancien mot de passe" v-model="password.oldPassword" show-password></el-input>
                                    <i v-show="errors.has('ancien mot de passe')" class="fa fa-warning" style="color:orange;"></i>
                                    <span v-show="errors.has('ancien mot de passe')" class="errorStyle">{{ errors.first('ancien mot de passe') }}</span>
                             </el-form-item>

        Nouveau mot de passe : <el-form-item>
                                    <el-input name="nouveau mot de passe" ref="nouveau mot de passe" v-validate="'required|min:6'" placeholder="Insérez votre nouveau mot de passe" v-model="password.newPassword" show-password></el-input>
                                    <i v-show="errors.has('nouveau mot de passe')" class="fa fa-warning" style="color:orange;"></i>
                                    <span v-show="errors.has('nouveau mot de passe')" class="errorStyle">{{ errors.first('nouveau mot de passe') }}</span>
                             </el-form-item>

        
        Confirmer le nouveau mot de passe : <el-form-item>
                                                <el-input name="confirmation" v-validate="'required|min:6|confirmed:nouveau mot de passe'" placeholder="Confimer votre mot de passe" v-model="password.confirmNewPassword" show-password></el-input>
                                                <i v-show="errors.has('confirmation')" class="fa fa-warning" style="color:orange;"></i>
                                                <span v-show="errors.has('confirmation')" class="errorStyle">{{ errors.first('confirmation') }}</span>
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
            } catch(e) {
                this.error.push(e.response.data)
            }
        }
    }
}
</script>

<style>

</style>
