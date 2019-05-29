<template>
    <div>
        <el-button type="primary" icon="el-icon-document-add" @click="dialogVisible = true">Importer un fichier .csv</el-button>
    
        <el-dialog
            title="Ajouter une liste d'élèves via un csv"
            :visible.sync="dialogVisible"
            width="30%"
            :before-close="handleClose"
            class="dialog--uploadxcel">
            <el-form enctype="mutltipart/form-data">
                <input type="file" name="file" accept=".csv" ref="file" @change="handleFileChange()"/>
                <el-button type="button" @click="submitFile()">télécharger</el-button>
            </el-form>

        </el-dialog>
    </div>
</template>

<script lang="ts">
import Vue from "vue"
import { Component, Prop } from "vue-property-decorator"
import { getUserList } from "../api/UserApi"
import axios from "axios"
import { sendCsv } from "../api/csvApi"

@Component
export default class CsvImport extends Vue {
    @Prop(String) type!: ''
    private dialogVisible: boolean = false
    private file: any = ""
    private env: any = process.env.VUE_APP_BACKEND
    private formData: FormData = new FormData()
    private idZone: number = 4

    handleFileChange() {
        const test =  this.$refs.file.files[0]
        this.formData.append("file", test)
        this.formData.append('type', this.type)
        for ( const i of this.formData.entries()) {
            console.log(i)
        }
    }
    async submitFile() {
        try{
            await sendCsv(this.formData)
        }
        catch(e){
            console.log(e)
        }
        finally{
            this.dialogVisible = false
            var co = this.$store.state.connectionStaffMember
            await co.invoke("RefreshList", this.idZone)
        }

    }
}
</script>

<style lang="scss">
.dialog--uploadxcel{
    margin-left: 5%
}


.el-table th div {
 white-space:initial;
 display: block;
}
</style>
