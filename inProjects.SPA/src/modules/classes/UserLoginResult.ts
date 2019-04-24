import { UserInfo } from "./UserInfo"

class UserLoginResult {
    public userInfo!: UserInfo
    public isSuccess!: boolean
    public isUnregisteredUser!: boolean
    public loginFailureCode!: number
    public loginFailureResponse!: string
}

export { UserLoginResult }