import {
    getAsync,
 
} from "../helpers/apiHelper";

const endpoint = process.env.VUE_APP_BACKEND + "/api/test";

export async function Test(count : Number) {
    return await getAsync(`${endpoint}/Chat?q=${count}`);
}