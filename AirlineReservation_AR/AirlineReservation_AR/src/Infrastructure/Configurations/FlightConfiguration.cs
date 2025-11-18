using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineReservation_AR.src.AirlineReservation.Domain.Entities
{
    public class FlightConfiguration : IEntityTypeConfiguration<Flight>
    {
        public void Configure(EntityTypeBuilder<Flight> builder)
        {
            builder.ToTable("Flights");

            builder.HasKey(f => f.FlightId);

            builder.Property(f => f.FlightId)
                .HasColumnName("FlightID")
                .ValueGeneratedOnAdd();

            builder.Property(f => f.AirlineId)
                .HasColumnName("AirlineID");

            builder.Property(f => f.FlightNumber)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(f => f.AircraftId)
                .HasColumnName("AircraftID");

            builder.Property(f => f.DepartureAirportId)
                .HasColumnName("DepartureAirportID");

            builder.Property(f => f.ArrivalAirportId)
                .HasColumnName("ArrivalAirportID");

            builder.Property(f => f.FlightDate)
                .HasColumnType("date");

            builder.Property(f => f.DepartureTime)
                .HasColumnType("time");

            builder.Property(f => f.ArrivalTime)
                .HasColumnType("time");

            builder.Property(f => f.DurationMinutes);

            builder.Property(f => f.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Available");

            builder.Property(f => f.BasePrice)
                .HasColumnType("decimal(12,2)");

            builder.HasIndex(f => new { f.FlightNumber, f.FlightDate })
                .IsUnique()
                .HasDatabaseName("IX_Flights_Number_Date");

            builder.HasIndex(f => new { f.FlightDate, f.Status })
                .HasDatabaseName("IX_Flights_Date_Status");

            builder.HasIndex(f => new { f.DepartureAirportId, f.ArrivalAirportId })
                .HasDatabaseName("IX_Flights_Airports");

            builder.HasIndex(f => f.AirlineId)
                .HasDatabaseName("IX_Flights_Airline");

            builder.HasIndex(f => f.AircraftId)
                .HasDatabaseName("IX_Flights_Aircraft");

            builder.HasCheckConstraint("CK_Different_Airports", "[DepartureAirportID] <> [ArrivalAirportID]");
            builder.HasCheckConstraint("CK_Flight_Status", "[Status] IN ('Available','Full','Cancelled')");
            builder.HasCheckConstraint("CK_BasePrice_Positive", "[BasePrice] > 0");

            builder.HasOne(f => f.Airline)
                .WithMany(a => a.Flights)
                .HasForeignKey(f => f.AirlineId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(f => f.Aircraft)
                .WithMany(a => a.Flights)
                .HasForeignKey(f => f.AircraftId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(f => f.DepartureAirport)
                .WithMany(a => a.DepartingFlights)
                .HasForeignKey(f => f.DepartureAirportId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(f => f.ArrivalAirport)
                .WithMany(a => a.ArrivingFlights)
                .HasForeignKey(f => f.ArrivalAirportId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
