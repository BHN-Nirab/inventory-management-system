import { User } from './models/user';

import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class AuthService {

  private apiUrl = 'http://localhost:5002/api/user/';

  constructor(private http: HttpClient) { }

  public createAccount(user:User): Observable<any> {
    return this.http.post<any>(this.apiUrl + 'createaccount', user);
  }

  public login(user:User): Observable<any> {
    return this.http.post<any>(this.apiUrl + 'login', user);
  }

  public isValidSession(sessionData:any): Observable<any> {
    return this.http.post<any>(this.apiUrl + 'isvalidsession', sessionData);
  }

}
