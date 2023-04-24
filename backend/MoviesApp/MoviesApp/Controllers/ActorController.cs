using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApp.DTO;
using MoviesApp.Services.ActorRepo;

namespace MoviesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly IActorService _actorService;
        public ActorController(IActorService actorService)
        {
            _actorService = actorService;
        }
        [HttpGet]
        public IActionResult GetActors() 
        {
            return Ok(_actorService.GetActors());
        }
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetMoviesByActorId(int id,[FromQuery]int page=1)
        {
            if (!_actorService.IsActorExist(id))
            {
                return NotFound();
            }
            return Ok(_actorService.GetMoviesByActorId(id,page));
        }
        [HttpPost]
        public IActionResult AddActor(NewActorDto actorDto)
        {
            if(actorDto.ActorName=="")
            {
                return BadRequest();
            }
            if(_actorService.IsActorNameAlreadyExist(actorDto.ActorName))
            {
                return BadRequest();
            }
            if (_actorService.AddActor(actorDto))
            {
                return Ok("New actor successfully added");
            }
            else
            {
                return StatusCode(500);
            }
        }
        
    }
}
