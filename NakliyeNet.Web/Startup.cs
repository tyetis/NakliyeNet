using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TransportationApp.Application;
using TransportationApp.Domain.Common;
using TransportationApp.Infrastructure;

namespace TransportationApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddHttpContextAccessor();
            services.AddInfrastructure(Configuration);
            services.AddApplication(Configuration);
            services.AddAuthorization(c =>
            {
                c.AddPolicy("user", policy => policy.RequireClaim(ClaimTypes.Role, ((int)MemberType.User).ToString()));
                c.AddPolicy("company", policy => policy.RequireClaim(ClaimTypes.Role, ((int)MemberType.Company).ToString()));
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Membership/Login"; // Giriş yapılacak sayfa URL'si
                    options.AccessDeniedPath = "/Membership/Login";
                    options.Cookie.HttpOnly = true;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Oturum süresi
                    options.SlidingExpiration = true;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "user",
                  pattern: "User/{controller=Home}/{action=Index}/{id?}",
                  defaults: new { area = "User" }
                ).RequireAuthorization("user");

                endpoints.MapControllerRoute(
                  name: "company",
                  pattern: "Company/{controller=Home}/{action=Index}/{id?}",
                  defaults: new { area = "Company" }
                ).RequireAuthorization("company");

                endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
