using MoviesApp.DTO;
using MoviesApp.Models;

namespace MoviesApp.Services.UserRepo
{
    public interface IUserService
    {
        bool RegisterUser(UserRegistration userRegistration);
        bool IsEmailAlreadyExist(string email);
        AuthResponse LoginUser(UserLogin userLogin);
        CookieOptions SetRefreshToken(RefreshToken refreshToken);
        AuthResponse RefreshToken(string token);
        bool IsTokenExpired(string token);
    }
}
