import { HttpClient } from '@angular/common/http';
import { EventEmitter, Injectable } from '@angular/core';
import { Registration } from '../interfaces/registration';
import { Login } from '../interfaces/login';
import { Router } from '@angular/router';
import {JwtHelperService} from '@auth0/angular-jwt';
import { RefreshToken } from '../interfaces/refreshToken';
@Injectable({
  providedIn: 'root'
})
export class UserService {
  private defaultPath = "https://localhost:7179/";
  userPayload:any;
  constructor(private http:HttpClient,private router:Router) {
    this.userPayload = this.decodedToken
   }

  registerUser(user:Registration){
    return this.http.post<any>(`${this.defaultPath}api/User/register`,user,{observe: 'response'});
  }
  loginUser(user:Login){
    return this.http.post<any>(`${this.defaultPath}api/User/login`,user,{observe: 'response'})
  }
  storeToken(tokenValue:string){
    localStorage.setItem("token",tokenValue);
  }
  storeRefreshToken(token:string){
    localStorage.setItem("refreshToken",token);

  }
  getToken(){
    return localStorage.getItem("token");
  }
  getRefreshToken(){
    return localStorage.getItem("refreshToken");
  }
  isLoggedIn():boolean{
    return !!localStorage.getItem("token");
  }
  signOut(){
    localStorage.removeItem("token");
    localStorage.removeItem("refreshToken");
    this.router.navigate(["/login"]);

  }
  decodedToken(){
    const jwtHelper = new JwtHelperService();
    const token = this.getToken()!;
    return jwtHelper.decodeToken(token); 
  }
  refreshToken(token:RefreshToken){
    return this.http.post<any>(`${this.defaultPath}api/User/refresh-token`,token,{ observe: 'response' });
  }
  
  statusEmitter = new EventEmitter<boolean>();
  

  setStatusEmitter(status:boolean){
    this.statusEmitter.emit(status);
  }
  
}
