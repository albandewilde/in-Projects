class Project {
    public name!: string
    public logo!: string
    public posX!: number
    public posY!: number
    public width!: number
    public height!: number
    public semester!: string
    public classRoom!: string

    constructor(name: string, posX: number, posY: number, width: number, height: number, semester: string, classroom: string) {
        this.name = name
        this.posX = posX
        this.posY = posY
        this.width = width
        this.height = height
        this.semester = semester
        this.classRoom = classroom
    }
}
export { Project }