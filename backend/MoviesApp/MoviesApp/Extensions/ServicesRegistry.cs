using MoviesApp.Services.ActorRepo;
using MoviesApp.Services.CategoryRepo;
using MoviesApp.Services.DirectorRepo;
using MoviesApp.Services.MovieRepo;
using MoviesApp.Services.RatingRepo;
using MoviesApp.Services.UserRepo;

namespace MoviesApp.Extensions
{
    public static class ServicesRegistry
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services) {

            services.AddScoped<IActorService, ActorService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IDirectorService, DirectorService>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRatingService, RatingService>();

            return services;
        }
    }
}
