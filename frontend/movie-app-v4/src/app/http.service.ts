import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { MoviesResponse } from './interfaces/moviesResponse';

@Injectable({
  providedIn: 'root'
})
export class HttpService {
  private defaultPath = "https://localhost:7179/"
  constructor(private http:HttpClient) { }

  getMovies(page:number,ordering:string):Observable<any>{
    let params = new HttpParams().set('page',page).set('ordering',ordering);
    return this.http.get<any>(`${this.defaultPath}`+'api/Movie',{params:params});
  }
  getMoviesBySearchedTerm(search:string,page:number):Observable<any>{
    let params = new HttpParams().set("term",search).set("page",page);
    return this.http.get<any>(`${this.defaultPath}`+'api/Movie/search',{params:params});
  }
  getMoviesByCategoryId(categoryId:number, page:number):Observable<any>{
    let params = new HttpParams().set("page",page);
    return this.http.get<any>(`${this.defaultPath}api/Category/${categoryId}`,{params:params});
  }
  getMovieDetails(movieId:number):Observable<any>{
    return this.http.get<any>(`${this.defaultPath}api/Movie/details/${movieId}`);
  }
}
