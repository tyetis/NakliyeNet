using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NakliyeNet.Domain.Repository;
using NakliyeNet.Infrastructure.EntityFramework;
using NakliyeNet.Infrastructure.EntityFramework.Repository;

namespace NakliyeNet.Infrastructure
{
    public static class ServiceExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped< IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<DbContext, AppDbContext>();
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(GetConnectionString(configuration));
            });
        }
        
        private static string GetConnectionString(IConfiguration configuration)
        {
            var rootFolder = Directory.GetParent(Environment.CurrentDirectory);
            return string.Format(configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value, rootFolder);
        }
    }
}
