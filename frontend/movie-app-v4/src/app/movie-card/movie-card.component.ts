import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { movieDetails } from '../interfaces/movieDetails';
import { HttpService } from '../services/http.service';
import { MovieCard } from '../interfaces/movieCardInterface';
import {faImdb} from '@fortawesome/free-brands-svg-icons'
import { FormControl } from '@angular/forms';
import { Rating } from '../interfaces/rating';
import { Subscription } from 'rxjs';
import { UserService } from '../services/user.service';
import { RatingResponse } from '../interfaces/ratingResponse';
import { SignalrService } from '../services/signalr.service';
import { calculateRating } from '../interfaces/calculateRating';
import { HubConnection } from '@microsoft/signalr';
import { User } from '../interfaces/user';

@Component({
  selector: 'app-movie-card',
  templateUrl: './movie-card.component.html',
  styleUrls: ['./movie-card.component.css']
})
export class MovieCardComponent implements OnInit {
  @Input() defaultPath:string 
  @Input() data:MovieCard;
  @Output() movieId = new EventEmitter();
  @Input() newAvgRating:number;
  movieDetails:movieDetails
  modalOpen:boolean
  imdb = faImdb
  rating:Rating 
  isUserLoggedIn:boolean
  ratingSubscribe:Subscription
  ratingResponse:RatingResponse
  ratedCheck:boolean = false;
  // userRating:calculateRating;
  avgRating:number;
  ratedMovieId:number;
  loggedUser:User;
  constructor(private http:HttpService, private userService:UserService,private signalr:SignalrService){
    this.data = {} as MovieCard;
    this.defaultPath = {} as string;
    this.movieDetails = {} as movieDetails;
    this.modalOpen = false;
    this.rating = {} as Rating;
    this.ratingSubscribe = {} as Subscription;
    this.isUserLoggedIn = this.userService.isLoggedIn(); 
    this.ratingResponse = {} as RatingResponse;
    // this.userRating = {} as calculateRating
    this.avgRating = {} as number;
    this.ratedMovieId = {} as number;
    this.loggedUser = {} as User;
    // this.updateRating(this);
    this.newAvgRating = {} as number;
  }
  // updateRating(){
  //   console.log(this.newAvgRating);
  //   this.data.rating = this.newAvgRating
  //   this.getUserRating();

  // }
  getUserRating(){
    if(this.isUserLoggedIn){
      this.userService.getUserRatings(this.data.movieId).subscribe((res)=>{
        if(res.movieId !== 0){
          this.ratingResponse.movieId = res.movieId;
          this.ratingResponse.ratingValue = res.ratingValue;
          this.ratingResponse.userId = res.userId;
          this.ratedCheck = true;
        }else{
          this.ratedCheck = false;
        }
      })
    }
  }
  
  ngOnInit(): void {
    this.getUserRating();
    
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
      // console.log(res);
      // this.refreshParent.emit();
      this.signalr.UpdateRating(movieId);
      this.signalr.avgRating.subscribe((val)=>{
      this.data.rating = val;
      this.getUserRating();})
    });

    this.modalOpen = false; 
  }

  closeModal(event:any){
    event.preventDefault();
    event.stopPropagation();
    this.modalOpen = false;
  }
  
}
