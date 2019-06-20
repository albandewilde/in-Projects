<template>
    <div>
        <div class="okok">
            <li class="dropdown">
                <el-button id="buttons" class="dropbtn">Periode</el-button>
                <div class="dropdown-content">
                    <el-button id="buttons" @click="redirect('/createPeriod')">Créer une période</el-button>
                    <el-button id="buttons" @click="redirect('/listPeriod')">Voir toutes les periodes</el-button>
                </div>        
            </li>
        </div>
        <div class="okok">
            <li class="dropdown">
                <el-button id="buttons" class="dropbtn">Liste</el-button>
                <div class="dropdown-content">
                    <el-button id="buttons" @click="redirect('/student')">Liste des étudiants</el-button>
                    <el-button id="buttons"  @click="redirect('/staffMember')">Liste des professeurs</el-button>
                </div>        
            </li>
        </div>
        <div class="okok">
            <li class="dropdown">
                <el-button id="buttons" class="dropbtn">Forum</el-button>
                <div class="dropdown-content">
                    <el-button id="buttons" @click="redirect('/plan')">Plan</el-button>
                    <el-button id="buttons"  @click="redirect('/addJury')">Gestion forum</el-button>
                </div>        
            </li>
        </div>
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
        this.$router.replace(destination)
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
  width: 100%
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
//   padding: 12px 16px;
  text-decoration: none;
  display: block;
  text-align: left;
}
#buttons {
  display: block;
  color: white;
  /* padding: 8px 16px; */
  text-decoration: none;
  background-color: #2d3e4f;
  border: none;
}

.dropdown-content a:hover {background-color: red;}

.dropdown:hover .dropdown-content {
  display: block;
}

.okok{
    display: block;
}
</style>
