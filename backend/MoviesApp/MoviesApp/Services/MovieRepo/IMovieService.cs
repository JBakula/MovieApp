using MoviesApp.DTO;

namespace MoviesApp.Services.MovieRepo
{
    public interface IMovieService
    {
        bool AddMovie(MovieRequest movieRequest);
        string GenerateImagePath(IFormFile image);
        MoviesResponsePaginated GetMovies(int page,string ordering);
        double NumberOfPages(float numberofItemsPerPage);
        MovieDetails GetMovieDetails(int id);
        bool IsMovieExist(int id);
        public int NumberOfRatings(int id);
        public float CalculateMovieRating(int id);
        MoviesResponsePaginated GetMovieBySearchTerm(string searchTerm,string ordering,int page);
        //MoviesResponsePaginated GetMoviesOrderedByYear(int page);
    }
}
