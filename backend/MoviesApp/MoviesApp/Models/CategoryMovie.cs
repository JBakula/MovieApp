using System.ComponentModel.DataAnnotations;

namespace MoviesApp.Models
{
    public class CategoryMovie
    {
        [Key]
        public int CategoryMovieId { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } 
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
