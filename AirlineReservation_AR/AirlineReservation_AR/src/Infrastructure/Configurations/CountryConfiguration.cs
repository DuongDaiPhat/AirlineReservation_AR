using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineReservation_AR.src.AirlineReservation.Domain.Entities
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("Countries");

            builder.HasKey(c => c.CountryCode);

            builder.Property(c => c.CountryCode)
                .HasMaxLength(3)
                .IsFixedLength();

            builder.Property(c => c.CountryName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Currency)
                .HasMaxLength(3)
                .IsFixedLength();

            builder.Property(c => c.IsActive)
                .HasDefaultValue(true);
        }
    }
}
