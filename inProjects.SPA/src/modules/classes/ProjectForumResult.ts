class ProjectForumResult {
    public name:  string
    public projectId:  number
    public average: number
    public individualGrade: [string, number]
    public jurysId: number[]
    
    constructor()
    constructor(name: string, average: number,projectId: number, individualGrade: [string, number],jurysId : number[]  )
    constructor(name: string = "", average: number = 0,projectId: number = 0, individualGrade:[string, number] = ["",0],jurysId : number[] = []) {
        this.name = name
        this.projectId = projectId;
        this.average = average
        this.individualGrade = individualGrade
        this.jurysId = jurysId
    }
}
export { ProjectForumResult }