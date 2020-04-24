import { Injectable, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import * as CryptoJS from 'crypto-js';
@Injectable({
  providedIn: 'root'
})
export class GlobleService {
  constructor(private http: HttpClient) { }
  public encryptData(data): any {
    try {
      var encryptData = CryptoJS.AES.encrypt(JSON.stringify(data), 'currentUser').toString();
      return encryptData;
    } catch (e) {
    }
  }

  public decryptData(data): any {
    try {
      const bytes = CryptoJS.AES.decrypt(data, 'currentUser');
      if (bytes.toString()) {
        return JSON.parse(bytes.toString(CryptoJS.enc.Utf8));
      }
      return data;
    } catch (e) {
    }
  }

  public get getCurrentUser() {
    return this.decryptData(localStorage.getItem("currentUser"));
  }
  emmiter = new EventEmitter();
}
