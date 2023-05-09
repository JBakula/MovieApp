import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MoviesContainerComponent } from './movies-container/movies-container.component';
import { MovieDetailsComponent } from './movie-details/movie-details.component';
import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';
import { AuthGuard } from './guards/auth.guard';

const routes: Routes = [
  {
    path:'',
    component: MoviesContainerComponent
  },
  {
    path:'search/:movie-search',
    component:MoviesContainerComponent
  },
  {
    path:'category/:categoryId',component:MoviesContainerComponent
  },
  {
    path:'details/:movieId', component:MovieDetailsComponent
  },
  {
    path:'director/:directorId',component:MoviesContainerComponent
  },
  {
    path:'actor/:actorId',component:MoviesContainerComponent
  },
  {
    path:'login',component:LoginComponent,canActivate:[AuthGuard]
  },
  {
    path:'signup',component:SignupComponent,canActivate:[AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
