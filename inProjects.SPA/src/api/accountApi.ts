import { postAsync, getAsync } from "../helpers/apiHelper"
import { User } from "../modules/classes/user"
import { UserInfo } from "../modules/classes/UserInfo"
import { PasswordChangeInfo } from "../modules/classes/PasswordChangeInfo"
import { InfosAccount } from '@/modules/classes/InfosAccount'

const endpoint = process.env.VUE_APP_BACKEND + "/api/account"

export async function register(data: User): Promise<string> {
    const response = await postAsync(`${endpoint}/register`, data)

    return response.data
}

export async function getUserName(data: User): Promise<UserInfo> {
    const response = await postAsync(`${endpoint}/getUserName`, data)
    const result: UserInfo = new UserInfo()
    result.userName = response.data.userName

    return result
}

export async function changePassword(data: PasswordChangeInfo): Promise<any> {
    const response = await postAsync(`${endpoint}/changePassword`, data)
    return response
}


export async function getAccountInfos(idZone: number): Promise<any> {
    const response = await getAsync(`${endpoint}/getInfosAccount?idZone=${idZone}`)
    return response.data
}


export async function changeAccountInfos(data: InfosAccount): Promise<any> {
    const response = await postAsync(`${endpoint}/changeInfosAccount`, data)
    return response
}