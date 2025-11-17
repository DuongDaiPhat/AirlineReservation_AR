using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineReservation_AR.src.AirlineReservation.Domain.Entities
{
    public class SeatClassConfiguration : IEntityTypeConfiguration<SeatClass>
    {
        public void Configure(EntityTypeBuilder<SeatClass> builder)
        {
            builder.ToTable("SeatClasses");

            builder.HasKey(sc => sc.SeatClassId);

            builder.Property(sc => sc.SeatClassId)
                .HasColumnName("SeatClassID")
                .ValueGeneratedOnAdd();

            builder.Property(sc => sc.ClassName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(sc => sc.DisplayName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(sc => sc.PriceMultiplier)
                .HasColumnType("decimal(4,2)")
                .HasDefaultValue(1.0m);

            builder.Property(sc => sc.BaggageAllowanceKg)
                .HasColumnName("BaggageAllowance_KG")
                .HasDefaultValue(20);

            builder.Property(sc => sc.CabinBaggageAllowanceKg)
                .HasColumnName("CabinBaggageAllowance_KG")
                .HasDefaultValue(7);

            builder.Property(sc => sc.Description)
                .HasMaxLength(255);

            builder.Property(sc => sc.Features)
                .HasMaxLength(500);

            builder.HasIndex(sc => sc.ClassName)
                .IsUnique();

            builder.HasCheckConstraint("CK_PriceMultiplier_Valid", "[PriceMultiplier] > 0");
        }
    }
}
