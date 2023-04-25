import { Component } from '@angular/core';
import { HttpService } from './http.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'movie-app-v4';
  searchValue = "";
  constructor(private router:Router){

  }

  handleKeyUp(event:any){
    event.target.value == "" ? this.router.navigate(['']):
    this.router.navigate(['search',event.target.value]);
    
  }
  
}
