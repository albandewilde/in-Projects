import { postAsync } from "../helpers/apiHelper"

const endpoint = process.env.VUE_APP_BACKEND + "/api/Project"

export async function SubmitProject(link: string, projectType: number, userId: number): Promise<[boolean, string]> {
    const resp = await postAsync(`${endpoint}/submitProject`, {link: link, projectType: projectType, userId: userId})

    return [resp.data.item1, resp.data.item2]
}