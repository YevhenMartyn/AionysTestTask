namespace Infrastructure.Entities
{
    public class Film : BaseEntity
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string Director { get; set; }
        public required string Genre { get; set; }
        public required int ReleaseYear { get; set; }
        public required double Rating { get; set; }

    }
}
