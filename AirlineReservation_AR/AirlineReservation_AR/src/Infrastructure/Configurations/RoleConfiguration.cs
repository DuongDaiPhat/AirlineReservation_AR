using AirlineReservation.src.AirlineReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineReservation.src.AirlineReservation.Domain.Entities
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");

            builder.HasKey(r => r.RoleId);

            builder.Property(r => r.RoleId)
                .HasColumnName("RoleID")
                .ValueGeneratedOnAdd();

            builder.Property(r => r.RoleName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(r => r.Description)
                .HasMaxLength(255);

            builder.Property(r => r.IsActive)
                .HasDefaultValue(true);

            builder.HasIndex(r => r.RoleName)
                .IsUnique();
        }
    }
}
