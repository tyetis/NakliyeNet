using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NakliyeNet.Application.Models.Membership;
using NakliyeNet.Application.Services;
using NakliyeNet.Domain.Common;
using NakliyeNet.Domain.Models.Membership;
using NakliyeNet.Domain.Services;
using NakliyeNet.Web.Models.Membership;

namespace NakliyeNet.Controllers
{
    public class MembershipController : Controller
    {
        IUserService UserService { get; set; }
        ICompanyService CompanyService { get; set; }
        IMembershipService MembershipService { get; set; }

        public MembershipController(IUserService userService, ICompanyService companyService, IMembershipService membershipService)
        {
            UserService = userService;
            CompanyService = companyService;
            MembershipService = membershipService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return CheckLogin();
        }

        [HttpGet]
        public IActionResult CompanyLogin()
        {
            return CheckLogin();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            var user = UserService.GetUser(viewModel.Email, viewModel.Password);
            if (user == null)
            {
                ModelState.AddModelError("", "Kullanıcı Bulunamadı");
                return View();
            }
            else
            {
                await MembershipService.LoginAsync(new LoggedUser
                {
                    Id = user.Id,
                    Type = (int)MemberType.User,
                    UserName = $"{user.Name} {user.Surname}"
                });
                return Redirect("/user/requests/list");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CompanyLogin(LoginViewModel viewModel)
        {
            var company = CompanyService.GetCompany(viewModel.Email, viewModel.Password);
            if (company == null)
            {
                ModelState.AddModelError("", "Kullanıcı Bulunamadı");
                return View();
            }
            else
            {
                await MembershipService.LoginAsync(new LoggedUser
                {
                    Id = company.Id,
                    Type = (int)MemberType.Company,
                    UserName = $"{company.Name}",
                    LogoUrl = company.LogoUrl
                });
                return Redirect("/company/requests/list");
            }
        }

        public IActionResult Logout()
        {
            MembershipService.Logout();
            return Redirect("/");
        }

        public IActionResult SignUp()
        {
            return View();
        }

        public IActionResult CompanySignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpModel model)
        {
            var hasUser = UserService.GetUser(model.Email, model.Password);
            if (hasUser != null)
            {
                ModelState.AddModelError("", "Kullanıcı zaten var");
                return View();
            }
            else if (model.Password != model.AgainPassword)
            {
                ModelState.AddModelError("", "Şifreler uyuşmuyor");
                return View();
            }
            var newUser = UserService.SignUp(model);
            await MembershipService.LoginAsync(new LoggedUser
            {
                Id = newUser.Id,
                Type = (int)MemberType.User,
                UserName = $"{newUser.Name} {newUser.Surname}"
            });
            return Redirect("/user/requests/list");
        }

        [HttpPost]
        public async Task<IActionResult> CompanySignUp(SignUpModel model)
        {
            var hasUser = CompanyService.GetCompany(model.Email, model.Password);
            if (hasUser != null)
            {
                ModelState.AddModelError("", "Kullanıcı zaten var");
                return View();
            }
            else if(model.Password != model.AgainPassword)
            {
                ModelState.AddModelError("", "Şifreler uyuşmuyor");
                return View();
            }
            var user = CompanyService.SignUp(model);
            await MembershipService.LoginAsync(new LoggedUser
            {
                Id = user.Id,
                Type = (int)MemberType.Company,
                UserName = user.Name,
                LogoUrl = user.LogoUrl
            });
            return Redirect("/company/requests/list");
        }

        private IActionResult CheckLogin()
        {
            if (MembershipService.LoggedUser.Type == (int)MemberType.User) return Redirect("/user/requests/list");
            else if (MembershipService.LoggedUser.Type == (int)MemberType.Company)return Redirect("/company/requests/list");
            else return View();
        }
    }
}
