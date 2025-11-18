using System.Collections.Generic;

namespace AirlineReservation_AR.src.AirlineReservation.Domain.Entities
{
    public class AircraftType
    {
        public int AircraftTypeId { get; set; }
        public string TypeName { get; set; } = null!;
        public string? DisplayName { get; set; }

        public ICollection<Aircraft> Aircraft { get; set; } = new HashSet<Aircraft>();
    }
}
