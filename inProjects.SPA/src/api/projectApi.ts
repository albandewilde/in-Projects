import {getAsync} from "../helpers/apiHelper"
import {Project} from "@/modules/classes/Project"
import {ProjectSheet, ProjectPiSheet, ProjectPfhSheet} from "../modules/classes/ProjectSheet"

const endpoint = process.env.VUE_APP_BACKEND + "/api/Project"

export async function GetAllProject(): Promise<Project[]> {
    const resp = await getAsync(`${endpoint}/getAllProjects`)
    return resp.data
}

export async function GetAllSheet(school: number, type: string, semester: number): Promise<ProjectSheet[] | ProjectPiSheet[] | ProjectPfhSheet[]> {
    const rep = await getAsync(`${endpoint}/getAllSheet?school=${school}&type=${type}&semester=${semester}`)

    let projects
    if (type === "i") {
        projects = new Array<ProjectPiSheet>()

        for (const project of rep.data) {
            projects.push(
                new ProjectPiSheet(
                    project.place,
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
    } else if (type === "h") {
        projects = new Array<ProjectPfhSheet>()

        for (const project of rep.data) {
            projects.push(
                new ProjectPfhSheet(
                    project.place,
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
                    project.background
                )
            )
        }
    } else {
        projects = new Array<ProjectSheet>()

        for (const project of rep.data) {
            projects.push(
                new ProjectSheet(
                    project.place,
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
                )
            )
        }
    }

    return projects
}