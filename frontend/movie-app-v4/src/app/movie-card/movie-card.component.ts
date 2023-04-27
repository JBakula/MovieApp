import { Component, Input } from '@angular/core';
import { movieDetails } from '../interfaces/movieDetails';
import { HttpService } from '../http.service';
import { MovieCard } from '../interfaces/movieCardInterface';
import {faImdb} from '@fortawesome/free-brands-svg-icons'
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-movie-card',
  templateUrl: './movie-card.component.html',
  styleUrls: ['./movie-card.component.css']
})
export class MovieCardComponent {
  @Input() defaultPath:string 
  @Input() data:MovieCard;
  movieDetails:movieDetails
  modalClosed:boolean
  // descriptionToggle:boolean 
  imdb = faImdb
  
  constructor(private http:HttpService){
    this.data = {} as MovieCard;
    this.defaultPath = {} as string;
    this.movieDetails = {} as movieDetails;
    this.modalClosed = false;
  }
  ratingPopUp(event:any){
    event.preventDefault();
    event.stopPropagation();
    this.modalClosed = true;
    console.log(this.modalClosed);
  }
  ratingControl = new FormControl(0);
  getRating(event:any){
    event.preventDefault();
    event.stopPropagation();
    console.log(this.ratingControl.value);
  }
  closeModal(event:any){
    event.preventDefault();
    event.stopPropagation();
    this.modalClosed = false;
  }
  
}
