<template>
    <div>
        <div class="calendar-parent">
            <calendar-view
                :events="events"
                :show-date="showDate"
                @click-event="onClickEvent"
                class="theme-default holiday-us-traditional holiday-us-official">
                <calendar-view-header
                    slot="header"
                    slot-scope="t"
                    :header-props="t.headerProps"
                    @input="setShowDate" />
            </calendar-view>
         </div>
           <el-dialog :title='eventClicked.name' :visible.sync="outerVisible">

                 <el-date-picker  type="datetime" :clearable="false" v-model="eventClicked.begDate"  @change="changeDate(eventClicked.eventId)"></el-date-picker>   
                 <el-date-picker  type="datetime" :clearable="false" v-model="eventClicked.endDate"  @change="changeDate(eventClicked.eventId)"></el-date-picker>    
                
                <div slot="footer" class="dialog-footer">
                <el-button @click="outerVisible = false">Annuler</el-button>
                <el-button type="danger" @click="innerVisible = true">Supprimer Evenements</el-button>
                </div>

                <el-dialog width="30%" title=" Etes-vous sur de vouloir supprimer ?" :visible.sync="innerVisible" append-to-body>
                    <el-button @click="innerVisible = false">Non</el-button>
                    <el-button type="danger" @click="innerVisible = true">Oui</el-button>
                </el-dialog>

            </el-dialog>
    </div>
</template>

<script lang="js">
import { CalendarView, CalendarViewHeader } from "vue-simple-calendar"
import {GetEventsSchool, UpdateEvents, CreateEvents, GetEventsType} from "../api/eventApi"
import {Event} from "../modules/classes/EventSchool"

	require("vue-simple-calendar/static/css/default.css")
	require("vue-simple-calendar/static/css/holidays-us.css")

	export default {
		data() {
			return { 
                showDate: new Date(),
                events : [],
                eventsInfo: [],
                innerVisible: false,
                outerVisible: false,
                eventClicked: new Event(),
                eventSaved: new Event(),
                eventChange: {}

            }
		},
		components: {
			CalendarView,
			CalendarViewHeader,
        },

        async created(){
            this.eventsInfo = await GetEventsSchool()
            await this.CreateEvents()
        },
        
		methods: {
			setShowDate(d) {
				this.showDate = d;
            },
            
            async CreateEvents(){
                    this.eventsInfo.forEach(element => {
                    var list = {};

                    list.startDate = element.begDate
                    list.endDate = element.endDate
                    list.id = element.eventId
                    list.title = element.name
                    this.events.push(list)
                });
            },

            onClickEvent(e) {
                this.outerVisible = true

                this.eventClicked.eventId = e.originalEvent.id
                this.eventClicked.begDate = e.originalEvent.startDate
                this.eventClicked.endDate = e.originalEvent.endDate
                this.eventClicked.name = e.originalEvent.title

                this.eventSaved = this.eventClicked.clone()
                this.eventChange = e.originalEvent
            },

            async changeDate() {
                 this.error = []

                if (!this.checkDate()) {
                    this.eventClicked = this.eventSaved
                    this.$message({showClose: true,duration: 5000, message: "Date de debut superieur ou egale à celle de fin",type: 'error'});
                } else {
                    try {
                    this.eventsInfo = await UpdateEvents(this.eventClicked)
                    this.$message({
                            message: "L'évènement " + this.eventClicked.name + " a bien été changé",
                            type: "success"
                    })
                    this.eventChange.startDate = this.eventClicked.begDate;
                    this.eventChange.endDate = this.eventClicked.endDate;
                    } catch (e) {
                        this.eventClicked = this.eventSaved
                        this.$message({
                            showClose: true,
                            duration: 5000,
                            message: e.message,
                            type: 'error'
                        });
                    }

                }
            },
            checkDate(){
                let begDate = new Date(this.eventClicked.begDate)
                let endDate = new Date(this.eventClicked.endDate)
                if (begDate >= endDate) {
                    console.log(false)
                    return false
                }else{
                    return true
                }
            },

	}
}
</script>
<style>
.calendar-parent {
	display: flex;
	flex-direction: column;
	flex-grow: 1;
	overflow-x: hidden;
	overflow-y: hidden;
	max-height: 100vh;
	background-color: white;

	font-family: Calibri, sans-serif;
	width: auto;
    height: auto;
	min-width: 30rem;
	max-width: 100rem;
	min-height: 50rem;
    margin-left: 10%
}
</style>
