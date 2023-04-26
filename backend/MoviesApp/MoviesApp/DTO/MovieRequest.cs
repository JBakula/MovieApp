using MoviesApp.Models;

namespace MoviesApp.DTO
{
    public class MovieRequest
    {
        public string MovieName { get; set; }=string.Empty;
        public int Year { get; set; }
        public IFormFile CoverImage { get; set; }
        public float IMDbRating { get; set; }
        public int DirectorId { get; set; }
        public string? Description { get; set; }= string.Empty;
        //public List<Category> Categories { get;set; }
        //public List<Actor> Actors { get; set; }
        public int[] Categories { get; set; } 
        public int[] StarActors { get; set; }
        public int[] Actors { get; set; }
        
        public IFormFile[] Images { get; set; }

    }
}
