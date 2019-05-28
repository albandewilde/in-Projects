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

    handleFileChange() {
        const test =  this.$refs.file.files[0]
        this.formData.append("file", test)
        this.formData.append('type', this.type)
        for ( const i of this.formData.entries()) {
            console.log(i)
        }
    }
    async submitFile() {
        await sendCsv(this.formData)
        this.dialogVisible = false
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
