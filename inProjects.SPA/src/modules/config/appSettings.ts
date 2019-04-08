import merge from 'lodash/merge'

let c = require( "@/../appSettings.json" )
let ce = require( `@/../appSettings.${process.env.NODE_ENV}.json` )
merge(c,ce);
Object.freeze( c )

export const appSettings = c