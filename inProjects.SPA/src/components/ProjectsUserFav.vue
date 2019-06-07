<template>
    <div>
        <el-carousel :interval="4000" type="card" height="200px" width="50px">
            <el-carousel-item class="carousel-item-fav" v-for="(o, idx) in usersFav" :key="idx">
                <h3><img class="image-fav" :src="o.logo"/></h3>
                {{o.groupName}}
            </el-carousel-item>
        </el-carousel>
  </div>
</template>


<script lang="ts">
import { Component, Vue } from "vue-property-decorator"
import { ProjectFav } from "@/modules/classes/ProjectFav"
import { getProjectsFav} from "../api/accountApi"

@Component
export default class ProjectUserFav extends Vue {
    usersFav: ProjectFav[] = []

    async created() {
        this.usersFav = await getProjectsFav()
        console.log(this.usersFav)
    }
}
</script>


<style>
.image-fav{
    height: auto;
    width: 10vw;
}
  .carousel-item-fav {
    color: #ffffff;
    background-color:#ffffff;
  }


</style>
