using MovieApi.DTOs.Movie;
using MovieApi.Helpers;
using MovieApi.Models;

namespace MovieApi.Interface
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetAllAsync(QueryObject query);
        Task<Movie?> GetByIdAsync(int id); 
        Task<Movie?> CreateAsync(Movie movie);
        Task<Movie?> UpdateAsync(int id, UpdateMovieDto updateDto);
        Task<Movie?> DeleteAsync(int id);
    }
}
