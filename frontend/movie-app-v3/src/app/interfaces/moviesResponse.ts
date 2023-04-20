import { MovieCard } from "./movieCardInterface";

export interface MoviesResponse{
    currentPage:number,
    numberOfPages:number,
    movies:MovieCard
}