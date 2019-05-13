<template>
    <div>
        <el-button type="primary" icon="el-icon-document-add" @click="dialogVisible = true">Importer un fichier .xlsx</el-button>

        <el-dialog
            title="Ajouter une liste"
            :visible.sync="dialogVisible"
            width="30%"
            :before-close="handleClose"
            class="dialog--uploadxcel">
                <el-upload
                    class="upload-demo"
                    drag
                    action="/getUserList"
                    :on-preview="handlePreview"
                    :on-remove="handleRemove"
                    :file-list="fileList"
                    multiple>
                    <i class="el-icon-upload"></i>
                    <div class="el-upload__text">Déposer les fichiers ici ou<em>cliquez pour envoyer</em></div>
                    <div class="el-upload__tip" slot="tip">Fichiers jpg/png avec une taille inférieure à 500kb</div>
                </el-upload>
            <span slot="footer" class="dialog-footer">
                <el-button @click="dialogVisible = false">Annuler</el-button>
                <el-button type="primary" @click="dialogVisible = false">Confirmer</el-button>
            </span>
        </el-dialog>
    </div>
</template>

<script lang="ts">
import Vue from "vue"
import { Component } from "vue-property-decorator"
import { getUserList } from "../api/UserApi"

@Component
export default class ExcelImport extends Vue {
    private dialogVisible: boolean = false
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
