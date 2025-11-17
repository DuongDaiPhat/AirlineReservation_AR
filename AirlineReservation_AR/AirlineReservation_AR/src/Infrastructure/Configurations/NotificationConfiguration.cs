using AirlineReservation.src.AirlineReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineReservation.src.AirlineReservation.Domain.Entities
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("Notifications");

            builder.HasKey(n => n.NotificationId);

            builder.Property(n => n.NotificationId)
                .HasColumnName("NotificationID")
                .ValueGeneratedOnAdd();

            builder.Property(n => n.UserId)
                .HasColumnName("UserID");

            builder.Property(n => n.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(n => n.Message)
                .IsRequired();

            builder.Property(n => n.Type)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(n => n.Channel)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(n => n.RelatedBookingId)
                .HasColumnName("RelatedBookingID");

            builder.Property(n => n.IsRead)
                .HasDefaultValue(false);

            builder.Property(n => n.SentAt)
                .HasColumnType("datetime");

            builder.Property(n => n.CreatedAt)
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETDATE()");

            builder.HasIndex(n => new { n.UserId, n.IsRead })
                .HasDatabaseName("IX_Notifications_User_Read");

            builder.HasCheckConstraint("CK_Notification_Type", "[Type] IN ('Booking','Payment','Flight','Promotion','System')");
            builder.HasCheckConstraint("CK_Notification_Channel", "[Channel] IN ('Email','SMS','Push','In-App')");

            builder.HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(n => n.RelatedBooking)
                .WithMany(b => b.Notifications)
                .HasForeignKey(n => n.RelatedBookingId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
