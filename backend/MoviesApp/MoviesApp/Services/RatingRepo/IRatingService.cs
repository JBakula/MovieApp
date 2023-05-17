using MoviesApp.DTO;
using MoviesApp.Models;

namespace MoviesApp.Services.RatingRepo
{
    public interface IRatingService
    {
        RatingResponse RateMovie(RatingRequest ratingRequest,string token);
        bool IsMovieExist(int id);
        RatingResponse UserRating(string token, int movieId);
    }
}
