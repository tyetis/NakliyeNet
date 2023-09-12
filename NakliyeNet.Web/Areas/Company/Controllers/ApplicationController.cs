using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportationApp.Application.Models.Membership;
using TransportationApp.Domain.Services;

namespace TransportationApp.Areas.Company.Controllers
{ 
    [Area("Company")]
    public class ApplicationController : Controller
    {
        IApplicationService RequestService { get; set; }
        LoggedUser LoggedUser { get; set; }

        public ApplicationController(IApplicationService requestService, LoggedUser loggedUser)
        {
            RequestService = requestService;
            LoggedUser = loggedUser;
        }

        public IActionResult List()
        {
            var applications = RequestService.GetApplications(LoggedUser.Id);
            return View(applications);
        }

        [HttpPost]
        public IActionResult Send(int requestId, decimal amount)
        {
            RequestService.Send(requestId, amount);
            return Json(true);
        }
    }
}
