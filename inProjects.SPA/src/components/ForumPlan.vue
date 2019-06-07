<template>
    <div id="plan">
        <center>
            <el-button id="saveButton" @click="SavePlan" type="success">Sauvegarder</el-button>
        </center>
        <chacheli-designer v-show="editMode" ref="designer" :layout="layout" :chachelis="chachelis" />
    </div>
</template>

<script lang="js">
import Vue from "vue"
import { Component } from "vue-property-decorator"
import { Project } from "../modules/classes/Project"
import { getPlan, getProjects, savePlan as saveForumPlan } from "../api/forumApi"
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
            basicHeight: 2,
            basicWidth: 3,
            projects: new Array(),
            savedPlan: new Array()
        }
    },

    watch: {
        async savedPlan() {
            const response = await saveForumPlan(this.savedPlan)
        }
    },

    async mounted() {
        this.plan = await getPlan()
        this.projects = await getProjects()
        
        this.layout.cols = this.plan.width
        this.layout.rows = this.plan.height

        for (let i = 0; i < this.projects.length; i += 1) {
            const c = new Chacheli(i + 1, this.projects[i].posX, this.projects[i].posY, this.basicWidth,
                this.basicHeight, this.projects[i].name, true, "dummy-green")
            this.chachelis.push(c)
        }
    },

    methods: {
        async SavePlan() {
            for (let chacheli of this.chachelis) {
                if (!chacheli.available) {
                    for (let classroom of this.plan.classRooms) {
                        if (chacheli.x >= classroom.originX && chacheli.x <= classroom.endPositionX) {
                            if (chacheli.y >= classroom.originY && chacheli.y <= classroom.endPositionY) {
                                chacheli.classRoom = classroom.name
                                const item = this.savedPlan.find(project => project.id + " - " + project.name === chacheli.text)
                                if (!item) {
                                    const project = this.projects.find(projectItem => projectItem.id + " - " + projectItem.name === chacheli.text)
                                    const p = new Project(chacheli.text, chacheli.x, chacheli.y,
                                        chacheli.w, chacheli.h, project.semester, chacheli.classRoom)
                                    this.savedPlan.push(p)
                                    break
                                } else {
                                    item.classRoom = classroom.name
                                    item.posX = chacheli.x
                                    item.posY = chacheli.y
                                    item.height = chacheli.h
                                    item.width = chacheli.w
                                    await saveForumPlan(this.savedPlan)
                                }
                            }
                        }
                    }
                } else {
                    const idxProject = this.savedPlan.indexOf(chacheli)
                    if (idxProject >= 0) {
                        this.savedPlan.splice(idxProject, 1)
                    }
                }
            }
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

#saveButton {
    width: 50%;
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

.chacheli-designer-layout .grid {
    color: transparent;
}

.chacheli-designer-layout .chacheli .content {
    background-color: transparent;
    /* opacity: 0; */
    color: #bd10e0;
    font-weight: 900;
    font-size: 22px;
    border: none;
}
</style>
