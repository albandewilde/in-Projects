import { postAsync, getAsync } from "../helpers/apiHelper"
import { Plan } from "../modules/classes/Plan"
import { ClassRoom } from '@/modules/classes/ClassRoom'
import { Project } from "@/modules/classes/Project"
import * as jsonPlan from "../../plan.json"
import * as jsonProjects from "../../projects.json"

const endpoint = process.env.VUE_APP_BACKEND + "/api/forum"

export async function getPlan(): Promise<Plan> {
    //const response = await getAsync(`${endpoint}/getPlan`)

    // For testing purpose
    let plan = new Plan()    
    plan.classRooms = []
    jsonPlan.Classrooms.forEach(room => {
        let newroom: ClassRoom = new ClassRoom
        newroom.endPositionX = room.EndPositionX
        newroom.endPositionY = room.EndPositionY
        newroom.name = room.Name
        newroom.originX = room.OriginX
        newroom.originY = room.OriginY

        plan.classRooms.push(newroom)
    });
    plan.height = jsonPlan.height
    plan.width = jsonPlan.width
    return plan

    //return response.data
}

export async function getProjects(): Promise<Project[]> {
    let projects: Project[] = []

    jsonProjects.Projects.forEach(project => {
        let np = new Project()
        np.name = project.Name
        np.posX = project.PosX
        np.posY = project.PosY
        np.semester = project.Semester
        np.classroom = project.ClassRoom
        projects.push(np)
    })

    return projects
}
