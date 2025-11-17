using AirlineReservation.src.AirlineReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineReservation.src.AirlineReservation.Domain.Entities
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("Cities");

            builder.HasKey(c => c.CityCode);

            builder.Property(c => c.CityCode)
                .HasMaxLength(3)
                .IsFixedLength();

            builder.Property(c => c.CityName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.CountryCode)
                .IsRequired()
                .HasMaxLength(3)
                .IsFixedLength();

            builder.Property(c => c.IsActive)
                .HasDefaultValue(true);

            builder.HasOne(c => c.Country)
                .WithMany(co => co.Cities)
                .HasForeignKey(c => c.CountryCode)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
