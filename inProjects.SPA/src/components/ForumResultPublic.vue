<template>
    <div class="forum-result-public">
        <table class="table-forum-result-public">
        <tr v-for="(item,index) in projectsPublicResult" :key="index">
                    <td><span > {{item.name}}</span></td>
                    <td><span style="font-weight: bold;" class="percentage-forum-result-public">{{getPercentage(index)}}%</span></td>
                    <td v-if="getPercentage(index) > 0">
                        <div class="w3-light" style="width: 100vw">
                             <div class="w3-blue" :style="'height:12px;width:'+getPercentage(index) +'%'"></div>
                         </div>

                           <!-- <div class="w3-light-grey" style="width: 100vw">
                                <div class="w3-container w3-green" :style="'width:'+getPercentage(index)+'%'">a</div>
                         </div> -->
                    </td>
                    <br/>
                    <br/>
               
        </tr>

        </table>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import { ProjectForumResult } from '../modules/classes/ProjectForumResult';
import { getAllPublicNote } from '../api/forumApi';

@Component
export default class ForumResultPublic extends Vue {
    private projectsPublicResult : ProjectForumResult[] = []
    private diviseur : number = 0

    async created(){
        this.projectsPublicResult = await getAllPublicNote()
        this.diviseur = this.projectsPublicResult[0].countTotalVote * 20
        setInterval(this.update, 180000)
    }

    async update(){
        this.projectsPublicResult = await getAllPublicNote()
        this.diviseur = this.projectsPublicResult[0].countTotalVote * 20
    }

    getPercentage(index: number){
       let percentage = Math.round(((this.projectsPublicResult[index].average/this.diviseur)*100))
       console.log("width:" + percentage + "%")
       return percentage
    }
}
</script>

<style>
@import '../styles/w3.css';

.span-name-project-result{
font-weight: bold;
text-align: end
}

.forum-result-public{
   overflow-x: hidden;
}

.percentage-forum-result-public{
    margin-left: 100%
}

.w3-light{
    margin-left: 3%;

}

.w3-blue{
    border: 1px solid black;
    background-color: #75afff!important
}


</style>
