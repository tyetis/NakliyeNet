using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using NakliyeNet.Application.Models.Membership;
using NakliyeNet.Domain.Common;

namespace NakliyeNet.Application.Services
{
    public class MembershipService : IMembershipService
    {
        private HttpContext HttpContext { get; set; }

        public LoggedUser LoggedUser {
            get
            {
                if (!HttpContext.User.Identity.IsAuthenticated) return new LoggedUser();
                var claims = HttpContext.User.Claims.ToList();
                return new LoggedUser
                {
                    Id = Convert.ToInt32(claims.FirstOrDefault(n => n.Type == ClaimTypes.Sid).Value),
                    UserName = claims.FirstOrDefault(n => n.Type == ClaimTypes.Name).Value,
                    LogoUrl = claims.FirstOrDefault(n => n.Type == ClaimTypes.Actor).Value,
                    Type = Convert.ToInt32(claims.FirstOrDefault(n => n.Type == ClaimTypes.Role).Value)
                };
            }
        }

        public MembershipService(IHttpContextAccessor httpContextAccessor)
        {
            HttpContext = httpContextAccessor.HttpContext;
        }

        public async Task LoginAsync(LoggedUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Type.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Actor, user.LogoUrl ?? ""),
             };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        public async Task Logout()
        {
            await HttpContext.SignOutAsync();
        }
    }
}
