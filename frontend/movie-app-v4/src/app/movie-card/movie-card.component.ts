import { Component, Input } from '@angular/core';
import { movieDetails } from '../interfaces/movieDetails';
import { HttpService } from '../http.service';
import { MovieCard } from '../interfaces/movieCardInterface';

@Component({
  selector: 'app-movie-card',
  templateUrl: './movie-card.component.html',
  styleUrls: ['./movie-card.component.css']
})
export class MovieCardComponent {
  @Input() defaultPath:string 
  @Input() data:MovieCard;
  movieDetails:movieDetails
  descriptionToggle:boolean 
  
  constructor(private http:HttpService){
    this.data = {} as MovieCard;
    this.defaultPath = {} as string;
    this.movieDetails = {} as movieDetails;
    this.descriptionToggle = false;
  }
  
}
