import { stripSummaryForJitNameSuffix } from '@angular/compiler/src/aot/util'

export class EmployeeModel {
    id?: number 
    name?: string 
    dateOfBirth?: Date
    qualificationId?: string
    experience?: number
    joiningDate?: Date
    salary?: number
    hobbyies?:string 
    qualification?: string 
    designation?: string 
    constructor() { 
        this.hobbyies ="";
    }
}

export class HobbyModel {
    id?: number 
    name?: string   
    status?:boolean
}
export class Qualification {
    id?: number 
    name?: string  
}
export class UserModel {
    email?: string 
    password?: string  
}