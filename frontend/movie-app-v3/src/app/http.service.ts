import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MoviesResponse } from './interfaces/moviesResponse';
import { Observable } from 'rxjs/internal/Observable';
import { movieDetails } from './interfaces/movieDetails';

@Injectable({
  providedIn: 'root'
})
export class HttpService {
  private defaultPath = "https://localhost:7179/"
  constructor(private http:HttpClient) { }

  getMovies(url:string):Observable<any>{
    return this.http.get<any>(`${this.defaultPath}`+url);
  }
  getMovieDetails(url:string):Observable<any>{
    return this.http.get<any>(`${this.defaultPath}`+url);
  }
}
