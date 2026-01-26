using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Domain.DTOs.AI_DTO;

namespace AirlineReservation_AR.src.Application.Services.AI
{
    public class FlightVectorService
    {
        private readonly decimal _minPrice;
        private readonly decimal _maxPrice;
        private readonly int _minDuration;
        private readonly int _maxDuration;

        public FlightVectorService(List<FlightResultDTO> flights)
        {
            _minPrice = flights.Min(f => f.Price);
            _maxPrice = flights.Max(f => f.Price);

            _minDuration = flights.Min(f => f.DurationMinutes);
            _maxDuration = flights.Max(f => f.DurationMinutes);
        }

        public double[] BuildVector(
            FlightResultDTO flight,
            UserPreference user,
            int transitCount)
        {
            return new double[]
            {
            GetPriceScore(flight.Price),
            GetTimeScore(flight.DepartureTime),
            GetDurationScore(flight.DurationMinutes),
            GetTransitScore(transitCount),
            GetAirlineScore(flight.AirlineName, user.PreferredAirlines)
            };
        }

        private double GetPriceScore(decimal price)
        {
            if (_maxPrice == _minPrice) return 1;
            return 1 - (double)((price - _minPrice) / (_maxPrice - _minPrice));
        }

        private double GetTimeScore(TimeSpan time)
        {
            if (time.Hours >= 6 && time.Hours <= 9) return 1.0;
            if (time.Hours <= 12) return 0.7;
            if (time.Hours <= 18) return 0.4;
            return 0.1;
        }

        private double GetDurationScore(int duration)
        {
            if (_maxDuration == _minDuration) return 1;
            return 1 - (double)(duration - _minDuration) / (_maxDuration - _minDuration);
        }

        private double GetTransitScore(int transit)
        {
            if (transit == 0) return 1.0;
            if (transit == 1) return 0.5;
            return 0.1;
        }

        private double GetAirlineScore(string airline, List<string> preferred)
        {
            if (preferred == null || preferred.Count == 0)
                return 0.5;

            return preferred.Any(a =>
                a.Equals(airline, StringComparison.OrdinalIgnoreCase))
                ? 1.0
                : 0.0;
        }
    }



}
