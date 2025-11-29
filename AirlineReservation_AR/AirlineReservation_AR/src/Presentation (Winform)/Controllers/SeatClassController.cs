using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.Application.Interfaces;

namespace AirlineReservation_AR.src.Presentation__Winform_.Controllers
{
    public class SeatClassController
    {
        private readonly ISeatClassService _service;
        public SeatClassController(ISeatClassService service)
        {
            _service = service;
        }
        public async Task<List<SeatClass>> GetAll() {
            return await _service.GetSeatClass();
        }
    }
}
