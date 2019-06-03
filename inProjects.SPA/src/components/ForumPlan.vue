<template>
    <div id="plan">
        <!-- <center><el-button style="width: 50%;" @click="this.SavePlan()" type="success">Sauvegarder</el-button></center><br> -->
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
            const c = new Chacheli(i + 1, this.projects[i].posX, this.projects[i].posY, this.projects[i].width,
                this.projects[i].height, this.projects[i].name, true, "dummy-green")
            this.chachelis.push(c)
        }
        setInterval(this.SavePlan, 10000)
    },

    methods: {
        async SavePlan() {
            for(let i = 0; i < this.chachelis.length; i += 1) {
                if(!this.chachelis[i].available) {
                    for(let j = 0; j < this.plan.classRooms.length; j += 1) {
                        if(this.chachelis[i].x >= this.plan.classRooms[j].originX && this.chachelis[i].x <= this.plan.classRooms[j].endPositionX) {
                            if(this.chachelis[i].y >= this.plan.classRooms[j].originY && this.chachelis[i].y <= this.plan.classRooms[j].endPositionY) {
                                this.chachelis[i].classRoom = this.plan.classRooms[j].name
                                const item = this.savedPlan.find(project => project.name === this.chachelis[i].text)
                                if(!item) {
                                    const project = this.projects.find(project => project.name === this.chachelis[i].text)
                                    const p = new Project(this.chachelis[i].text, this.chachelis[i].x, this.chachelis[i].y, 
                                        this.chachelis[i].w, this.chachelis[i].h, project.semester, this.chachelis[i].classRoom)
                                    this.savedPlan.push(p)
                                    break
                                } else {
                                    item.classRoom = this.plan.classRooms[j].name
                                    item.posX = this.chachelis[i].x
                                    item.posY = this.chachelis[i].y
                                    item.height = this.chachelis[i].h
                                    item.width = this.chachelis[i].w
                                    await saveForumPlan(this.savedPlan)
                                }
                            }
                        }
                    }
                } else {
                    const idxProject = this.savedPlan.indexOf(this.chachelis[i])
                    if(idxProject >= 0) {
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
    background-color: hsla(0, 0%, 94%, 0.7);
}
</style>
