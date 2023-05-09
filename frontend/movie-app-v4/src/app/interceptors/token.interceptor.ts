import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, catchError, switchMap, throwError } from 'rxjs';
import { UserService } from '../services/user.service';
import { Router } from '@angular/router';
import { RefreshToken } from '../interfaces/refreshToken';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  refreshToken:RefreshToken
  constructor(private userService:UserService,private router:Router) {
    this.refreshToken = {} as RefreshToken
  }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const token = this.userService.getToken();
    if(token){
      request = request.clone({
        setHeaders:{Authorization:`Bearer ${token}`}
      })
    }
    return next.handle(request).pipe(
      catchError((err:any)=>{
        if(err instanceof HttpErrorResponse){
          if(err.status===401){
            return this.handleUnAuthorizedError(request,next);
          }
          
        }
        return throwError(()=>err)
      })
    );
  }

  handleUnAuthorizedError(req:HttpRequest<any>,next:HttpHandler){
     this.refreshToken.refreshToken = this.userService.getRefreshToken()!;
    return this.userService.refreshToken(this.refreshToken).pipe(
      switchMap((data:any)=>{
        this.userService.storeToken(data.body.jwtToken);
        this.userService.storeRefreshToken(data.body.refreshToken);
        console.log(data);
        req = req.clone({
          setHeaders:{Authorization:`Bearer ${data.body.jwtToken}`}
        })
        return next.handle(req);
      }),
      catchError((err)=>{
        return throwError(()=>{
          this.router.navigate(["login"]);
          this.userService.signOut();
          this.userService.setStatusEmitter(false);
        });

      })
    )
    
  }
}
