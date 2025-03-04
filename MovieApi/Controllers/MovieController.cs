using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApi.DTOs.Movie;
using MovieApi.Helpers;
using MovieApi.Interface;
using MovieApi.Models;

namespace MovieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _movieRepo;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        public MovieController(IMovieRepository movieRepo, IMapper mapper, IPhotoService photoService)
        {
            _movieRepo = movieRepo;
            _mapper = mapper;
            _photoService = photoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMovie([FromQuery] QueryObject query)
        {   
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var movie = await _movieRepo.GetAllAsync(query);

            var movieDtos = _mapper.Map<IEnumerable<MovieDto>>(movie);

            return Ok(movieDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var movies = await _movieRepo.GetByIdAsync(id);

            if (movies == null) return NotFound();

            var movieDto = _mapper.Map<MovieDto>(movies);

            return Ok(movieDto);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateMovie([FromForm] CreateMovieDto createDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var movieModel = _mapper.Map<Movie>(createDto);

            if (createDto.Image != null)
            {
                var result = await _photoService.AddPhotoAsync(createDto.Image);
                if (result.Error != null)
                {
                    return BadRequest(new { error = result.Error.Message });
                }

                movieModel.Image = result.Url.ToString();
            }

            var createdMovie = await _movieRepo.CreateAsync(movieModel);
            return CreatedAtAction(nameof(GetById), new { id = createdMovie.Id }, _mapper.Map<MovieDto>(createdMovie));
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateMovie([FromRoute] int id, [FromForm] UpdateMovieDto updateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existingMovie = await _movieRepo.GetByIdAsyncNoTracking(id);
            if (existingMovie == null) return NotFound();

            if (updateDto.Image != null)
            {
                if (!string.IsNullOrEmpty(existingMovie.Image))
                {
                    await _photoService.DeletePhotoAsync(existingMovie.Image);
                }

                var result = await _photoService.AddPhotoAsync(updateDto.Image);
                if (result.Error != null)
                {
                    return BadRequest(new { error = result.Error.Message });
                }

                updateDto.ImageUrl = result.Url.ToString(); 
            }
            else
            {
                updateDto.ImageUrl = existingMovie.Image; 
            }

            var updatedMovie = await _movieRepo.UpdateAsync(id, updateDto);
            if (updatedMovie == null) return NotFound();

            return Ok(_mapper.Map<MovieDto>(updatedMovie));
        }


        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteMovie([FromRoute] int id)
        {
            var movie = await _movieRepo.GetByIdAsync(id);
            if (movie == null) return NotFound();

            if (!string.IsNullOrEmpty(movie.Image))
            {
                await _photoService.DeletePhotoAsync(movie.Image);
            }

            await _movieRepo.DeleteAsync(id);
            return NoContent();
        }
    }
}
