using Microsoft.AspNetCore.SignalR;
using MoviesApp.Migrations;
using MoviesApp.Models;
using MoviesApp.Services.MovieRepo;

namespace MoviesApp.Hubs
{
    public class RatingHub:Hub
    {
        private readonly IMovieService _movieService;
        private readonly MoviesDbContext _context;
        public RatingHub(IMovieService movieService, MoviesDbContext context) {
            _movieService = movieService;
            _context = context;

        }


        public async Task UpdateRating(int movieId)
        {

            var ratingResponse = _movieService.CalculateMovieRating(movieId);


            //await Clients.Client(Context.ConnectionId).SendAsync("avgMovieRating", ratingResponse);
            await Clients.Caller.SendAsync("avgMovieRating", new
            {
                avgRating = ratingResponse,
                id = movieId
            });
        }
    }
}
