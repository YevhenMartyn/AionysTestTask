using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Entities;

namespace Infrastructure.Data.DataConfigurations
{
    public class FilmConfiguration : IEntityTypeConfiguration<Film>
    {
        public void Configure(EntityTypeBuilder<Film> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(f => f.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(f => f.Director)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(f => f.Genre)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(f => f.ReleaseYear)
                .IsRequired();

            builder.Property(f => f.Rating)
                .IsRequired()
                .HasPrecision(3, 1);
        }
    }
}
