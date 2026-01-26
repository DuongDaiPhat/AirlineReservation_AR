using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.Application.Services.AI;
using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Domain.DTOs.AI_DTO;

namespace AirlineReservation_AR.src.Application.Services.AI_Service
{
    public class BestFlightService
    {
        private readonly FlightVectorService _vectorService;
        private readonly FlightScoringService _scoringService = new();

        public BestFlightService(List<FlightResultDTO> flights)
        {
            _vectorService = new FlightVectorService(flights);
        }

        public List<FlightResultDTO> RankFlights(
            List<FlightResultDTO> flights,
            UserPreference user,
            Func<FlightResultDTO, int> transitResolver)
        {
            return flights
                .Select(f =>
                {
                    int transit = transitResolver(f);
                    var vector = _vectorService.BuildVector(f, user, transit);
                    var score = _scoringService.Calculate(vector, user.Weights);
                    return new { Flight = f, Score = score };
                })
                .OrderByDescending(x => x.Score)
                .Select(x => x.Flight)
                .ToList();
        }
    }


}
