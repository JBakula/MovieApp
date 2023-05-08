using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using MoviesApp.DTO;
using MoviesApp.Models;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MoviesApp.Services.UserRepo
{
    public class UserService:IUserService
    {
        private readonly MoviesDbContext _context;
        private readonly IConfiguration _configuration;
        public UserService(MoviesDbContext context,IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public bool RegisterUser(UserRegistration userRegistration)
        {
            CreatePasswordHash(userRegistration.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var newUser = new User()
            {
                Name = userRegistration.Name,
                Lastname = userRegistration.Lastname,
                Email = userRegistration.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
            return true;
        }
        private void CreatePasswordHash(string password,out byte[] passwordHash,out byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        public bool IsEmailAlreadyExist(string email)
        {
            return _context.Users.Where(u=>u.Email == email).Any();
        }
        public AuthResponse LoginUser(UserLogin userLogin)
        {
            AuthResponse authResponse = new AuthResponse();
            if(IsEmailAlreadyExist(userLogin.Email))
            {
                var user = _context.Users.Where(u=>u.Email == userLogin.Email).FirstOrDefault();
                if (!VerifyPasswordHash(userLogin.Password, user.PasswordHash,user.PasswordSalt))
                {
                    return null;
                }
                else
                {
                    var refreshToken = GenerateRefreshToken(user);
                    var jwtToken =  CreateToken(user);

                    authResponse.JwtToken = jwtToken;
                    authResponse.RefreshToken = refreshToken;
                    return authResponse;
                    
                }
            }
            else
            {
                return null;
            }
            
        }
        public AuthResponse RefreshToken(string token)
        {
            AuthResponse authResponse = new AuthResponse();
            var oldRefreshToken = _context.RefreshTokens.Where(r=>r.Token == token).FirstOrDefault();
            if(oldRefreshToken != null)
            {
                var user = _context.Users.Where(u=>u.UserId == oldRefreshToken.UserId).FirstOrDefault();
                string newJwtToken = CreateToken(user);
                var newRefreshToken = GenerateRefreshToken(user);
                authResponse.JwtToken = newJwtToken;
                authResponse.RefreshToken = newRefreshToken;
                _context.Remove(oldRefreshToken);
                _context.SaveChanges();
                return authResponse;
            }
            else
            {
                return null;
            }
        }
        public bool IsTokenExpired(string token)
        {
            var refreshToken = _context.RefreshTokens.Where(r=>r.Token == token ).FirstOrDefault(); 
            if(refreshToken == null || (refreshToken.ExpiresAt < DateTime.UtcNow))
            {
              
                return true;
            }
            return false;
            
        }
        //public CookieOptions SetRefreshToken(RefreshToken refreshToken)
        //{
        //    var cookieOptions = new CookieOptions
        //    {
        //        HttpOnly = true,
        //        Expires = refreshToken.ExpiresAt
        //    };
        //    return cookieOptions;
        //}
        private RefreshToken GenerateRefreshToken(User user)
        {
            var refreshToken = new RefreshToken()
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddHours(5),
                User = user,
                UserId = user.UserId,
                
            };
            _context.RefreshTokens.Add(refreshToken);
            _context.SaveChanges();
            return refreshToken;
        }
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("UserId", user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Surname,user.Lastname)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:SecretKey").Value));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddSeconds(15),
                signingCredentials: credentials);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
        
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
                
            }
        }
    }
}
