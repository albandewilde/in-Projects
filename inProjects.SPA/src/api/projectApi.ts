import { getAsync} from "../helpers/apiHelper"
import { Project } from "@/modules/classes/Project"
import {ProjectSheet} from "../modules/classes/ProjectSheet"

const endpoint = process.env.VUE_APP_BACKEND + "/api/Project"

export async function GetAllProject(): Promise<Project[]> {
    const resp = await getAsync(`${endpoint}/getAllProjects`)
    return resp.data
}

export async function GetAllSheet(): Promise<Array<ProjectSheet>> {
    const rep = await getAsync(`${endpoint}/getAllPiSheet`)
    let projects: Array<ProjectSheet> = new Array<ProjectSheet>()

    for (const project of rep.data) {
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
                project.technos
            )
        )
    }
    return projects
}