import { postAsync, getAsync } from "../helpers/apiHelper"
import { Plan } from "../modules/classes/Plan"
import { ClassRoom } from "@/modules/classes/ClassRoom"
import { Project } from "@/modules/classes/Project"
import { Chacheli } from "../modules/classes/Chacheli"
import * as jsonPlan from "../../plan.json"
import * as jsonProjects from "../../projects.json"
import { NOTIMP } from 'dns';

const endpoint = process.env.VUE_APP_BACKEND + "/api/forum"

export async function getPlan() {
    const response = await getAsync(`${endpoint}/getPlan`)

    const plan = new Plan()
    plan.height = response.data.height
    plan.width = response.data.width
    plan.classRooms = response.data.classRooms
    
    return plan
}

export async function getProjects(): Promise<Project[]> {
    let projects: Project[] = []
    // const response = await getAsync(`${endpoint}/getProjects`)

    // projects = response.data

    jsonProjects.Projects.forEach(project => {
        const np = new Project()
        np.name = project.Name
        np.posX = project.PosX
        np.posY = project.PosY
        np.height = project.Height
        np.width = project.Width
        np.semester = project.Semester
        np.classroom = project.ClassRoom
        np.projectId = project.ProjectId
        projects.push(np)
    })

    return projects
}

export async function savePlan(plan: Chacheli[]): Promise<any> {
    const response = await postAsync(`${endpoint}/savePlan`, plan)
    return response
}