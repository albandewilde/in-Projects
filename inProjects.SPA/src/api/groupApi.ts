import { postAsync,getAsync } from "../helpers/apiHelper"
import { PeriodCreate } from '@/modules/classes/PeriodCreate';

const endpoint = process.env.VUE_APP_BACKEND + "/api/group"

export async function getTemplateGroupsAsync(): Promise<Array<string>> {
    const response = await getAsync(`${endpoint}/getAllGroupTemplate`)
    return response.data
}