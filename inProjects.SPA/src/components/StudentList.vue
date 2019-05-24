<template>
<div>
    <el-table
        :data="userListDisplay.filter(data => !search || data.firstName.toLowerCase().includes(search.toLowerCase()) || data.lastName.toLowerCase().includes(search.toLowerCase()) || data.groupName.toLowerCase().includes(search.toLowerCase()))"
        stripe
        style="width: 100%">
        <el-table-column
            prop="lastName"
            label="Nom"
            width="180"
            sortable>
        </el-table-column>
        <el-table-column
            prop="firstName"
            label="Prénom"
            width="180"
            sortable>
        </el-table-column>
        <el-table-column
            prop="groupName"
            label="Semestre actuel"
            sortable
        >
        </el-table-column>
        <el-table-column>
            <template slot="header" slot-scope="scope">
                <el-input
                    v-model="search"
                    size="mini"
                    placeholder="Taper pour chercher"/>
            </template>
        </el-table-column>
        <el-table-column>
            <template slot="header" slot-scope="scope" v-if="isAdmin">
                <CsvImport></CsvImport>
            </template>
        </el-table-column>
    </el-table>
</div>
</template>

<script lang ="ts">
import Vue from "vue"
import { User } from "../modules/classes/User"
import { BddInfo } from "../modules/classes/BddInfo"
import { Component } from "vue-property-decorator"
import { getUserList } from "../api/UserApi"
    import CsvImport from "@/components/CsvImport.vue"
import { getGroupUserAccessPanel } from "../api/groupApi"

@Component({
    components: {
     CsvImport
    }
})

export default class StudentList extends Vue {
    private bddInfo: BddInfo = new BddInfo()
    private studentList!: User[]
    private userListDisplay: User[] = []
    private search: string = ""
    private isAdmin: boolean = false
    private userState: String[] = []
    private zoneId: number = 4

     async mounted() {
        this.bddInfo.tableName = "TimedStudent"
        this.bddInfo.tableId = "StudentId"
        this.studentList = await getUserList(this.bddInfo)
        for (const user of this.studentList) {
            this.userListDisplay.push(user)
        }
        console.log(this.userListDisplay.length + "n°")
        await this.checkAdmin()
        console.log(this.isAdmin)
    }

    async checkAdmin(){
        this.userState = await getGroupUserAccessPanel(this.zoneId)
        for(var i = 0; i < this.userState.length; i++){
            console.log(this.userState[i])
            if(this.userState[i] == "Administration"){
                this.isAdmin = true
            }
        }        
    }
}

</script>

<style>
</style>