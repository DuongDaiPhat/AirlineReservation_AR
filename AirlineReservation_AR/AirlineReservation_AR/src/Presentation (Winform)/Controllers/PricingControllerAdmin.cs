using AirlineReservation_AR.src.AirlineReservation.Application.Services;
using AirlineReservation_AR.src.Application.Interfaces;
using AirlineReservation_AR.src.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AirlineReservation_AR.src.Application.Interfaces.IFlightPricingServiceAdmin;

namespace AirlineReservation_AR.src.Presentation__Winform_.Controllers
{
    public class PricingControllerAdmin
    {
        private readonly IFlightPricingServiceAdmin _service;

        public PricingControllerAdmin(IFlightPricingServiceAdmin service)
        {
            _service = service;
        }

        public async Task<ServiceResponse<IEnumerable<FlightPricingDtoAdmin>>> GetAllPricings()
        {
            return await _service.GetAllPricingsAsync();
        }

        public async Task<ServiceResponse<FlightPricingDtoAdmin>> GetPricingById(int pricingId)
        {
            return await _service.GetPricingByIdAsync(pricingId);
        }

        public async Task<ServiceResponse<IEnumerable<FlightPricingDtoAdmin>>> SearchPricings(
            FlightPricingFilterDtoAdmin filter)
        {
            return await _service.GetPricingsWithFiltersAsync(filter);
        }

        public async Task<ServiceResponse<FlightPricingDtoAdmin>> CreatePricing(
            CreateFlightPricingDtoAdmin dto)
        {
            return await _service.CreatePricingAsync(dto);
        }

        public async Task<ServiceResponse<FlightPricingDtoAdmin>> UpdatePricing(
            UpdateFlightPricingDtoAdmin dto)
        {
            return await _service.UpdatePricingAsync(dto);
        }

        public async Task<ServiceResponse<bool>> DeletePricing(int pricingId)
        {
            return await _service.DeletePricingAsync(pricingId);
        }

        public async Task<ServiceResponse<IEnumerable<FlightPricingDtoAdmin>>> GetPricingsByRoute(
            string route)
        {
            var filter = new FlightPricingFilterDtoAdmin { Route = route };
            return await _service.GetPricingsWithFiltersAsync(filter);
        }

        public async Task<ServiceResponse<IEnumerable<FlightPricingDtoAdmin>>> GetPricingsBySeatClass(
            string seatClass)
        {
            var filter = new FlightPricingFilterDtoAdmin { SeatClass = seatClass };
            return await _service.GetPricingsWithFiltersAsync(filter);
        }

        public async Task<ServiceResponse<IEnumerable<FlightPricingDtoAdmin>>> GetDiscountedPricings(
            int minDiscountPercent)
        {
            var filter = new FlightPricingFilterDtoAdmin
            {
                MinDiscountPercent = minDiscountPercent
            };
            return await _service.GetPricingsWithFiltersAsync(filter);
        }
    } 
}
