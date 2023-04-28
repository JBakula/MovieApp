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
        [HttpPost]
        public IActionResult RateMovie(RatingRequest ratingRequest)
        {
            if (!_ratingService.IsMovieExist(ratingRequest.MovieId))
            {
                return NotFound("Movie doesnt exist");
            }
            if(ratingRequest.Rating < 1 || ratingRequest.Rating > 10)
            {
                return BadRequest();
            }
            if (_ratingService.RateMovie(ratingRequest))
            {
                return Ok();
            }
            else
            {
                return StatusCode(500);
            }
        }
    }
}
