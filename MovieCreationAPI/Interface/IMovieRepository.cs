﻿using MovieCreationAPI.Model.Domain;
using MovieCreationAPI.Model.Dto;

namespace MovieCreationAPI.Interface
{
    public interface IMovieRepository
    {
        Task<List<Movie>> GetAllAsync();
        Task<Movie?> GetByIdAsync(long Id);
        Task<Movie?> CreateRegionAsync(Movie movieRequestDTO, IFormFile image);
        Task<Movie?> UpdateRegionAsync(long Id, Movie movieRequestDTO);
        Task<Movie?> DeleteRegionAsync(long Id);
    }
}