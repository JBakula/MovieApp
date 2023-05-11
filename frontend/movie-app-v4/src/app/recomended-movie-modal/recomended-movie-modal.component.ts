import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-recomended-movie-modal',
  templateUrl: './recomended-movie-modal.component.html',
  styleUrls: ['./recomended-movie-modal.component.css']
})
export class RecomendedMovieModalComponent {
  @Input() isOpen:boolean;
  constructor(){
    this.isOpen = {} as boolean
  }
}
