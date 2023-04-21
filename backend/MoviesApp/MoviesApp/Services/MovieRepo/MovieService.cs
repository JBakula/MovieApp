using Microsoft.AspNetCore.Hosting;
using MoviesApp.DTO;
using MoviesApp.Models;
using System.Linq;

namespace MoviesApp.Services.MovieRepo
{
    public class MovieService:IMovieService
    {
        private readonly MoviesDbContext _context;
        public static IWebHostEnvironment _webHostEnvironment;
        public MovieService(MoviesDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public bool AddMovie(MovieRequest movieRequest)
        {
            var director = _context.Directors.Where(d=>d.DirectorId== movieRequest.DirectorId).FirstOrDefault();
            var newMovie = new Movie()
            {
                MovieName = movieRequest.MovieName,
                Year = movieRequest.Year,
                CoverImage = GenerateImagePath(movieRequest.CoverImage),
                Description = movieRequest.Description,
                Director = director,
                DirectorId = movieRequest.DirectorId,
            };
            _context.Movies.Add(newMovie);
            _context.SaveChanges();

            
            foreach(var category in movieRequest.Categories)
            {
                var categoryFromDb = _context.Categories.Find(category);
                var newCategoryMovie = new CategoryMovie()
                {
                    CategoryId = category,
                    MovieId = newMovie.MovieId,
                    Movie = newMovie,
                    Category = categoryFromDb
                };
                _context.CategoryMovies.Add(newCategoryMovie);
                _context.SaveChanges();
            }
            foreach(var star in movieRequest.StarActors)
            {
                var starFromDb = _context.Actors.Find(star);
                var newStarActor = new MovieActor()
                {
                    ActorId = star,
                    MovieId = newMovie.MovieId,
                    Movie = newMovie,
                    Actor = starFromDb,
                    Star = true
                };
                _context.MovieActors.Add(newStarActor);
                _context.SaveChanges();
            }
            foreach(var actor in movieRequest.Actors)
            {
                var actorFromDb = _context.Actors.Find(actor);
                var newMovieActor = new MovieActor()
                {
                    ActorId = actor,
                    MovieId = newMovie.MovieId,
                    Movie = newMovie,
                    Actor = actorFromDb,
                    Star = false
                };
                _context.MovieActors.Add(newMovieActor);
                _context.SaveChanges(); 
            }
            foreach (var image in movieRequest.Images)
            {
                var newImage = new Image()
                {
                    ImageName = GenerateImagePath(image),
                    MovieId = newMovie.MovieId,
                    Movie = newMovie
                };
                _context.Images.Add(newImage);
                _context.SaveChanges();
            }
            return true;
        }
        public string GenerateImagePath(IFormFile image)
        {
            try
            {


                var imageName = (Guid.NewGuid().ToString().Replace("-", "")) + "_" + image.FileName;


                var path = Path.Combine(_webHostEnvironment.WebRootPath, "images", imageName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }

                return "/images/" + imageName;
            }
            catch (Exception e)
            {
                return "";
            }
        }

        public MoviesResponsePaginated GetMovies(int page, string ordering)
        {
           
            var numberOfMoviesPerPage = 8f;
            var numberOfPages = NumberOfPages(numberOfMoviesPerPage);


            var movies = _context.Movies.OrderBy(m => m.MovieName)
                               .Skip((page - 1) * (int)numberOfPages)
                               .Take((int)numberOfMoviesPerPage)
                               .Select(m => new
                               {
                                   m.MovieId,
                                   m.MovieName,
                                   m.CoverImage,
                                   m.Year,
                                   m.Description

                               }).ToList();

            var moviesResponeList = new List<MoviesResponse>();
            foreach (var movie in movies)
            {
                var rating = CountMovieRating(movie.MovieId);
                moviesResponeList.Add(new MoviesResponse()
                {
                    MovieId = movie.MovieId,
                    MovieName = movie.MovieName,
                    Rating = rating,
                    Year = movie.Year,
                    CoverImage = movie.CoverImage,
                    Description = movie.Description
                });
            }
            
            var moviesResponsePaginated = new MoviesResponsePaginated()
            {
                Movies = moviesResponeList,
                CurrentPage = page,
                NumberOfPages = (int)numberOfPages
            };
            
            return moviesResponsePaginated;
                               
            
        }
        private float CountMovieRating(int id)
        {
            if (NumberOfRatings(id) == 0)
            {
                return 0;
            }
            return _context.Ratings.Where(r => r.MovieId == id).Select(r => r.Value).Sum()
                                            / NumberOfRatings(id);
        }
        private int NumberOfRatings(int id)
        {
            return _context.Ratings.Where(r => r.MovieId == id).Count();
        }
        public double NumberOfPages(float numberofItemsPerPage)
        {
            return Math.Ceiling(_context.Movies.Count() / numberofItemsPerPage);
        }

        public MovieDetails GetMovieDetails(int id)
        {
            var movie = _context.Movies.Where(m=>m.MovieId == id)
                                       .Select(m =>new
                                       {
                                           m.MovieId,
                                           m.MovieName,
                                           m.Year,
                                           m.CoverImage,
                                           m.Description,
                                           m.DirectorId,
                                           m.Director.DirectorName,
                                       })
                                       .FirstOrDefault();
            
            var images = _context.Images.Where(i=>i.MovieId == id).ToList();
            var categories = _context.CategoryMovies.Where(cm=>cm.MovieId == id).
                                                    Select(cm=>new { 
                                                        cm.CategoryId,
                                                        cm.Category.CategoryName
                                                    }).ToList();
            
            var categoryResponseList = new List<CategoryResponse>();
            foreach (var category in categories)
            {
                categoryResponseList.Add(new CategoryResponse()
                {
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName,
                  
                });
            }
            var actors = _context.MovieActors.Where(ma => ma.MovieId == id)
                                             .Select(ma => new
                                             {
                                                 ma.ActorId,
                                                 ma.Actor.ActorName,
                                                 ma.Star
                                             }).ToList();
            var actorsResponseList = new List<ActorsInMovieDetalis>();
            foreach (var actor in actors)
            {
                actorsResponseList.Add(new ActorsInMovieDetalis()
                {
                    ActorId = actor.ActorId,
                    ActorName = actor.ActorName,
                    Star = actor.Star   
                });
            }

            var movieDetails = new MovieDetails()
            {
                MovieId = movie.MovieId,
                MovieName = movie.MovieName,
                Year = movie.Year,
                Rating = CountMovieRating(movie.MovieId),
                CoverImage = movie.CoverImage,
                Description = movie.Description,
                DirectorId = movie.DirectorId,
                DirectorName = movie.DirectorName,
                Images = images,
                Categories = categoryResponseList,
                Actors = actorsResponseList
            };

            return movieDetails;
        }
        public bool IsMovieExist(int id)
        {
            return _context.Movies.Where(m=>m.MovieId==id).Any();
        }

        public MoviesResponsePaginated GetMovieBySearchTerm(string searchTerm,int page)
        {
            var numberOfMoviesPerPage = 8f;
            var numberOfPages = NumberOfPages(numberOfMoviesPerPage);
            var movies = _context.Movies.Where(m => (m.MovieName.ToLower()).Contains(searchTerm.ToLower())
                                                    ||(m.Director.DirectorName.ToLower().Contains(searchTerm.ToLower()))
            ).Skip((page - 1) * (int)numberOfPages)
                               .Take((int)numberOfMoviesPerPage)
                               .Select(m => new
                               {
                                   m.MovieId,
                                   m.MovieName,
                                   m.CoverImage,
                                   m.Year,
                                   m.Description

                               }).ToList();




            var moviesResponeList = new List<MoviesResponse>();
            foreach (var movie in movies)
            {
                var rating = CountMovieRating(movie.MovieId);
                moviesResponeList.Add(new MoviesResponse()
                {
                    MovieId = movie.MovieId,
                    MovieName = movie.MovieName,
                    Rating = rating,
                    Year = movie.Year,
                    CoverImage = movie.CoverImage,
                    Description = movie.Description
                });
            }

            var moviesResponsePaginated = new MoviesResponsePaginated()
            {
                Movies = moviesResponeList,
                CurrentPage = page,
                NumberOfPages = (int)numberOfPages
            };

            return moviesResponsePaginated;
        }

        public MoviesResponsePaginated GetMoviesOrderedByYear(int page)
        {
            var numberOfMoviesPerPage = 8f;
            var numberOfPages = NumberOfPages(numberOfMoviesPerPage);
            var movies = _context.Movies.OrderBy(m=>m.Year)
                               .Skip((page - 1) * (int)numberOfPages)
                               .Take((int)numberOfMoviesPerPage)
                               .Select(m => new
                               {
                                   m.MovieId,
                                   m.MovieName,
                                   m.CoverImage,
                                   m.Year,
                                   m.Description

                               }).ToList();
            var moviesResponeList = new List<MoviesResponse>();
            foreach (var movie in movies)
            {
                var rating = CountMovieRating(movie.MovieId);
                moviesResponeList.Add(new MoviesResponse()
                {
                    MovieId = movie.MovieId,
                    MovieName = movie.MovieName,
                    Rating = rating,
                    Year = movie.Year,
                    CoverImage = movie.CoverImage,
                    Description = movie.Description
                });
            }
            var moviesResponsePaginated = new MoviesResponsePaginated()
            {
                Movies = moviesResponeList,
                CurrentPage = page,
                NumberOfPages = (int)numberOfPages
            };
            return moviesResponsePaginated;
        }
        


    }
}
