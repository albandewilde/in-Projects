<template>
<div>
        <el-submenu index="10">
                <template slot="title">
                    <font-awesome-icon icon="user-tie" size="lg" />
                    <span v-if="isCollapse == false"> Periode </span>
                </template>
                <el-menu-item-group>
                    <el-menu-item index="10-1" @click="redirect('/createPeriod')" >Creer Periode </el-menu-item>
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
        <el-menu-item index="12" @click="redirect('/plan')">
            <font-awesome-icon icon="clipboard" size="lg" />
            <span v-if="isCollapse == false"> Forum PI</span>
        </el-menu-item>
</div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator"
import * as SignalR from "@aspnet/signalr" 

@Component
export default class AdminPanel extends Vue {
@Prop()
isCollapse!: boolean
private connection!: SignalR.HubConnection
private idZone: number = 4

async created(){
    this.connection = await new SignalR.HubConnectionBuilder().withUrl(process.env.VUE_APP_BACKEND + "/staffMemberHub").build()
    await this.connection.start();
    await this.connection.invoke("JoinRoom", this.idZone)
    this.$store.state.connectionStaffMember = this.connection
}

 redirect(destination: string) {
        this.$router.replace(destination)
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
