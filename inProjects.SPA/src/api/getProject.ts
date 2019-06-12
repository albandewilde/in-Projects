import {postAsync} from "../helpers/apiHelper"

const endpoint = process.env.VUE_APP_BACKEND + "/api/Project"

export async function GetProject() {
    const resp = await postAsync(`${endpoint}/getProject`, {})
}