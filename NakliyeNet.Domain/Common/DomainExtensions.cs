using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportationApp.Domain.Common
{
    public static class DomainExtensions
    {
        public static string DateTimeFormat(this DateTime datetime)
        {
            if (DateTime.Now - datetime <= TimeSpan.FromHours(1)) return $"{(DateTime.Now - datetime).Minutes} Dakika";
            else if (DateTime.Now - datetime <= TimeSpan.FromDays(1)) return $"{(DateTime.Now - datetime).Hours} Saat";
            else if (DateTime.Today.Year - datetime.Year >= 1) return $"{DateTime.Today.Year - datetime.Year} Yıl";
            else return datetime.ToShortDateString();
        }
    }
}
