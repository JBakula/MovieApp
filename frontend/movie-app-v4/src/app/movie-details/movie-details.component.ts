import { Component } from '@angular/core';
import { HttpService } from '../services/http.service';
import { ActivatedRoute, Params } from '@angular/router';
import { movieDetails } from '../interfaces/movieDetails';
import{Image} from '../interfaces/image';
import { UserService } from '../services/user.service';
import { RatingResponse } from '../interfaces/ratingResponse';
import { FormControl } from '@angular/forms';
import { Rating } from '../interfaces/rating';
import { SignalrService } from '../services/signalr.service';

@Component({
  selector: 'app-movie-details',
  templateUrl: './movie-details.component.html',
  styleUrls: ['./movie-details.component.css']
})
export class MovieDetailsComponent {
  movieDetails:any;
  images:string[] = []
  defaultPath:string =  "https://localhost:7179/";
  currentIndex:number = 0;
  modalOpen = false;
  ratedMovies:RatingResponse[] = []
  userRating:number;
  isUserLoggedIn:boolean;
  rated:boolean = false;
  rating:Rating
  loader:boolean = false;
  avgRating:number;
  constructor(private http:HttpService, private userService:UserService, private activatedRoute:ActivatedRoute,private signalr:SignalrService){
    this.movieDetails = {} as movieDetails;
    this.userRating = {} as number;
    this.isUserLoggedIn = this.userService.isLoggedIn();
    this.rating = {} as Rating;
    this.avgRating = {} as number;
    this.signalr.startConnection();

  }
  load() : void {
    this.loader = true;
    setTimeout( () => this.loader = false, 500 );
  }
  getDetails(movieId:number){
    this.load();
    this.http.getMovieDetails(movieId).subscribe((res)=>{
      this.movieDetails = res;
      this.images.push(this.movieDetails.coverImage);
      this.movieDetails.images.forEach((image:Image)=>{
        this.images.push(image.imageName)
      })
    })

    // this.signalr.UpdateRating(movieId);
    // this.signalr.newMovieRatingListener();
    this.signalr.avgRating.subscribe((res)=>{
      this.avgRating = res
      console.log(this.avgRating);
    })
    
  }
  getRating(id:number){
    this.signalr.UpdateRating(id);

    if(this.isUserLoggedIn === true){
      
      this.userService.getUserRatings(id).subscribe((res)=>{
        if(res.movieId !== 0){
          this.userRating = res.ratingValue;
          this.rated = true;
        }else{
          this.rated = false;
        }
      })
    }
  }
  ratingPopUp(event:any){
    event.preventDefault();
    event.stopPropagation();
    this.modalOpen = true; 
  }
  closeModal(event:any){
    event.preventDefault();
    event.stopPropagation();
    this.modalOpen = false;
  }
  ratingControl = new FormControl(0);
  getRatingFromForm(event:any,movieId:number){
    event.preventDefault();
    event.stopPropagation();
    this.rating.MovieId = movieId;
    this.rating.Rating = this.ratingControl.value;
    this.http.rateMovie(this.rating).subscribe((res)=>{
      console.log(res);
      
      this.userRating = this.rating.Rating!!;

    });
    this.modalOpen = false;
    setTimeout(() => {
    this.signalr.UpdateRating(movieId);
      
    }, 30);
  }
  nextImage(){
    if(this.currentIndex === (this.images.length - 1) ){
      this.currentIndex = 0;
    }else{
      this.currentIndex += 1;
    }
  }
  previousImage(){
    if(this.currentIndex === 0){
      this.currentIndex = (this.images.length - 1);
    }else{
      this.currentIndex -= 1;
    }
  }
  ngOnInit(): void {
    this.signalr.newMovieRatingListener();
    this.signalr.avgRating.subscribe((res)=>{
      this.avgRating = res
      console.log(this.avgRating);
    })

    this.activatedRoute.params.subscribe((params:Params)=>{
      this.getDetails(params['movieId']);
      this.getRating(params['movieId']);
      })
      
    
    }
  }

