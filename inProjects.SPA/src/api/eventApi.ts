import { postAsync, getAsync } from "../helpers/apiHelper"
import {Event} from "../modules/classes/EventSchool"

const endpoint = process.env.VUE_APP_BACKEND + "/api/Event"

export async function GetEventsSchool() : Promise<Event[]> {
    const resp = await getAsync(endpoint)
    
    let events: Event[] = []

    for (let index = 0; index < resp.data.length; index++) {
       events.push(resp.data[index])
    }

    return events
}

export async function UpdateEvents(event :Event) : Promise<Event[]> {
    let send : Event = new Event(event.eventId,event.name,event.begDate,event.endDate,event.isOther)
    
    send.begDate.setHours(send.begDate.getHours() + 2)
    send.endDate.setHours(send.endDate.getHours() + 2)

    const resp = await postAsync(`${endpoint}/UpdateEvents`, send)
    
    let events: Event[] = []

    for (let index = 0; index < resp.data.length; index++) {
       events.push(resp.data[index])
    }

    return events
}


export async function CreateEvents(event :Event) : Promise<Event[]> {
    let send : Event = event.clone()

    send.begDate.setHours(send.begDate.getHours() + 2)
    send.endDate.setHours(send.endDate.getHours() + 2)
    
    const resp = await postAsync(`${endpoint}/CreateEvents`, send)
    
    let events: Event[] = []

    for (let index = 0; index < resp.data.length; index++) {
       events.push(resp.data[index])
    }

    return events
}

export async function GetEventsType() : Promise<string[]> {
    const resp = await getAsync(`${endpoint}/GetTypeEvents`)
    return resp.data
}