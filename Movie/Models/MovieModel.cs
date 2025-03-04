namespace Movie.Models
{
    public class MovieModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; } = ""; 
        public string Genre { get; set; }
        public string Director { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
    }
}
