using MoviesApp.Models;

namespace MoviesApp.DTO
{
    public class MovieDetails
    {
        public int MovieId { get;set; }
        public string MovieName { get;set; }
        public int Year { get; set; }
        public float Rating { get; set; }
        public string CoverImage { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int DirectorId { get;set; }
        public string DirectorName { get; set; }=string.Empty;
        public List<ActorsInMovieDetalis> Actors { get; set; }
        public List<Image> Images { get; set; }
        public List<CategoryResponse> Categories { get; set; }

    }
}
