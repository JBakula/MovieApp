using MoviesApp.DTO;
using MoviesApp.Models;

namespace MoviesApp.Services.CategoryRepo
{
    public interface ICategoryService
    {
        ICollection<CategoryResponse> GetCategories();
        ICollection<MoviesResponse> GetMoviesByCategoryId(int categoryId);
        bool IsCategoryExist(int id);
        bool IsCategoryNameAlreadExist(string name);
        bool AddCategory(CategoryRequest categoryRequest);
    }
}
