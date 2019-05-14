import { getAsync } from "../helpers/apiHelper"
import { GroupPeriod } from "@/modules/classes/Periode/GroupPeriod"

const endpoint = process.env.VUE_APP_BACKEND + "/api/group"

export async function getTemplateGroupsAsync(): Promise<GroupPeriod[]> {
    const response = await getAsync(`${endpoint}/getAllGroupTemplate`)
    return response.data
}