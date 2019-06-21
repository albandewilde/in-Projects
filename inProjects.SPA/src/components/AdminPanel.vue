<template>
<div>
        <el-submenu index="10">
                <template slot="title">
                <i class="fa fa-calendar-plus-o" aria-hidden="true" style="color: white; font-size: 24px" ></i>
                    <span v-if="isCollapse == false"> Periode </span>
                </template>
                <el-menu-item-group>
                    <el-menu-item index="10-1" @click="redirect('/createPeriod')" >Creer Periode </el-menu-item>
                    <el-menu-item index="10-2" @click="redirect('/listPeriod')" >List Periode </el-menu-item>
                </el-menu-item-group>

        </el-submenu>
        <el-submenu index="11">
            <template slot="title">
                <font-awesome-icon icon="user-tie" size="lg"/>
                    <span v-if="isCollapse == false"> Listes</span>
            </template>
                <el-menu-item-group>
                    <el-menu-item index="11-1" @click="redirect('/student')">Liste des étudiants</el-menu-item>
                </el-menu-item-group>
                <el-menu-item-group>
                    <el-menu-item index="11-2" @click="redirect('/staffMember')">Liste des professeurs</el-menu-item>
                </el-menu-item-group>
        </el-submenu>
        <el-submenu index="12">
            <template slot="title">
                <font-awesome-icon icon="clipboard" size="lg" />
                <span v-if="isCollapse == false">Gestion ForumPI</span>
            </template>
            <el-menu-item index="12-1" @click="redirect('/plan')">Plan</el-menu-item>
            <el-menu-item index="12-2" @click="redirect('/addJury')">Ajouter la liste des jurys</el-menu-item>
        </el-submenu>

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

<style>
.el-menu-vertical:not(.el-menu--collapse) {
    width: 200px;
    min-height: 400px;
}
.el-menu-vertical{
    width: 80px;
    min-height: 400px;
    z-index: 10
}
</style>
