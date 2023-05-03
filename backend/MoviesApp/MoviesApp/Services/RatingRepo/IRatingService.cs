using MoviesApp.DTO;

namespace MoviesApp.Services.RatingRepo
{
    public interface IRatingService
    {
        bool RateMovie(RatingRequest ratingRequest,string token);
        bool IsMovieExist(int id);
    }
}
