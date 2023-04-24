using MoviesApp.DTO;
using MoviesApp.Models;

namespace MoviesApp.Services.ActorRepo
{
    public interface IActorService
    {
        ICollection<ActorsResponse> GetActors();
        MoviesResponsePaginated GetMoviesByActorId(int actorId,int page);
        bool IsActorExist(int actorId);
        bool AddActor(NewActorDto actor);
        bool IsActorNameAlreadyExist(string actorName);

    }
}
