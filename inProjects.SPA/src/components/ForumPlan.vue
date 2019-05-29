<template>
    <div id="plan">
        <chacheli-designer v-show="editMode" ref="designer" :layout="layout" :chachelis="chachelis" />
    </div>
</template>

<script lang="js">
import Vue from "vue"
import { Component } from "vue-property-decorator"
import { Project } from "../modules/classes/Project"
import { getPlan, getProjects } from "../api/forumApi"
import { Plan } from "../modules/classes/Plan"
import { Layout } from "../modules/classes/Layout"
import { Chacheli } from "../modules/classes/Chacheli"

import ChacheliDesigner from "@shellybits/v-chacheli/dist/ChacheliDesigner"
import "@shellybits/v-chacheli/dist/ChacheliDesigner.css"

export default {
    name: "app",
    components: { ChacheliDesigner },

    data() {
        return {
            editMode: true,
            chachelis: new Array(),
            plan: new Plan(),
            layout: {
                cols: 0,
                rows: 0
            },
            projects: new Array()
        }
    },

    async mounted() {
        this.plan = await getPlan()
        this.projects = await getProjects()

        this.layout.cols = this.plan.width
        this.layout.rows = this.plan.height

        for (let i = 0; i < this.projects.length; i += 1) {
            const c = new Chacheli(i + 1, this.projects[i].posX, this.projects[i].posY, this.projects[i].width,
                this.projects[i].height, this.projects[i].name, true, "dummy-green")
            this.chachelis.push(c)
        }
    }
}
</script>

<style>
#plan {
	height: 100%;
	display: flex;
    flex-direction: column;
}
body {
	margin: 0;
	padding: 0;
}
* {
	box-sizing: border-box;
}
.chacheli-designer-layout {
    background-image: url("/plan.jpg");
    background-size: 100% 100%;
}
</style>
