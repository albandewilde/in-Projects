<template>
    <div>
        <StudentList></StudentList>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator"
import StudentList from "@/components/StudentList.vue"

@Component({
    components: {
        StudentList
    }
})
export default class Student extends Vue {
}
</script>

<style>

</style>