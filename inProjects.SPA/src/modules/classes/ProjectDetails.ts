import { User } from "./User"
import { Project } from "./Project"

class ProjectDetails {
    project!: Project
    students! : User[]

    test(){
        console.log(this.project)
        console.log(this.students)
    }
}
export { ProjectDetails }