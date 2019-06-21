<template>
    <div>
        <span style="font-size:200%">Créer un évenement</span>
           <br/>

        <el-form :label-position="'top'" ref="form" :model="event" label-width="120px" inline=true>
            <el-form-item label="Nom de l'évènement">
                <el-select ref="select" v-model="value" placeholder="Forum Pi" v-validate="'required'">
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
            <div>
                <el-button type="primary" @click="onSubmit">Create</el-button>
            </div>
        </el-form>

        <br/>
        <br/>
        <span style="font-size:200%">Calendrier des évenements</span>
        <!-- <el-table :data="events">
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
        </el-table> -->

         <calendar ></calendar>
         <br/>
         <br/>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator"
import {GetEventsSchool, UpdateEvents, CreateEvents, GetEventsType} from "../api/eventApi"
import {Event} from "../modules/classes/EventSchool"
import Error from "./Erreur.vue"
import Calendar from "./Calendar.vue"

@Component({
    components: {
        Error,
        Calendar
    }
})
export default class EventSchool extends Vue {
    public error: string[] = []
    private events: Event[] = []
    private event: Event = new Event()
    private value: string = ""
    private select: string[] = []
    private dateNow: Date = new Date()

    async created() {
        this.dateNow.setHours(0, 0, 0, 0)
        this.events = await GetEventsSchool()
        this.select = await GetEventsType()
    }

    async onSubmit() {
        this.error = []

        if (!this.checkDate()) {
            this.$message({showClose: true, duration: 5000, message: "Date de debut superieur ou egale à celle de fin", type: "error"})
        } else if (this.event.endDate < this.dateNow) {
            this.$message({showClose: true, duration: 5000, message: "Date de fin inférieur à celle d'aujourd'hui", type: "error"})
        } else {
            try {
                if (await this.$validator.validateAll()) {
                    if (this.value != "Autre") {
                        this.event.name = this.value
                    } else {
                        this.event.isOther = true
                    }
                    this.events = await CreateEvents(this.event)
                    console.log(this.events[this.events.length - 1])
                    this.$message({message: "L'évènement " + this.events[this.events.length - 1].name + " a bien été rajouté au calendrier", type: "success" })
                }

                this.$root.$emit("AddEvent", this.events[this.events.length - 1])
                this.event = new Event()
                this.value = ""
            } catch (e) {
                this.$message({
                    showClose: true,
                    duration: 5000,
                    message: e.message,
                    type: "error"
                 })
            }
        }
    }

     checkDate(): boolean {
        let begDate!: Date
        let endDate!: Date
        begDate = new Date(this.event.begDate)
        endDate = new Date(this.event.endDate)

        return begDate >= endDate ? true : false
    }
}
</script>

<style>

</style>
