import { postAsync } from "../helpers/apiHelper"
import { PeriodCreate } from '@/modules/classes/Periode/PeriodCreate';

const endpoint = process.env.VUE_APP_BACKEND + "/api/period"

export async function createPeriodAsync(data: PeriodCreate): Promise<any> {
    const response = await postAsync(`${endpoint}/createPeriod`, data)
    return response
}