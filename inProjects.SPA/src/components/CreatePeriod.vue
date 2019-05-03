<template>
    <div>
        Dates pour la nouvelle periode : 
        <br>
     <el-date-picker
      v-model="date"
      type="daterange"
      align="right"
      start-placeholder="Date de début"
      end-placeholder="Date de fin">
    </el-date-picker>
    <br>
    <br>
    <br>
    <br>
    <el-input class="el-input-groupe" placeholder="Ajouter un groupe" v-model="groupName"></el-input>
    <el-button @click="Add()">Ajouter</el-button>
    <br>
    <br>
    Choissisez les groupes de la nouvelle periode :
    <el-table :data="listGroup" class="el-table-period" >

        <el-table-column >
            {{listGroup[indexMethod]}}
        </el-table-column>

        <el-table-column >
            <el-button @click="Test()"> Delete</el-button>
        </el-table-column>

    </el-table>
     <br>
     <br>
    <el-button @click="Test()">Test</el-button>
    <el-button @click="CreatePeriod()">Creer Periode</el-button>
    </div>

</template>

<script lang="ts">

import { Component, Vue, Prop } from "vue-property-decorator"
import { DatePickerType } from 'element-ui/types/date-picker';
import { PeriodCreate } from "../modules/classes/PeriodCreate"
import { createPeriodAsync } from "../api/periodApi"
import { getTemplateGroupsAsync } from "../api/groupApi"


@Component
export default class CreatePeriod extends Vue {
    private date: Array<Date> = new Array<Date>()
    private listGroup : Array<String> = new Array<String>()
    private groupName : String = ""
    async created(){
        this.listGroup = await getTemplateGroupsAsync()
    }

    async CreatePeriod(){
        let date = new Date();

        if(this.date.length != 0){
            const pc : PeriodCreate = new PeriodCreate();
            pc.begDate = this.date[0];
            pc.endDate = this.date[1];
            pc.Kind = "S";
            pc.Groups = this.listGroup;
            pc.idZone = 4;
            let test = await createPeriodAsync(pc);
        }else{
            console.log(":(((" + this.date);
        }
     }

     Add(){
         this.listGroup.push(this.groupName);
     }
        
     Test(){
         console.log(this.date);
         console.log(this.listGroup);
     }
}
</script>


<style>
   .el-table-period {
        margin-left: 30%;
        height: 40%;
        width: 40%;
    }
   .el-input-groupe{
       width: 10%;
   }
</style>
