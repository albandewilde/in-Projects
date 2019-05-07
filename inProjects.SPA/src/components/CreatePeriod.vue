<template>
    <div>
        <Error :error="error"/>
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
    &nbsp;
    <el-button @click="Add()">Ajouter</el-button>
    <br>
    <br>
    Choissisez les groupes de la nouvelle periode :
        
        <div class="square-container">

            <div class="card-container">Groupes à ajouter : 
                <el-card class="square">
                    <el-table :data="listGroup" class="list-groups-period">
                         <el-table-column label="Name" prop="name" ></el-table-column>
                         <el-table-column label="Supprimer" width="90">
                            <template slot-scope="scope">
                                <el-button @click="Delete(scope.$index)" type="danger" class="el-icon-remove" circle></el-button> 
                            </template>
                         </el-table-column>
                         <el-table-column label="Operations">
                            <template slot-scope="scope">
                                <div v-if="listGroup[scope.$index].isAlreadyPermanent == false" class="switch-container">
                                    <el-switch :value="getState(scope.$index)" @change="setState(scope.$index)" active-text="Permanent" inactive-text="Temporaire" active-color="#13ce66" inactive-color="#ff4949"></el-switch>
                                </div>
                            </template>
                         </el-table-column>
                    </el-table>
                <!-- <div v-for="(i,idx) in listGroup" :key="idx" class="list-groups-period">
                    {{i.name}}&nbsp;&nbsp;<el-button @click="Delete(idx)" type="danger" class="el-icon-remove" circle></el-button> 
                    <div v-if="listGroup[idx].isAlreadyPermanent == false">
                        <el-switch :value="getState(idx)" @change="setState(idx)" active-text="Rendre Permanent" inactive-text="Rendre Temporaire" active-color="#13ce66" inactive-color="#ff4949"></el-switch>
                    </div>
                </div> -->
                </el-card>
            </div>

            <div class="card-container">Groupes supprimer: 
                <el-card class="square">
                 <el-table :data="listRemove" class="list-groups-period">
                         <el-table-column label="Name" prop="name" ></el-table-column>
                         <el-table-column label="Ajouter">
                            <template slot-scope="scope">
                                <el-button @click="AddToGroup(scope.$index)" type="success" class="el-icon-circle-plus" circle></el-button> 
                            </template>
                         </el-table-column>

                    </el-table>
                </el-card>
            </div>
        </div>
     <br>
     <br>
    <el-button type="primary" @click="CreatePeriod()">Creer Periode</el-button>
    </div>

</template>

<script lang="ts">

import { Component, Vue, Prop } from "vue-property-decorator"
import { DatePickerType } from 'element-ui/types/date-picker';
import { PeriodCreate } from '@/modules/classes/Periode/PeriodCreate';
import { GroupPeriod } from '@/modules/classes/Periode/GroupPeriod';
import { createPeriodAsync } from "../api/periodApi"
import { getTemplateGroupsAsync } from "../api/groupApi"
import Error from "./Erreur.vue"


@Component({
    components: {
        Error
    }
})
export default class CreatePeriod extends Vue {
    private date: Array<Date> = new Array<Date>()
    private listGroup : Array<GroupPeriod> = new Array<GroupPeriod>()
    private groupName : String = ""
    private listRemove : Array<GroupPeriod> = new Array<GroupPeriod>()
    public error : Array<String> = new Array<String>()

    async created(){
        this.listGroup = await getTemplateGroupsAsync()
    }

    async CreatePeriod(){
        let date = new Date();

        this.error = new Array<String>();
            if(this.date.length != 0){
                const pc : PeriodCreate = new PeriodCreate();
                pc.begDate = this.date[0];
                pc.endDate = this.date[1];
                pc.Kind = "S";
                pc.Groups = this.listGroup;
                pc.idZone = 4;
                try{
                let lol = await createPeriodAsync(pc);
                }catch(e){
                console.log(e.response);
                this.error.push(e.response.data);
                }
            }else{
            this.error.push("Les dates ne sont pas choisies");
            }
        
     }

    AddToGroup(idx : number){
         this.listGroup.push(this.listRemove[idx]);
         this.listRemove.splice(idx,1);
    }

    getState(idx : number){
        return this.listGroup[idx].state;
    }

    setState(idx : number){
        this.listGroup[idx].state = !this.listGroup[idx].state
    }

     Add(){
         let addToList : GroupPeriod = new GroupPeriod();
         addToList.name = this.groupName;
         addToList.state = false;
         addToList.isAlreadyPermanent = false;
         this.listGroup.push(addToList);
     }

     Delete(idx : number){
         this.listRemove.push(this.listGroup[idx]);
         this.listGroup.splice(idx,1);
     }
        
     Test(){
         this.error = new Array<String>();
         console.log(this.listGroup);
     }
}
</script>


<style>
   .list-groups-period {
        margin-left: 1%;
      
    }
   .el-input-groupe{
       width: 10%;
   }
   .square-container{
       display: flex;
       justify-content: space-evenly;
   }
   .card-container{
       display: flex;
       flex-direction: column;
       
       width: 30vw;
   }
   .square {
    -webkit-box-pack: center;
    -ms-flex-pack: center;
    justify-content: space-evenly;
    

    overflow: hidden;
    overflow-y: auto;
    max-height: 40vh;
    height: 100%;
    width: 100%;
    }
    .el-icon-remove:hover{
        opacity: 0.6;
        transform: scale(1.4);
        cursor: pointer;
        transition: all 0.5s;
    }
    .el-icon-circle-plus:hover{
        opacity: 0.6;
        transform: scale(1.4);
        cursor: pointer;
        transition: all 0.5s;
    }
    .switch-container{
        width: 11vw;
        
    }
    .delete-container{
      
    }

    #keep{
    border: 4px solid rgb(0,255,0);
    }
    
    #remove{
    border: 4px solid rgb(220,20,60);
    }
    
</style>
