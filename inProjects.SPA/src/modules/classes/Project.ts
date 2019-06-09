class Project {
    public name!: string
    public name!: string
    public projectStudentId: number
    public logo!: string
    public pitch !: string
    public slogan !: string
    public traitName !: string
    public type !: string
    public firstName !: string[]
    public lastName !: string[]
    public timedUserId !: number[]
    public begDate !: Date
    public endDate !: Date
    public userId !: number[]
    public traitId!: number
    public semester!: string
    public technologies: string[]
    public leaderId !: number
    public isFav !: number
    public posX!: number
    public posY!: number
    public width!: number
    public height!: number
    public semester!: string
    public classRoom!: string
    public forumNumber!: number
    public projectId!: number
    constructor(name: string, logo: string, slogan: string, pitch: string, leaderId: number, semester: string, technologies: string[])
    constructor(name: string, logo: string, slogan: string, pitch: string, leaderId: number, semester: string, technologies: string[], projectId: number, type: string, traitId: number)
    constructor(
        name: string = "",
        logo: string = "",
        slogan: string = "",
        pitch: string = "",
        leaderId: number = 0,
        semester: string = "",
        technologies: string[] = [],
        projectStudentId: number = 0,
        type: string = "",
        traitId: number = 0,
    ) {
        this.projectStudentId = projectStudentId
        this.name = name
        this.logo = logo
        this.slogan = slogan
        this.pitch = pitch
        this.leaderId = leaderId
        this.type = type
        this.traitId = traitId
        this.semester = semester
        this.technologies = technologies
    }

    constructor(name: string, posX: number, posY: number, width: number, height: number, semester: string, classroom: string, forumNumber: number, projectId: number) {
        this.name = name
        this.posX = posX
        this.posY = posY
        this.width = width
        this.height = height
        this.semester = semester
        this.classRoom = classroom
        this.forumNumber = forumNumber
        this.projectId = projectId
    }
    

}
export { Project }