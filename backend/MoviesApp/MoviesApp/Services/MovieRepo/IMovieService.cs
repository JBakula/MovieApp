using MoviesApp.DTO;

namespace MoviesApp.Services.MovieRepo
{
    public interface IMovieService
    {
        bool AddMovie(MovieRequest movieRequest);
        string GenerateImagePath(IFormFile image);
        MoviesResponsePaginated GetMovies(int page);
        double NumberOfPages(float numberofItemsPerPage);
        MovieDetails GetMovieDetails(int id);
        bool IsMovieExist(int id);
        ICollection<MoviesSearchDTO> GetMovieBySearchTerm(string searchTerm);
        MoviesResponsePaginated GetMoviesOrderedByYear(int page);
    }
}
