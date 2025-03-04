using System.ComponentModel.DataAnnotations;

namespace MovieApi.DTOs.Movie
{
    public class UpdateMovieDto
    {
        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Genre is required.")]
        [MaxLength(50, ErrorMessage = "Genre cannot exceed 50 characters.")]
        public string Genre { get; set; } = string.Empty;
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
        [MaxLength(100, ErrorMessage = "Director name cannot exceed 100 characters.")]
        public string Director { get; set; } = string.Empty;

        [Range(1888, 2100, ErrorMessage = "Year must be between 1888 and 2100.")]
        public int Year { get; set; }

        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; } = string.Empty;
    }
}
