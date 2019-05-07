<template>
    <el-table
        :data="test.filter(data => !search || data.firstName.toLowerCase().includes(search.toLowerCase()) || data.lastName.toLowerCase().includes(search.toLowerCase()) || data.semester.toLowerCase().includes(search.toLowerCase())) "
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
            prop="semester"
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
        </el-table>

</template>

<script lang ="ts">
    import Vue from "vue"
    import { User } from "../modules/classes/User"
    import { Component } from "vue-property-decorator"
    import { getStudentList } from "../api/UserApi"

@Component
export default class StudentList extends Vue {
    private studentList!: User[]
    private test: User[] = []
    private search: string = ""
    
    async mounted() {
        this.studentList = await getStudentList()
        for (const user of this.studentList) {
            console.log(user)
            this.test.push(user)

        }
    }
}

</script>

<style>
</style>