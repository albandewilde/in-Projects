import pdfMake from "pdfmake/build/pdfmake"
import pdfFonts from "pdfmake/build/vfs_fonts"
pdfMake.vfs = pdfFonts.pdfMake.vfs
import {ProjectSheet, ProjectPiSheet, ProjectPfhSheet} from "../../modules/classes/ProjectSheet"

export function GeneratePiSheet(project: ProjectPiSheet) {
    // format data
    const place: string = project.place.join("\n")
    const semester: string = "Semestre " + project.semester + (project.sector ? " - " + project.sector : "")

    project.team[1] = removeNonString(project.team[1])
    const leader = project.team[0] + (project.team[1].length > 0 && !["", " ", undefined, null].includes(project.team[0]) ? ", " : "")
    const members = project.team[1].join(", ")

    project.technos.length > 9 ? project.technos[9] = "..." : null
    project.technos = project.technos.slice(0, 10)
    let missing = 11 - project.technos.length
    for (let idx = 0; idx < missing; idx += 1) {project.technos.push("")}
    const technos: string = project.technos.join("\n")

    project.logo = "data:image/jpeg;base64," + project.logo

    // create the pdf
    const sheet = {
        footer: {
            columns: [
                { text : [
                    {
                        text: leader,
                        style: "leader"
                    },

                    {
                        text: members,
                        style: "members"
                    }
                ],
                alignment:"center"
                }
            ]
          },
        content: [
            {
                text: place,
                style: "place"
            },

            {
                text: project.name,
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
                image: project.logo,
                width: 250,
                height: 250,
                style: "logo"
            },

            {
                text: technos,
                style: "techno_list"
            },

            {
                text: project.slogan,
                style: "slogan"
            },

            {
                text: project.pitch,
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

export function GeneratePfhSheet(project: ProjectPfhSheet){}

export function GenerateSheet(project: ProjectSheet){}

const removeNonString = function(array: Array<any>) {
    let new_array: Array<string> = []
    for (let idx = 0; idx < array.length; idx += 1) {
        if (typeof(array[idx]) === typeof("string")) {
            new_array.push(array[idx])
        }
    }
    return new_array
}