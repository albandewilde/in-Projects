<template>
    <div>
        Mon Profile :
        <PasswordChange></PasswordChange>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator"
import PasswordChange from "@/components/PasswordChange.vue"


@Component({
    components: {
        PasswordChange
    },
})
export default class MyProfil extends Vue {
}
</script>

<style>

</style>