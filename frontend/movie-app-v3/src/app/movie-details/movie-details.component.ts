import { Component, Input, OnInit } from '@angular/core';
import { movieDetails } from '../interfaces/movieDetails';
import { HttpService } from '../http.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-movie-details',
  templateUrl: './movie-details.component.html',
  styleUrls: ['./movie-details.component.css']
})
export class MovieDetailsComponent implements OnInit {
  movieDetails:any;
  constructor(private http:HttpService, private activatedRoute:ActivatedRoute){
    this.movieDetails = {} as movieDetails;
  }
  ngOnInit(): void {
    this.http.getMovieDetails("api/Movie/details/"+this.activatedRoute.snapshot.paramMap.get('id')).subscribe((res)=>{
      this.movieDetails = res;
    })
  }
}
