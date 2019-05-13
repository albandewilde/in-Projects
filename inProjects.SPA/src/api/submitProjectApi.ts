import { postAsync } from "../helpers/apiHelper"

const endpoint = process.env.VUE_APP_BACKEND + "/api/User"

export async function SubmitProject(link: string): Promise<[boolean, string]>{
    const resp = await postAsync(`${endpoint}/submitProject`, {link: link})

    return [resp.data.item1, resp.data.item2]
}