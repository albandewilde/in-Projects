<template>
    <div>
        <StaffMemberList></StaffMemberList>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator"
import StaffMemberList from "@/components/StaffMemberList.vue"

@Component({
    components: {
        StaffMemberList
    }
})
export default class Student extends Vue {
}
</script>

<style>

</style>