import { getAsync, postAsync } from "../helpers/apiHelper"
import { Project } from "@/modules/classes/Project"
import { TypeTimedUser } from "@/modules/classes/TimedUserEnum"
const endpoint = process.env.VUE_APP_BACKEND + "/api/Project"

export async function GetAllProject(): Promise<Project[]> {
    const resp = await getAsync(`${endpoint}/getAllProjects`)
    return resp.data
}

export async function GetAllTypeProjectsOfSchool(idSchool: number, type:string): Promise<Project[]> {
    const resp = await getAsync(`${endpoint}/getAllTypeProjectsOfASchool?idSchool=${idSchool}&&type=${type}`)
    return resp.data
}

export async function GetSelectorGrade(): Promise<number[]> {
    const resp = await getAsync(`${endpoint}/getSelectorGrade`)
    return resp.data
}

export async function GetEvaluateProject(idSchool: number): Promise<Project[]> {
    const resp = await getAsync(`${endpoint}/getProjectEval?idSchool=${idSchool}`)
    return resp.data
}

export async function noteProject(idProject: number, newGrade: number,idSchool: number, timedUser: TypeTimedUser): Promise<any> {
    const resp = await postAsync(`${endpoint}/noteProject?`,{ProjectId: idProject,Grade: newGrade,SchoolId: idSchool,User:timedUser})
    return resp.data
}

