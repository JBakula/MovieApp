using Microsoft.EntityFrameworkCore;
using MoviesApp.Models;
using MoviesApp.Services.ActorRepo;
using MoviesApp.Services.CategoryRepo;
using MoviesApp.Services.DirectorRepo;
using MoviesApp.Services.MovieRepo;
using MoviesApp.Services.RatingRepo;
using MoviesApp.Services.UserRepo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IActorService,ActorService>();
builder.Services.AddScoped<ICategoryService,CategoryService>();
builder.Services.AddScoped<IDirectorService,DirectorService>();
builder.Services.AddScoped<IMovieService,MovieService>();
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<IRatingService, RatingService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MoviesDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("MoviesDB")));

builder.Services.AddAutoMapper(typeof(Program).Assembly);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        builder =>
        {
            builder.WithOrigins("http://localhost",
                "http://localhost:4200",
                "https://localhost:7230",
                "http://localhost:90")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowedToAllowWildcardSubdomains();
        });
});

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();

app.MapControllers();

app.Run();
