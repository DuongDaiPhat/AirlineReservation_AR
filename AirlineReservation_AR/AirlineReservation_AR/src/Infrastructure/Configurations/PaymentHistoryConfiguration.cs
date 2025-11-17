using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineReservation_AR.src.AirlineReservation.Domain.Entities
{
    public class PaymentHistoryConfiguration : IEntityTypeConfiguration<PaymentHistory>
    {
        public void Configure(EntityTypeBuilder<PaymentHistory> builder)
        {
            builder.ToTable("PaymentHistory");

            builder.HasKey(ph => ph.HistoryId);

            builder.Property(ph => ph.HistoryId)
                .HasColumnName("HistoryID")
                .ValueGeneratedOnAdd();

            builder.Property(ph => ph.PaymentId)
                .HasColumnName("PaymentID");

            builder.Property(ph => ph.Status)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(ph => ph.TransactionTime)
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(ph => ph.Note)
                .HasMaxLength(255);

            builder.HasOne(ph => ph.Payment)
                .WithMany(p => p.PaymentHistories)
                .HasForeignKey(ph => ph.PaymentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
