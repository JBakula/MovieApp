using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApp.DTO;
using MoviesApp.Services.CategoryRepo;

namespace MoviesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService) 
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public IActionResult GetCategories() 
        {
            return Ok(_categoryService.GetCategories());
        }
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetMoviesByCategoryId([FromRoute]int id, [FromQuery] string ordering,[FromQuery]int page=1)
        {
            
            if (!_categoryService.IsCategoryExist(id))
            {
                return NotFound("Kategorija ne postoji");
            }
            return Ok(_categoryService.GetMoviesByCategoryId(id,ordering,page));
        }
        [HttpPost]
        public IActionResult AddCategories(CategoryRequest categoryRequest)
        {
            if(categoryRequest.CategoryName == "")
            {
                return BadRequest();
            }
            if(_categoryService.IsCategoryNameAlreadExist(categoryRequest.CategoryName)) {
                return BadRequest();    
            }
            if (_categoryService.AddCategory(categoryRequest))
            {
                return Ok("Category successfully added");
            }
            else
            {
                return StatusCode(500);
            }
        }
    }
}
