import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { Product } from './models/product';

@Injectable()
export class ProductService {

  private apiUrl = 'http://localhost:5002/api/product/';
  private logApiUrl = 'http://localhost:5002/api/log/';

  constructor(private http: HttpClient) { }

  public getAll(): Observable<any> {
    return this.http.post<any>(this.apiUrl + 'getAll', '');
  }

  public add(product:any): Observable<any> {
    return this.http.post<any>(this.apiUrl + 'add', product);
  }

  public delete(productId:number): Observable<any> {

    var data = {
      Id: productId,
      token: localStorage.getItem('token'),
      UserId: Number.parseInt(localStorage.getItem('Id')),
    };

    return this.http.post<any>(this.apiUrl + 'delete', data);
  }

  public update(product:Product): Observable<any> {

    var data = {
      Product: product,
      token: localStorage.getItem('token'),
      UserId: Number.parseInt(localStorage.getItem('Id')),
    };

    return this.http.post<any>(this.apiUrl + 'update', data);
  }

  public purchase(productId:Number, unit:Number): Observable<any> {

    var data = {
      Id: productId,
      Unit: unit,
      token: localStorage.getItem('token'),
      UserId: Number.parseInt(localStorage.getItem('Id')),
    };

    return this.http.post<any>(this.apiUrl + 'purchase', data);
  }

  public sale(productId:Number, unit:Number): Observable<any> {

    var data = {
      Id: productId,
      Unit: unit,
      token: localStorage.getItem('token'),
      UserId: Number.parseInt(localStorage.getItem('Id')),
    };

    return this.http.post<any>(this.apiUrl + 'sale', data);
  }

  public getReport(Month:Date, isDaily: boolean): Observable<any> {
    console.log(Month);
    console.log(isDaily);

    var monthlyData = {
      Month: Month,
      token: localStorage.getItem('token'),
      UserId: Number.parseInt(localStorage.getItem('Id')),
    };

    var dailyData = {
      Day: Month,
      token: localStorage.getItem('token'),
      UserId: Number.parseInt(localStorage.getItem('Id')),
    };

    var reportUrl = isDaily? 'dailyreports' : 'monthlyreports';

    return this.http.post<any>(this.logApiUrl + reportUrl, isDaily? dailyData : monthlyData);
  }

}
