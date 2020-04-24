import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import {  EmployeeListComponent} from './employee/employee-list/employee-list.component';
import {   EmployeeModule} from './employee/employee.module';
import {   EmployeeAddComponent} from './employee/employee-add/employee-add.component';
import {      LogInComponent} from './employee/log-in/log-in.component';
import { FormsModule } from '@angular/forms';
import { Ng4LoadingSpinnerModule } from 'ng4-loading-spinner';
import { HttpClientModule, HttpClient } from '@angular/common/http'; 
@NgModule({
   declarations: [
      AppComponent,
      EmployeeListComponent,
      EmployeeAddComponent,
      LogInComponent  
   ],
   imports: [
      FormsModule,
      HttpClientModule,
      BrowserModule,
      EmployeeModule,
      Ng4LoadingSpinnerModule.forRoot(),
   ],
   providers: [],
   bootstrap: [AppComponent]
})

export class AppModule { }