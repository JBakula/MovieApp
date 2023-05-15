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


            var movies = _context.Movies.Where(m => m.DirectorId == id).ToList();

            var moviesResponseList = new List<MoviesResponse>();
            foreach (var movie in movies)
            {
                float rating = CalculateMovieRating(movie.MovieId);


                moviesResponseList.Add(new MoviesResponse()
                {
                    MovieId = movie.MovieId,
                    MovieName = movie.MovieName,
                    Rating = rating,
                    Year = movie.Year,
                    /*UserRating = userRating*/
                    CoverImage = movie.CoverImage,
                    IMDbRating = (float)movie.IMDbRating,
                    Description = movie.Description
                });
            }
            var moviesResponseListOrdered = new List<MoviesResponse>();
            switch (ordering)
            {
                case "Title ascending":
                    moviesResponseListOrdered = moviesResponseList.OrderBy(m => m.MovieName).Skip((page - 1) * (int)numberOfMoviesPerPage).Take((int)numberOfMoviesPerPage).ToList();
                    break;
                case "Title descending":
                    moviesResponseListOrdered = moviesResponseList.OrderByDescending(m => m.MovieName).Skip((page - 1) * (int)numberOfMoviesPerPage).Take((int)numberOfMoviesPerPage).ToList();
                    break;
                case "Year ascending":
                    moviesResponseListOrdered = moviesResponseList.OrderBy(m => m.Year).Skip((page - 1) * (int)numberOfMoviesPerPage).Take((int)numberOfMoviesPerPage).ToList();
                    break;
                case "Year descending":
                    moviesResponseListOrdered = moviesResponseList.OrderByDescending(m => m.Year).Skip((page - 1) * (int)numberOfMoviesPerPage).Take((int)numberOfMoviesPerPage).ToList();
                    break;
                case "IMDb rating ascending":
                    moviesResponseListOrdered = moviesResponseList.OrderBy(m => m.IMDbRating).Skip((page - 1) * (int)numberOfMoviesPerPage).Take((int)numberOfMoviesPerPage).ToList();
                    break;
                case "IMDb rating descending":
                    moviesResponseListOrdered = moviesResponseList.OrderByDescending(m => m.IMDbRating).Skip((page - 1) * (int)numberOfMoviesPerPage).Take((int)numberOfMoviesPerPage).ToList();
                    break;
                case "Users rating ascending":
                    moviesResponseListOrdered = moviesResponseList.OrderBy(m => m.Rating).Skip((page - 1) * (int)numberOfMoviesPerPage).Take((int)numberOfMoviesPerPage).ToList();
                    break;
                case "Users rating descending":
                    moviesResponseListOrdered = moviesResponseList.OrderByDescending(m => m.Rating).Skip((page - 1) * (int)numberOfMoviesPerPage).Take((int)numberOfMoviesPerPage).ToList();
                    break;

                default:
                    moviesResponseListOrdered = moviesResponseList.OrderBy(m => m.MovieName).Skip((page - 1) * (int)numberOfMoviesPerPage).Take((int)numberOfMoviesPerPage).ToList();
                    break;

            }

            var moviesResponsePaginated = new MoviesResponsePaginated()
            {
                Movies = moviesResponseListOrdered,
                CurrentPage = page,
                NumberOfPages = (int)numberOfPages
            };

            return moviesResponsePaginated;
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
