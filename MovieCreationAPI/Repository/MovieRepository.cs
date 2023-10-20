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
        public async Task<Movie?> CreateRegionAsync(Movie movieRequestDTO, IFormFile image)
        {
            if (image != null)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    await image.CopyToAsync(memoryStream);
                    byte[] imageBytes = memoryStream.ToArray();
                    string base64String = Convert.ToBase64String(imageBytes);
                    movieRequestDTO.ImageBase64 = base64String;
                }
            }
            await _dBContext.movie.AddAsync(movieRequestDTO);
            await _dBContext.SaveChangesAsync();
            return movieRequestDTO;
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
                Genre = deleteMovies.Genre,
                TicketPrice = deleteMovies.TicketPrice,
                ImageBase64 = deleteMovies.ImageBase64
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
            existingMovies.ImageBase64 = movieRequestDTO.ImageBase64;

            await _dBContext.SaveChangesAsync();
            return existingMovies;
        }
    }
}
