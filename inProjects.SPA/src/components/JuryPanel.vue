<template>
<div>
   <el-menu-item index="73" @click="redirect(`/ProjectJuryVote`)">
        <font-awesome-icon icon="thumbs-up" />
        <span v-if="isCollapse == false"> Evaluez projets !</span>
    </el-menu-item>

</div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator"

@Component
export default class JuryPanel extends Vue {
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
