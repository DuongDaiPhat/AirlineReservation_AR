using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineReservation_AR.src.AirlineReservation.Domain.Entities
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Tickets");

            builder.HasKey(t => t.TicketId);

            builder.Property(t => t.TicketId)
                .HasColumnName("TicketID")
                .HasDefaultValueSql("NEWID()");

            builder.Property(t => t.BookingFlightId)
                .HasColumnName("BookingFlightID");

            builder.Property(t => t.PassengerId)
                .HasColumnName("PassengerID");

            builder.Property(t => t.SeatClassId)
                .HasColumnName("SeatClassID");

            builder.Property(t => t.TicketNumber)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(t => t.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Issued");

            builder.Property(t => t.CheckedInAt)
                .HasColumnType("datetime");

            builder.Property(t => t.SeatNumber)
                .HasMaxLength(5);

            builder.HasIndex(t => t.TicketNumber)
                .HasDatabaseName("IX_Tickets_Number")
                .IsUnique();

            builder.HasIndex(t => t.BookingFlightId)
                .HasDatabaseName("IX_Tickets_BookingFlight");

            builder.HasCheckConstraint("CK_Ticket_Status", "[Status] IN ('Issued','CheckedIn','Boarded','Cancelled','Refunded', 'Rescheduled')");
            

            builder.HasOne(t => t.BookingFlight)
                .WithMany(bf => bf.Tickets)
                .HasForeignKey(t => t.BookingFlightId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.Passenger)
                .WithMany(p => p.Tickets)
                .HasForeignKey(t => t.PassengerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.SeatClass)
                .WithMany(sc => sc.Tickets)
                .HasForeignKey(t => t.SeatClassId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
