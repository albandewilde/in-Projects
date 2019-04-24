class User {
    public firstName: string = ""
    public lastName: string = ""
    public email: string = ""
    public password: string = ""

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