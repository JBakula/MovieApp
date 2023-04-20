using System.ComponentModel.DataAnnotations;

namespace MoviesApp.Models
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }
        public string MovieName { get; set; } = string.Empty;
        public int Year { get; set; }
        public string CoverImage { get; set; }= string.Empty;
        public string? Description { get; set; } = string.Empty;
        public int DirectorId { get; set; }
        public Director Director { get; set; }
        public ICollection<MovieActor> MovieActors { get; set; }
        public ICollection<CategoryMovie> CategoryMovies { get; set; }
        public ICollection<Image> Images { get; set; }
        public ICollection<Rating> Ratings { get; set; }


    }
}
