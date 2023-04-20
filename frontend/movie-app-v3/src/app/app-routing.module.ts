import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MoviesContainerComponent } from './movies-container/movies-container.component';
import { MovieDetailsComponent } from './movie-details/movie-details.component';

const routes: Routes = [
  {
    path:'',component:MoviesContainerComponent
  },
  {
    path:'details/:id',component:MovieDetailsComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
