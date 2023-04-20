using AutoMapper;
using MoviesApp.DTO;
using MoviesApp.Models;

namespace MoviesApp.Services.ActorRepo
{
    public class ActorService:IActorService
    {
        private readonly MoviesDbContext _context;
        private readonly IMapper _mapper;
        public ActorService(MoviesDbContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        public ICollection<ActorsResponse> GetActors() 
        {
            var actors = _context.Actors.OrderBy(a => a.ActorName).ToList();
            var actorsDto = _mapper.Map<List<ActorsResponse>>(actors);

            return actorsDto;
        }
        
        public ICollection<MoviesResponse> GetMoviesByActorId(int actorId) 
        {
            var movies = _context.MovieActors.Where(ma => ma.ActorId == actorId)
                                            .Select(ma => new
                                            {
                                                ma.Movie.MovieId,
                                                ma.Movie.CoverImage,
                                                ma.Movie.Year,
                                                ma.Movie.MovieName,
                                                ma.Movie.Description
                                            })
                                            .ToList();
            var moviesResponseList = new List<MoviesResponse>();
            foreach (var movie in movies)
            {
                moviesResponseList.Add(new MoviesResponse()
                {
                    MovieId = movie.MovieId,
                    MovieName = movie.MovieName,
                    CoverImage = movie.CoverImage,
                    Year = movie.Year,
                    Description = movie.Description
                });
            }
            return moviesResponseList;
        }
        public bool IsActorExist(int actorId)
        {
            return _context.Actors.Where(a => a.ActorId == actorId).Any();
        }
        public bool AddActor(NewActorDto actor)
        {
            var newActor = _mapper.Map<Actor>(actor);
            _context.Actors.Add(newActor);
            _context.SaveChanges();
            return true;
        }
        public bool IsActorNameAlreadyExist(string actorName)
        {
            return _context.Actors.Where(a=>a.ActorName == actorName).Any();
        }
       
    }
}
