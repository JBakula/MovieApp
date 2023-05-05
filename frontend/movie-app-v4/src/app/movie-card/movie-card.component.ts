import { Component, Input, OnInit } from '@angular/core';
import { movieDetails } from '../interfaces/movieDetails';
import { HttpService } from '../services/http.service';
import { MovieCard } from '../interfaces/movieCardInterface';
import {faImdb} from '@fortawesome/free-brands-svg-icons'
import { FormControl } from '@angular/forms';
import { Rating } from '../interfaces/rating';
import { Subscription } from 'rxjs';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-movie-card',
  templateUrl: './movie-card.component.html',
  styleUrls: ['./movie-card.component.css']
})
export class MovieCardComponent implements OnInit {
  @Input() defaultPath:string 
  @Input() data:MovieCard;
  movieDetails:movieDetails
  modalOpen:boolean
  imdb = faImdb
  rating:Rating 
  isUserLoggedIn:boolean
  ratingSubscribe:Subscription
  constructor(private http:HttpService, private userService:UserService){
    this.data = {} as MovieCard;
    this.defaultPath = {} as string;
    this.movieDetails = {} as movieDetails;
    this.modalOpen = false;
    this.rating = {} as Rating;
    this.ratingSubscribe = {} as Subscription;
    this.isUserLoggedIn = this.userService.isLoggedIn(); 
  }
  ngOnInit(): void {
    this.userService.statusEmitter.subscribe((val)=>{
      this.isUserLoggedIn = val;
    })
  }
  ratingPopUp(event:any){
    event.preventDefault();
    event.stopPropagation();
    this.modalOpen = true; 
  }
  ratingControl = new FormControl(0);
  getRating(event:any,movieId:number){
    event.preventDefault();
    event.stopPropagation();
    this.rating.MovieId = movieId;
    this.rating.Rating = this.ratingControl.value;
    this.ratingSubscribe=this.http.rateMovie(this.rating).subscribe((res)=>{
      console.log(res);
    });
    this.modalOpen = false;
  }
  closeModal(event:any){
    event.preventDefault();
    event.stopPropagation();
    this.modalOpen = false;
  }
  
  
  
}
