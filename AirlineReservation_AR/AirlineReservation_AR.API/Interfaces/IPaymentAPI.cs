using AirlineReservation_AR.src.Domain.DTOs;

namespace AirlineReservation_AR.API.Interfaces
{
    public interface IPaymentAPI
    {
        int CreatePayment(PaymentCreateDTO dto);
    }
}
