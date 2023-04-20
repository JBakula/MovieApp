using System.ComponentModel.DataAnnotations;

namespace MoviesApp.Models
{
    public class User
    {
        [Key] 
        public int UserId { get;set; }
        public string Name { get; set; } = string.Empty;
        public string Lastname { get;set; }=string.Empty;
        public string Email { get; set; } = string.Empty;
        
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public ICollection<Rating> Ratings { get; set; }
    }
}
