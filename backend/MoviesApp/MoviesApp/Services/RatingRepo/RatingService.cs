using MoviesApp.DTO;
using MoviesApp.Models;
using System.IdentityModel.Tokens.Jwt;

namespace MoviesApp.Services.RatingRepo
{
    public class RatingService : IRatingService
    {
        private readonly MoviesDbContext _context;
        public RatingService(MoviesDbContext context)
        {
            _context = context;
        }

        public bool RateMovie(RatingRequest ratingRequest, string token)
        {

            
            var handler = new JwtSecurityTokenHandler();
            var decodedToken = handler.ReadJwtToken(token);


            var userId = int.Parse( decodedToken.Claims.First(c => c.Type == "UserId").Value);
            var ratedMovie = _context.Ratings.Where(r => r.MovieId == ratingRequest.MovieId).Where(r=>r.UserId == userId).FirstOrDefault();
            if (ratedMovie !=null)
            {
                _context.Ratings.Remove(ratedMovie);
                _context.SaveChanges();
            }
            var movie = _context.Movies.Where(m=>m.MovieId == ratingRequest.MovieId).FirstOrDefault();
            var user = _context.Users.Where(u=>u.UserId == userId).FirstOrDefault();
            var rating = new Rating()
            {
                User = user,
                UserId = userId,
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
