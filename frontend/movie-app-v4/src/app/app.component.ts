import { Component, OnInit } from '@angular/core';
import { HttpService } from './services/http.service';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from './services/user.service';
import { User } from './interfaces/user';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'movie-app-v4';
  searchValue = "";
  isLoggedIn:boolean;
  userDataModel:User
  helper:JwtHelperService
  constructor(private router:Router,private http:UserService){
    this.isLoggedIn = this.http.isLoggedIn()
    this.userDataModel = {} as User
    this.helper = new JwtHelperService()
  }
  
  logout(){
    this.http.signOut();
    this.isLoggedIn = this.http.isLoggedIn();
  }
  readTokenData(){
    
    let decodedToken = this.helper.decodeToken(this.http.getToken()!!);
    this.userDataModel.userId = decodedToken.UserId
    this.userDataModel.email = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress']     
    this.userDataModel.name = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'] 
    this.userDataModel.lastname = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname'] 
    console.log(this.userDataModel);
  }
  handleKeyUp(event:any){
    event.target.value == "" ? this.router.navigate(['']):
    this.router.navigate(['search',event.target.value]);
    
  }
  // handleUserData(event:any){
  //   console.log("value"+event.value);
  // }
  ngOnInit(): void {
    this.http.statusEmitter.subscribe((value)=>{
      this.isLoggedIn = value;
    })
    if(this.isLoggedIn===true){
      this.readTokenData();
    }
  }
}
