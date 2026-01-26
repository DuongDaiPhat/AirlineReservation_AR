using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    public class PassengerWithServicesDTO
    {
        public Passenger Passenger { get; set; }
        public ServiceOption SelectedServices { get; set; }
    }
}
