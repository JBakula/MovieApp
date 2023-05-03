using Microsoft.EntityFrameworkCore;

namespace MoviesApp.Models
{
    public class MoviesDbContext:DbContext
    {
        public MoviesDbContext(DbContextOptions<MoviesDbContext> options) : base(options) { }

        public DbSet<Actor> Actors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryMovie> CategoryMovies { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieActor> MovieActors { get; set; }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Rating>Ratings { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}

