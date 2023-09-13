using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NakliyeNet.Domain.Entity;
using NakliyeNet.Domain.Models.Common;
using NakliyeNet.Domain.Models.Membership;

namespace NakliyeNet.Domain.Services
{
    public interface ICompanyService
    {
        Company GetCompany(string email, string password);
        Company GetCompany(int id);
        Company GetProfile();
        List<CompanyVehicle> GetProfileVehicles();
        CompanyTeam GetProfileTeam();
        PaginationResult<Company> GetCompanies();
        Company SignUp(SignUpModel model);
        void Update(Company model);
        void AddVehicle(CompanyVehicle model);
        void DeleteVehicle(int id);
        void UpdateTeam(CompanyTeam model);
    }
}
