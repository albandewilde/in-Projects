import { getAsync, postAsync } from "../helpers/apiHelper"
import { Project } from "@/modules/classes/Project"
const endpoint = process.env.VUE_APP_BACKEND + "/api/Project"

export async function GetAllProject(): Promise<Project[]> {
    const resp = await getAsync(`${endpoint}/getAllProjects`)
    return resp.data
}

export async function GetAllTypeProjectsOfSchool(idSchool: number, type:string): Promise<Project[]> {
    const resp = await getAsync(`${endpoint}/getAllTypeProjectsOfASchool?idSchool=${idSchool}&&type=${type}`)
    return resp.data
}

export async function noteProject(idProject: number, newGrade: number,idSchool: number): Promise<any> {
    const resp = await postAsync(`${endpoint}/noteProject?`,{ProjectId: idProject,Grade: newGrade,SchoolId: idSchool})
    return resp.data
}
