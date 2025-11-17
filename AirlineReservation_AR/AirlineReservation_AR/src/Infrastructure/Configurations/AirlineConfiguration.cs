using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineReservation_AR.src.AirlineReservation.Domain.Entities
{
    public class AirlineConfiguration : IEntityTypeConfiguration<Airline>
    {
        public void Configure(EntityTypeBuilder<Airline> builder)
        {
            builder.ToTable("Airlines");

            builder.HasKey(a => a.AirlineId);

            builder.Property(a => a.AirlineId)
                .HasColumnName("AirlineID")
                .ValueGeneratedOnAdd();

            builder.Property(a => a.AirlineName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.IataCode)
                .IsRequired()
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("IATACode");

            builder.Property(a => a.CountryCode)
                .IsRequired()
                .HasMaxLength(3)
                .IsFixedLength();

            builder.Property(a => a.ContactEmail)
                .HasMaxLength(100);

            builder.Property(a => a.ContactPhone)
                .HasMaxLength(20);

            builder.Property(a => a.Website)
                .HasMaxLength(100);

            builder.Property(a => a.LogoUrl)
                .HasMaxLength(255)
                .HasColumnName("LogoURL");

            builder.Property(a => a.IsActive)
                .HasDefaultValue(true);

            builder.HasIndex(a => a.IataCode)
                .IsUnique();

            builder.HasOne(a => a.Country)
                .WithMany(c => c.Airlines)
                .HasForeignKey(a => a.CountryCode)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
