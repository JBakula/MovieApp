using System.ComponentModel.DataAnnotations;

namespace MoviesApp.Models
{
    public class Image
    {
        [Key]
        public int ImageId { get; set; }    
        public string ImageName { get; set; }=string.Empty;
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
