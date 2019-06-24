import { getAsync } from "../helpers/apiHelper"
import {ProjectPiSheet, ProjectPfhSheet} from "../modules/classes/ProjectSheet"

const endpoint = process.env.VUE_APP_BACKEND + "/api/Project"

export async function GetProject(idx: number): Promise<any> {
    const resp = await getAsync(`${endpoint}/getProjectSheet?idx=${idx}`)
    console.log(resp.data)

    if (resp.data.technos) {    // if technos are present, it's a Pi
        return new ProjectPiSheet(
            resp.data.name,
            resp.data.semester,
            resp.data.sector,
            resp.data.logo,
            resp.data.slogan,
            resp.data.pitch,
            [
                resp.data.team.item1,
                resp.data.team.item2
            ],
            resp.data.technos
        )
    } else {
        return new ProjectPfhSheet(
            resp.data.name,
            resp.data.semester,
            resp.data.sector,
            resp.data.logo,
            resp.data.slogan,
            resp.data.pitch,
            [
                resp.data.team.item1,
                resp.data.team.item2
            ],
            resp.data.background
        )
    }
}