using AutoMapper;
using MoviesApp.DTO;
using MoviesApp.Models;

namespace MoviesApp.Services.DirectorRepo
{
    public class DirectorService:IDirectorService
    {
        private readonly MoviesDbContext _context;
        private readonly IMapper _mapper;
        public DirectorService(MoviesDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public ICollection<DirectorResponse> GetDirectors()
        {
            var directors = _context.Directors.OrderBy(d=>d.DirectorName).ToList();
            return _mapper.Map<List<DirectorResponse>>(directors);
        }
        public ICollection<Movie> GetMoviesByDirectoryId(int id)
        {
            return _context.Movies.Where(m=>m.DirectorId == id).ToList();
        }
        public bool IsDirectorExist(int id)
        {
            return _context.Movies.Any(m => m.DirectorId == id);
        }
        public bool AddDirector(DirectorRequest directorRequest)
        {
            var director = _mapper.Map<Director>(directorRequest);
            _context.Directors.Add(director);
            _context.SaveChanges();
            return true;
        }
        public bool DirectorNameExist(string name)
        {
            return _context.Directors.Where(d=>d.DirectorName == name).Any();
        }
    }
}
