using AirlineReservation.src.AirlineReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineReservation.src.AirlineReservation.Domain.Entities
{
    public class PromotionConfiguration : IEntityTypeConfiguration<Promotion>
    {
        public void Configure(EntityTypeBuilder<Promotion> builder)
        {
            builder.ToTable("Promotions");

            builder.HasKey(p => p.PromotionId);

            builder.Property(p => p.PromotionId)
                .HasColumnName("PromotionID")
                .ValueGeneratedOnAdd();

            builder.Property(p => p.PromoCode)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(p => p.PromoName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Description)
                .HasMaxLength(255);

            builder.Property(p => p.DiscountType)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(p => p.DiscountValue)
                .HasColumnType("decimal(10,2)");

            builder.Property(p => p.MinimumAmount)
                .HasColumnType("decimal(12,2)")
                .HasDefaultValue(0m);

            builder.Property(p => p.MaximumDiscount)
                .HasColumnType("decimal(10,2)");

            builder.Property(p => p.UsageLimit);

            builder.Property(p => p.UsageCount)
                .HasDefaultValue(0);

            builder.Property(p => p.UserUsageLimit)
                .HasDefaultValue(1);

            builder.Property(p => p.ValidFrom)
                .HasColumnType("datetime");

            builder.Property(p => p.ValidTo)
                .HasColumnType("datetime");

            builder.Property(p => p.IsActive)
                .HasDefaultValue(true);

            builder.HasIndex(p => p.PromoCode)
                .IsUnique();

            builder.HasCheckConstraint("CK_Discount_Type", "[DiscountType] IN ('Percent','Fixed')");
            builder.HasCheckConstraint("CK_Discount_Value_Valid", "([DiscountType] = 'Percent' AND [DiscountValue] BETWEEN 0.01 AND 100) OR ([DiscountType] = 'Fixed' AND [DiscountValue] > 0)");
            builder.HasCheckConstraint("CK_Promo_Date_Range_Valid", "[ValidFrom] < [ValidTo]");
            builder.HasCheckConstraint("CK_Usage_Valid", "[UsageCount] <= ISNULL([UsageLimit],[UsageCount])");
        }
    }
}
