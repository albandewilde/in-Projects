<template>
    <div>
        <Error :error="error"/>
        <span style="font-size:200%">Créer un évenement</span>
           <br/>

        <el-form :label-position="'top'" ref="form" :model="event" label-width="120px">
            <el-form-item label="Nom de l'évènement">

                <el-select ref="select" v-model="value" placeholder="Select" v-validate="'required'">
                    <el-option  v-for="(item,index) in select" :key="index" :label="select[index]" :value="select[index]" />
                </el-select>
                <br/>
                <div v-if="value == 'Autre'">
                    <el-input  v-validate.immediate="'required_if:select,Autre'" placeholder="Precisez ici" style="width: 20%" type="text" v-model="event.name" ></el-input>
                </div>

            </el-form-item>
            
            <el-form-item label="Date de début">
                <el-date-picker placeholder="Selectionnez date et horaire" type="datetime" :clearable="false" v-model="event.begDate" > </el-date-picker>  
            </el-form-item>

            <el-form-item label="Date de fin">
                 <el-date-picker placeholder="Selectionnez date et horaire"  type="datetime" :clearable="false" v-model="event.endDate" > </el-date-picker>  
            </el-form-item>
        <el-button type="primary" @click="onSubmit">Create</el-button>
        </el-form>

        <br/>
        <br/>

        <span style="font-size:200%">Liste des évenements</span>
        <el-table :data="events">
             <el-table-column label="Nom de l'evenement" prop="name" ></el-table-column>
             <el-table-column label="Date de debut">
                  <template slot-scope="scope">
                    <el-date-picker  type="datetime" :clearable="false" v-model="events[scope.$index].begDate"  @change="changeDate(scope.$index)">    
                    </el-date-picker>
                 </template>
             </el-table-column>
             <el-table-column label="Date de fin">
                  <template slot-scope="scope">
                    <el-date-picker  type="datetime" :clearable="false" v-model="events[scope.$index].endDate"  @change="changeDate(scope.$index)" >    
                    </el-date-picker>
                 </template>
             </el-table-column>
        </el-table>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator"
import {GetEventsSchool, UpdateEvents, CreateEvents} from "../api/eventApi"
import {Event} from "../modules/classes/EventSchool"
import Error from "./Erreur.vue"

@Component({
    components: {
        Error
    }
})
export default class EventSchool extends Vue {
    public error: string[] = []
    private events: Event[] = []
    private event: Event = new Event()
    private value: string = ""
    private select: string[] = []

    async created(){
        this.events = await GetEventsSchool()

        this.select.push("ForumPI")
        this.select.push("Projet Sur Scene")
        this.select.push("JPO")
        this.select.push("Autre")
    }
    

     async changeDate(idx: number) {
        let begDate!: Date
        let endDate!: Date

        begDate = new Date(this.events[idx].begDate)
        endDate = new Date(this.events[idx].endDate)

        this.error = []

        if (begDate >= endDate) {
            this.error.push("Date de debut superieur ou egale à celle de fin")
        } else {
            try {
            begDate.setHours(begDate.getHours() + 2)
            endDate.setHours(endDate.getHours() + 2)

             this.events[idx].begDate = begDate
             this.events[idx].endDate = endDate
             this.events = await UpdateEvents(this.events[idx])
             this.$message({
                    message: "L'évènement " + this.events[idx].name + " a bien été changé",
                    type: "success"
                })
            } catch (e) {
                this.error.push(e.message)
            }

        }
    }

    async onSubmit(){
        let begDate!: Date
        let endDate!: Date

        begDate = new Date(this.event.begDate)
        endDate = new Date(this.event.endDate)

        this.error = []

        if (begDate >= endDate) {
            this.error.push("Date de debut superieur ou egale à celle de fin")
        }else{
            try {
                
                if (await this.$validator.validateAll()) {
                    if(this.value != "Autre"){
                        this.event.name = this.value
                    }else{
                        this.event.isOther = true;
                    }

                    begDate.setHours(begDate.getHours() + 2)
                    endDate.setHours(endDate.getHours() + 2)

                    this.event.begDate = begDate
                    this.event.endDate = endDate
                    this.events = await CreateEvents(this.event)

                }
    
            } catch (e) {
                this.error.push(e.message)
            }
        }
    }
}
</script>

<style>

</style>
