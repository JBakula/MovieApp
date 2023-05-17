import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { movieDetails } from '../interfaces/movieDetails';
import { HttpService } from '../services/http.service';
import { MovieCard } from '../interfaces/movieCardInterface';
import {faImdb} from '@fortawesome/free-brands-svg-icons'
import { FormControl } from '@angular/forms';
import { Rating } from '../interfaces/rating';
import { Subscription } from 'rxjs';
import { UserService } from '../services/user.service';
import { RatingResponse } from '../interfaces/ratingResponse';

@Component({
  selector: 'app-movie-card',
  templateUrl: './movie-card.component.html',
  styleUrls: ['./movie-card.component.css']
})
export class MovieCardComponent implements OnInit {
  @Input() defaultPath:string 
  @Input() data:MovieCard;
  @Output() refreshParent = new EventEmitter();
  movieDetails:movieDetails
  modalOpen:boolean
  imdb = faImdb
  rating:Rating 
  isUserLoggedIn:boolean
  ratingSubscribe:Subscription
  ratingResponse:RatingResponse
  ratedCheck:boolean = false;
  constructor(private http:HttpService, private userService:UserService){
    this.data = {} as MovieCard;
    this.defaultPath = {} as string;
    this.movieDetails = {} as movieDetails;
    this.modalOpen = false;
    this.rating = {} as Rating;
    this.ratingSubscribe = {} as Subscription;
    this.isUserLoggedIn = this.userService.isLoggedIn(); 
    this.ratingResponse = {} as RatingResponse;
  }
  // getUserRating(){
  //   if(this.isUserLoggedIn){
  //     this.userService.getUserRatings(this.data.movieId).subscribe((res)=>{
  //       if(res.movieId !== 0){
  //         this.ratingResponse.movieId = res.movieId;
  //         this.ratingResponse.ratingValue = res.ratingValue;
  //         this.ratingResponse.userId = res.userId;
  //         this.ratedCheck = true;
  //       }else{
  //         this.ratedCheck = false;
  //       }
  //     })
  //   }
  // }
  ngOnInit(): void {
    this.userService.statusEmitter.subscribe((val)=>{
      this.isUserLoggedIn = val;
    })
    // this.getUserRating();
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
      this.refreshParent.emit();
    });
    
    this.modalOpen = false;
    
  }
  closeModal(event:any){
    event.preventDefault();
    event.stopPropagation();
    this.modalOpen = false;
  }
  
  
  
}
