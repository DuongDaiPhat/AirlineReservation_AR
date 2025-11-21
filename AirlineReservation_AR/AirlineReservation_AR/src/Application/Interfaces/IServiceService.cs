using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirlineReservation_AR.src.Domain.DTOs;

namespace AirlineReservation_AR.src.Application.Interfaces
{
    public interface IServiceService
    {
        Task AddBaggageAsync(int bookingId, List<BaggageOptionDTO> list);
    }
}
