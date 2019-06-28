class ProjectSheet {
    public name: string
    public semester: string
    public sector: string
    public logo: string
    public slogan: string
    public pitch: string
    public team: [string, string[]]

    constructor(name: string, semester: string, sector: string, logo: string, slogan: string, pitch: string, team: [string, string[]]) {
        this.name = name
        this.semester = semester
        this.sector = sector
        this.logo = logo
        this.slogan = slogan
        this.pitch = pitch
        this.team = team
    }

    generate_sheet() {
        throw new Error()
    }
}

class ProjectPiSheet extends ProjectSheet {
    public place: string[]
    public technos: string[]

    constructor(place: string[], name: string, semester: string, sector: string, logo: string, slogan: string, pitch: string, team: [string, string[]], technos: string[]) {
        super(name, semester, sector, logo, slogan, pitch, team)
        this.place = place
        this.technos = technos;
    }

    generate_sheet() {
        // format data
        const place: string = this.place.join("\n")
        const semester: string = "Semestre " + this.semester + (this.sector ? " - " + this.sector : "")

        // this.team[1] = removeNonString(this.team[1])
        const leader = this.team[0] + (this.team[1].length > 0 && !["", " ", undefined, null].includes(this.team[0]) ? ", " : "")
        const members = this.team[1].join(", ")

        this.technos.length > 9 ? this.technos[9] = "..." : null
        this.technos = this.technos.slice(0, 10)
        let missing = 11 - this.technos.length
        for (let idx = 0; idx < missing; idx += 1) {this.technos.push("")}
        const technos: string = this.technos.join("\n")

        this.logo = "data:image/png;base64," + this.logo

        // create the pdf
        const sheet = {
            footer: {
                columns: [
                    {
                        text: [
                            {
                                text: leader,
                                style: "leader"
                            },

                            {
                                text: members,
                                style: "members"
                            }
                        ],
                        alignment: "center"
                    }
                ]
            },
            content: [
                {
                    text: place,
                    style: "place"
                },

                {
                    text: this.name,
                    style: "project_name"
                },

                {
                    text: semester,
                    style: "semester"
                },

                {
                    text: "Technologies",
                    style: "techno"
                },

                {
                    image: this.logo,
                    width: 250,
                    height: 250,
                    style: "logo"
                },

                {
                    text: technos,
                    style: "techno_list"
                },

                {
                    text: this.slogan,
                    style: "slogan"
                },

                {
                    text: this.pitch,
                    style: "pitch"
                }
            ],

            styles: {
                place: {
                    alignment: "right",
                    fontSize: 20,
                    color: "blue",
                    margin: [0, -10, -10, 0]
                },

                project_name: {
                    alignment: "center",
                    fontSize: 70
                },

                semester: {
                    fontSize: 18,
                    margin: [0, 30, 20, 0],
                    alignment: "right"
                },

                techno: {
                    alignment: "right",
                    bolt: true,
                    decoration: "underline",
                    fontSize: 18,
                    margin: [0, 20, 20, 0]
                },

                logo: {
                    margin: [60, -50, 0, 0]
                },

                techno_list: {
                    alignment: "right",
                    margin: [0, -190, 20, 0],
                    fontSize: 14
                },

                slogan: {
                    alignment: "center",
                    color: "blue",
                    fontSize: 20,
                    margin: [0, 50, 0, 0]
                },

                pitch: {
                    margin: [0, 50, 0, 0]
                },

                leader: {
                    bold: true
                },

                team: {
                    alignment: "center",
                    italics: true,
                    margin: [0, 30, 0, 0]
                }
            }
        }

        return sheet
    }
}

class ProjectPfhSheet extends ProjectSheet {
    public background: string

    constructor(name: string, semester: string, sector: string, logo: string, slogan: string, pitch: string, team: [string, string[]], background: string) {
        super(name, semester, sector, logo, slogan, pitch, team)
        this.background = background;
    }

    generate_sheet() {
    }
}

const removeNonString = function(array: Array<any>) {
    let new_array: Array<string> = []
    for (let idx = 0; idx < array.length; idx += 1) {
        if (typeof(array[idx]) === typeof("string")) {
            new_array.push(array[idx])
        }
    }
    return new_array
}

export {ProjectSheet, ProjectPiSheet, ProjectPfhSheet}