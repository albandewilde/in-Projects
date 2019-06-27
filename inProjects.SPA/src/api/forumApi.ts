import { postAsync, getAsync } from "../helpers/apiHelper"
import { Plan } from "../modules/classes/Plan"
import { Project } from "@/modules/classes/Project"
import { Chacheli } from "../modules/classes/Chacheli"

const endpoint = process.env.VUE_APP_BACKEND + "/api/forum"

export async function getPlan() {
    const response = await getAsync(`${endpoint}/getPlan`)

    const plan = new Plan()
    plan.height = response.data.height
    plan.width = response.data.width
    plan.classRooms = response.data.classRooms

    return plan
}

export async function getProjects(userId: number): Promise<Project[]> {
    let projects: Project[] = []
    const response = await getAsync(`${endpoint}/getProjects?userId=${userId}`)
    projects = response.data

    return projects
}

export async function savePlan(plan: Chacheli[]): Promise<any> {
    const response = await postAsync(`${endpoint}/savePlan`, plan)
    return response
}