using MoviesApp.DTO;
using MoviesApp.Models;

namespace MoviesApp.Services.DirectorRepo
{
    public interface IDirectorService
    {
        ICollection<DirectorResponse> GetDirectors();
        MoviesResponsePaginated GetMoviesByDirectoryId(int id,int page);
        bool AddDirector(DirectorRequest directorRequest);
        bool IsDirectorExist(int id);
        bool DirectorNameExist(string name);
        double NumberOfPages(int id,float numberOfMoviesPerPage);
    }
}
