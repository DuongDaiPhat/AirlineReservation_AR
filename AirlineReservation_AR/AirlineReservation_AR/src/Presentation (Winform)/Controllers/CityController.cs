using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirlineReservation_AR.src.AirlineReservation.Application.Services;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;

namespace AirlineReservation_AR.src.Presentation__Winform_.Controllers
{
    public class CityController
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }
        public async Task<IEnumerable<City>> GetAllCitiesAsync(bool includeInactive = false)
        {
            return await _cityService.GetAllAsync(includeInactive);
        }
    }
}
