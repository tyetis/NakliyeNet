using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportationApp.Application.Models.Membership;
using TransportationApp.Application.Services;
using TransportationApp.Domain.Models.Request;
using TransportationApp.Domain.Services;

namespace TransportationApp.Areas.User.Controllers
{ 
    [Area("User")]
    public class ApplicationController : Controller
    {
        IApplicationService ApplicationService { get; set; }
        LoggedUser LoggedUser { get; set; }

        public ApplicationController(IApplicationService applicationService, LoggedUser loggedUser)
        {
            ApplicationService = applicationService;
            LoggedUser = loggedUser;
        }

        [HttpPost]
        public IActionResult Approve(int id)
        {
            ApplicationService.Approve(id);
            return Json(true);
        }

        [HttpPost]
        public IActionResult SetReservation(int applicationId, DateTime reservationDate, string description)
        {
            ApplicationService.SetReservation(applicationId, reservationDate, description);
            return Json(true);
        }
    }
}
