using AutoMapper;
using MovieCreationAPI.Model.Domain;
using MovieCreationAPI.Model.Dto;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MovieCreationAPI.Mapping
{
    public class Automapping : Profile
    {
        public Automapping()
        {
            CreateMap<Movie, movieRequestDTO>().ReverseMap();
            CreateMap<addMovieRequestDTO, Movie>().ReverseMap();
            CreateMap<updateMovieRequestDTO, Movie>().ReverseMap();
        }
    }
}
