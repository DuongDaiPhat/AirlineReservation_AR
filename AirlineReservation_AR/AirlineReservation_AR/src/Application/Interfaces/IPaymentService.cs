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
        int CreatePayment(PaymentCreateDTO dto);
        //void HandlePaymentStatus(int bookingId);
    }
}
