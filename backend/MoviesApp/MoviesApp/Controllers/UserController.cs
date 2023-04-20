using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApp.DTO;
using MoviesApp.Services.UserRepo;

namespace MoviesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService) 
        {
            _userService = userService;
        }
        [HttpPost]
        [Route("register")]
        public IActionResult RegisterUser(UserRegistration userRegistration)
        {
            if(userRegistration.Password.Length<6 || userRegistration.Name=="" || userRegistration.Lastname=="" || _userService.IsEmailAlreadyExist(userRegistration.Email)){
                return BadRequest("Neuspjesna registracija");
            }
            if(_userService.RegisterUser(userRegistration))
            {
                return Ok("Uspjesna registracija");
            }
            else
            {
                return StatusCode(500);
            }
        }
        [HttpPost]
        [Route("login")]
        public IActionResult LoginUser(UserLogin userLogin)
        {
            var token = _userService.LoginUser(userLogin);
            if (token=="")
            {
                return BadRequest("Pogresno ime ili lozinka");
            }
            else
            {
                return Ok(token);
            }
        }
    }
}
