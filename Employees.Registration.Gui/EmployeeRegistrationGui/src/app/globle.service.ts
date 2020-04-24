import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpGenericService } from './../http/http-genric.service'; 
import { BehaviorSubject, Observable } from 'rxjs';

import { Ng4LoadingSpinnerService } from 'ng4-loading-spinner';

@Injectable({
  providedIn: 'root'
})
export class HttpGenericRouteSerivce extends HttpGenericService {
  currentLoaderTextSubject: BehaviorSubject<any>;
  public currentLoaderText: Observable<any>;

  constructor(http: HttpClient, private loader: Ng4LoadingSpinnerService,
    ) {
    super(http);
    this.currentLoaderTextSubject = new BehaviorSubject<any>('Please wait...');
    this.currentLoaderText = this.currentLoaderTextSubject.asObservable();
  }
  show(msg?: any) {
    msg ? this.currentLoaderTextSubject.next(msg) : '';
    this.loader.show();
  }
  hide() {
    this.loader.hide();
  }
  
  public generateNumber = async (n) => {
    var add = 1, max = 12 - add;   
    if (n > max) {
      return this.generateNumber(max) + this.generateNumber(n - max);
    }
}}
