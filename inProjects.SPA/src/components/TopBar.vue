<template>
    <el-menu
        class="el-menu-horizontal"
        background-color="#545c64"
        default-active="1"
        text-color="white"
        active-text-color=#ffd04b
        mode="horizontal"
        @select="handleSelect"
    >

    </el-menu>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator"

@Component
export default class TopBar extends Vue {
    things: boolean = true
}
</script>

<style>
.el-menu-horizontal{
    position: fixed; /* Set the navbar to fixed position */
    top: 0; /* Position the navbar at the top of the page */
    width: 100%; /* Full width */
}

</style>