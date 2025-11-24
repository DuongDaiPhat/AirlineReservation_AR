using AirlineReservation_AR.src.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Application.Interfaces
{
    public interface IFlightServiceAdmin
    {
        Task<IEnumerable<FlightListDtoAdmin>> GetAllFlightsAsync();
    }
}
