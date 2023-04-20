using System.ComponentModel.DataAnnotations;

namespace MoviesApp.Models
{
    public class Rating
    {
        [Key]
        public int RatingId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public int Value { get; set; }
    }
}
