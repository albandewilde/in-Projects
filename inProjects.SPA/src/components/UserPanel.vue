<template>
<div>
   
    <el-submenu index="52">
            <template slot="title">
                <font-awesome-icon icon="file-alt" size="lg" />
                <span v-if="isCollapse == false"> Projets</span>
            </template>
        <el-menu-item-group>
            <el-menu-item index="4-1" @click="redirect(`/projectList`)">Liste des projets</el-menu-item>
            <el-menu-item index="4-2">Trouver un projet</el-menu-item>
        </el-menu-item-group>
    </el-submenu>

    <el-menu-item index="53" @click="redirect(`/ProjectUserVote`)">
        <font-awesome-icon icon="thumbs-up" />
        <span v-if="isCollapse == false"> Votez pour les projets !</span>
    </el-menu-item>
    
</div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator"
import { FontAwesomeIcon, FontAwesomeLayers, FontAwesomeLayersText } from '@fortawesome/vue-fontawesome'

@Component
export default class UserPanel extends Vue {
@Prop()
isCollapse!: boolean

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
