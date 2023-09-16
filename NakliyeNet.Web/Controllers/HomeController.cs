using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NakliyeNet.Domain.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using NakliyeNet.Domain.Services;
using NakliyeNet.Models;

namespace NakliyeNet.Controllers
{
    public class HomeController : Controller
    {
        private ILocationService LocationService { get; set; }
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ILocationService locationService)
        {
            _logger = logger;
            LocationService = locationService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult GetCities()
        {
            return Json(LocationService.GetCities());
        }

        [HttpPost]
        public IActionResult GetDistricts(string city)
        {
            return Json(LocationService.GetDistrict(city));
        }

        public async Task<string> OpenStreetMapSearch(string q)
        {
            var result = await LocationService.GetDistance(q);
            return result;
        }
    }
}
