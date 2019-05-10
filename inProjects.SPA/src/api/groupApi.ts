import { postAsync,getAsync } from "../helpers/apiHelper"
import { PeriodCreate } from '../modules/classes/Periode/PeriodCreate';
import { GroupPeriod } from '@/modules/classes/Periode/GroupPeriod';

const endpoint = process.env.VUE_APP_BACKEND + "/api/group"

export async function getTemplateGroupsAsync(): Promise<Array<GroupPeriod>> {
    const response = await getAsync(`${endpoint}/getAllGroupTemplate`)
    return response.data
}