using AirlineReservation.src.AirlineReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineReservation.src.AirlineReservation.Domain.Entities
{
    public class PassengerConfiguration : IEntityTypeConfiguration<Passenger>
    {
        public void Configure(EntityTypeBuilder<Passenger> builder)
        {
            builder.ToTable("Passengers");

            builder.HasKey(p => p.PassengerId);

            builder.Property(p => p.PassengerId)
                .HasColumnName("PassengerID")
                .ValueGeneratedOnAdd();

            builder.Property(p => p.BookingId)
                .HasColumnName("BookingID");

            builder.Property(p => p.PassengerType)
                .HasMaxLength(10)
                .HasDefaultValue("Adult");

            builder.Property(p => p.Title)
                .HasMaxLength(10);

            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.MiddleName)
                .HasMaxLength(50);

            builder.Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.DateOfBirth)
                .HasColumnType("date");

            builder.Property(p => p.Gender)
                .HasMaxLength(1)
                .IsFixedLength();

            builder.Property(p => p.IdNumber)
                .HasMaxLength(20);

            builder.HasCheckConstraint("CK_Passenger_Type", "[PassengerType] IN ('Adult','Child','Infant')");
            builder.HasCheckConstraint("CK_Passenger_Gender", "[Gender] IS NULL OR [Gender] IN ('M','F','O')");

            builder.HasOne(p => p.Booking)
                .WithMany(b => b.Passengers)
                .HasForeignKey(p => p.BookingId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
