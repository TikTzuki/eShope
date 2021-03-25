import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({providedIn: 'root'})
export class LoginService{
  constructor(private http:HttpClient) { }
  
  generateToken(request){
    console.log(request);
    return this.http.post<string>('http://localhost:8080/authenticate', request, {responseType: 'text' as 'json'});
  }

  public welcome(token){
    let tokenStr = 'Bearer' + token;
    const headers = new HttpHeaders().set('Authorization', tokenStr);
    return this.http.get<string>('http://localhost:8080', {headers, responseType: 'text' as 'json'});
  }
}