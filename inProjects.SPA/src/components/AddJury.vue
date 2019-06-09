<template>
    <div>
        <span><h3>Ajouter la liste des jurys</h3></span>
        <CsvImport type="jury"></CsvImport>
    </div>
</template>


<script lang="ts">
import { Component, Vue } from "vue-property-decorator"
import CsvImport from "@/components/CsvImport.vue"

@Component({
    components: {
     CsvImport
    }
})
export default class AddJury extends Vue{

}
</script>
<style>

</style>
