using MoviesApp.DTO;
using MoviesApp.Models;

namespace MoviesApp.Services.ActorRepo
{
    public interface IActorService
    {
        ICollection<ActorsResponse> GetActors();
        MoviesResponsePaginated GetMoviesByActorId(int actorId,string ordering,int page);
        bool IsActorExist(int actorId);
        bool AddActor(NewActorDto actor);
        bool IsActorNameAlreadyExist(string actorName);

    }
}
