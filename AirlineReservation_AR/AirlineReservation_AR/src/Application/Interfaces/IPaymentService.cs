using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirlineReservation_AR.src.Domain.DTOs;

namespace AirlineReservation_AR.src.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<int> CreatePaymentAsync(PaymentCreateDTO dto);
        void HandlePaymentStatus(int bookingId);
        void MarkPaymentSuccess(int bookingId, string momoTransId);
        void MarkPaymentFailed(int bookingId, string? reason = null);

    }
}
