<template>
    <div>
        <div v-for="(item,index) in projectsPublicResult" :key="index">
           
            {{item.name}}&nbsp;{{item.average}}&nbsp;points
            <br/>
            <br/>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import { ProjectForumResult } from '../modules/classes/ProjectForumResult';
import { getAllPublicNote } from '../api/forumApi';

@Component
export default class ForumResultPublic extends Vue {
    private projectsPublicResult : ProjectForumResult[] = []

    async created(){
        this.projectsPublicResult = await getAllPublicNote()
        setInterval(this.update, 180000)
    }

    async update(){
        this.projectsPublicResult = await getAllPublicNote()
        console.log(this.projectsPublicResult)
    }
}
</script>

<style>

</style>
