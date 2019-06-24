import { getAsync, postAsync } from "../helpers/apiHelper"
import { Project } from "@/modules/classes/Project"
import {ProjectSheet} from "../modules/classes/ProjectSheet"

const endpoint = process.env.VUE_APP_BACKEND + "/api/Project"

export async function GetAllProject(): Promise<Project[]> {
    const resp = await getAsync(`${endpoint}/getAllProjects`)
    return resp.data
}

export async function GetAllSheet(): Promise<Array<ProjectSheet>> {
    console.log("hey can i ger all sheets ?")
    const rep = await getAsync(`${endpoint}/getAllPiSheet`)
    console.log("the serv response is arrived")
    let projects: Array<ProjectSheet> = new Array<ProjectSheet>()

    console.log("the for loop")
    for (const project of rep.data) {
        console.log("att to the list")
        projects.push(
            new ProjectSheet(
                project.name,
                project.semester,
                project.sector,
                project.logo,
                project.slogan,
                project.pitch,
                [
                    project.team.item1,
                    project.team.item2
                ],
                // project.technos
            )
        )
        console.log("+1")
    }
    console.log("i get all")
    return projects
}