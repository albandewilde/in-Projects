<template>
    <div>
        <Error :error="error"/>
        <el-table :data="listPeriods">
             <el-table-column label="Non de la periode" prop="groupName" ></el-table-column>
             <el-table-column label="Date de debut">
                  <template slot-scope="scope">
                    <el-date-picker :clearable="false" v-model="listPeriods[scope.$index].begDate"  @change="changeDate(scope.$index,'begDate')" type="date">    
                    </el-date-picker>
                 </template>
             </el-table-column>
             <el-table-column label="Date de fin">
                  <template slot-scope="scope">
                    <el-date-picker :clearable="false" v-model="listPeriods[scope.$index].endDate"  @change="changeDate(scope.$index,'endDate')" type="date">    
                    </el-date-picker>
                 </template>
             </el-table-column>
        </el-table>
    </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator"
import { Period } from "@/modules/classes/Periode/Period"
import { getAllPeriod, changeDateOfPeriod} from "../api/periodApi"
import Error from "./Erreur.vue"
import getDayOfYear from 'date-fns/get_day_of_year'

@Component({
    components: {
        Error
    }
})
export default class ListPeriod extends Vue {
    private listPeriods: Period[] = []
    private idZone: number = 0
    public error: string[] = []


    async created() {
        this.idZone = 4
        this.listPeriods = await getAllPeriod(this.idZone)
    }

     getDate(idx: number, type: string): Date{
         if(type == "begDate"){
            return this.listPeriods[idx].begDate
         }else{
            return this.listPeriods[idx].endDate
         }
    }

      async changeDate(idx: number, type: string){
         let begDate!: Date
         let endDate!: Date

         begDate = new Date(this.listPeriods[idx].begDate);
         endDate = new Date(this.listPeriods[idx].endDate);

         //Change hours to compare equals Date, if Date is the same but hours different begDate might be bigger than endDate
         begDate.setHours(0,0,0,0)
         endDate.setHours(0,0,0,0)
         console.log("BEG " + begDate)
         console.log("END " + endDate)

         this.error = []

        if(begDate >= endDate){
            this.error.push("Date de debut superieur ou egale à celle de fin")
        }else{
            try{
             await changeDateOfPeriod(this.listPeriods[idx].childId,this.listPeriods[idx].begDate,this.listPeriods[idx].endDate);
             this.$message({
                    message: "La date de la période " + this.listPeriods[idx].groupName + " a bien été changé",
                    type: "success"
                })
            
            }catch(e){
                this.error.push(e.message)
            }

             this.listPeriods = await getAllPeriod(this.idZone)

        }
        
    }

}
</script>

<style>

</style>
