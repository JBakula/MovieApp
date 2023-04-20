namespace MoviesApp.DTO
{
    public class MoviesResponse
    {
        public int MovieId { get; set; }
        public string MovieName { get; set;}=string.Empty;
        public int Year { get; set; }
        public float Rating { get; set; }
        public string CoverImage { get; set; }
        public string Description { get; set; }
    }
}
