using MoviesApp.DTO;
using MoviesApp.Models;

namespace MoviesApp.Services.CategoryRepo
{
    public interface ICategoryService
    {
        ICollection<CategoryResponse> GetCategories();
        MoviesResponsePaginated GetMoviesByCategoryId(int id, string ordering,int page);
        bool IsCategoryExist(int id);
        bool IsCategoryNameAlreadExist(string name);
        bool AddCategory(CategoryRequest categoryRequest);
    }
}
