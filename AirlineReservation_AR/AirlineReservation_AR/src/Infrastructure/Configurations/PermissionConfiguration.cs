using AirlineReservation.src.AirlineReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineReservation.src.AirlineReservation.Domain.Entities
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permissions");

            builder.HasKey(p => p.PermissionId);

            builder.Property(p => p.PermissionId)
                .HasColumnName("PermissionID")
                .ValueGeneratedOnAdd();

            builder.Property(p => p.PermissionName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Description)
                .HasMaxLength(255);

            builder.Property(p => p.Module)
                .HasMaxLength(50);

            builder.HasIndex(p => p.PermissionName)
                .IsUnique();
        }
    }
}
