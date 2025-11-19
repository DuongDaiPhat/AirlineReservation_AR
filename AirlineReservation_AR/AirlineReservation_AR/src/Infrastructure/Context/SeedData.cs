using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Shared.Utils;
using Microsoft.EntityFrameworkCore;

namespace AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context
{
    public static class SeedData
    {
        private readonly static PasswordHasher hasher = new PasswordHasher();
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    RoleId = 1,
                    RoleName = "Admin",
                    Description = "System administrator with full privileges",
                    IsActive = true
                },
                new Role
                {
                    RoleId = 2,
                    RoleName = "Staff",
                    Description = "Operational staff role",
                    IsActive = true
                },
                new Role
                {
                    RoleId = 3,
                    RoleName = "Customer",
                    Description = "Registered customer role",
                    IsActive = true
                });

            modelBuilder.Entity<SeatClass>().HasData(
                new SeatClass
                {
                    SeatClassId = 1,
                    ClassName = "Economy",
                    DisplayName = "Economy",
                    PriceMultiplier = 1.00m,
                    BaggageAllowanceKg = 20,
                    CabinBaggageAllowanceKg = 7,
                    Description = "Standard economy seating"
                },
                new SeatClass
                {
                    SeatClassId = 2,
                    ClassName = "Premium Economy",
                    DisplayName = "Premium Economy",
                    PriceMultiplier = 1.25m,
                    BaggageAllowanceKg = 25,
                    CabinBaggageAllowanceKg = 8,
                    Description = "Extra legroom and comfort compared to standard economy"
                },
                new SeatClass
                {
                    SeatClassId = 3,
                    ClassName = "Business",
                    DisplayName = "Business",
                    PriceMultiplier = 1.75m,
                    BaggageAllowanceKg = 30,
                    CabinBaggageAllowanceKg = 10,
                    Description = "Business class with enhanced comfort"
                },
                new SeatClass
                {
                    SeatClassId = 4,
                    ClassName = "First",
                    DisplayName = "First",
                    PriceMultiplier = 2.50m,
                    BaggageAllowanceKg = 40,
                    CabinBaggageAllowanceKg = 15,
                    Description = "Premium first-class experience"
                });

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = Guid.Parse("d3f9a7c2-8b1e-4f3a-9c2a-7e4f9a1b2c3d"),
                    FullName = "Admin System",
                    Email = "adminsystem@gmail.com",
                    Phone = "+84901234567",
                    PasswordHash = hasher.HashPassword("Admin@12345"),
                    IsVerified = true,
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 10, 20, 0, 0, 0, 0)
                });

            modelBuilder.Entity<UserRole>().HasData(
                new UserRole
                {
                    UserId = Guid.Parse("d3f9a7c2-8b1e-4f3a-9c2a-7e4f9a1b2c3d"),
                    RoleId = 1,
                    AssignedAt = new DateTime(2025, 10, 20, 0, 0, 0, 0),
                    AssignedBy = null
                });
        }
    }
}
