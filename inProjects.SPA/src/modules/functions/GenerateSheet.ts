import pdfMake from "pdfmake/build/pdfmake"
import pdfFonts from "pdfmake/build/vfs_fonts"
pdfMake.vfs = pdfFonts.pdfMake.vfs

export default function GenerateSheet(
    plc: Array<string> = ["E07", "26"],
    project_name: string = "ITI'Humain",
    sem: string = "Semestre 1",
    sector: string = "SR",
    tec: Array < string > =["Rust", "Kotlin", "Python", "Bottle"],
    logo: string = "",
    slogan: string = "Parce qu'on aurait aimé en profiter nous aussi.",
    pitch: string = "Le chat commence par une tête et se termine par une queue qui suis son corps. Elle s'arrête au bout d'un moment.\nLe chat est un animal entouré de poils noir, qui sont parfois gris ou blanc. S'il était rayés, ce serait un petit zèbre.\nIl a deux pattes devant et deux derrière. Il a aussi deux pattes de chaque côté. Les pattes de devant servent a courir, avec les pattes de derrière il freine.\nDe temps en temps le char se dit: \"Tien, je vais faire des petit.\" Quand il les a faits, on dit que c'est une chatte. Les petit s'appellent des chatelots.\nQuand il est dans le jardin, il miaule pour attirer les oiseaux. S'ils ne viennent pas, il grimpe dans les arbres et enlève les œufs dont il nourrit ses petit.",
    team: [string, Array<string>] = ["Julie Agopian", ["Arthur Cheng", "Dan Chiche", "Melvin Delpierre", "Alban De Wilde"]]
) {
    // format data
    const place: string = plc.join("\n")
    const semester: string = "Semestre " + sem + (sector ? " - " + sector : "")

    team[1] = removeNonString(team[1])
    const leader = team[0] + (team[1].length > 0 && !["", " ", undefined, null].includes(team[0]) ? ", " : "")
    const members = team[1].join(", ")

    tec.length > 9 ? tec[9] = "..." : null
    tec = tec.slice(0, 10)
    let missing = 11 - tec.length
    for (let idx = 0; idx < missing; idx += 1) { tec.push("") }
    const technos: string = tec.join("\n")

    logo = "data:image/jpeg;base64," + logo

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
                text: project_name,
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
                image: logo,
                width: 250,
                height: 250,
                style: "logo"
            },

            {
                text: technos,
                style: "techno_list"
            },

            {
                text: slogan,
                style: "slogan"
            },

            {
                text: pitch,
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

const removeNonString = function(array: Array<any>) {
        let new_array: Array<string> = []
        for (let idx = 0; idx < array.length; idx += 1) {
            if (typeof(array[idx]) === typeof("string")) {
                new_array.push(array[idx])
            }
        }
        return new_array
    }

export {GenerateSheet}