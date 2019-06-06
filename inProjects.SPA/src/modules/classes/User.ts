class User {

    constructor();
    constructor(firstName:string, lastName:string);
    constructor(firstName?:string,lastName?:string){
        this.firstName = firstName,
        this.lastName = lastName

    }
    public firstName: string = ""
    public lastName: string = ""
    public email: string = ""
    public password: string = ""
    public groupName: string = ""
    public emailSecondary: string = ""

    /**
     * Set or Reset a User to default values
     */
    public reset() {
        this.firstName = ""
        this.lastName = ""
        this.email = ""
        this.password = ""
    }
}
export { User }