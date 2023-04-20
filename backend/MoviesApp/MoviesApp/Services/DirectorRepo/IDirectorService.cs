using MoviesApp.DTO;
using MoviesApp.Models;

namespace MoviesApp.Services.DirectorRepo
{
    public interface IDirectorService
    {
        ICollection<DirectorResponse> GetDirectors();
        ICollection<Movie> GetMoviesByDirectoryId(int id);
        bool AddDirector(DirectorRequest directorRequest);
        bool IsDirectorExist(int id);
        bool DirectorNameExist(string name);
    }
}
