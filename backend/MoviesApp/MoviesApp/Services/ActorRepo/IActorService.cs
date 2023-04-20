using MoviesApp.DTO;
using MoviesApp.Models;

namespace MoviesApp.Services.ActorRepo
{
    public interface IActorService
    {
        ICollection<ActorsResponse> GetActors();
        ICollection<MoviesResponse>GetMoviesByActorId(int actorId);
        bool IsActorExist(int actorId);
        bool AddActor(NewActorDto actor);
        bool IsActorNameAlreadyExist(string actorName);

    }
}
