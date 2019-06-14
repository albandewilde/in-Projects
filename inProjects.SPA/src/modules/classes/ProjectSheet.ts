class ProjectSheet {
    public name: string
    public semester: string
    public sector: string
    public logo: string
    public slogan: string
    public pitch: string
    public team: [string, string[]]
    public technos: string[]

    constructor(name: string, semester: string, sector: string, logo: string, slogan: string, pitch: string, team: [string, string[]], technos: string[]) {
        this.name = name;
        this.semester = semester;
        this.sector = sector;
        this.logo = logo;
        this.slogan = slogan;
        this.pitch = pitch;
        this.team = team;
        this.technos = technos;
    } 
}

export {ProjectSheet}