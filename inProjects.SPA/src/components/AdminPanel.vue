<template>
    <div>
        <div class="okok">
            <li class="dropdown">
                <a class="dropbtn">Periode</a>
                <div class="dropdown-content">
                    <a @click="redirect('/createPeriod')">Créer une période</a>
                    <a @click="redirect('/listPeriod')">Voir toutes les periodes</a>
                </div>        
            </li>
        </div>
        <div class="okok">
            <li class="dropdown">
                <a class="dropbtn">Liste</a>
                <div class="dropdown-content">
                    <a @click="redirect('/student')">Liste des étudiants</a>
                    <a  @click="redirect('/staffMember')">Liste des professeurs</a>
                </div>        
            </li>
        </div>
        <div class="okok">
            <li class="dropdown">
                <a class="dropbtn">Forum</a>
                <div class="dropdown-content">
                    <a @click="redirect('/plan')">Plan</a>
                    <a  @click="redirect('/addJury')">Gestion forum</a>
                </div>        
            </li>
        </div>
        <div class="okok">
            <li>
                <a class="dropbtn" @click="redirect(`/Events`)">Evenements de l'ecole !</a>
            </li>
        </div>
            <el-menu-item index="12-3" @click="redirect('/ForumResult')">Resultats Forum PI </el-menu-item>
    </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator"
import * as SignalR from "@aspnet/signalr"
import { SignalRGestion } from "../modules/classes/SignalR"

@Component
export default class AdminPanel extends Vue {
@Prop()
isCollapse!: boolean
private connection!: SignalR.HubConnection
private idZone: number = 4
private signalr: SignalRGestion = new SignalRGestion()

async created() {
    await this.signalr.connect()
    console.log("co")
    console.log(this.$store.state.connectionStaffMember)

    // this.$store.state.connectionStaffMember = this.connection
}

 redirect(destination: string) {
        this.$router.push(destination)
    }
}
</script>

<style lang="scss">

.dropbtn {
  display: inline-block;
  color: white;
  text-align: center;
  text-decoration: none;
 }


.dropdown {
  display: inline-block;
}

.dropdown-content {
  display: none;
  position: absolute;
  background-color: #2d3e4f;
  min-width: 160px;
  box-shadow: 0px 8px 16px 0px rgba(0,0,0,0.2);
  z-index: 1;
}

.dropdown-content a {
  color: black;
  padding: 12px 16px;
  text-decoration: none;
  display: block;
  text-align: left;
}

.dropdown-content a:hover {background-color: red;}

.dropdown:hover .dropdown-content {
  display: block;
}

.okok{
    display: block  
}
</style>
