import { Component, OnInit } from '@angular/core';
import { HttpService } from '../http.service';
import { ActivatedRoute, Params } from '@angular/router';
import { MoviesResponse } from '../interfaces/moviesResponse';
import { MovieCard } from '../interfaces/movieCardInterface';

@Component({
  selector: 'app-movies-container',
  templateUrl: './movies-container.component.html',
  styleUrls: ['./movies-container.component.css']
})
export class MoviesContainerComponent implements OnInit{
  defaultPath:string =  "https://localhost:7179/"
  moviesResponse:MoviesResponse
  cards:MovieCard[] = []
  page:number = 1;
  
  totalPages:number[] = [];
  constructor(private http:HttpService, private activatedRoute:ActivatedRoute){
    this.moviesResponse = {} as MoviesResponse
  }
  getData(page:number,ordering:string){
    this.http.getMovies(page,ordering).subscribe((res)=>{
      this.cards = res.movies;
      this.moviesResponse = res;
      this.cards = res.movies;
      this.totalPages = Array.from(new Array(res.numberOfPages),(x,i)=>i+1)
      console.log(this.totalPages)
    });
  }
  getDataBySearch(searchedTerm:string,page:number){
    this.http.getMoviesBySearchedTerm(searchedTerm,page).subscribe((res)=>{
      this.moviesResponse = res;
      this.cards = res.movies;
      this.totalPages = Array.from(new Array(res.numberOfPages),(x,i)=>i+1)
      
    })
    
  }
  getDataByCategoryId(categoryId:number, page:number){
    this.http.getMoviesByCategoryId(categoryId,page).subscribe((res)=>{
      console.log(res)
      this.moviesResponse = res;
      this.cards = res.movies
      this.totalPages = Array.from(new Array(res.numberOfPages),(x,i)=>i+1)
    })
  }
  handlePageChange(p:number){
    this.page = p;
    console.log(this.page);
    
    this.getData(this.page,"Name");
  }
  handlePrevious(){
   this.page == 1 ? this.page = 1 : this.page = this.page - 1; 
   console.log(this.page);
   this.getData(this.page,"Name");
  }
  handleNext(){
   this.page == this.totalPages.length ? this.page = this.totalPages.length : this.page = this.page + 1; 
   console.log(this.page);
   this.getData(this.page,"Name");

  }
  ngOnInit(): void {
    this.activatedRoute.params.subscribe((params:Params)=>{
      if(params['movie-search']){
        this.getDataBySearch(params['movie-search'],this.page);
      }else if(params['categoryId']){
        this.getDataByCategoryId(params['categoryId'],this.page);
      }else{
        this.getData(1,"Name");
      }
    })
  }
}
