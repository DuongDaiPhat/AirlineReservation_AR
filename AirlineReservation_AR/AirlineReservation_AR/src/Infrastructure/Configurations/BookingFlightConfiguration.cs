using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineReservation_AR.src.AirlineReservation.Domain.Entities
{
    public class BookingFlightConfiguration : IEntityTypeConfiguration<BookingFlight>
    {
        public void Configure(EntityTypeBuilder<BookingFlight> builder)
        {
            builder.ToTable("BookingFlights");

            builder.HasKey(bf => bf.BookingFlightId);

            builder.Property(bf => bf.BookingFlightId)
                .HasColumnName("BookingFlightID")
                .ValueGeneratedOnAdd();

            builder.Property(bf => bf.BookingId)
                .HasColumnName("BookingID");

            builder.Property(bf => bf.FlightId)
                .HasColumnName("FlightID");

            builder.Property(bf => bf.TripType)
                .HasMaxLength(10)
                .HasDefaultValue("OneWay");

            builder.HasCheckConstraint("CK_Trip_Type", "[TripType] IN ('OneWay','Outbound','Return')");

            builder.HasOne(bf => bf.Booking)
                .WithMany(b => b.BookingFlights)
                .HasForeignKey(bf => bf.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(bf => bf.Flight)
                .WithMany(f => f.BookingFlights)
                .HasForeignKey(bf => bf.FlightId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(bf => bf.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Booked");

            builder.HasCheckConstraint(
                "CK_BookingFlight_Status",
                "[Status] IN ('Booked','Rescheduled','Cancelled')"
            );

        }
    }
}
