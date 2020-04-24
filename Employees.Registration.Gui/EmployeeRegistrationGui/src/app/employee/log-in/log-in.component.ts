import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { UserModel } from '../../employee/employeeModel'

import * as consts from '../../globle.constants'
import { HttpGenericRouteSerivce } from 'src/app/globle.service'; 

@Component({
  selector: 'log-in',
  templateUrl: './log-in.component.html',
})
export class LogInComponent implements OnInit {
  constructor(private globleService: HttpGenericRouteSerivce, private route: Router) {
  }

  userModel: UserModel = {};

  ngOnInit() { 
  }
  
  login() {
      if(this.userModel.email=="admin@gmail.com" && this.userModel.password=="123456")
      {
    this.route.navigate([`./list`]);
      }
      else
      {
          alert('Invalid User')
      }
  }
}
