import { AuthService } from './../auth.service';
import { User } from '../models/user';

import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthComponent implements OnInit {

  isValidSession: boolean = false;
  isLoginTab: boolean = true;
  authError: string = '';
  user: User = new User();

  constructor(private authService: AuthService) { }

  ngOnInit() {
    this.getSession();
  }

  toggleLoginTab(value:boolean){
    this.authError = '';
    this.isLoginTab = value;
  }

  createAccount(){
    this.authService.createAccount(this.user).subscribe(response=>{
      this.user = new User();
      this.authError = response.ErrorMessage;
      
      if(response.StatusCode==200)
        this.isLoginTab = true;
    });
  }

  login(){
    this.authService.login(this.user).subscribe(response=>{
      this.user = new User();
      this.authError = response.ErrorMessage;

      if(response.StatusCode==200)
      {
        localStorage.setItem('token', response.token);
        localStorage.setItem('Id', response.Id);
        localStorage.setItem('Name', response.Name);
        this.isValidSession = true;
      }
    });
  }

  getSession(){
    
    var Id = Number.parseInt(localStorage.getItem('Id'));
    var Token = localStorage.getItem('token');

    var sessionData = {
      Id : Id,
      token : Token
    }

    this.authService.isValidSession(sessionData).subscribe(response=>{
      if(response==200)
        this.isValidSession = true;
      else
        this.isValidSession = false;

    });
  }

  logout(){
    this.isValidSession = false;
    this.authError = '';
    this.user = new User();
    localStorage.removeItem('Id');
    localStorage.removeItem('token');
    localStorage.removeItem('Name');
  }

}
