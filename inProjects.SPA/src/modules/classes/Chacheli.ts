class Chacheli {
    public id!: number
    public x!: number
    public y!: number
    public w!: number
    public h!: number
    public text!: string
    public available!: boolean
    public comp!: string

    constructor(id: number, x: number, y: number, w: number, h: number, 
        text: string, available: boolean, comp: string){
        
        this.id = id
        this.x = x
        this.y =  y
        this.w =  w
        this.h =  h
        this.text = text
        this.available = available
        this.comp = comp
    }
}

export { Chacheli }