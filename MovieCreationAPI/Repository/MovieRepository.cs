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
        //public async Task<Movie?> CreateMovie(Movie movieRequestDTO, IFormFile file)
        //{
        //    if (file != null && file.Length > 0)
        //    {
        //        string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
        //        string filePath = Path.Combine("your_upload_directory", uniqueFileName);

        //        using (var stream = new FileStream(filePath, FileMode.Create))
        //        {
        //            await file.CopyToAsync(stream);
        //        }

        //        // Update the 'Movie' entity to store the file path.
        //        movieRequestDTO.Photo = filePath.ToString();
        //    }

        //    // Add the modified 'movieRequestDTO' to the context and save it.
        //    await _dBContext.movie.AddAsync(movieRequestDTO);
        //    await _dBContext.SaveChangesAsync();

        //    return movieRequestDTO;
        //}


        public async Task<Movie?> CreateMovie(Movie movieRequestDTO, IFormFile photo)
        {
            if (photo != null && photo.Length > 0)
            {
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                string filePath = Path.Combine("ImagesFolder", uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await photo.CopyToAsync(stream);
                }

                // Update the 'Movie' entity to store the file path.
                movieRequestDTO.Photo = filePath;

                // Add the modified 'movieRequestDTO' to the context and save it.
                await _dBContext.movie.AddAsync(movieRequestDTO);
                await _dBContext.SaveChangesAsync();
            }

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
                Genres = deleteMovies.Genres,
                TicketPrice = deleteMovies.TicketPrice,
                Photo = deleteMovies.Photo
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
            existingMovies.Genres = movieRequestDTO.Genres;
            existingMovies.Photo = movieRequestDTO.Photo;

            await _dBContext.SaveChangesAsync();
            return existingMovies;
        }
    }
}
