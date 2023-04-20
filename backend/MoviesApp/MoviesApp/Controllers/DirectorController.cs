using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApp.DTO;
using MoviesApp.Services.DirectorRepo;

namespace MoviesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorController : ControllerBase
    {
        private readonly IDirectorService _directorService;
        public DirectorController(IDirectorService directorService) 
        {
            _directorService = directorService;
        }
        [HttpGet]
        public IActionResult GetDirectors()
        {
            return Ok(_directorService.GetDirectors());
        }
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetMoviesByDirectoryId([FromRoute]int id)
        {
            if(!_directorService.IsDirectorExist(id))
            {
                return NotFound("Redatelj ne postoji");
            }
            return Ok(_directorService.GetMoviesByDirectoryId(id));
        }

        [HttpPost]
        public IActionResult AddDirector(DirectorRequest directorRequest)
        {
            if (directorRequest.DirectorName == "")
            {
                return BadRequest("Director name cannot be empty");
            }
            if (_directorService.DirectorNameExist(directorRequest.DirectorName))
            {
                return BadRequest("Director name already exist");
            }
            if (_directorService.AddDirector(directorRequest))
            {
                return Ok("Movie director is successfully added");
            }
            else
            {
                return StatusCode(500);
            }
        }
        
    }
}
