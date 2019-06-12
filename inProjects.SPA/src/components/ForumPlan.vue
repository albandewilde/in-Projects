<template>
    <div id="plan">
        <center>
            <el-button id="saveButton" @click="SavePlan" type="success">Sauvegarder</el-button>
        </center>
        <br>
        <chacheli-designer 
            @chacheli-closed="closeChacheli"
            @chacheli-moved="checkClassroom"
            v-show="editMode" ref="designer" :layout="layout" :chachelis="chachelis" 
        />
    </div>
</template>

<script lang="js">
import Vue from "vue"
import { Component } from "vue-property-decorator"
import { ForumProject } from "../modules/classes/ForumProject"
import { getPlan, getProjects, savePlan as saveForumPlan } from "../api/forumApi"
import { Plan } from "../modules/classes/Plan"
import { Layout } from "../modules/classes/Layout"
import { Chacheli } from "../modules/classes/Chacheli"
import { AuthService } from "@signature/webfrontauth"
import { getAuthService } from "../modules/authService"

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
            basicHeight: 3,
            basicWidth: 4,
            projects: new Array(),
            authService: null
        }
    },

    async mounted() {
        this.authService = getAuthService()
        this.plan = await getPlan()
        this.projects = await getProjects(this.authService.authenticationInfo.user.userId)
        this.layout.cols = this.plan.width
        this.layout.rows = this.plan.height

        for (let i = 0; i < this.projects.length; i += 1) {
            const displayedName = this.projects[i].forumNumber + " - " + this.projects[i].name
            let isAvailable = false

            if (this.projects[i].posX == -1) {
                isAvailable = true
            }

            const c = new Chacheli(this.projects[i].forumNumber, this.projects[i].name, this.projects[i].posX, this.projects[i].posY, this.basicWidth,
                this.basicHeight, displayedName, isAvailable, "dummy-green", this.projects[i].projectId)
            this.chachelis.push(c)
        }
    },

    methods: {
        async SavePlan() {
            await saveForumPlan(this.projects)        
        },
        checkClassroom(chacheli) {
            chacheliMoved(chacheli)
            for (let classroom of this.plan.classRooms) {
                if (chacheli.x >= classroom.originX && chacheli.x <= classroom.endPositionX) {
                    if (chacheli.y >= classroom.originY && chacheli.y <= classroom.endPositionY) {
                        this.projects[chacheli.forumNumber - 1] = classRoom.name
                    }
                }
            }            
        },
        closeChacheli(chacheli) {
            const project = this.projects.find(projectItem => projectItem.projectId === chacheli.projectId)
            project.posX = -1
            project.posY = -1
            project.width = this.basicWidth
            project.height = this.basicHeight
        }
        chacheliMoved(chacheli) {
            const project = this.projects.find(projectItem => projectItem.projectId === chacheli.projectId)
            project.posX = chacheli.x
            project.posY = chacheli.y
            project.width = chacheli.w
            project.height = chacheli.h
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
    background-image: url("/plan_ecole.jpg");
    background-size: 100% 100%;
}

.chacheli-designer-layout .grid {
    color: transparent;
}

.chacheli-designer-layout .chacheli .content {
    /* background-color: transparent; */
    /* opacity: 0; */
    color: #000000;
    font-weight: 700;
    font-size: 15px;
    border: none;
}
</style>
