import { postAsync, getAsync } from "../helpers/apiHelper"
import { Plan } from "../modules/classes/Plan"
import { ClassRoom } from "@/modules/classes/ClassRoom"
import { Project } from "@/modules/classes/Project"
import { Chacheli } from "../modules/classes/Chacheli"
import * as jsonPlan from "../../plan.json"
import * as jsonProjects from "../../projects.json"

const endpoint = process.env.VUE_APP_BACKEND + "/api/forum"

export async function getPlan() {
    // const response = await getAsync(`${endpoint}/getPlan`)

    // For testing purpose
    const plan = new Plan()
    plan.classRooms = []
    jsonPlan.Classrooms.forEach(room => {
        const newroom: ClassRoom = new ClassRoom()
        newroom.endPositionX = room.EndPositionX
        newroom.endPositionY = room.EndPositionY
        newroom.name = room.Name
        newroom.originX = room.OriginX
        newroom.originY = room.OriginY

        plan.classRooms.push(newroom)
    })
    plan.height = jsonPlan.height
    plan.width = jsonPlan.width
    return plan

    // return response.data
}

export async function getProjects(): Promise<Project[]> {
    const projects: Project[] = []

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