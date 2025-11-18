using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineReservation_AR.src.AirlineReservation.Domain.Entities
{
    public class SeatConfiguration : IEntityTypeConfiguration<Seat>
    {
        public void Configure(EntityTypeBuilder<Seat> builder)
        {
            builder.ToTable("Seats");

            builder.HasKey(s => s.SeatId);

            builder.Property(s => s.SeatId)
                .HasColumnName("SeatID")
                .ValueGeneratedOnAdd();

            builder.Property(s => s.AircraftId)
                .HasColumnName("AircraftID");

            builder.Property(s => s.SeatClassId)
                .HasColumnName("SeatClassID");

            builder.Property(s => s.SeatNumber)
                .IsRequired()
                .HasMaxLength(5);

            builder.Property(s => s.IsAvailable)
                .HasDefaultValue(true);

            builder.HasIndex(s => new { s.AircraftId, s.SeatNumber })
                .IsUnique()
                .HasDatabaseName("UQ_Aircraft_Seat");

            builder.HasOne(s => s.Aircraft)
                .WithMany(a => a.Seats)
                .HasForeignKey(s => s.AircraftId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(s => s.SeatClass)
                .WithMany(sc => sc.Seats)
                .HasForeignKey(s => s.SeatClassId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
