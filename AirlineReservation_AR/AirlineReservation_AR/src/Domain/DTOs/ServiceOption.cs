using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;

namespace AirlineReservation_AR.src.Domain.DTOs
{
   public class ServiceOption
    {
        public Service Baggage{ get; set; }
        public Service Priority { get; set; }
        public Service Meal { get; set; }

        public decimal totalPrice {
            get
            {
                return Baggage.BasePrice + Priority.BasePrice + Meal.BasePrice;
            }
        }

        public ServiceOption()
        {
            Baggage = new Service
            {
                ServiceId = 0,
                ServiceName = "0kg Baggage",
                BasePrice = 0
            };

            Priority = new Service
            {
                ServiceId = 0,
                ServiceName = "No Priority",
                BasePrice = 0
            };
            Meal = new Service
            {
                ServiceId = 0,
                ServiceName = "No Meal",
                BasePrice = 0
            };
        }
    }
}
