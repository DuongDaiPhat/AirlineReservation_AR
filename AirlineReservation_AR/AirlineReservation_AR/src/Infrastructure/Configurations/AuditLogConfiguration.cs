using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineReservation_AR.src.AirlineReservation.Domain.Entities
{
    public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.ToTable("AuditLogs");

            builder.HasKey(al => al.LogId);

            builder.Property(al => al.LogId)
                .HasColumnName("LogID")
                .ValueGeneratedOnAdd();

            builder.Property(al => al.UserId)
                .HasColumnName("UserID");

            builder.Property(al => al.TableName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(al => al.Operation)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(al => al.RecordId)
                .HasColumnName("RecordID")
                .HasMaxLength(50);

            builder.Property(al => al.OldValues)
                .HasColumnType("nvarchar(max)");

            builder.Property(al => al.NewValues)
                .HasColumnType("nvarchar(max)");

            builder.Property(al => al.Timestamp)
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(al => al.IpAddress)
                .HasColumnName("IPAddress")
                .HasMaxLength(45);

            builder.Property(al => al.UserAgent)
                .HasMaxLength(255);

            builder.HasOne(al => al.User)
                .WithMany(u => u.AuditLogs)
                .HasForeignKey(al => al.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
