using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TransportationApp.Domain.Entity;
using TransportationApp.Domain.Services;
using TransportationApp.Web.Areas.Company.Models.Profile;
using TransportationApp.Web.Utils;

namespace TransportationApp.Areas.Company.Controllers
{
    [Area("Company")]
    public class ProfileController : Controller
    {
        ICompanyService CompanyService { get; set; }
        IWebHostEnvironment _hostingEnvironment { get; set; }

        public ProfileController(ICompanyService companyService, IWebHostEnvironment environment)
        {
            CompanyService = companyService;
            _hostingEnvironment = environment;
        }

        [HttpGet]
        public IActionResult Account()
        {
            var profile = CompanyService.GetProfile();
            return View(profile);
        }

        [HttpPost]
        public IActionResult Account(CompanyModel model)
        {
            CompanyService.Update(new Domain.Entity.Company
            {
                Name = model.Name,
                Email = model.Email,
                Password = model.Password,
                Address = model.Address,
                Description = model.Description,
                LogoUrl = model.LogoImage == null ? null : WebUtils.SaveFile(model.LogoImage, _hostingEnvironment)
            });
            return RedirectToAction("Account", new { area = "company" });
        }

        [HttpGet]
        public IActionResult Vehicle()
        {
            var vehicles = CompanyService.GetProfileVehicles();
            return View(vehicles);
        }

        [HttpPost]
        public IActionResult AddVehicle(CompanyVehicle model)
        {
            CompanyService.AddVehicle(model);
            return Json(true);
        }

        [HttpPost]
        public IActionResult DeleteVehicle(int id)
        {
            CompanyService.DeleteVehicle(id);
            return Json(true);
        }

        [HttpGet]
        public IActionResult Team()
        {
            var team = CompanyService.GetProfileTeam();
            return View(team);
        }

        [HttpPost]
        public IActionResult Team(CompanyTeam model)
        {
            CompanyService.UpdateTeam(model);
            return View();
        }
    }
}
