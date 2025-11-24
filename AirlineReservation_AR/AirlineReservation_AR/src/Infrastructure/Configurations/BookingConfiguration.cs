using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineReservation_AR.src.AirlineReservation.Domain.Entities
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.ToTable("Bookings");

            builder.HasKey(b => b.BookingId);

            builder.Property(b => b.BookingId)
                .HasColumnName("BookingID")
                .ValueGeneratedOnAdd();

            builder.Property(b => b.BookingReference)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(b => b.UserId)
                .HasColumnName("UserID");

            builder.Property(b => b.BookingDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(b => b.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Pending");

            builder.Property(b => b.Currency)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasDefaultValue("VND");

            builder.Property(b => b.ContactEmail)
                .HasMaxLength(100);

            builder.Property(b => b.ContactPhone)
                .HasMaxLength(20);

            builder.Property(b => b.SpecialRequests)
                .HasMaxLength(500);

            builder.HasIndex(b => b.BookingReference)
                .HasDatabaseName("IX_Bookings_Reference")
                .IsUnique();
            builder.Property(b => b.Price)
                .HasColumnType("decimal(12,2)");

            builder.Property(b => b.Taxes)
                .HasColumnType("decimal(10,2)")
                .HasDefaultValue(0m);

            builder.Property(b => b.Fees)
                .HasColumnType("decimal(10,2)")
                .HasDefaultValue(0m);
            builder.HasIndex(b => b.UserId)
                .HasDatabaseName("IX_Bookings_User");

            builder.HasIndex(b => new { b.BookingDate, b.Status })
                .HasDatabaseName("IX_Bookings_Date_Status");

            builder.HasCheckConstraint("CK_Booking_Status", "[Status] IN ('Pending','Confirmed','Cancelled','Completed','Expired')");
            builder.HasCheckConstraint("CK_Booking_Price_Positive", "[Price] > 0");

            builder.HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
