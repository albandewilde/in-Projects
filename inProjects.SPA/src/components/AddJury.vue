<template>
    <div>
        <div>  
            <span><h3>Attribuer un n° au projets</h3></span>
            <CsvImport type="projectNumber"></CsvImport>
        </div>
        <div>  
            <span><h3>Attribuer un n° au proje</h3></span>
            <CsvImport type="jury">t</CsvImport>
        </div>
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
export default class AddJury extends Vue {

}
</script>
<style>

</style>
