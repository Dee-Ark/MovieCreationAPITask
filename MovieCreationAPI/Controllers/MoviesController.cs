using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieCreationAPI.CustomeValidateAttribute;
using MovieCreationAPI.Interface;
using MovieCreationAPI.Model.Domain;
using MovieCreationAPI.Model.Dto;
using MovieCreationAPI.Repository;
using System.Text.Json;

namespace MovieCreationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<MoviesController> _logger;
        private List<Movie> _movies = new List<Movie>();

        public MoviesController(IMovieRepository movieRepository, IMapper mapper, ILogger<MoviesController> logger)
        {
            _repository = movieRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                _logger.LogInformation("GetAllRegions Method was called");
                var moviesRes = await _repository.GetAllAsync();

                var moviesDTO = _mapper.Map<List<movieDTO>>(moviesRes);
                _logger.LogInformation($"Fetch data from databse: {JsonSerializer.Serialize(moviesDTO)}");
                return Ok(moviesDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error has occured, please, check again", ex.Message);
            }
            return BadRequest();
        }


        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] long Id)
        {
            try
            {
                _logger.LogInformation("GetById Method was called");
                var movies = await _repository.GetByIdAsync(Id);
                if (movies == null)
                {
                    return NotFound();
                }
                var moviesDTO = _mapper.Map<movieDTO>(movies);
                _logger.LogInformation($"Fetch data from databse by MovieId: {JsonSerializer.Serialize(moviesDTO)}");
                return Ok(moviesDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error has occured, please, check again", ex.Message);
            }
            return BadRequest();
        }

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> CreateMovie([FromForm] addMovieRequestDTO requestDTO, IFormFile Photo)
        {
            try
            {
                if (requestDTO.Rating < 1 || requestDTO.Rating > 5)
                {
                    return BadRequest(requestDTO);
                }
                else
                {
                    _logger.LogInformation("To Create a movie Method was called");
                    var movieMap = _mapper.Map<Movie>(requestDTO);

                    movieMap = await _repository.CreateMovie(movieMap, Photo);

                    var moviesDTO = _mapper.Map<movieDTO>(movieMap);

                    _logger.LogInformation($"Creating data into the databse: {JsonSerializer.Serialize(moviesDTO)}");
                    return Ok(moviesDTO);
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError("An error has occured, please, check again", ex.Message);
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("{Id}")]
        [ValidateModelState]
        public async Task<IActionResult> UpdateUserRegion([FromRoute] long Id, [FromBody] updateMovieRequestDTO updateRegionRequest)
        {
            try
            {
                _logger.LogInformation("To Update a movie Method was called");

                var movieDomainModel = _mapper.Map<Movie>(updateRegionRequest);

                movieDomainModel = await _repository.UpdateRegionAsync(Id, movieDomainModel);

                if (movieDomainModel == null)
                {
                    return NotFound();
                }

                var moviesDTO = _mapper.Map<updateMovieRequestDTO>(movieDomainModel);

                _logger.LogInformation($"Updating data in the databse: {JsonSerializer.Serialize(moviesDTO)}");

                return Ok(moviesDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error has occured, please, check again", ex.Message);
            }
            return BadRequest();

        }


        [HttpDelete]
        [Route("{Id}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] long Id)
        {
            try
            {
                _logger.LogInformation("To Delete a movie Method was called");

                var movies = await _repository.DeleteRegionAsync(Id);
                if (movies == null)
                {
                    return NotFound();
                }
                var moviesDTO = _mapper.Map<movieDTO>(movies);
                _logger.LogInformation($"Updating data in the databse: {JsonSerializer.Serialize(moviesDTO)}");
                return Ok(moviesDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error has occured, please, check again", ex.Message);
            }
            return BadRequest();
        }
    }
}
