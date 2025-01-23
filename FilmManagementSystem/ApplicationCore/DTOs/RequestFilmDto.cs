namespace ApplicationCore.DTOs
{
    public class RequestFilmDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }
        public int ReleaseYear { get; set; }
        public double Rating { get; set; }
    }
}
