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
  selectListOptions:string[] = ["Title ascending","Title descending","Year ascending","Year descending","IMDb rating ascending","IMDb rating descending"];
  order:string = "Title ascending";
  modalActive:boolean;
  categories:Category[] = [];
  newAvgRating:number;
  constructor(private http:HttpService, private recommender:RecommendationService,
    private user:UserService,private activatedRoute:ActivatedRoute,private router:Router,private signalr:SignalrService){
    this.moviesResponse = {} as MoviesResponse,
    this.isLoggedIn = {} as boolean,
    this.modalActive = false,
    this.recommendedMovieCard = {} as MovieCard
    this.newAvgRating = {} as number
  } 
  getData(page:number,ordering:string){
    this.load();
    this.http.getMovies(page,ordering).subscribe((res)=>{
      this.cards = res.movies;
      this.cards.forEach(card => {
        this.signalr.UpdateRating(card.movieId);

      });
      let index = 0
      this.signalr.avgRating.subscribe((val)=>{
        if(this.cards[index]!=undefined){
          this.cards[index].rating = val 
          index++;
        }
        
      })
      this.totalPages = Array.from(new Array(res.numberOfPages),(x,i)=>i+1)
      
    });
    
  }
  load() : void {
    this.loader = true;
    setTimeout( () => this.loader = false, 500 );
  }
  // selectedOption:string = "Any";
  handleCategoryChange(event:any){
    this.load();
    let value = event.target.value;
    if(value !== "any"){
      this.page = 1;
      this.currentPath = `/category/${value}`;
      this.router.navigate([this.currentPath]);
    }else{
      this.page = 1;
      this.currentPath = '/home';
      this.router.navigate([this.currentPath]);
    }
  }
  
  
  getDataBySearch(searchedTerm:string,order:string,page:number){
    this.http.getMoviesBySearchedTerm(searchedTerm,order,page).subscribe((res)=>{
      this.cards = res.movies;
      this.cards.forEach(card => {
        this.signalr.UpdateRating(card.movieId);

      });
      let index = 0
      this.signalr.avgRating.subscribe((val)=>{
        if(this.cards[index]!=undefined){
          this.cards[index].rating = val 
          index++;
        }
      })
      this.totalPages = Array.from(new Array(res.numberOfPages),(x,i)=>i+1) 
    })
    
  }
  getDataByCategoryId(categoryId:number, order:string,page:number){
    this.http.getMoviesByCategoryId(categoryId,order,page).subscribe((res)=>{
      this.cards = res.movies;
      this.cards.forEach(card => {
        this.signalr.UpdateRating(card.movieId);

      });
      let index = 0
      this.signalr.avgRating.subscribe((val)=>{
        if(this.cards[index]!=undefined){
          this.cards[index].rating = val 
          index++;
        }
      })
      this.totalPages = Array.from(new Array(res.numberOfPages),(x,i)=>i+1)
    })
  }

  getDataByDirectorId(directoryId:number,order:string,page:number){
    this.http.getMoviesByDirectoryId(directoryId,order,page).subscribe((res)=>{
      this.cards = res.movies;
      this.cards.forEach(card => {
        this.signalr.UpdateRating(card.movieId);

      });
      let index = 0
      this.signalr.avgRating.subscribe((val)=>{
        if(this.cards[index]!=undefined){
          this.cards[index].rating = val 
          index++;
        }
      })
      this.totalPages = Array.from(new Array(res.numberOfPages),(x,i)=>i+1)
    })
  }
  getDataByActorId(actorId:number,order:string,page:number){
    this.http.getMoviesByActorId(actorId,order,page).subscribe((res)=>{
      this.cards = res.movies;
      this.cards.forEach(card => {
        this.signalr.UpdateRating(card.movieId);

      });
      let index = 0
      this.signalr.avgRating.subscribe((val)=>{
        if(this.cards[index]!=undefined){
          this.cards[index].rating = val 
          index++;
        }
      })
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
  updateRating(event:any){
    this.signalr.UpdateRating(event);
    this.signalr.avgRating.subscribe((val)=>{
      console.log(val)
      this.newAvgRating = val;
    })
    
  }
  ngOnInit(): void {
    this.signalr.startConnection();
    this.signalr.newMovieRatingListener();
    this.http.getCategories().subscribe((res)=>{
      this.categories = res
      this.categories.unshift({categoryId:"any",categoryName:"Any"})
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
