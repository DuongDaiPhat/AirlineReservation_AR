using AirlineReservation_AR.src.AirlineReservation.Application.Services;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.Application.Interfaces;
using AirlineReservation_AR.src.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AirlineReservation_AR.src.Application.Interfaces.IFlightPricingServiceAdmin;

namespace AirlineReservation_AR.src.Application.Services
{
    public class FlightPricingServiceAdmin : IFlightPricingServiceAdmin
    {
        private readonly IUnitOfWorkAdmin _unitOfWork;

        public FlightPricingServiceAdmin(IUnitOfWorkAdmin unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponse<IEnumerable<FlightPricingDtoAdmin>>> GetAllPricingsAsync()
        {
            try
            {
                var pricings = await _unitOfWork.FlightPricings.GetAllAsync();
                var dtos = pricings.Select(MapToDto).ToList();
                return ServiceResponse<IEnumerable<FlightPricingDtoAdmin>>.SuccessResponse(dtos);
            }
            catch (Exception ex)
            {
                return ServiceResponse<IEnumerable<FlightPricingDtoAdmin>>.ErrorResponse(
                    $"Lỗi khi tải danh sách giá vé: {ex.Message}",
                    new List<string> { ex.ToString() }
                    );
            }
        }

        public async Task<ServiceResponse<FlightPricingDtoAdmin>> GetPricingByIdAsync(int pricingId)
        {
            try
            {
                var pricing = await _unitOfWork.FlightPricings.GetByIdAsync(pricingId);
                if (pricing == null)
                {
                    return ServiceResponse<FlightPricingDtoAdmin>.ErrorResponse("Không tìm thấy giá vé");
                }

                return ServiceResponse<FlightPricingDtoAdmin>.SuccessResponse(MapToDto(pricing));
            }
            catch (Exception ex)
            {
                return ServiceResponse<FlightPricingDtoAdmin>.ErrorResponse(
                    "Lỗi khi tải thông tin giá vé", new List<string> { ex.Message });
            }
        }

        public async Task<ServiceResponse<IEnumerable<FlightPricingDtoAdmin>>> GetPricingsWithFiltersAsync(FlightPricingFilterDtoAdmin filter)
        {
            try
            {
                var pricings = await _unitOfWork.FlightPricings.GetWithFiltersAsync(
                    filter.Route,
                    filter.SeatClass,
                    filter.MinDiscountPercent);

                var dtos = pricings.Select(MapToDto).ToList();
                return ServiceResponse<IEnumerable<FlightPricingDtoAdmin>>.SuccessResponse(dtos);
            }
            catch (Exception ex)
            {
                return ServiceResponse<IEnumerable<FlightPricingDtoAdmin>>.ErrorResponse(
                    "Lỗi khi lọc giá vé", new List<string> { ex.Message });
            }
        }

        public async Task<ServiceResponse<FlightPricingDtoAdmin>> CreatePricingAsync(CreateFlightPricingDtoAdmin dto)
        {
            try
            {
                var pricing = new FlightPricing
                {
                    FlightId = dto.FlightId,
                    SeatClassId = dto.SeatClassId,
                    Price = dto.Price,
                    BookedSeats = 0
                };

                var created = await _unitOfWork.FlightPricings.AddAsync(pricing);
                await _unitOfWork.SaveChangesAsync();

                return ServiceResponse<FlightPricingDtoAdmin>.SuccessResponse(
                    MapToDto(created), "Tạo giá vé thành công");
            }
            catch (Exception ex)
            {
                return ServiceResponse<FlightPricingDtoAdmin>.ErrorResponse(
                    "Lỗi khi tạo giá vé", new List<string> { ex.Message });
            }
        }

        public async Task<ServiceResponse<FlightPricingDtoAdmin>> UpdatePricingAsync(UpdateFlightPricingDtoAdmin dto)
        {
            try
            {
                var pricing = await _unitOfWork.FlightPricings.GetByIdAsync(dto.PricingId);
                if (pricing == null)
                {
                    return ServiceResponse<FlightPricingDtoAdmin>.ErrorResponse("Không tìm thấy giá vé");
                }

                pricing.Price = dto.Price;
                await _unitOfWork.FlightPricings.UpdateAsync(pricing);
                await _unitOfWork.SaveChangesAsync();

                return ServiceResponse<FlightPricingDtoAdmin>.SuccessResponse(
                    MapToDto(pricing), "Cập nhật giá vé thành công");
            }
            catch (Exception ex)
            {
                return ServiceResponse<FlightPricingDtoAdmin>.ErrorResponse(
                    "Lỗi khi cập nhật giá vé", new List<string> { ex.Message });
            }
        }

        public async Task<ServiceResponse<bool>> DeletePricingAsync(int pricingId)
        {
            try
            {
                await _unitOfWork.FlightPricings.DeleteAsync(pricingId);
                await _unitOfWork.SaveChangesAsync();

                return ServiceResponse<bool>.SuccessResponse(true, "Xóa giá vé thành công");
            }
            catch (Exception ex)
            {
                return ServiceResponse<bool>.ErrorResponse(
                    "Lỗi khi xóa giá vé", new List<string> { ex.Message });
            }
        }

        private FlightPricingDtoAdmin MapToDto(FlightPricing pricing)
        {
            var flight = pricing.Flight;
            var airline = flight?.Airline;
            var seatClass = pricing.SeatClass;
            var aircraft = flight?.Aircraft;

            // Calculate discount
            var basePrice = flight?.BasePrice ?? pricing.Price;
            var discountPercent = basePrice > 0
                ? (int)((basePrice - pricing.Price) / basePrice * 100)
                : 0;

            // Get available seats
            var totalSeats = 0;
            if (aircraft?.SeatConfigurations != null)
            {
                // Tổng tất cả ghế của hạng này trong aircraft
                totalSeats = aircraft.SeatConfigurations
                    .Where(sc => sc.SeatClassId == pricing.SeatClassId)
                    .Sum(sc => sc.SeatCount);
            }
            var availableSeats = Math.Max(0, totalSeats - pricing.BookedSeats);

            var route = flight != null
                ? $"{flight.DepartureAirport?.IataCode} → {flight.ArrivalAirport?.IataCode}"
                : "";

            return new FlightPricingDtoAdmin
            {
                PricingId = pricing.PricingId,
                FlightId = pricing.FlightId,
                FlightNumber = flight?.FlightNumber ?? "",
                AirlineName = airline?.AirlineName ?? "",
                Route = route,
                SeatClass = seatClass?.ClassName ?? "",
                OriginalPrice = basePrice,
                Price = pricing.Price,
                DiscountPercent = discountPercent,
                DiscountedPrice = pricing.Price,
                BookedSeats = pricing.BookedSeats,
                TotalSeats = totalSeats,
                AvailableSeats = availableSeats,
                FlightDate = flight?.FlightDate ?? DateTime.Now,
                DepartureTime = flight?.DepartureTime.ToString(@"hh\:mm") ?? "",
                ArrivalTime = flight?.ArrivalTime.ToString(@"hh\:mm") ?? ""
            };
        }

    }
}
