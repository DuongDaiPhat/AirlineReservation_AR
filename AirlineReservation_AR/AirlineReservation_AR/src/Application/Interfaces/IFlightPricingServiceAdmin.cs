using AirlineReservation_AR.src.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Application.Interfaces
{
    public interface IFlightPricingServiceAdmin
    {
        Task<ServiceResponse<IEnumerable<FlightPricingDtoAdmin>>> GetAllPricingsAsync();
        Task<ServiceResponse<FlightPricingDtoAdmin>> GetPricingByIdAsync(int pricingId);
        Task<ServiceResponse<IEnumerable<FlightPricingDtoAdmin>>> GetPricingsWithFiltersAsync(FlightPricingFilterDtoAdmin filter);
        Task<ServiceResponse<FlightPricingDtoAdmin>> CreatePricingAsync(CreateFlightPricingDtoAdmin dto);
        Task<ServiceResponse<FlightPricingDtoAdmin>> UpdatePricingAsync(UpdateFlightPricingDtoAdmin dto);
        Task<ServiceResponse<bool>> DeletePricingAsync(int pricingId);
        public class ServiceResponse<T>
        {
            public bool Success { get; set; }
            public string Message { get; set; } = string.Empty;
            public T? Data { get; set; }
            public List<string> Errors { get; set; } = new List<string>();

            public static ServiceResponse<T> SuccessResponse(T data, string message = "Success")
            {
                return new ServiceResponse<T>
                {
                    Success = true,
                    Message = message,
                    Data = data
                };
            }

            public static ServiceResponse<T> ErrorResponse(string message, string error)
            {
                return new ServiceResponse<T>
                {
                    Success = false,
                    Message = message,
                    Errors = new List<string> { error }
                };
            }
            public static ServiceResponse<T> ErrorResponse(string message)
            {
                return new ServiceResponse<T>
                {
                    Success = false,
                    Message = message,
                    Errors = new List<string>()
                };
            }
            public static ServiceResponse<T> ErrorResponse(string message, List<string> errors)
            {
                return new ServiceResponse<T>
                {
                    Success = false,
                    Message = message,
                    Errors = errors
                };
            }
        }
    }
}
