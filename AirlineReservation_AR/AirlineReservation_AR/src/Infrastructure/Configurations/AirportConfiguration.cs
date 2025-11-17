using AirlineReservation.src.AirlineReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineReservation.src.AirlineReservation.Domain.Entities
{
    public class AirportConfiguration : IEntityTypeConfiguration<Airport>
    {
        public void Configure(EntityTypeBuilder<Airport> builder)
        {
            builder.ToTable("Airports");

            builder.HasKey(a => a.AirportId);

            builder.Property(a => a.AirportId)
                .HasColumnName("AirportID")
                .ValueGeneratedOnAdd();

            builder.Property(a => a.AirportName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.IataCode)
                .IsRequired()
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("IATACode");

            builder.Property(a => a.CityCode)
                .IsRequired()
                .HasMaxLength(3)
                .IsFixedLength();

            builder.Property(a => a.IsActive)
                .HasDefaultValue(true);

            builder.HasIndex(a => a.IataCode)
                .IsUnique();

            builder.HasOne(a => a.City)
                .WithMany(c => c.Airports)
                .HasForeignKey(a => a.CityCode)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(a => a.DepartingFlights)
                .WithOne(f => f.DepartureAirport)
                .HasForeignKey(f => f.DepartureAirportId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(a => a.ArrivingFlights)
                .WithOne(f => f.ArrivalAirport)
                .HasForeignKey(f => f.ArrivalAirportId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
