namespace MoviesApp.DTO
{
    public class MoviesResponsePaginated
    {
        public List<MoviesResponse> Movies { get; set; }
        public int NumberOfPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
