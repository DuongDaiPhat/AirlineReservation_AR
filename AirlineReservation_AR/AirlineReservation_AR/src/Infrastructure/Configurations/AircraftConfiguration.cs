using AirlineReservation.src.AirlineReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineReservation.src.AirlineReservation.Domain.Entities
{
    public class AircraftConfiguration : IEntityTypeConfiguration<Aircraft>
    {
        public void Configure(EntityTypeBuilder<Aircraft> builder)
        {
            builder.ToTable("Aircraft");

            builder.HasKey(a => a.AircraftId);

            builder.Property(a => a.AircraftId)
                .HasColumnName("AircraftID")
                .ValueGeneratedOnAdd();

            builder.Property(a => a.AirlineId)
                .HasColumnName("AirlineID");

            builder.Property(a => a.AircraftTypeId)
                .HasColumnName("AircraftTypeID");

            builder.Property(a => a.AircraftName)
                .HasMaxLength(50);

            builder.HasOne(a => a.Airline)
                .WithMany(al => al.Aircraft)
                .HasForeignKey(a => a.AirlineId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.AircraftType)
                .WithMany(at => at.Aircraft)
                .HasForeignKey(a => a.AircraftTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
