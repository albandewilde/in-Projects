<template>
    <div>
        <center>
        <div style="width: 1000px; text-align: justify">

        Les fiches de présentation de vos projets PI et PFH doivent-être disposible en publique sur au moins l'une des plateformes suivantes: Google Drive, OneDrive ou DropBox.<br>
        Leur niveau d'accessibilité doit être publique. Cela signifie que tout utilisateur d'internet peut y avoir accès sans inscription à la plateforme avec le lien que vous nous donnerez.<br> 
        Il en est de même pour toutes les ressources que vous nous joindrez sous forme de liens tels que votre logo et votre image de fond en PFH. Le lien doit mener directement à votre image, non à un site sur lequel votre image est.<br>
        <br>
        Ces fiches doivent-être nomé ficheprojet.toml et placé à la racine de votre repertoire en ligne.<br>
        Pour plus d'informations sur les fichiers en toml <a target="_blank" href="https://github.com/toml-lang/toml">cliquez ici</a>.<br>
        Les resources jointes, hors images, doivent-être au format PDF.<br>
        <br>
        Les fichiers d'exemples se trouvent dans les repertoires ./projet_PFH et ./projet_PI. Ces derniers doivent contenir toutes les tables et clés présentes dans les templates. Les tables ou clés en plus seront ignoré.<br>
        Il y a une exception, c'est la table othersDocuments qui est facultative. Dans le cas où vous n'avez pas d'autres ressources en plus à joindre, veillez à effacer ces lignes.<br>
        <br>
        Pour les valeurs possible à chaque clés referez vous aux fichiers d'exemple.<br>
        Les fichiers d'exemples se trouvent dans les repertoires ./projet_PFH et ./projet_PI.<br>
        <br>
        
        <el-button type="primary" @click="DownloadTemplates()">Télécherger les templates</el-button>

        </div>
        </center>

        <br>
        <br>
        <br>

        <el-form :inline="true" :model="formInline">
            <b>
                <el-form-item label="Lien du fichier toml: ">
                    <el-input
                            placeholder="Lien de partege du fichier toml"
                            v-model="projectLink"
                            clearable
                            style="width: 400px"
                    >
                    </el-input>
                </el-form-item>
                <el-form-item>
                    <el-button type="primary" @click="SubmitProject()">Soumettre le projet</el-button>
                </el-form-item>
            </b>
        </el-form>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator"

@Component
export default class SubmitProject extends Vue {
    private projectLink: string = ""
    private error: string = ""

    SubmitProject() {
        console.log(this.projectLink)
    }

    DownloadTemplates() {
        console.log("please give me templates")
    }
}
</script>


<style>
input {
    text-align: center;
}
</style>