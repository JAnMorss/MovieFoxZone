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
        public MovieController(IMovieRepository movieRepo, IMapper mapper)
        {
            _movieRepo = movieRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllMovie([FromQuery] QueryObject query)
        {   
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var movie = await _movieRepo.GetAllAsync(query);

            var movieDtos = _mapper.Map<IEnumerable<MovieDto>>(movie);

            return Ok(movieDtos);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
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
        public async Task<IActionResult> CreateMovie([FromBody] CreateMovieDto createDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var movie = _mapper.Map<Movie>(createDto);

            var createdmovie = await _movieRepo.CreateAsync(movie);

            return CreatedAtAction(nameof(GetById), new { id = createdmovie.Id }, _mapper.Map<MovieDto>(createdmovie));
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateMovie([FromRoute] int id, [FromBody] UpdateMovieDto updateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updatemovie = await _movieRepo.UpdateAsync(id, updateDto);

            if (updatemovie == null) return NotFound();

            return Ok(_mapper.Map<MovieDto>(updatemovie));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteMovie([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var deleteMovie = await _movieRepo.DeleteAsync(id);

            if (deleteMovie == null) return NotFound();

            return NoContent();

        }
    }
}
