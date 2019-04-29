<template>
    <el-table
        :data="studentList"
        stripe
        style="width: 100%">
        <el-table-column
            prop="lastName"
            label="Nom"
            width="180">
        </el-table-column>
        <el-table-column
            prop="firstName"
            label="Prénom"
            width="180">
        </el-table-column>
        <el-table-column
            prop="semester"
            label="Semestre actuel"
        >
        </el-table-column>
    </el-table>
</template>

<script lang ="ts">
    import Vue from "vue"
    import { User } from "../modules/classes/User"
    import { Component } from "vue-property-decorator"

@Component
export default class StudentList extends Vue{
    private studentList!: User[];
}

</script>

<style>
</style>