import { Component, OnInit,ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { EmployeeModel } from '../../employee/employeeModel'

import * as consts from '../../globle.constants'
import { HttpGenericRouteSerivce } from 'src/app/globle.service';
import { from } from 'rxjs';
import * as XLSX from 'xlsx';  

  



@Component({
  selector: 'employee-list',
  templateUrl: './employee-list.component.html',
})
export class EmployeeListComponent implements OnInit {
  constructor(private globleService: HttpGenericRouteSerivce, private route: Router) {
  }

  employeesList: EmployeeModel[] = [];
  filteredItems: EmployeeModel[] = [];

  ngOnInit() {
    this.GetEmployees();
  }
  fileName = 'Employees.xlsx';

  assignCopy(){
      this.filteredItems = Object.assign([], this.employeesList);
   }
   filterItem(value){
      if(!value){
          this.assignCopy();
      } // when nothing has typed
      this.filteredItems = Object.assign([], this.employeesList).filter(
         item => item.name.toLowerCase().indexOf(value.toLowerCase()) > -1
      )
   }
  exportexcel(): void {
    /* table id is passed over here */
    let element = document.getElementById('employee-table');
    const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(element);

    /* generate workbook and add the worksheet */
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');
    /* save to file */
    XLSX.writeFile(wb, this.fileName);

  }
  GetEmployees() {
    this.globleService.fetchAll(`${consts.GetEmployees}`).pipe(first()).subscribe(resultList => {
      this.employeesList = resultList;
      this.assignCopy();
    });
  }
}
