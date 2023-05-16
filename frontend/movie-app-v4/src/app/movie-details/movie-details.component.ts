import { Component } from '@angular/core';
import { HttpService } from '../services/http.service';
import { ActivatedRoute, Params } from '@angular/router';
import { movieDetails } from '../interfaces/movieDetails';
import{Image} from '../interfaces/image';
import { UserService } from '../services/user.service';
import { RatingResponse } from '../interfaces/ratingResponse';

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
  yourRating:number = 0;
  ratedMovies:RatingResponse[] = []
  constructor(private http:HttpService, private userService:UserService, private activatedRoute:ActivatedRoute){
    this.movieDetails = {} as movieDetails;
    
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
      })
    
    
    }
    
  }

