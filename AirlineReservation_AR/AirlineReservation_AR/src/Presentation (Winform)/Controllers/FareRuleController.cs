using AirlineReservation_AR.src.Application.Interfaces;
using AirlineReservation_AR.src.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Presentation__Winform_.Controllers
{
    public class FareRuleController
    {
        private IFareRuleService? _iFareRuleService;

        public FareRuleController(IFareRuleService iFareRuleService)
        {
            _iFareRuleService = iFareRuleService;
        }

        public async Task<FareRule?> GetFareRuleForFlightAsync(int flightId, int seatClassId)
        {
            return await _iFareRuleService!.GetFareRuleForFlightAsync(flightId, seatClassId);
        }
        public async Task<FareRule?> GetFareRuleForTicketAsync(Guid ticketId)
        {
            return await _iFareRuleService!.GetFareRuleForTicketAsync(ticketId);
        }

        public async Task<bool> CanChangeAsync(Guid ticketId){
            return await _iFareRuleService!.CanChangeAsync(ticketId);
        }
    }
}
