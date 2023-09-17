using NakliyeNet.Domain.Entity;
using NakliyeNet.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NakliyeNet.Domain.Repository;
using System.Net.Http;

namespace NakliyeNet.Application.Services
{
    public class LocationService : ILocationService
    {
        IRepository<City> RepoCity { get; set; }
        IRepository<District> RepoDistrict { get; set; }

        public LocationService(IRepository<City> repoCity, IRepository<District> repoDistrict)
        {
            RepoCity = repoCity;
            RepoDistrict = repoDistrict;
        }

        public List<string> GetCities()
        {
            return RepoCity.GetAll().Select(n => n.Name).ToList();
        }

        public List<string> GetDistrict(string city)
        {
            return RepoDistrict.GetAll(n => n.City.Name == city).Select(n => n.Name).ToList();
        }

        public async Task<string> GetDistance(string q)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Referrer = new Uri("http://www.nakliye.net");
                var response = await client.GetAsync($"https://nominatim.openstreetmap.org/search?format=json&q={q}");
                var json = await response.Content.ReadAsStringAsync();
                return json;
            }
        }
    }
}
