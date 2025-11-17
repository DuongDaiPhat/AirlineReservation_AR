using AirlineReservation.src.AirlineReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineReservation.src.AirlineReservation.Domain.Entities
{
    public class FlightPricingConfiguration : IEntityTypeConfiguration<FlightPricing>
    {
        public void Configure(EntityTypeBuilder<FlightPricing> builder)
        {
            builder.ToTable("FlightPricing");

            builder.HasKey(fp => fp.PricingId);

            builder.Property(fp => fp.PricingId)
                .HasColumnName("PricingID")
                .ValueGeneratedOnAdd();

            builder.Property(fp => fp.FlightId)
                .HasColumnName("FlightID");

            builder.Property(fp => fp.SeatClassId)
                .HasColumnName("SeatClassID");

            builder.Property(fp => fp.Price)
                .HasColumnType("decimal(12,2)");

            builder.Property(fp => fp.BookedSeats)
                .HasDefaultValue(0);

            builder.HasIndex(fp => fp.FlightId)
                .HasDatabaseName("IX_FlightPricing_Flight");

            builder.HasIndex(fp => new { fp.FlightId, fp.SeatClassId })
                .IsUnique();

            builder.HasCheckConstraint("CK_Pricing_Price_Positive", "[Price] > 0");
            builder.HasCheckConstraint("CK_Pricing_Booked_Valid", "[BookedSeats] >= 0");

            builder.HasOne(fp => fp.Flight)
                .WithMany(f => f.FlightPricings)
                .HasForeignKey(fp => fp.FlightId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(fp => fp.SeatClass)
                .WithMany(sc => sc.FlightPricings)
                .HasForeignKey(fp => fp.SeatClassId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
