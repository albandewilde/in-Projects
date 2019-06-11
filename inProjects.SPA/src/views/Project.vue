<template>
    <div>
        <ProjectDetail></ProjectDetail>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator"
import ProjectDetail from "@/components/ProjectDetail.vue"

@Component({
    components: {
        ProjectDetail
    }
})
export default class Project extends Vue {
}
</script>

<style>

</style>