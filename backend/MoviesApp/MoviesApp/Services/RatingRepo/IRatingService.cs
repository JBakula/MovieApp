using MoviesApp.DTO;

namespace MoviesApp.Services.RatingRepo
{
    public interface IRatingService
    {
        bool RateMovie(RatingRequest ratingRequest);
        bool IsMovieExist(int id);
    }
}
