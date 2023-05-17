using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApp.DTO;

using MoviesApp.Services.RatingRepo;

namespace MoviesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;
        public RatingController(IRatingService ratingService) 
        {
            _ratingService = ratingService;
        }
        [HttpPost, Authorize]
        public IActionResult RateMovie(RatingRequest ratingRequest)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!_ratingService.IsMovieExist(ratingRequest.MovieId))
            {
                return NotFound("Movie doesnt exist");
            }
            if(ratingRequest.Rating < 1 || ratingRequest.Rating > 10)
            {
                return BadRequest();
            }
            return Ok(_ratingService.RateMovie(ratingRequest, token));
        }
        [HttpGet, Authorize]
        public IActionResult UserRating(int movieId)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var rating = _ratingService.UserRating(token, movieId);
            return Ok(rating);
        }
    }
}
