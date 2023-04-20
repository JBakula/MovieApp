import { Component, Input } from '@angular/core';
import { MovieCard } from '../interfaces/movieCardInterface';
import { HttpService } from '../http.service';
import { movieDetails } from '../interfaces/movieDetails';

@Component({
  selector: 'app-movie-card',
  templateUrl: './movie-card.component.html',
  styleUrls: ['./movie-card.component.css']
})
export class MovieCardComponent {
  @Input() defaultPath:string 
  @Input() data:MovieCard;
  movieDetails:movieDetails
  constructor(private http:HttpService){
    this.data = {} as MovieCard;
    this.defaultPath = {} as string;
    this.movieDetails = {} as movieDetails;
  }
  // onClickHandle(){
  //   this.http.getMovieDetails("api/Movie/details/"+this.data.movieId).subscribe((res)=>{
  //     this.movieDetails = res;
  //   })
  // }
}
