import {
    postAsync
} from "../helpers/apiHelper";
import { User } from "../modules/classes/user"

const endpoint = process.env.VUE_APP_BACKEND + "/api/account";

export async function register(data: User) {
    console.log(data)
    return await postAsync(`${endpoint}`, data)
}