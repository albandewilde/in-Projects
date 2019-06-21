class ProjectForumResult {
    public name:  string
    public average: number
    public individualGrade: [string, number]
    
    constructor()
    constructor(name: string, average: number, individualGrade: [string, number])
    constructor(name: string = "", average: number = 0, individualGrade:[string, number] = ["",0] ) {
        this.name = name
        this.average = average
        this.individualGrade = individualGrade
    }
}
export { ProjectForumResult }