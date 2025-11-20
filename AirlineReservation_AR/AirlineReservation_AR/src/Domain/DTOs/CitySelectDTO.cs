using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    public class CitySelectDTO
    {
        public string Code { get; set; }
        public string DisplayName { get; set; }

        public CitySelectDTO() { }

        public CitySelectDTO(City city)
        {
            Code = city.CityCode;
            DisplayName = $"{city.CityName} ({city.CityCode})";
        }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
