using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineReservation_AR.src.AirlineReservation.Domain.Entities
{
    public class BookingPromotionConfiguration : IEntityTypeConfiguration<BookingPromotion>
    {
        public void Configure(EntityTypeBuilder<BookingPromotion> builder)
        {
            builder.ToTable("BookingPromotions");

            builder.HasKey(bp => bp.BookingPromotionId);

            builder.Property(bp => bp.BookingPromotionId)
                .HasColumnName("BookingPromotionID")
                .ValueGeneratedOnAdd();

            builder.Property(bp => bp.BookingId)
                .HasColumnName("BookingID");

            builder.Property(bp => bp.PromotionId)
                .HasColumnName("PromotionID");

            builder.Property(bp => bp.DiscountAmount)
                .HasColumnType("decimal(10,2)");

            builder.Property(bp => bp.AppliedAt)
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETDATE()");

            builder.HasIndex(bp => new { bp.BookingId, bp.PromotionId })
                .IsUnique();

            builder.HasOne(bp => bp.Booking)
                .WithMany(b => b.BookingPromotions)
                .HasForeignKey(bp => bp.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(bp => bp.Promotion)
                .WithMany(p => p.BookingPromotions)
                .HasForeignKey(bp => bp.PromotionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
