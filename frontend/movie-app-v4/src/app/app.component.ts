import { Component, OnInit } from '@angular/core';
import { HttpService } from './services/http.service';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from './services/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'movie-app-v4';
  searchValue = "";
  isLoggedIn:boolean;
  constructor(private router:Router,private http:UserService){
    this.isLoggedIn = this.http.isLoggedIn()
  }
  
  logout(){
    this.http.signOut();
    this.isLoggedIn = this.http.isLoggedIn();
  }
  
  handleKeyUp(event:any){
    event.target.value == "" ? this.router.navigate(['']):
    this.router.navigate(['search',event.target.value]);
    
  }
  ngOnInit(): void {
    this.http.statusEmitter.subscribe((value)=>{
      this.isLoggedIn = value;
    })
  }
}
