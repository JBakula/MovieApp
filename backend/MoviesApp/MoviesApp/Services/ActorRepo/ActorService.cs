﻿using AutoMapper;
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
        
        public MoviesResponsePaginated GetMoviesByActorId(int actorId,int page) 
        {
            var numberOfMoviesPerPage = 8f;
            var numberOfPages = NumberOfPages(actorId, numberOfMoviesPerPage);


            var movies = _context.MovieActors.Where(m => m.ActorId == actorId)
                               .Skip((page - 1) * (int)numberOfPages)
                               .Take((int)numberOfMoviesPerPage)
                               .Select(m => new
                               {
                                   m.Movie.MovieId,
                                   m.Movie.MovieName,
                                   m.Movie.CoverImage,
                                   m.Movie.Year,
                                   m.Movie.Description

                               }).ToList();
            

            var moviesResponeList = new List<MoviesResponse>();
            foreach (var movie in movies)
            {
                var rating = CountMovieRating(movie.MovieId);
                moviesResponeList.Add(new MoviesResponse()
                {
                    MovieId = movie.MovieId,
                    MovieName = movie.MovieName,
                    Rating = rating,
                    Year = movie.Year,
                    CoverImage = movie.CoverImage,
                    Description = movie.Description
                });
            }

            var moviesResponsePaginated = new MoviesResponsePaginated()
            {
                Movies = moviesResponeList,
                CurrentPage = page,
                NumberOfPages = (int)numberOfPages
            };

            return moviesResponsePaginated;
        }
        public double NumberOfPages(int id, float numberOfMoviesPerPage)
        {
            return Math.Ceiling((_context.MovieActors.Where(ma=>ma.ActorId == id).Count() / numberOfMoviesPerPage));
        }
        private float CountMovieRating(int id)
        {
            if (NumberOfRatings(id) == 0)
            {
                return 0;
            }
            return _context.Ratings.Where(r => r.MovieId == id).Select(r => r.Value).Sum()
                                            / NumberOfRatings(id);
        }
        private int NumberOfRatings(int id)
        {
            return _context.Ratings.Where(r => r.MovieId == id).Count();
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
