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
    public class FareRuleConfiguration : IEntityTypeConfiguration<FareRule>
    {
        public void Configure(EntityTypeBuilder<FareRule> builder)
        {
            builder.ToTable("FareRules");

            builder.HasKey(fr => fr.FareRuleId);

            builder.Property(fr => fr.FareRuleId)
                .HasColumnName("FareRuleID")
                .ValueGeneratedOnAdd();

            builder.Property(fr => fr.FareCode)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(fr => fr.AllowChange)
                .HasDefaultValue(false);

            builder.Property(fr => fr.ChangeFee)
                .HasColumnType("decimal(12,2)")
                .HasDefaultValue(0m);

            builder.Property(fr => fr.MinHoursBeforeChange)
                .HasDefaultValue(0);

            builder.Property(fr => fr.AllowRefund)
                .HasDefaultValue(false);

            builder.Property(fr => fr.RefundFee)
                .HasColumnType("decimal(12,2)")
                .HasDefaultValue(0m);

            builder.HasIndex(fr => fr.FareCode)
                .IsUnique();

            builder.HasCheckConstraint("CK_FareRule_ChangeFee_Positive", "[ChangeFee] >= 0");
            builder.HasCheckConstraint("CK_FareRule_RefundFee_Positive", "[RefundFee] >= 0");
        }
    }
}
