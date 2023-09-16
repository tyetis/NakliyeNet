using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NakliyeNet.Domain.Services
{
    public interface ILocationService
    {
        List<string> GetCities();
        List<string> GetDistrict(string city);
        Task<string> GetDistance(string q);
    }
}
