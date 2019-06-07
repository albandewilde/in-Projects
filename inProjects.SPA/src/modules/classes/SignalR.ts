import * as SignalR from "@aspnet/signalr"
import store from "../../store"
class SignalRGestion {
    public connection!: SignalR.HubConnection
    public idZone: number = 4

    public async connect() {
        this.connection = await new SignalR.HubConnectionBuilder()
            .withUrl(process.env.VUE_APP_BACKEND + "/staffMemberHub").build()
        await this.connection.start()
        await this.connection.invoke("JoinRoom", this.idZone)
        console.log("store")
        console.log(store.state.connectionStaffMember)
        store.state.connectionStaffMember = this.connection
    }
}

export { SignalRGestion }