import { Component } from '@angular/core';
import { HttpService } from '../services/http.service';
import { ActivatedRoute, Params } from '@angular/router';
import { movieDetails } from '../interfaces/movieDetails';
import{Image} from '../interfaces/image';
import { UserService } from '../services/user.service';
import { RatingResponse } from '../interfaces/ratingResponse';
import { FormControl } from '@angular/forms';
import { Rating } from '../interfaces/rating';

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
  constructor(private http:HttpService, private userService:UserService, private activatedRoute:ActivatedRoute){
    this.movieDetails = {} as movieDetails;
    this.userRating = {} as number;
    this.isUserLoggedIn = this.userService.isLoggedIn();
    this.rating = {} as Rating
  }
  getDetails(movieId:number){
    
    this.http.getMovieDetails(movieId).subscribe((res)=>{
      this.movieDetails = res;
      this.images.push(this.movieDetails.coverImage);
      this.movieDetails.images.forEach((image:Image)=>{
        this.images.push(image.imageName)
      })
     
    })
  }
  getRating(id:number){
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
      this.getDetails(movieId);
      this.userRating = this.rating.Rating!!;

    });
    console.log(this.ratingControl.value);
    this.modalOpen = false;
    
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
    this.activatedRoute.params.subscribe((params:Params)=>{
      this.getDetails(params['movieId']);
      this.getRating(params['movieId']);
      })
    
    }
  }

