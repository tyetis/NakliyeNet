using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NakliyeNet.Application.Models.Membership;
using NakliyeNet.Application.Services;
using NakliyeNet.Domain.Models.Request;
using NakliyeNet.Domain.Services;

namespace NakliyeNet.Areas.User.Controllers
{ 
    [Area("User")]
    public class RequestsController : Controller
    {
        IRequestService RequestService { get; set; }
        LoggedUser LoggedUser { get; set; }

        public RequestsController(IRequestService requestService, LoggedUser loggedUser)
        {
            RequestService = requestService;
            LoggedUser = loggedUser;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateRequestModel model)
        {
            var result = RequestService.Create(model);
            return RedirectToAction("List");
        }

        public IActionResult CreateProperties(int categoryId)
        {
            return PartialView("Partial/RequestProperiesByCategory", categoryId);
        }

        public IActionResult List()
        {
            var requests = RequestService.GetRequests(LoggedUser.Id);
            return View(requests);
        }

        public IActionResult Detail(int id)
        {
            var request = RequestService.GetRequest(id);
            return View(request);
        }

        [HttpPost]
        public IActionResult Cancel(int id)
        {
            var result = RequestService.Cancel(id);
            return Json(result);
        }

        [HttpPost]
        public IActionResult Complete(int id, int rating, string comment)
        {
            var result = RequestService.Complete(id, rating, comment);
            return Json(result);
        }

        [HttpPost]
        public IActionResult CalculateAmount(CreateRequestModel model)
        {
            return Json(RequestService.CalculateAmount(model));
        }
    }
}
