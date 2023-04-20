using System.ComponentModel.DataAnnotations;

namespace MoviesApp.Models
{
    public class Actor
    {
        [Key]
        public int ActorId { get; set; }
        [Required]
        public string ActorName { get; set; } = string.Empty;

        public ICollection<MovieActor> MovieActors { get; set; }


    }
}
