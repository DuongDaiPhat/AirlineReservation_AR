using AirlineReservation.src.AirlineReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineReservation.src.AirlineReservation.Domain.Entities
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.UserId);

            builder.Property(u => u.UserId)
                .HasColumnName("UserID")
                .HasDefaultValueSql("NEWID()");

            builder.Property(u => u.FullName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Phone)
                .HasMaxLength(20);

            builder.Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(u => u.DateOfBirth)
                .HasColumnType("date");

            builder.Property(u => u.Gender)
                .HasMaxLength(1)
                .IsFixedLength();

            builder.Property(u => u.CityCode)
                .HasMaxLength(3)
                .IsFixedLength();

            builder.Property(u => u.Address)
                .HasMaxLength(255);

            builder.Property(u => u.IsVerified)
                .HasDefaultValue(false);

            builder.Property(u => u.IsActive)
                .HasDefaultValue(true);

            builder.Property(u => u.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            builder.Property(u => u.UpdatedAt)
                .HasColumnType("datetime");

            builder.HasIndex(u => u.Email)
                .HasDatabaseName("IX_Users_Email")
                .IsUnique();

            builder.HasIndex(u => u.Phone)
                .HasDatabaseName("IX_Users_Phone")
                .IsUnique();

            builder.HasIndex(u => u.IsActive)
                .HasDatabaseName("IX_Users_IsActive");

            builder.HasIndex(u => u.CityCode)
                .HasDatabaseName("IX_Users_CityCode");

            builder.HasCheckConstraint("CK_Email_Valid", "[Email] LIKE '_%@_%._%'");
            builder.HasCheckConstraint("CK_Phone_Valid", "[Phone] IS NULL OR ([Phone] NOT LIKE '%[^0-9+()-]%' AND LEN([Phone]) BETWEEN 9 AND 15)");
            builder.HasCheckConstraint("CK_UpdatedAt_Valid", "[UpdatedAt] IS NULL OR [CreatedAt] <= [UpdatedAt]");
            builder.HasCheckConstraint("CK_DateOfBirth_Valid", "[DateOfBirth] IS NULL OR [DateOfBirth] <= DATEADD(YEAR, -12, GETDATE())");
            builder.HasCheckConstraint("CK_Users_Gender", "[Gender] IS NULL OR [Gender] IN ('M','F','O')");

            builder.HasOne(u => u.City)
                .WithMany(c => c.Users)
                .HasForeignKey(u => u.CityCode)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.UserRoles)
                .WithOne(ur => ur.User)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.AssignedRoles)
                .WithOne(ur => ur.AssignedByUser)
                .HasForeignKey(ur => ur.AssignedBy)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
