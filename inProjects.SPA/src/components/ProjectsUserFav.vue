<template>
    <div>
        <div v-if="usersFav.length == 0">
            Vous n'aimez aucun projet 
            <br/>
            <img class="image-grumpy" src="../assets/grumpyCat.jpg">
        </div>
        <div v-else>
            <el-carousel  class="carousel-fav" trigger="click" :interval="4000" type="card">
                <el-carousel-item class="carousel-item-fav" v-for="(o, idx) in usersFav" :key="idx">
                    <span class="text-fav">{{o.groupName}}</span>
                    <img @click="redirect(o.projectId)" class="image-fav" :src="o.logo"/>
                </el-carousel-item>
            </el-carousel>
        </div>
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
    }

    redirect(idProject: string) {
        this.$router.replace("/Project/" + idProject)
    }
}
</script>


<style>
.image-fav{
    height: auto;
    width: 10vw;
    padding-top:10%;
    margin-left :10%;
}
.image-grumpy{
    height: 20vh;
    width: 30vw;

}
.carousel-fav{
      height:100%; 
      width:100%;
}
  .carousel-item-fav {
    background-color: transparent;
    width:20vw;
    margin-left: 10%
  }
 
.text-fav{
    background :#cccccc;
    color: black;
    position: absolute;
    text-align: start;
    width: 100%;
    
}


</style>
