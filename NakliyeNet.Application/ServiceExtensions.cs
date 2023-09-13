using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NakliyeNet.Application.Services;
using NakliyeNet.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NakliyeNet.Application.Models.Membership;
using NakliyeNet.Application.Services;

namespace NakliyeNet.Application
{
    public static class ServiceExtensions
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IRequestService, RequestService>();
            services.AddScoped<IApplicationService, ApplicationService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IMembershipService, MembershipService>();
            services.AddScoped(n => n.GetRequiredService<IMembershipService>().LoggedUser);
        }
    }
}
