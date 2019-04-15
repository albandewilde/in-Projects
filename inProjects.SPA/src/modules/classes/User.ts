class User {
    private _firstName: string = ""
    private _lastName: string = ""
    private _email: string = ""
    private _password: string = ""
    
    /**
     * Set or Reset a User to default values
     */
    public reset() {
        this.firstName = ""
        this.lastName = ""
        this.email = ""
        this.password = ""
    }

    public get firstName(){
        return this._firstName
    }
    public set firstName(value){
        this._firstName = value
    }

    public get lastName(){
        return this._lastName
    }
    public set lastName(value){
        this._lastName = value
    }

    public get email(){
        return this._email
    }
    public set email(value){
        this._email = value
    }

    public get password(){
        return this._password
    }
    public set password(value){
        this._password = value
    }
}
export { User }