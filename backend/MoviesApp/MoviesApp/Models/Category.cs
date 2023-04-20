using System.ComponentModel.DataAnnotations;

namespace MoviesApp.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; } = string.Empty;
        public ICollection<CategoryMovie> CategoryMovies { get; set; }

    }
}
