using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApp.DTO;
using MoviesApp.Services.MovieRepo;

namespace MoviesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MovieController(IMovieService movieService) 
        {
            _movieService = movieService;
        }

        [HttpGet]
        public IActionResult GetMovies([FromQuery]int page = 1, [FromQuery] string ordering= "Name ascending")
        {
            var movies = _movieService.GetMovies(page, ordering);
            if(movies!=null)
            {
                return Ok(movies);
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult AddMovie([FromForm] MovieRequest movieRequest)
        {
            if (movieRequest.Year == 0 || movieRequest.MovieName=="" || 
                movieRequest.CoverImage == null || movieRequest.DirectorId == 0 ||
                movieRequest.Actors.Length<=0 || movieRequest.StarActors.Length<=0 || movieRequest.Images.Length<=0){
                
                return BadRequest();

            }
            if (!_movieService.AddMovie(movieRequest))
            {
                return StatusCode(500);
            }
            else
            {
                return Ok("Uspjesno dodano");
            }
        }
        [HttpGet]
        [Route("details/{id:int}")]
        public IActionResult GetMovieDetails([FromRoute]int id)
        {
            if (!_movieService.IsMovieExist(id))
            {
                return NotFound("Film nije pronadjen");
            }
            return Ok(_movieService.GetMovieDetails(id));
        }
        [HttpGet]
        [Route("search")]
        public IActionResult GetMoviesBySearchTerm([FromQuery]string term, [FromQuery] string ordering,[FromQuery] int page=1)
        {
            return Ok(_movieService.GetMovieBySearchTerm(term,ordering,page));
        }
        //[HttpGet]
        //[Route("year/asc/{page:int}")]
        //public IActionResult GetMoviesByYear([FromRoute]int page)
        //{
        //    return Ok(_movieService.GetMoviesOrderedByYear(page));
        //}
        

    }
}
