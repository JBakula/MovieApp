using MoviesApp.Models;

namespace MoviesApp.DTO
{
    public class AuthResponse
    {
        public string JwtToken { get; set; } = string.Empty;
        public RefreshToken RefreshToken { get; set; } 

    }
}
