using AutoMapper;
using MoviesApp.DTO;
using MoviesApp.Models;

namespace MoviesApp.Services.CategoryRepo
{
    public class CategoryService:ICategoryService
    {
        private readonly MoviesDbContext _context;
        private readonly IMapper _mapper;
        public CategoryService(MoviesDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        public ICollection<CategoryResponse> GetCategories()
        {
            var categories = _context.Categories.OrderBy(c=>c.CategoryName).ToList();
            var categoriesDto = _mapper.Map<List<CategoryResponse>>(categories);
            return categoriesDto;
        }
        public MoviesResponsePaginated GetMoviesByCategoryId(int id,string ordering, int page) 
        {
            var numberOfMoviesPerPage = 8f;
            var numberOfPages = NumberOfPages(id,numberOfMoviesPerPage);
            var movies = _context.CategoryMovies.Where(c=>c.CategoryId == id)
                                                .Skip((page - 1) * (int)numberOfPages)
                                                .Take((int)numberOfMoviesPerPage)
                                                .Select(c => new
                                                {
                                                    c.MovieId,
                                                    c.Movie.MovieName,
                                                    c.Movie.CoverImage,
                                                    c.Movie.IMDbRating,
                                                    c.Movie.Year,
                                                    c.Movie.Description
                                                }).ToList();
            switch (ordering)
            {
                case "Title ascending":
                    movies = _context.CategoryMovies.Where(c => c.CategoryId == id)
                                                .OrderBy(c=>c.Movie.MovieName)
                                                .Skip((page - 1) * (int)numberOfPages)
                                                .Take((int)numberOfMoviesPerPage)
                                                .Select(c => new
                                                {
                                                    c.MovieId,
                                                    c.Movie.MovieName,
                                                    c.Movie.CoverImage,
                                                    c.Movie.IMDbRating,
                                                    c.Movie.Year,
                                                    c.Movie.Description
                                                }).ToList();
                    break;
                case "Title descending":
                    movies = _context.CategoryMovies.Where(c => c.CategoryId == id)
                                                .OrderByDescending(c => c.Movie.MovieName)
                                                .Skip((page - 1) * (int)numberOfPages)
                                                .Take((int)numberOfMoviesPerPage)
                                                .Select(c => new
                                                {
                                                    c.MovieId,
                                                    c.Movie.MovieName,
                                                    c.Movie.CoverImage,
                                                    c.Movie.IMDbRating,
                                                    c.Movie.Year,
                                                    c.Movie.Description
                                                }).ToList();
                    break;
                case "Year ascending":
                    movies = _context.CategoryMovies.Where(c => c.CategoryId == id)
                                                .OrderBy(c => c.Movie.Year)
                                                .Skip((page - 1) * (int)numberOfPages)
                                                .Take((int)numberOfMoviesPerPage)
                                                .Select(c => new
                                                {
                                                    c.MovieId,
                                                    c.Movie.MovieName,
                                                    c.Movie.CoverImage,
                                                    c.Movie.IMDbRating,
                                                    c.Movie.Year,
                                                    c.Movie.Description
                                                }).ToList();
                    break;
                case "Year descending":
                    movies = _context.CategoryMovies.Where(c => c.CategoryId == id)
                                                 .OrderByDescending(c => c.Movie.Year)
                                                 .Skip((page - 1) * (int)numberOfPages)
                                                .Take((int)numberOfMoviesPerPage)
                                                .Select(c => new
                                                {
                                                    c.MovieId,
                                                    c.Movie.MovieName,
                                                    c.Movie.CoverImage,
                                                    c.Movie.IMDbRating,
                                                    c.Movie.Year,
                                                    c.Movie.Description
                                                }).ToList();
                    break;
                case "IMDb rating ascending":
                    movies = _context.CategoryMovies.Where(c => c.CategoryId == id)
                                                 .OrderBy(c => c.Movie.IMDbRating)
                                                 .Skip((page - 1) * (int)numberOfPages)
                                                .Take((int)numberOfMoviesPerPage)
                                                .Select(c => new
                                                {
                                                    c.MovieId,
                                                    c.Movie.MovieName,
                                                    c.Movie.CoverImage,
                                                    c.Movie.IMDbRating,
                                                    c.Movie.Year,
                                                    c.Movie.Description
                                                }).ToList();
                    break;
                case "IMDb rating descending":
                    movies = _context.CategoryMovies.Where(c => c.CategoryId == id)
                                                 .OrderByDescending(c => c.Movie.IMDbRating)
                                                 .Skip((page - 1) * (int)numberOfPages)
                                                .Take((int)numberOfMoviesPerPage)
                                                .Select(c => new
                                                {
                                                    c.MovieId,
                                                    c.Movie.MovieName,
                                                    c.Movie.CoverImage,
                                                    c.Movie.IMDbRating,
                                                    c.Movie.Year,
                                                    c.Movie.Description
                                                }).ToList();
                    break;
                default:
                    movies = _context.CategoryMovies.Where(c => c.CategoryId == id).OrderBy(c => c.Movie.MovieName)

                                                .Skip((page - 1) * (int)numberOfPages)
                                                .Take((int)numberOfMoviesPerPage)
                                                .Select(c => new
                                                {
                                                    c.MovieId,
                                                    c.Movie.MovieName,
                                                    c.Movie.CoverImage,
                                                    c.Movie.IMDbRating,
                                                    c.Movie.Year,
                                                    c.Movie.Description
                                                }).ToList();
                    break;

            }
            var moviesResponseList = new List<MoviesResponse>();
            foreach (var movie in movies)
            {
                var rating = CalculateMovieRating(movie.MovieId);
                moviesResponseList.Add(new MoviesResponse()
                {
                    MovieId = movie.MovieId,
                    MovieName = movie.MovieName,
                    Year = movie.Year,
                    Rating = rating,
                    CoverImage = movie.CoverImage,
                    IMDbRating = (float)movie.IMDbRating,
                    Description = movie.Description,
                    
                });
            }
            var moviesResponsePaginated = new MoviesResponsePaginated()
            {
                Movies = moviesResponseList,
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
        public bool IsCategoryExist(int id)
        {
            return _context.Categories.Any(m => m.CategoryId == id);
        }
        public bool IsCategoryNameAlreadExist(string name)
        {
            return _context.Categories.Where(c=>c.CategoryName == name).Any();
        }
        public bool AddCategory(CategoryRequest categoryRequest)
        {
            var newCategory = _mapper.Map<Category>(categoryRequest);
            _context.Categories.Add(newCategory);
            _context.SaveChanges();
            return true;
        }
        public double NumberOfPages(int categoryId,float numberofItemsPerPage)
        {
            return Math.Ceiling(_context.CategoryMovies.Where(cm=>cm.CategoryId == categoryId).Count() / numberofItemsPerPage);
        }


    }
}
