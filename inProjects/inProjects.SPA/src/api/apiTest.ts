import {
    getAsync,
    postAsync
} from "../helpers/apiHelper"

const endpoint = process.env.VUE_APP_BACKEND + "/api/test"

export async function Test(count: number) {
    return await getAsync(`${endpoint}/Chat?q=${count}`)
}

export async function testPost(data: any) {
    return await postAsync(`${endpoint}/postTest`, data)
}