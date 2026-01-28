using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirlineReservation_AR.src.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AirlineReservation_AR.src.Infrastructure.Configurations
{
    public class PasswordResetOtpConfiguration
        : IEntityTypeConfiguration<PasswordResetOtp>
    {
        public void Configure(EntityTypeBuilder<PasswordResetOtp> builder)
        {
            builder.ToTable("PasswordResetOtp");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.UserId)
                   .IsRequired();

            builder.Property(x => x.OtpCode)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(x => x.ExpiredAt)
                   .IsRequired();

            builder.Property(x => x.IsUsed)
                   .HasDefaultValue(false)
                   .IsRequired();

            builder.Property(x => x.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()")
                   .IsRequired();

            // Index (khuyến nghị)
            builder.HasIndex(x => x.UserId);
            builder.HasIndex(x => x.OtpCode);
        }
    }
}
