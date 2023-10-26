using MovieCreationAPI.Model.Domain;
using MovieCreationAPI.Model.Dto;

namespace MovieCreationAPI.Interface
{
    public interface IMovieRepository
    {
        Task<List<Movie>> GetAllAsync();
        Task<Movie?> GetByIdAsync(long Id);
        Task<Movie?> CreateMovie(Movie movieRequestDTO, IFormFile Photo);
        Task<Movie?> UpdateMoviesAsync(long Id, Movie movieRequestDTO, IFormFile Photo);
        Task<Movie?> DeleteMoviesAsync(long Id);
    }
}
