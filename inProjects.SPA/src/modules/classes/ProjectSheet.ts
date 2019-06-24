class ProjectSheet {
    public name: string
    public semester: string
    public sector: string
    public logo: string
    public slogan: string
    public pitch: string
    public team: [string, string[]]

    constructor(name: string, semester: string, sector: string, logo: string, slogan: string, pitch: string, team: [string, string[]]) {
        this.name = name;
        this.semester = semester;
        this.sector = sector;
        this.logo = logo;
        this.slogan = slogan;
        this.pitch = pitch;
        this.team = team;
    } 
}

class ProjectPiSheet extends ProjectSheet {
    public technos: string[]

    constructor(name: string, semester: string, sector: string, logo: string, slogan: string, pitch: string, team: [string, string[]], technos: string[]) {
        super(name, semester, sector, logo, slogan, pitch, team)
        this.technos = technos;
    } 
}

class ProjectPfhSheet extends ProjectSheet{
    public background: string

    constructor(name: string, semester: string, sector: string, logo: string, slogan: string, pitch: string, team: [string, string[]], background: string) {
        super(name, semester, sector, logo, slogan, pitch, team)
        this.background = background;
    } 
}
export {ProjectSheet, ProjectPiSheet, ProjectPfhSheet}