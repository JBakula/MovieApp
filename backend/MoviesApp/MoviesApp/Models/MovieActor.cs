using System.ComponentModel.DataAnnotations;

namespace MoviesApp.Models
{
    public class MovieActor
    {
        [Key]
        public int MovieActorId { get; set; }
        public int ActorId { get; set;}
        public Actor Actor { get; set;}
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public bool? Star { get; set; }

    }
}
