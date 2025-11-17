using AirlineReservation.src.AirlineReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineReservation.src.AirlineReservation.Domain.Entities
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments");

            builder.HasKey(p => p.PaymentId);

            builder.Property(p => p.PaymentId)
                .HasColumnName("PaymentID")
                .ValueGeneratedOnAdd();

            builder.Property(p => p.BookingId)
                .HasColumnName("BookingID");

            builder.Property(p => p.PaymentMethod)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.PaymentProvider)
                .HasMaxLength(50);

            builder.Property(p => p.Amount)
                .HasColumnType("decimal(15,2)");

            builder.Property(p => p.Currency)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasDefaultValue("VND");

            builder.Property(p => p.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Pending");

            builder.Property(p => p.TransactionId)
                .HasColumnName("TransactionID")
                .HasMaxLength(100);

            builder.Property(p => p.ProcessedAt)
                .HasColumnType("datetime");

            builder.Property(p => p.CompletedAt)
                .HasColumnType("datetime");

            builder.Property(p => p.RefundedAmount)
                .HasColumnType("decimal(15,2)")
                .HasDefaultValue(0m);

            builder.HasIndex(p => p.BookingId)
                .HasDatabaseName("IX_Payments_Booking");

            builder.HasIndex(p => p.Status)
                .HasDatabaseName("IX_Payments_Status");

            builder.HasCheckConstraint("CK_Payment_Status", "[Status] IN ('Pending','Processing','Completed','Failed','Cancelled','Refunded')");
            builder.HasCheckConstraint("CK_Payment_Amount_Positive", "[Amount] > 0");
            builder.HasCheckConstraint("CK_Refund_Valid", "[RefundedAmount] >= 0 AND [RefundedAmount] <= [Amount]");

            builder.HasOne(p => p.Booking)
                .WithMany(b => b.Payments)
                .HasForeignKey(p => p.BookingId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
