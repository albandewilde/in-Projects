import {
    postAsync
} from "../helpers/apiHelper"
import { User } from "../modules/classes/user"
import { UserLoginResult } from "../modules/classes/UserLoginResult"
import { UserInfo } from "../modules/classes/UserInfo"

const endpoint = process.env.VUE_APP_BACKEND + "/api/account"

export async function register(data: User): Promise<UserLoginResult> {
    return await postAsync(`${endpoint}/register`, data)
}

export async function getUserName(data: User): Promise<UserInfo> {
    var response = await postAsync(`${endpoint}/getUserName`, data)
    var result: UserInfo = new UserInfo
    result.userName = response.data.userName

    return result
}