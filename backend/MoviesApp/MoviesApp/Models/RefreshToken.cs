using System.ComponentModel.DataAnnotations;

namespace MoviesApp.Models
{
    public class RefreshToken
    {
        [Key]
        public int RefreshTokenId { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
 