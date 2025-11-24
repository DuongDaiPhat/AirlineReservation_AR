using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.Application.Interfaces;
using AirlineReservation_AR.src.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Presentation__Winform_.Controllers
{
    public class PromotionController
    {
        private readonly IPromotionService _promotionService;

        public PromotionController(IPromotionService service)
        {
            _promotionService = service;
        }

        public List<PromotionDTO> GetActivePromotions()
            => _promotionService.GetActivePromotions();
    }
}
