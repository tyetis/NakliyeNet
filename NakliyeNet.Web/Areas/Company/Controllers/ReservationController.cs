using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportationApp.Domain.Services;

namespace TransportationApp.Areas.Company.Controllers
{ 
    [Area("Company")]
    public class ReservationController : Controller
    {
        IReservationService ReservationService { get; set; }

        public ReservationController(IReservationService reservationService)
        {
            ReservationService = reservationService;
        }

        public IActionResult List()
        {
            var reservations = ReservationService.GetReservations();
            return View(reservations);
        }
    }
}
