import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { UserService } from '../services/user.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard  {
  constructor(private userService:UserService){

  }
  canActivate():boolean{
    if(this.userService.isLoggedIn()){
      return false;
    }else{
      return true;
    }
  }
  
}
