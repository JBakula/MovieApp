using AutoMapper;
using MoviesApp.DTO;
using MoviesApp.Models;

namespace MoviesApp.Helper
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Actor, ActorsResponse>();
            CreateMap<Category, CategoryResponse>();
            CreateMap<Director, DirectorResponse>();
            CreateMap<DirectorRequest, Director>();
            CreateMap<NewActorDto, Actor>();
            CreateMap<ActorRequest, Actor>();
            CreateMap<CategoryRequest, Category>();
        }
    }
}
