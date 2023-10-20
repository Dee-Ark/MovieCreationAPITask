using Microsoft.EntityFrameworkCore;
using MovieCreationAPI.Data;
using MovieCreationAPI.Interface;
using MovieCreationAPI.Model.Domain;
using MovieCreationAPI.Model.Dto;

namespace MovieCreationAPI.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDBContext _dBContext;
        public MovieRepository(ApplicationDBContext dBContext)
        {
            _dBContext = dBContext;
        }
        public async Task<Movie?> CreateMovie(Movie movieRequestDTO)
        {
            if (movieRequestDTO.Photo != null)
            {
                movieRequestDTO.Photo = await ConvertPhotoToBase64StringAsync(movieRequestDTO.Photo);
            }

            await _dBContext.movie.AddAsync(movieRequestDTO);
            await _dBContext.SaveChangesAsync();

            return movieRequestDTO;
        }

        private async Task<string> ConvertPhotoToBase64StringAsync(IFormFile photo)
        {
            using (var stream = new MemoryStream())
            {
                await photo.CopyToAsync(stream);
                byte[] bytes = stream.ToArray();
                return Convert.ToBase64String(bytes);
            }
        }

        public async Task<Movie?> DeleteRegionAsync(long Id)
        {
            var deleteMovies = await _dBContext.movie.FirstOrDefaultAsync(x => x.Id == Id);


            if (deleteMovies == null)
            {
                return null;
            }
            _dBContext.movie.Remove(deleteMovies);
            await _dBContext.SaveChangesAsync();
            var movieDTO = new movieRequestDTO()
            {
                Name = deleteMovies.Name,
                Country = deleteMovies.Country,
                Rating = deleteMovies.Rating,
                Description = deleteMovies.Description,
                ReleaseDate = deleteMovies.ReleaseDate,
                Genre = deleteMovies.Genre.Roman,
                TicketPrice = deleteMovies.TicketPrice,
                Photo = deleteMovies.Photo.Name
            };
            return deleteMovies;
        }

        public async Task<List<Movie>> GetAllAsync()
        {
            var getMovies = await _dBContext.movie.ToListAsync();
            return getMovies;
        }

        public async Task<Movie?> GetByIdAsync(long Id)
        {
            var movies = await _dBContext.movie.FirstOrDefaultAsync(x => x.Id == Id);
            return movies;
        }

        public async Task<Movie?> UpdateRegionAsync(long Id, Movie movieRequestDTO)
        {
            var existingMovies = await _dBContext.movie.FirstOrDefaultAsync(b => b.Id == Id);
            if (existingMovies == null)
            {
                return null;
            }

            existingMovies.Name = movieRequestDTO.Name;
            existingMovies.ReleaseDate = movieRequestDTO.ReleaseDate;
            existingMovies.TicketPrice = movieRequestDTO.TicketPrice;
            existingMovies.Rating = movieRequestDTO.Rating;
            existingMovies.Description = movieRequestDTO.Description;
            existingMovies.Country = movieRequestDTO.Country;
            existingMovies.Genre = movieRequestDTO.Genre;
            existingMovies.Photo = movieRequestDTO.Photo;

            await _dBContext.SaveChangesAsync();
            return existingMovies;
        }
    }
}
