using MoviesApp.DTO;
using MoviesApp.Models;

namespace MoviesApp.Services.RatingRepo
{
    public class RatingService : IRatingService
    {
        private readonly MoviesDbContext _context;
        public RatingService(MoviesDbContext context)
        {
            _context = context;
        }

        public bool RateMovie(RatingRequest ratingRequest)
        {
            var ratedMovie = _context.Ratings.Where(r => r.MovieId == ratingRequest.MovieId).FirstOrDefault();
            if (ratedMovie !=null)
            {
                _context.Ratings.Remove(ratedMovie);
                _context.SaveChanges();
            }
            var movie = _context.Movies.Where(m=>m.MovieId == ratingRequest.MovieId).FirstOrDefault();
            var rating = new Rating()
            {
                MovieId = ratingRequest.MovieId,
                Value = ratingRequest.Rating,
                Movie = movie
            };
            _context.Ratings.Add(rating);
            _context.SaveChanges();
            return true;

        }
        public bool IsMovieExist(int id)
        {
            return _context.Movies.Where(m => m.MovieId == id).Any();
        }
    }
}
