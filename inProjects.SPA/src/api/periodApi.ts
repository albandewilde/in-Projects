import { postAsync } from "../helpers/apiHelper"
import { PeriodCreate } from '@/modules/classes/PeriodCreate';

const endpoint = process.env.VUE_APP_BACKEND + "/api/administator"

export async function createPeriodAsync(data: PeriodCreate): Promise<any> {
    const response = await postAsync(`${endpoint}/createPeriod`, data)
    console.log(response)
    return response
}