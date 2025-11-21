using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirlineReservation_AR.src.Application.Interfaces;
using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Infrastructure.DI;

namespace AirlineReservation_AR.src.Application.Services
{
    public class ServiceServices : IServiceService
    {
        public async Task AddBaggageAsync(int bookingId, List<BaggageOptionDTO> list)
        {
            using var _db = DIContainer.CreateDb();
            foreach (var bg in list)
            {
                var entity = new AirlineReservation.Domain.Entities.BookingService
                {
                    BookingId = bookingId,
                    ServiceId = bg.ServiceId,
                    Quantity = 1,
                    UnitPrice = bg.Price
                };

                _db.BookingServices.Add(entity);
            }

            await _db.SaveChangesAsync();
        }
    }
}
