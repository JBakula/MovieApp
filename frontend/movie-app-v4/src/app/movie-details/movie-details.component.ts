import { Component } from '@angular/core';
import { HttpService } from '../http.service';
import { ActivatedRoute, Params } from '@angular/router';
import { movieDetails } from '../interfaces/movieDetails';

@Component({
  selector: 'app-movie-details',
  templateUrl: './movie-details.component.html',
  styleUrls: ['./movie-details.component.css']
})
export class MovieDetailsComponent {
  movieDetails:any;
  constructor(private http:HttpService, private activatedRoute:ActivatedRoute){
    this.movieDetails = {} as movieDetails;
  }
  getDetails(movieId:number){
    this.http.getMovieDetails(movieId).subscribe((res)=>{
      console.log(res)
      this.movieDetails = res;
    })
  }
  ngOnInit(): void {
    this.activatedRoute.params.subscribe((params:Params)=>{
      this.getDetails(params['movieId']);
      })
    }
  }
