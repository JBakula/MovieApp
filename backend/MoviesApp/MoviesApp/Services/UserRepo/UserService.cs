using Microsoft.IdentityModel.Tokens;
using MoviesApp.DTO;
using MoviesApp.Models;
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
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        public bool IsEmailAlreadyExist(string email)
        {
            return _context.Users.Where(u=>u.Email == email).Any();
        }
        public string LoginUser(UserLogin userLogin)
        {
            if(IsEmailAlreadyExist(userLogin.Email))
            {
                var user = _context.Users.Where(u=>u.Email == userLogin.Email).FirstOrDefault();
                if (!VerifyPasswordHash(userLogin.Password, user.PasswordHash,user.PasswordSalt))
                {
                    return "";
                }
                else
                {
                    return CreateToken(user);
                }
            }
            else
            {
                return "";
            }
            
        }
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Surname,user.Lastname)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:SecretKey").Value));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
                
            }
        }
    }
}
