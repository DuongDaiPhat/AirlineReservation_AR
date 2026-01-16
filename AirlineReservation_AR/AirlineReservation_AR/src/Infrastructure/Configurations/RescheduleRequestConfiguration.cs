using AirlineReservation_AR.src.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Infrastructure.Configurations
{
    public class RescheduleRequestConfiguration:IEntityTypeConfiguration<RescheduleRequest>
    {
        public void Configure(EntityTypeBuilder<RescheduleRequest> builder)
        {
            builder.ToTable("RescheduleRequests");

            builder.HasKey(r => r.RescheduleRequestId);

            builder.Property(r => r.RescheduleRequestId)
                .HasColumnName("RescheduleRequestID")
                .ValueGeneratedOnAdd();

            builder.Property(r => r.BookingId)
                .HasColumnName("BookingID");

            builder.Property(r => r.BookingFlightId)
                .HasColumnName("BookingFlightID");

            builder.Property(r => r.OldFlightId)
                .HasColumnName("OldFlightID");

            builder.Property(r => r.NewFlightId)
                .HasColumnName("NewFlightID");

            builder.Property(r => r.OldDepartureDateTime)
                .HasColumnType("datetime");

            builder.Property(r => r.NewDepartureDateTime)
                .HasColumnType("datetime");

            builder.Property(r => r.FareDifference)
                .HasColumnType("decimal(15,2)");

            builder.Property(r => r.PenaltyFee)
                .HasColumnType("decimal(15,2)");

            builder.Property(r => r.TotalAmount)
                .HasColumnType("decimal(15,2)");

            builder.Property(r => r.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Completed");

            builder.Property(r => r.CreatedAt)
                .HasColumnType("datetime");

            builder.Property(r => r.PaymentId)
                .HasColumnName("PaymentID");

            builder.HasCheckConstraint(
                "CK_RescheduleRequest_Amounts_Positive",
                "[FareDifference] >= 0 AND [PenaltyFee] >= 0 AND [TotalAmount] >= 0"
            );

            builder.HasOne(r => r.Booking)
                .WithMany()
                .HasForeignKey(r => r.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.BookingFlight)
                .WithMany()
                .HasForeignKey(r => r.BookingFlightId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Payment)
                .WithMany()
                .HasForeignKey(r => r.PaymentId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
