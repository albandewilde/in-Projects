import {
    postAsync
} from "../helpers/apiHelper"
import { User } from "../modules/classes/user"
import { UserLoginResult } from "../modules/classes/UserLoginResult"

const endpoint = process.env.VUE_APP_BACKEND + "/api/account"

export async function register(data: User): Promise<UserLoginResult> {
    return await postAsync(`${endpoint}/register`, data)
}

export async function login(data: User): Promise<UserLoginResult> {
    return await postAsync(`${endpoint}/login`, data)
}