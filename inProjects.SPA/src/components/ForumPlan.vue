<template>
    <div id="test">
        <chacheli-designer style="width: 85vw; height: 90vh;" v-show="editMode" 
            ref="designer" :layout="layout" :chachelis="chachelis" />
    </div>
</template>

<script lang="ts">
import Vue from 'vue'
import { Component } from 'vue-property-decorator'
import { Project } from "../modules/classes/Project"
import { getPlan, getProjects } from "../api/forumApi"
import { Plan } from '../modules/classes/Plan'
import { Layout } from '../modules/classes/Layout'
import { Chacheli } from '../modules/classes/Chacheli'

import ChacheliDesigner from '@shellybits/v-chacheli/dist/ChacheliDesigner'
import '@shellybits/v-chacheli/dist/ChacheliDesigner.css'

@Component({
    components: {
        ChacheliDesigner
    },
})
export default class ForumPlan extends Vue {
    editMode: Boolean = true
    chachelis: Chacheli[] = []
    plan!: Plan
    layout: Layout = new Layout()
    projects: Project[] = []
    idCounter!: number

    async beforeCreate() {
        this.plan = await getPlan()
        this.projects = await getProjects()  

        this.layout.cols = this.plan.height
        this.layout.rows = this.plan.width

        for(let i = 0; i < this.projects.length; i += 1) {
            let c = new Chacheli(i+1, this.projects[i].posX, this.projects[i].posY, this.projects[i].width, 
                this.projects[i].height, this.projects[i].name, true, 'dummy-green')
            this.chachelis.push(c)
        }
    }
}
</script>

<style lang="scss">
html,
body,
#test {
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

</style>
