class Event {
    public eventId: number
    public name: string
    public begDate: Date
    public endDate: Date
    public isOther: boolean

    constructor()
    constructor(eventId:number, name:string, begDate: Date,endDate: Date )
    constructor(eventId:number = 0, name:string = "", begDate = new Date(),endDate = new Date()){
        this.eventId =eventId;
        this.name = name;
        this.begDate = begDate;
        this.endDate = endDate;
        this.isOther = false;
    }
}
export { Event }