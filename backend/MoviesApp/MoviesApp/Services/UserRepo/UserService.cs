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
    public class UserService : IUserService
    {
        private readonly MoviesDbContext _context;
        private readonly IConfiguration _configuration;
        public UserService(MoviesDbContext context, IConfiguration configuration)
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
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        public bool IsEmailAlreadyExist(string email)
        {
            return _context.Users.Where(u => u.Email == email).Any();
        }
        public AuthResponse LoginUser(UserLogin userLogin)
        {
            AuthResponse authResponse = new AuthResponse();
            if (IsEmailAlreadyExist(userLogin.Email))
            {
                var user = _context.Users.Where(u => u.Email == userLogin.Email).FirstOrDefault();
                if (!VerifyPasswordHash(userLogin.Password, user.PasswordHash, user.PasswordSalt))
                {
                    return null;
                }
                else
                {
                    var refreshToken = GenerateRefreshToken(user);
                    var jwtToken = CreateToken(user);

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
            var oldRefreshToken = _context.RefreshTokens.Where(r => r.Token == token).FirstOrDefault();
            if (oldRefreshToken != null)
            {
                var user = _context.Users.Where(u => u.UserId == oldRefreshToken.UserId).FirstOrDefault();
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
            var refreshToken = _context.RefreshTokens.Where(r => r.Token == token).FirstOrDefault();
            if (refreshToken == null || (refreshToken.ExpiresAt < DateTime.UtcNow))
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
                ExpiresAt = DateTime.UtcNow.AddHours(10),
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
                expires: DateTime.UtcNow.AddSeconds(30),
                signingCredentials: credentials);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);

            }
        }
        public MoviesResponse MoviesRecommendation(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var decodedToken = handler.ReadJwtToken(token);
            var userId = int.Parse(decodedToken.Claims.First(c => c.Type == "UserId").Value);
            var userFavouriteMovies = _context.Ratings.Where(r => ((r.UserId == userId) && (r.Value > 7))).Select(r => new
            {
                r.Movie.MovieId,
                r.Movie.MovieName,
                r.Movie.CoverImage,
                r.Movie.Description,
                r.Movie.Year,
                r.Movie.DirectorId,
                r.Movie.Director.DirectorName,
                r.Movie.IMDbRating,
            }).ToList();

            var favouriteCategoriesList = new List<int>();

            foreach (var movie in userFavouriteMovies)
            {
                var categoryMovie = _context.CategoryMovies.Where(cm => cm.MovieId == movie.MovieId)
                    .Select(cm => new
                    {
                        cm.CategoryId,
                        cm.MovieId,
                        cm.Category.CategoryName
                    })
                    .ToList();
                foreach (var category in categoryMovie)
                {
                    favouriteCategoriesList.Add(category.CategoryId);
                }

            }

            var favouriteDirectors = new List<DirectorResponse>();

            foreach (var director in userFavouriteMovies)
            {
                var newDirectorResponse = new DirectorResponse()
                {
                    DirectorId = director.DirectorId,
                    DirectorName = director.DirectorName
                };
                favouriteDirectors.Add(newDirectorResponse);
            }

            var ratedMovies = _context.Ratings.Where(r=>r.UserId == userId).Select(r=>r.MovieId).ToList();



            var notRatedMovies = _context.Ratings.Where(r => r.UserId != userId).Select(r => new
            {
                r.Movie.MovieId,
                r.Movie.MovieName,
                r.Movie.CoverImage,
                r.Movie.Description,
                r.Movie.Year,
                r.Movie.IMDbRating,
            }).ToList();

            List<MoviesResponse> recommendedList = new List<MoviesResponse>();

            foreach (var movie in notRatedMovies)
            {
                if(!ratedMovies.Contains(movie.MovieId))
                {
                    var categories = _context.CategoryMovies.Where(c => c.MovieId == movie.MovieId).Select(c => new { c.CategoryId }).ToList();
                    foreach (var category in categories)
                    {
                        if (favouriteCategoriesList.Contains(category.CategoryId))
                        {
                            recommendedList.Add(new MoviesResponse()
                            {
                                MovieId = movie.MovieId,
                                MovieName = movie.MovieName,
                                CoverImage = movie.CoverImage,
                                Description = movie.Description,
                                Year = movie.Year,
                                IMDbRating = (float)movie.IMDbRating,
                                Rating = CalculateMovieRating(movie.MovieId)
                            }); 
                        }
                    }
                }
                
            }

            List<MoviesResponse> uniqueList = CustomDistinctMethod(recommendedList);
            Random rd = new Random();
           
            return uniqueList[rd.Next(0, uniqueList.Count)];
            
            
        }
        
        private float CalculateMovieRating(int id)
        {
            int numberOfRatings = NumberOfRatings(id);
            if (numberOfRatings == 0)
            {
                return 0;
            }
            int rating = _context.Ratings.Where(r => r.MovieId == id).Sum(r => r.Value);


            return (float)Math.Round((double)((float)rating / (float)numberOfRatings), 1);
        }
        private int NumberOfRatings(int id)
        {
            return _context.Ratings.Where(r => r.MovieId == id).Count();
        }

        private List<MoviesResponse> CustomDistinctMethod(List<MoviesResponse> movies)
        {
            List<MoviesResponse> newList = new List<MoviesResponse>();
            for(int i=0;i<movies.Count;i++)
            {
                bool check = false;
                for(int j = 0; j < i; j++)
                {
                    if (movies[i].MovieId == movies[j].MovieId)
                    {
                        check = true; break;

                    }
                }
                if (!check)
                {
                    newList.Add(movies[i]);
                }
            }
            return newList;
        }
    }
}
