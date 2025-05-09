﻿using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.DTOs.Movie;
using MovieApi.Helpers;
using MovieApi.Interface;
using MovieApi.Models;

namespace MovieApi.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieDbContext _context;
        public MovieRepository(MovieDbContext context)
        {
            _context = context;
        }
        public async Task<Movie?> CreateAsync(Movie movie)
        {
            await _context.AddAsync(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        public async Task<Movie?> DeleteAsync(int id)
        {
            var deleteMovie = await _context.Movies.FirstOrDefaultAsync(c => c.Id == id);

            if (deleteMovie == null) return null;

            _context.Movies.Remove(deleteMovie);
            await _context.SaveChangesAsync();
            return deleteMovie;
        }

        public async Task<IEnumerable<Movie>> GetAllAsync(QueryObject query)
        {
            IQueryable<Movie> movie = _context.Movies.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Title))
            {
                movie = movie.Where(s => s.Title.Contains(query.Title));
            }

            if (!string.IsNullOrWhiteSpace(query.Genre))
            {
                movie = movie.Where(s => s.Genre.Contains(query.Genre));
            }
                
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                switch (query.SortBy.ToLower())
                {
                    case "title":
                        movie = query.IsDecsending ? movie.OrderByDescending(s => s.Title) : movie.OrderBy(s => s.Title);
                        break;
                    case "genre":
                        movie = query.IsDecsending ? movie.OrderByDescending(s => s.Genre) : movie.OrderBy(s => s.Genre);
                        break;
                    case "director":
                        movie = query.IsDecsending ? movie.OrderByDescending(s => s.Director) : movie.OrderBy(s => s.Director);
                        break;
                    default:
                        throw new ArgumentException("Invalid sort field.");
                }
            }
            else
            {
                movie = movie.OrderBy(s => s.Title);
            }

            //This is the calculation of Pagination
            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await movie.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Movie?> GetByIdAsync(int id)
        {
            return await _context.Movies.FirstOrDefaultAsync(i => i.Id == id);
        }

        public Task<Movie?> GetByIdAsyncNoTracking(int id)
        {
            return _context.Movies.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Movie?> UpdateAsync(int id, UpdateMovieDto updateDto)
        {
            var existingMovie = await _context.Movies.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (existingMovie == null) return null;

            var updatedMovie = new Movie
            {
                Id = id, 
                Title = updateDto.Title,
                Genre = updateDto.Genre,
                Director = updateDto.Director,
                Year = updateDto.Year,
                Description = updateDto.Description,
                Image = updateDto.ImageUrl 
            };

            _context.Movies.Update(updatedMovie);
            await _context.SaveChangesAsync();

            return updatedMovie;
        }


    }
}
