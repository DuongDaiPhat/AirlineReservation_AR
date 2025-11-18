using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineReservation_AR.src.AirlineReservation.Domain.Entities
{
    public class AircraftTypeConfiguration : IEntityTypeConfiguration<AircraftType>
    {
        public void Configure(EntityTypeBuilder<AircraftType> builder)
        {
            builder.ToTable("AircraftTypes");

            builder.HasKey(at => at.AircraftTypeId);

            builder.Property(at => at.AircraftTypeId)
                .HasColumnName("AircraftTypeID")
                .ValueGeneratedOnAdd();

            builder.Property(at => at.TypeName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(at => at.DisplayName)
                .HasMaxLength(100);
        }
    }
}
