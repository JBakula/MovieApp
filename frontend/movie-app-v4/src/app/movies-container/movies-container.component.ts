import { Component, OnInit } from '@angular/core';
import { HttpService } from '../http.service';
import { ActivatedRoute, Params, Router } from '@angular/router';
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
  page:number = 1
  currentPath:string = "";
  totalPages:number[] = [];
  constructor(private http:HttpService, private activatedRoute:ActivatedRoute,private router:Router){
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

  getDataByDirectorId(directoryId:number,page:number){
    this.http.getMoviesByDirectoryId(directoryId,page).subscribe((res)=>{
      console.log(res)
      this.moviesResponse = res;
      this.cards = res.movies
      this.totalPages = Array.from(new Array(res.numberOfPages),(x,i)=>i+1)
    })
  }
  getDataByActorId(actorId:number,page:number){
    this.http.getMoviesByActorId(actorId,page).subscribe((res)=>{
      console.log(res);
      this.moviesResponse = res;
      this.cards = res.movies;
      this.totalPages = Array.from(new Array(res.numberOfPages),(x,i)=>i+1)
       
    })
  }
  handlePageChange(event:number){
    this.page = event
    this.activatedRoute.params.subscribe((params:Params)=>{
      if(params['movie-search']){
        this.getDataBySearch(params['movie-search'],this.page);
      }else if(params['categoryId']){
        this.getDataByCategoryId(params['categoryId'],this.page);
      }else if(params['directorId']){
        this.getDataByDirectorId(params['directorId'],this.page)
      }else if(params['actorId']){
        this.getDataByActorId(params['actorId'],this.page)
      }else{
        this.getData(this.page,"Name");
      }
    })
  }
  
  ngOnInit(): void {
    this.currentPath = this.router.url;
    this.activatedRoute.params.subscribe((params:Params)=>{
      if(params['movie-search']){
        this.getDataBySearch(params['movie-search'],this.page);
      }else if(params['categoryId']){
        this.getDataByCategoryId(params['categoryId'],this.page);
      }else if(params['directorId']){
        this.getDataByDirectorId(params['directorId'],this.page)
      }else if(params['actorId']){
        this.getDataByActorId(params['actorId'],this.page)
      }else{
        this.getData(this.page,"Name");
      }
    })
  }
}
