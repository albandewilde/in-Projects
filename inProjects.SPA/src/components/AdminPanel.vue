<template>
<div>
        <el-submenu index="10">
                <template slot="title">
                    <font-awesome-icon icon="user-tie" size="lg" />
                    <span v-if="isCollapse == false"> Periode </span>
                </template>
                <el-menu-item-group>
                    <el-menu-item @click="redirect('/createPeriod')" >Creer Periode </el-menu-item>
                </el-menu-item-group>
        </el-submenu>
</div>
</template>

<script lang="ts">
import { Component, Vue,Prop } from "vue-property-decorator"

@Component
export default class AdminPanel extends Vue {
@Prop()
isCollapse! : boolean;

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
