import { getAsync, postAsync } from "../helpers/apiHelper"
import { Project } from '@/modules/classes/Project';
const endpoint = process.env.VUE_APP_BACKEND + "/api/Project"

export async function GetAllProject(): Promise<Project[]>{
    const resp = await getAsync(`${endpoint}/getAllProjects`)
    return resp.data
}
