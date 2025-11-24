using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Services;
using AirlineReservation_AR.src.Application.Interfaces;
using AirlineReservation_AR.src.Application.Services;
using AirlineReservation_AR.src.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Presentation__Winform_.Controllers
{
    public class FlightControllerAdmin
    {
        private readonly IFlightServiceAdmin _flightService;

        public FlightControllerAdmin(IFlightServiceAdmin flightService)
        {
            _flightService = flightService;
        }

        public async Task<IEnumerable<FlightListDtoAdmin>> GetAllFlightsAsync()
        {
            return await _flightService.GetAllFlightsAsync();
        }
    }
}
