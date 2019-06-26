class ProjectSheet {
    public place: string[]
    public name: string
    public semester: string
    public sector: string
    public logo: string
    public slogan: string
    public pitch: string
    public team: [string, string[]]

    constructor(place: string[], name: string, semester: string, sector: string, logo: string, slogan: string, pitch: string, team: [string, string[]]) {
        this.place = place
        this.name = name
        this.semester = semester
        this.sector = sector
        this.logo = logo
        this.slogan = slogan
        this.pitch = pitch
        this.team = team
    } 
}

class ProjectPiSheet extends ProjectSheet {
    public technos: string[]

    constructor(place: string[], name: string, semester: string, sector: string, logo: string, slogan: string, pitch: string, team: [string, string[]], technos: string[]) {
        super(place, name, semester, sector, logo, slogan, pitch, team)
        this.technos = technos;
    } 
}

class ProjectPfhSheet extends ProjectSheet{
    public background: string

    constructor(place: string[], name: string, semester: string, sector: string, logo: string, slogan: string, pitch: string, team: [string, string[]], background: string) {
        super(place, name, semester, sector, logo, slogan, pitch, team)
        this.background = background;
    } 
}
export {ProjectSheet, ProjectPiSheet, ProjectPfhSheet}