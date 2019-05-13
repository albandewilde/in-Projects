<template>
        <el-table
        :data="userListDisplay.filter(data => !search || data.firstName.toLowerCase().includes(search.toLowerCase()) || data.lastName.toLowerCase().includes(search.toLowerCase()) || data.groupName.toLowerCase().includes(search.toLowerCase())) "
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
            label="Rôle"
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

<script lang="ts">
    import Vue from "vue"
    import { BddInfo } from "../modules/classes/BddInfo"
    import { Component } from "vue-property-decorator"
    import { User } from "../modules/classes/User"
    import { getUserList } from "../api/UserApi"

@Component
export default class StaffMemberList extends Vue {
    private bddInfo: BddInfo = new BddInfo()
    private staffMemberList!: User[]
    private userListDisplay: User[] = []
    private search: string = ""

    async mounted() {
        this.bddInfo.tableName = "TimedStaffMember"
        this.bddInfo.tableId = "StaffMemberId"
        this.staffMemberList = await getUserList(this.bddInfo)
        for (const user of this.staffMemberList) {
            this.userListDisplay.push(user)
        }
    }
}
</script>

<style lang="scss">

</style>
