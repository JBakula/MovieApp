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
        public ICollection<MoviesResponse> GetMoviesByCategoryId(int categoryId) 
        {
            var movies = _context.CategoryMovies.Where(c=>c.CategoryId == categoryId)
                                                .Select(c => new
                                                {
                                                    c.MovieId,
                                                    c.Movie.MovieName,
                                                    c.Movie.CoverImage,
                                                    c.Movie.Year,
                                                    c.Movie.Description
                                                }).ToList();
            var moviesResponseList = new List<MoviesResponse>();
            foreach (var movie in movies)
            {
                moviesResponseList.Add(new MoviesResponse()
                {
                    MovieId = movie.MovieId,
                    MovieName = movie.MovieName,
                    Year = movie.Year,
                    CoverImage = movie.CoverImage,
                    Description = movie.Description
                });
            }
            return moviesResponseList;
                                          
            
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


    }
}
