using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;

namespace AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context
{
    public class AirlineReservationDbContext : DbContext
    {
        public AirlineReservationDbContext(DbContextOptions<AirlineReservationDbContext> options)
            : base(options) {}

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Permission> Permissions { get; set; } = null!;
        public DbSet<UserRole> UserRoles { get; set; } = null!;
        public DbSet<RolePermission> RolePermissions { get; set; } = null!;
        public DbSet<Country> Countries { get; set; } = null!;
        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<Airline> Airlines { get; set; } = null!;
        public DbSet<Airport> Airports { get; set; } = null!;
        public DbSet<AircraftType> AircraftTypes { get; set; } = null!;
        public DbSet<Aircraft> Aircraft { get; set; } = null!;
        public DbSet<SeatClass> SeatClasses { get; set; } = null!;
        public DbSet<AircraftSeatConfig> AircraftSeatConfigs { get; set; } = null!;
        public DbSet<Seat> Seats { get; set; } = null!;
        public DbSet<Flight> Flights { get; set; } = null!;
        public DbSet<FlightPricing> FlightPricings { get; set; } = null!;
        public DbSet<Booking> Bookings { get; set; } = null!;
        public DbSet<Passenger> Passengers { get; set; } = null!;
        public DbSet<BookingFlight> BookingFlights { get; set; } = null!;
        public DbSet<Ticket> Tickets { get; set; } = null!;
        public DbSet<Payment> Payments { get; set; } = null!;
        public DbSet<PaymentHistory> PaymentHistories { get; set; } = null!;
        public DbSet<Service> Services { get; set; } = null!;
        public DbSet<BookingService> BookingServices { get; set; } = null!;
        public DbSet<Promotion> Promotions { get; set; } = null!;
        public DbSet<BookingPromotion> BookingPromotions { get; set; } = null!;
        public DbSet<Notification> Notifications { get; set; } = null!;
        public DbSet<AuditLog> AuditLogs { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AirlineReservationDbContext).Assembly);
            SeedData.Configure(modelBuilder);
        }
    }
}
