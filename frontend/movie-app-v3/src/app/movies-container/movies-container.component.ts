
import { Component, OnInit } from '@angular/core';
import { MovieCard } from '../interfaces/movieCardInterface';
import { HttpService } from '../http.service';
import { MoviesResponse } from '../interfaces/moviesResponse';

@Component({
  selector: 'app-movies-container',
  templateUrl: './movies-container.component.html',
  styleUrls: ['./movies-container.component.css']
})
export class MoviesContainerComponent implements OnInit{
  cards:MovieCard[] = []
  id:number
  pages:number
  response:MoviesResponse
   defaultPath = "https://localhost:7179/"
  constructor(private http:HttpService)
  {
    this.response = {} as MoviesResponse
    this.id = {} as number 
    this.pages = {} as number 
  }
  ngOnInit(): void {
    this.http.getMovies('api/Movie/'+1).subscribe((res)=>{
      this.pages = res.numberOfPages;
      this.cards = res.movies;
      console.log(this.cards)
    })
  }
}
