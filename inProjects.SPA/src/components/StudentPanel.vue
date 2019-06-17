<template>
<div>
        <el-menu-item index="94" @click="redirect(`/submit_project`)">
             <font-awesome-icon icon="plus-square" size="lg" />
            <span v-if="isCollapse == false"> Deposer un Projet</span>
        </el-menu-item>
</div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator"

@Component
export default class StudentPanel extends Vue {
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
