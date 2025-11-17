using AirlineReservation.src.AirlineReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineReservation.src.AirlineReservation.Domain.Entities
{
    public class AircraftSeatConfigConfiguration : IEntityTypeConfiguration<AircraftSeatConfig>
    {
        public void Configure(EntityTypeBuilder<AircraftSeatConfig> builder)
        {
            builder.ToTable("AircraftSeatConfig");

            builder.HasKey(sc => sc.ConfigId);

            builder.Property(sc => sc.ConfigId)
                .HasColumnName("ConfigID")
                .ValueGeneratedOnAdd();

            builder.Property(sc => sc.AircraftId)
                .HasColumnName("AircraftID");

            builder.Property(sc => sc.SeatClassId)
                .HasColumnName("SeatClassID");

            builder.Property(sc => sc.SeatCount)
                .IsRequired();

            builder.HasIndex(sc => new { sc.AircraftId, sc.SeatClassId })
                .IsUnique();

            builder.HasCheckConstraint("CK_SeatCount_Positive", "[SeatCount] > 0");

            builder.HasOne(sc => sc.Aircraft)
                .WithMany(a => a.SeatConfigurations)
                .HasForeignKey(sc => sc.AircraftId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(sc => sc.SeatClass)
                .WithMany(seatClass => seatClass.SeatConfigurations)
                .HasForeignKey(sc => sc.SeatClassId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
