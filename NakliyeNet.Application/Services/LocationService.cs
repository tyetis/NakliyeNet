using NakliyeNet.Domain.Entity;
using NakliyeNet.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NakliyeNet.Domain.Repository;

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
    }
}
