import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {  EmployeeListComponent } from '../employee/employee-list/employee-list.component'; 
import {   EmployeeAddComponent } from '../employee/employee-add/employee-add.component'; 
import {    LogInComponent } from '../employee/log-in/log-in.component';  
import { Routes, RouterModule } from '@angular/router'; 
const routes: Routes = [
    { path: 'list', component: EmployeeListComponent },
    { path: 'add', component: EmployeeAddComponent },
    { path: 'login', component: LogInComponent }, 

  ];
  @NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
  })
export class EmployeeModule { }
