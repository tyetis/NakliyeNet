using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NakliyeNet.Application.Models.Membership;
using NakliyeNet.Domain.Services;

namespace NakliyeNet.Areas.Company.Controllers
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

        [HttpPost]
        public IActionResult Cancel(int applicationId)
        {
            RequestService.Cancel(applicationId);
            return Json(true);
        }
    }
}
