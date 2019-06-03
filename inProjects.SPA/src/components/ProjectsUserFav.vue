<template>
    <div>
        <el-carousel :interval="4000" type="card" height="200px">
            <el-carousel-item v-for="item in 6" :key="item">
                <h3 class="medium">{{ item }}</h3>
            </el-carousel-item>
        </el-carousel>
  </div>
</template>


<script lang="ts">
import { Component, Vue } from "vue-property-decorator"

@Component
export default class ProjectUserFav extends Vue {

}
</script>


<style>

</style>
