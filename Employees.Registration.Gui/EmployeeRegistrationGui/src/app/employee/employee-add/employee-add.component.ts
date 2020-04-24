import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { EmployeeModel, HobbyModel,Qualification } from '../../employee/employeeModel' 

import * as consts from '../../globle.constants'
import { HttpGenericRouteSerivce } from 'src/app/globle.service';
import { from } from 'rxjs'; 
@Component({
  selector: 'employee-add',
  templateUrl: './employee-add.component.html',
})
export class EmployeeAddComponent implements OnInit {
  constructor(private globleService: HttpGenericRouteSerivce, private route: Router) {
  }  
  employeeModel: EmployeeModel = {};
  hobbyModelList: HobbyModel[] = [];
  qualificationList: Qualification[] = [];

  ngOnInit() {
    this.GetQualifications();
    this.GetHobbies();
  }

  GetQualifications() {
    this.globleService.fetchAll(`${consts.GetQualifications}`).pipe(first()).subscribe(resultList => {
      this.qualificationList = resultList;
    });
  }
  GetHobbies() {
    this.globleService.fetchAll(`${consts.GetHobbies}`).pipe(first()).subscribe(resultList => {
      this.hobbyModelList = resultList;
    });
  }
  submitData() {
    this.globleService.show(); 

    for (var i in this.hobbyModelList) {
      if(this.hobbyModelList[i].status)
      {
         
        this.employeeModel.hobbyies =this.employeeModel.hobbyies+","+ this.hobbyModelList[i].id;
      }
    }
     

    this.globleService.postData(consts.PostEmployee, this.employeeModel).pipe(first()).subscribe(result => {
      this.employeeModel = result;
      alert("Save Successfull")
      this.employeeModel={};
    });
  }


}
