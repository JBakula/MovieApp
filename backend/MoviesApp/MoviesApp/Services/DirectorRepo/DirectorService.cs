using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoviesApp.DTO;
using MoviesApp.Models;

namespace MoviesApp.Services.DirectorRepo
{
    public class DirectorService:IDirectorService
    {
        private readonly MoviesDbContext _context;
        private readonly IMapper _mapper;
        public DirectorService(MoviesDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public ICollection<DirectorResponse> GetDirectors()
        {
            var directors = _context.Directors.OrderBy(d=>d.DirectorName).ToList();
            return _mapper.Map<List<DirectorResponse>>(directors);
        }
        public MoviesResponsePaginated GetMoviesByDirectoryId(int id, int page, string ordering)
        {
            var numberOfMoviesPerPage = 8f;
            var numberOfPages = NumberOfPages(id,numberOfMoviesPerPage);


            var movies = _context.Movies.Where(m => m.DirectorId == id)
                               .Skip((page - 1) * (int)numberOfPages)
                               .Take((int)numberOfMoviesPerPage)
                               .Select(m => new
                               {
                                   m.MovieId,
                                   m.MovieName,
                                   m.CoverImage,
                                   m.IMDbRating,
                                   m.Year,
                                   m.Description

                               }).ToList();

            switch (ordering)
            {
                case "Title ascending":
                    movies = _context.Movies.Where(m => m.DirectorId == id)
                              .OrderBy(m => m.MovieName)
                              .Skip((page - 1) * (int)numberOfPages)
                              .Take((int)numberOfMoviesPerPage)
                              .Select(m => new
                              {
                                  m.MovieId,
                                  m.MovieName,
                                  m.CoverImage,
                                  m.IMDbRating,
                                  m.Year,
                                  m.Description

                              }).ToList();
                    break;
                case "Title descending":
                    movies = _context.Movies.Where(m => m.DirectorId == id)
                              .OrderByDescending(m => m.MovieName)
                              .Skip((page - 1) * (int)numberOfPages)
                              .Take((int)numberOfMoviesPerPage)
                              .Select(m => new
                              {
                                  m.MovieId,
                                  m.MovieName,
                                  m.CoverImage,
                                  m.IMDbRating,
                                  m.Year,
                                  m.Description

                              }).ToList();
                    break;
                case "Year ascending":
                    movies = _context.Movies
                              .Where(m => m.DirectorId == id)
                              .OrderBy(m => m.Year)
                              .Skip((page - 1) * (int)numberOfPages)
                              .Take((int)numberOfMoviesPerPage)
                              .Select(m => new
                              {
                                  m.MovieId,
                                  m.MovieName,
                                  m.CoverImage,
                                  m.IMDbRating,
                                  m.Year,
                                  m.Description

                              }).ToList();
                    break;
                case "Year descending":
                    movies = _context.Movies
                              .Where(m => m.DirectorId == id)
                              .OrderByDescending(m => m.Year)
                              .Skip((page - 1) * (int)numberOfPages)
                              .Take((int)numberOfMoviesPerPage)
                              .Select(m => new
                              {
                                  m.MovieId,
                                  m.MovieName,
                                  m.CoverImage,
                                  m.IMDbRating,
                                  m.Year,
                                  m.Description

                              }).ToList();
                    break;
                case "IMDb rating ascending":
                    movies = _context.Movies
                              .Where(m => m.DirectorId == id)
                              .OrderBy(m => m.IMDbRating)
                              .Skip((page - 1) * (int)numberOfPages)
                              .Take((int)numberOfMoviesPerPage)
                              .Select(m => new
                              {
                                  m.MovieId,
                                  m.MovieName,
                                  m.CoverImage,
                                  m.IMDbRating,
                                  m.Year,
                                  m.Description

                              }).ToList();
                    break;
                case "IMDb rating descending":
                    movies = _context.Movies
                              .Where(m => m.DirectorId == id)
                              .OrderByDescending(m => m.IMDbRating)
                              .Skip((page - 1) * (int)numberOfPages)
                              .Take((int)numberOfMoviesPerPage)
                              .Select(m => new
                              {
                                  m.MovieId,
                                  m.MovieName,
                                  m.CoverImage,
                                  m.IMDbRating,
                                  m.Year,
                                  m.Description

                              }).ToList();
                    break;
                default:
                    movies = _context.Movies.Where(m => m.DirectorId == id)
                               .OrderBy(m => m.MovieName)
                               .Skip((page - 1) * (int)numberOfPages)
                               .Take((int)numberOfMoviesPerPage)
                               .Select(m => new
                               {
                                   m.MovieId,
                                   m.MovieName,
                                   m.CoverImage,
                                   m.IMDbRating,
                                   m.Year,
                                   m.Description

                               }).ToList();
                    break;

            }

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
                    IMDbRating = (float)movie.IMDbRating,
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
        public bool IsDirectorExist(int id)
        {
            return _context.Movies.Any(m => m.DirectorId == id);
        }
        public bool AddDirector(DirectorRequest directorRequest)
        {
            var director = _mapper.Map<Director>(directorRequest);
            _context.Directors.Add(director);
            _context.SaveChanges();
            return true;
        }
        public double NumberOfPages(int id,float numberOfMoviesPerPage)
        {
            return Math.Ceiling(_context.Movies.Where(m=>m.DirectorId == id).Count() / numberOfMoviesPerPage);
        }

        public bool DirectorNameExist(string name)
        {
            return _context.Directors.Where(d=>d.DirectorName == name).Any();
        }
    }
}
