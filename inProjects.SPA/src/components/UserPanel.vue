<template>
<div>
    <li><a @click="redirect('/projectList')">Liste des projets</a></li>
    <li><a @click="redirect(`ProjectUserVote`)">Votez pour les projets</a></li>
    
</div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator"
import { FontAwesomeIcon, FontAwesomeLayers, FontAwesomeLayersText } from "@fortawesome/vue-fontawesome"

@Component
export default class UserPanel extends Vue {
@Prop()
isCollapse!: boolean

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
