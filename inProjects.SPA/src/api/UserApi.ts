import { getAsync } from "../helpers/apiHelper"
import { User } from "../modules/classes/user"

const endpoint = process.env.VUE_APP_BACKEND + "/api/User"

export async function getStudentList(): Promise<User[]>{
    const response = await getAsync(`${endpoint}/getStudentList`)
    console.log("reponse : " + response);
    console.log("response.data: " + response.data)
    return response.data
}
