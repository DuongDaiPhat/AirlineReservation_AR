using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineReservation_AR.src.AirlineReservation.Domain.Entities
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.ToTable("Services");

            builder.HasKey(s => s.ServiceId);

            builder.Property(s => s.ServiceId)
                .HasColumnName("ServiceID")
                .ValueGeneratedOnAdd();

            builder.Property(s => s.ServiceName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.Category)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.Description)
                .HasMaxLength(255);

            builder.Property(s => s.BasePrice)
                .HasColumnType("decimal(10,2)")
                .HasDefaultValue(0m);

            builder.Property(s => s.Unit)
                .HasMaxLength(20);

            builder.Property(s => s.IsActive)
                .HasDefaultValue(true);

            builder.HasCheckConstraint("CK_Service_Category", "[Category] IN ('Baggage','Meal','Priority','Insurance','Other')");
        }
    }
}
