import { postAsync, getAsync } from "../helpers/apiHelper"
import {Event} from "../modules/classes/EventSchool"
import axios from "axios"

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
    const resp = await postAsync(`${endpoint}/UpdateEvents`, event)
    
    let events: Event[] = []

    for (let index = 0; index < resp.data.length; index++) {
       events.push(resp.data[index])
    }

    return events
}


export async function CreateEvents(event :Event) : Promise<Event[]> {
    const resp = await postAsync(`${endpoint}/CreateEvents`, event)
    
    let events: Event[] = []

    for (let index = 0; index < resp.data.length; index++) {
       events.push(resp.data[index])
    }

    return events
}