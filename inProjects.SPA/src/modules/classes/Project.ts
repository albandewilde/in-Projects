class Project {

    constructor();
    constructor(name: string,logo: string,slogan: string,pitch: string, leaderId: number,semester: string,technologies: string[]);
    
    constructor(name?: string,logo?: string,slogan?: string,pitch?: string,leaderId?: number,semester?: string,technologies?: string[]) {
        this.name = name;
        this.logo = logo;
        this.slogan = slogan;
        this.pitch = pitch;
        this.leaderId = leaderId;
        this.semester = semester;
        this.technologies = technologies;
        
    }

    public projectStudentId!: number
    public name!: string
    public logo!: string
    public slogan!: string
    public pitch!: string
    public leaderId!: number
    public type!: string
    public traitId!: number
    public semester!: string
    public technologies!: string[]

}
export { Project }