<template>
    <div>
        <Error :error="error"/>
	<a class="button" href="#popup2">Modifier votre mot de passe</a>
        <div id="popup2" class="overlay light">
            <a class="cancel" href="#"></a>
            <div class="popup">
                <a class="close" href="#">&times;</a>
                <div class="content-popup">
                    <form ref="password">
                    <h1>Modifier votre mot de passe</h1>
                    <fieldset>   
                        <label>Ancien mot de passe:</label>
                        <i v-show="errors.has('ancien mot de passe')" class="fa fa-warning" style="color:orange;"></i>
                        <span v-show="errors.has('ancien mot de passe')" class="errorStyle">{{ errors.first('ancien mot de passe') }}</span>
                        <input type="password" name="ancien mot de passe" ref="password" v-validate="'min:6'" v-model="password.oldPassword" show-password id="oldpwd">
                        <i id="pass-status" class="fa fa-eye" aria-hidden="true" @click="viewPassword('oldpwd')"></i>
                        <label>Nouveau mot de passe:</label>
                        <i v-show="errors.has('nouveau mot de passe')" class="fa fa-warning" style="color:orange;"></i>
                        <span v-show="errors.has('nouveau mot de passe')" class="errorStyle">{{ errors.first('nouveau mot de passe') }}</span>    
                        <input type="password" name="nouveau mot de passe" ref="nouveau mot de passe" v-validate="'min:6'" v-model="password.newPassword" show-password id="newpwd">
                        <i id="pass-status" class="fa fa-eye" aria-hidden="true" @click="viewPassword('newpwd')"></i>
                        <label for="password">Confirmer le nouveau mot de passe:</label>
                        <i v-show="errors.has('confirmation')" class="fa fa-warning" style="color:orange;"></i>
                        <span v-show="errors.has('confirmation')" class="errorStyle">{{ errors.first('confirmation') }}</span>
                        <input type="password" name="confirmation" v-validate="'required|min:6|confirmed:nouveau mot de passe'" v-model="password.confirmNewPassword" show-password id="confirm">
                        <i id="pass-status" class="fa fa-eye" aria-hidden="true" @click="viewPassword('confirm')"></i>
                    </fieldset>
                    <br>
                    <button class="button" @click="ChangePassword()" type="button">Valider</button>
                    </form>
                </div>
            </div>
        </div>        
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
    public error: string[] = []
    private password: PasswordChangeInfo = new PasswordChangeInfo()
    private openButton: any = document.querySelector(".dialog__trigger")
    private closebutton: any = document.querySelector("dialog__close")

    async ChangePassword() {
        
        if (await this.$validator.validateAll()) {
            this.error = []
            const passwordHashed = new PasswordChangeInfo()

            passwordHashed.oldPassword = sha256(this.password.oldPassword)
            passwordHashed.newPassword = sha256(this.password.newPassword)

            try {
                const changepassword = await changePassword(passwordHashed)
                this.$message({
                    message: "mot de passe changer avec succés",
                    type: "success"
                })
            } catch (e) {
                this.error.push(e.response.data)
            }
        }

        document.getElementsByClassName("cancel")
    }

    viewPassword(type:string){
        var passwordInput = document.getElementById(type)!
        var passStatus = document.getElementById('pass-status')!
        console.log(passwordInput)
            if ((<HTMLInputElement>passwordInput).type == 'password'){
                (<HTMLInputElement>passwordInput).type = 'text'
                passStatus.className='fa fa-eye-slash';
                console.log(passwordInput)
                
            }
            else {
                (<HTMLInputElement>passwordInput).type='password';
                passStatus.className='fa fa-eye';
            }         
    }

    
}
</script>

<style lang="scss">
$bg-color: white;
$text-color: #333333;
$highlight-color: #E74C3C;


* {
  box-sizing: border-box;
}


.button{
  border: 3px solid $text-color;
  background: $bg-color;
  padding: 15px 20px;
  font-size: 1.1rem;
  text-transform: uppercase;
  transition: all 150ms ease-out;
  transform: translateY(0px);
  text-decoration: none;
  &:hover, &:focus {
    outline: 0;
    transform: translateY(-5px);
    transition: all 100ms ease-in;
    box-shadow: 0 5px 10px rgba($text-color,0.4);
  }
  &:active {
    transform: translateY(-3px);
  }
}

h1 {
	text-align: center;
	font-family: "Trebuchet MS", Tahoma, Arial, sans-serif;
	color: #333;
	text-shadow: 0 1px 0 #fff;
	margin: 50px 0;
}

.overlay {
	position: absolute;
	top: 0;
	bottom: 0;
  left: 0;
  right: 0;
  background: rgba(0,0,0,0.5);
	transition: opacity 200ms;
  visibility: hidden;
	opacity: 0;
	&.light {
    background: rgba(255,255,255,0.5);
  }
  .cancel {
    position: absolute;
    width: 100%;
    height: 100%;
    cursor: default;
  }
  &:target {
    visibility: visible;
    opacity: 1;
  }
}

.popup {
	margin: 75px auto;
	padding: 20px;
	background: #fff;
	border: 1px solid #666;
	width: 30%;
	box-shadow: 0 0 50px rgba(0,0,0,0.5);
	position: relative;
  .light & {
    border-color: #aaa;
    box-shadow: 0 2px 10px rgba(0,0,0,0.25);
  }
  h2 {
    margin-top: 0;
    color: #666;
    font-family: "Trebuchet MS", Tahoma, Arial, sans-serif;
  }
  .close {
    position: absolute;
    width: 20px;
    height: 20px;
    top: 20px;
    right: 20px;
    opacity: 0.8;
    transition: all 200ms;
    font-size: 24px;
    font-weight: bold;
    text-decoration: none;
    color: #666;
    &:hover {
      opacity: 1;
    }
  }
  .content-popup {
    max-height: auto;
    overflow: auto;
  }
  p {
    margin: 0 0 1em;
    &:last-child {
      margin: 0;
    }
  }
}

#popup2{
    padding-left: 15%
}

*,
*:before,
*:after {
  -moz-box-sizing: border-box;
  -webkit-box-sizing: border-box;
  box-sizing: border-box;
}

body {
  font-family: 'Nunito', sans-serif;
  color: #384047;
}

form {
  max-width: 300px;
  margin: 10px auto;
  padding: 10px 20px;
  background: linear-gradient(135deg, rgba(251,251,254,1) 0%, rgba(238,238,251,1) 43%, rgba(225,225,228,1) 72%, rgba(0,0,0,1) 100%);}

h1 {
  margin: 0 0 30px 0;
  text-align: center;
}

input[type="text"],
input[type="password"],
input[type="date"],
input[type="datetime"],
input[type="email"],
input[type="number"],
input[type="search"],
input[type="tel"],
input[type="time"],
input[type="url"],
textarea,
select {
  border: none;
  font-size: 16px;
  height: auto;
  margin: 0;
  outline: 0;
  padding: 15px;
  width: 100%;
  box-shadow: 0 1px 0 rgba(0, 0, 0, 0.03) inset;
  margin-bottom: 30px;
}

input[type="radio"],
input[type="checkbox"] {
  margin: 0 4px 8px 0;
}

select {
  padding: 6px;
  height: 32px;
  border-radius: 2px;
}

.button-a {
  padding: 19px 39px 18px 39px;
  color: #FFF;
  background-color: #4bc970;
  font-size: 18px;
  text-align: center;
  font-style: normal;
  border-radius: 5px;
  width: 100%;
  border: 1px solid #3ac162;
  border-width: 1px 1px 3px;
  box-shadow: 0 -1px 0 rgba(255, 255, 255, 0.1) inset;
  margin-bottom: 10px;
}

fieldset {
  margin-bottom: 30px;
  border: none;
}

legend {
  font-size: 1.4em;
  margin-bottom: 10px;
}

label {
  display: block;
  margin-bottom: 8px;
}

label.light {
  font-weight: 300;
  display: inline;
}

.number {
  background-color: #5fcf80;
  color: #fff;
  height: 30px;
  width: 30px;
  display: inline-block;
  font-size: 0.8em;
  margin-right: 4px;
  line-height: 30px;
  text-align: center;
  text-shadow: 0 1px 0 rgba(255, 255, 255, 0.2);
  border-radius: 100%;
}

@media screen and (min-width: 480px) {
  form {
    max-width: 480px;
  }
}
</style>

