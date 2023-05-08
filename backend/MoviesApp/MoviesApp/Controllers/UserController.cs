using Microsoft.AspNetCore.Authorization;
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
                return Ok(new {
                    message="Signup successfull"});
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
            var authResponse = _userService.LoginUser(userLogin);
            if (authResponse == null)
            {
                return BadRequest("Pogresno ime ili lozinka");
            }
            else
            {
                //Response.Cookies.Append("refreshToken", authResponse.RefreshToken.Token, _userService.SetRefreshToken(authResponse.RefreshToken));
                return Ok(new
                {
                    message="Login success",
                    jwtToken= authResponse.JwtToken,
                    refreshToken=authResponse.RefreshToken.Token
                });
            }

        }
        [HttpPost]
        [Route("refresh-token")]
        public IActionResult RefreshToken(RefreshTokenRequest refreshToken)
        {
            
            if (_userService.IsTokenExpired(refreshToken.RefreshToken))
            {
                return StatusCode(500);
            }
            else
            {
                
               var refreshedToken =  _userService.RefreshToken(refreshToken.RefreshToken);
                if(refreshedToken != null)
                {
                    //Response.Cookies.Append("refreshToken", refreshedToken.RefreshToken.Token, _userService.SetRefreshToken(refreshedToken.RefreshToken));
                    return Ok(new {
                        refreshedToken.JwtToken,
                        refreshedToken.RefreshToken.Token
                    });
                }
                else
                {
                    return Unauthorized();
                }
                
            }
        }
    }
}
