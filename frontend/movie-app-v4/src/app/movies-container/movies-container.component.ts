import { Component, Input, OnInit } from '@angular/core';
import { HttpService } from '../services/http.service';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { MoviesResponse } from '../interfaces/moviesResponse';
import { MovieCard } from '../interfaces/movieCardInterface';
import { RecommendationService } from '../services/recommendation.service';
import { UserService } from '../services/user.service';
import { Category } from '../interfaces/category';
import { SignalrService } from '../services/signalr.service';

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
  isLoggedIn:boolean;
  recommendedMovieCard:MovieCard;
  loader:boolean = false;
  selectListOptions:string[] = ["Title ascending","Title descending","Year ascending","Year descending","IMDb rating ascending","IMDb rating descending","Users rating ascending",
                                "Users rating descending"];
  order:string = "Title ascending";
  modalActive:boolean;
  categories:Category[] = [];
  constructor(private http:HttpService, private recommender:RecommendationService,
    private user:UserService,private activatedRoute:ActivatedRoute,private router:Router,private signalr:SignalrService){
    this.moviesResponse = {} as MoviesResponse,
    this.isLoggedIn = {} as boolean,
    this.modalActive = false,
    this.recommendedMovieCard = {} as MovieCard
    
  } 
  getData(page:number,ordering:string){
    this.load();
    this.http.getMovies(page,ordering).subscribe((res)=>{
      this.cards = res.movies;
      this.moviesResponse = res;
      
      this.totalPages = Array.from(new Array(res.numberOfPages),(x,i)=>i+1)
      
    });
    
  }
  load() : void {
    this.loader = true;
    setTimeout( () => this.loader = false, 500 );
  }
  handleCategoryChange(event:any){
    this.load();
    let value = event.target.value;
    if(value === "any"){
      this.page = 1;
      this.router.navigate(['/']);
      this.currentPath = '/';
    }else{
      this.page = 1;
      this.router.navigate([`/category/${value}`]);
      this.currentPath = `/category/${value}`;
    }
  }
  
  refreshParent(){
    this.activatedRoute.params.subscribe((params:Params)=>{
      if(params['movie-search']){
        this.getDataBySearch(params['movie-search'],this.order,this.page);
      }else if(params['categoryId']){
        this.getDataByCategoryId(params['categoryId'],this.order,this.page);
      }else if(params['directorId']){
        this.getDataByDirectorId(params['directorId'],this.order,this.page)
      }else if(params['actorId']){
        this.getDataByActorId(params['actorId'],this.order,this.page)
      }else{
        this.getData(this.page,this.order);
      }
    })
  }
  getDataBySearch(searchedTerm:string,order:string,page:number){
    this.http.getMoviesBySearchedTerm(searchedTerm,order,page).subscribe((res)=>{
      this.moviesResponse = res;
      this.cards = res.movies;
      this.totalPages = Array.from(new Array(res.numberOfPages),(x,i)=>i+1)
      
    })
    
  }
  getDataByCategoryId(categoryId:number, order:string,page:number){
    this.http.getMoviesByCategoryId(categoryId,order,page).subscribe((res)=>{
      
      this.moviesResponse = res;
      this.cards = res.movies
      this.totalPages = Array.from(new Array(res.numberOfPages),(x,i)=>i+1)
    })
  }

  getDataByDirectorId(directoryId:number,order:string,page:number){
    this.http.getMoviesByDirectoryId(directoryId,order,page).subscribe((res)=>{
      console.log(res)
      this.moviesResponse = res;
      this.cards = res.movies
      this.totalPages = Array.from(new Array(res.numberOfPages),(x,i)=>i+1)
    })
  }
  getDataByActorId(actorId:number,order:string,page:number){
    this.http.getMoviesByActorId(actorId,order,page).subscribe((res)=>{
      this.moviesResponse = res;
      this.cards = res.movies;
      this.totalPages = Array.from(new Array(res.numberOfPages),(x,i)=>i+1)
       
    })
  }
  handlePageChange(event:number){
    this.page = event
    this.activatedRoute.params.subscribe((params:Params)=>{
      if(params['movie-search']){
        this.getDataBySearch(params['movie-search'],this.order,this.page);
      }else if(params['categoryId']){
        this.getDataByCategoryId(params['categoryId'],this.order,this.page);
      }else if(params['directorId']){
        this.getDataByDirectorId(params['directorId'],this.order,this.page)
      }else if(params['actorId']){
        this.getDataByActorId(params['actorId'],this.order,this.page)
      }else{
        this.getData(this.page,this.order);
      }
    })
  }
  handleChangeSelectOrder(event:any){
    this.order = event.target.value;
    this.activatedRoute.params.subscribe((params:Params)=>{
      if(params['movie-search']){
        this.getDataBySearch(params['movie-search'],this.order,this.page);
      }else if(params['categoryId']){
        this.getDataByCategoryId(params['categoryId'],this.order,this.page);
      }else if(params['directorId']){
        this.getDataByDirectorId(params['directorId'],this.order,this.page)
      }else if(params['actorId']){
        this.getDataByActorId(params['actorId'],this.order,this.page)
      }else{
        this.getData(this.page,this.order);
      }
    })
    
  }
  
  closeModal(){
    this.modalActive = false;
  }
  refreshRecommendedData(){
    this.recommender.pickAMoiveForMe().subscribe((res)=>{
      this.recommendedMovieCard = res;

    })
  }
  movieRecommendation(){
    this.modalActive = true
    this.recommender.pickAMoiveForMe().subscribe((res)=>{
      this.recommendedMovieCard = res;

    })
  }
  
  ngOnInit(): void {
    
    this.http.getCategories().subscribe((res)=>{
      this.categories = res
    })
    this.currentPath = this.router.url;
    this.isLoggedIn = this.user.isLoggedIn();
    this.activatedRoute.params.subscribe((params:Params)=>{
      if(params['movie-search']){
        this.getDataBySearch(params['movie-search'],this.order,this.page);
      }else if(params['categoryId']){
        this.getDataByCategoryId(params['categoryId'],this.order,this.page);
      }else if(params['directorId']){
        this.getDataByDirectorId(params['directorId'],this.order,this.page)
      }else if(params['actorId']){
        this.getDataByActorId(params['actorId'],this.order,this.page)
      }else{
        this.getData(this.page,this.order);
      }
    })
  }
}
