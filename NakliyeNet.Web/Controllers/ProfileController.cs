using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NakliyeNet.Domain.Services;

namespace NakliyeNet.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        ICompanyService CompanyService { get; set; }

        public ProfileController(ICompanyService companyService)
        {
            CompanyService = companyService;
        }

        public IActionResult Company(int id)
        {
            var company = CompanyService.GetCompany(id);
            return View(company);
        }

        public IActionResult Companies()
        {
            var company = CompanyService.GetCompanies();
            return View(company);
        }
    }
}
