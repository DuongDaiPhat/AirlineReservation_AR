using AirlineReservation.src.AirlineReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineReservation.src.AirlineReservation.Domain.Entities
{
    public class BookingServiceConfiguration : IEntityTypeConfiguration<BookingService>
    {
        public void Configure(EntityTypeBuilder<BookingService> builder)
        {
            builder.ToTable("BookingServices");

            builder.HasKey(bs => bs.BookingServiceId);

            builder.Property(bs => bs.BookingServiceId)
                .HasColumnName("BookingServiceID")
                .ValueGeneratedOnAdd();

            builder.Property(bs => bs.BookingId)
                .HasColumnName("BookingID");

            builder.Property(bs => bs.ServiceId)
                .HasColumnName("ServiceID");

            builder.Property(bs => bs.PassengerId)
                .HasColumnName("PassengerID");

            builder.Property(bs => bs.Quantity)
                .HasDefaultValue(1);

            builder.Property(bs => bs.UnitPrice)
                .HasColumnType("decimal(10,2)");

            builder.HasCheckConstraint("CK_Quantity_Positive", "[Quantity] > 0");

            builder.HasOne(bs => bs.Booking)
                .WithMany(b => b.BookingServices)
                .HasForeignKey(bs => bs.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(bs => bs.Service)
                .WithMany(s => s.BookingServices)
                .HasForeignKey(bs => bs.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(bs => bs.Passenger)
                .WithMany(p => p.BookingServices)
                .HasForeignKey(bs => bs.PassengerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
