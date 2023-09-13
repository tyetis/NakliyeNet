using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NakliyeNet.Domain.Services;

namespace NakliyeNet.Areas.Company.Controllers
{ 
    [Area("Company")]
    public class RequestsController : Controller
    {
        IRequestService RequestService { get; set; }

        public RequestsController(IRequestService requestService)
        {
            RequestService = requestService;
        }

        public IActionResult List()
        {
            var requests = RequestService.GetRequests(active: true);
            return View(requests);
        }

        public IActionResult Detail(int id)
        {
            var requests = RequestService.GetRequest(id);
            return View(requests);
        }
    }
}
