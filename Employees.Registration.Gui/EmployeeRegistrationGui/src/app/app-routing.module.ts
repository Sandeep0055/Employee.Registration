import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import {  EmployeeListComponent} from './employee/employee-list/employee-list.component';
import {   EmployeeAddComponent} from './employee/employee-add/employee-add.component';
import {    LogInComponent} from './employee/log-in/log-in.component';

const routes: Routes = [
  {path:'list',component:EmployeeListComponent},
  {path:'add',component:EmployeeAddComponent},
  {path:'login',component:LogInComponent},
  
  {path:'',redirectTo:'/list',pathMatch:'full'}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
