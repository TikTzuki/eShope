import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from './login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
username;
password;

authRequest: any = {
  "userName":"user1",
  "password":"password"
}

response: any;
  constructor(
    private service: LoginService,
    private router: Router
  ) {}

  ngOnInit() {
    this.getAccessToken(this.authRequest);
  }

  public getAccessToken(authRequest){
    let resp = this.service.generateToken(authRequest);
    resp.subscribe(data => this.accessApi(data));
  }

  public accessApi(token){
    let resp = this.service.welcome(token);
    resp.subscribe(data => {
      this.response = data;
      console.log(this.response);
    });
  }

  login(){
    
  }
}
