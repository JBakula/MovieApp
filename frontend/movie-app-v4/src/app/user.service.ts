import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Registration } from './interfaces/registration';
import { Login } from './interfaces/login';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private defaultPath = "https://localhost:7179/"
  constructor(private http:HttpClient) { }

  registerUser(user:Registration){
    return this.http.post<any>(`${this.defaultPath}api/User/register`,user,{observe: 'response'});
  }
  loginUser(user:Login){
    return this.http.post<any>(`${this.defaultPath}api/User/login`,user,{observe: 'response'})
  }
  storeToken(tokenValue:string){
    localStorage.setItem("token",tokenValue);
  }
  getToken(){
    return localStorage.getItem("token");
  }
  isLoggedIn():boolean{
    return !!localStorage.getItem("token");
  }
}
