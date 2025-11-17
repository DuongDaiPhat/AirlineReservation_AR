using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineReservation_AR.src.AirlineReservation.Domain.Entities
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRoles");

            builder.HasKey(ur => new { ur.UserId, ur.RoleId });

            builder.Property(ur => ur.UserId)
                .HasColumnName("UserID");

            builder.Property(ur => ur.RoleId)
                .HasColumnName("RoleID");

            builder.Property(ur => ur.AssignedAt)
                .HasDefaultValueSql("GETDATE()");

            builder.Property(ur => ur.AssignedBy)
                .HasColumnName("AssignedBy")
                .HasColumnType("uniqueidentifier");

            builder.Property(ur => ur.Note)
                .HasMaxLength(255);

            builder.HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ur => ur.AssignedByUser)
                .WithMany(u => u.AssignedRoles)
                .HasForeignKey(ur => ur.AssignedBy)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
