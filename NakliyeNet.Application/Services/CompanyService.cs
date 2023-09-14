using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NakliyeNet.Application.Models.Membership;
using NakliyeNet.Domain.Entity;
using NakliyeNet.Domain.Models.Common;
using NakliyeNet.Domain.Models.Membership;
using NakliyeNet.Domain.Repository;
using NakliyeNet.Domain.Services;

namespace NakliyeNet.Application.Services
{
    public class CompanyService : BaseService, ICompanyService
    {
        IRepository<Company> RepoCompany { get; set; }
        IRepository<CompanyVehicle> RepoVehicle { get; set; }
        IRepository<CompanyTeam> RepoTeam { get; set; }
        LoggedUser LoggedUser { get; set; }

        public CompanyService(IUnitOfWork unitOfWork, LoggedUser loggedUser) : base(unitOfWork)
        {
            RepoCompany = unitOfWork.GetRepository<Company>();
            RepoVehicle = unitOfWork.GetRepository<CompanyVehicle>();
            RepoTeam = unitOfWork.GetRepository<CompanyTeam>();
            LoggedUser = loggedUser;
        }

        public PaginationResult<Company> GetCompanies()
        {
            var query = RepoCompany.GetAll().Include(n => n.CompanyComments);
            return new PaginationResult<Company>
            {
                Data = query.ToList(),
                Total = query.Count()
            };
        }

        public Company GetCompany(string email, string password)
        {
            return RepoCompany.Get(n => n.Email == email && n.Password == password);
        }

        public Company GetCompany(int id)
        {
            return RepoCompany.GetAll(n => n.Id == id).Include(n => n.CompanyVehicles).Include(n => n.CompanyTeam)
                              .Include(n => n.RequestApplications).ThenInclude(n => n.Request)
                              .Include(n => n.CompanyComments).ThenInclude(n => n.User).FirstOrDefault();
        }

        public Company GetProfile()
        {
            return RepoCompany.GetById(LoggedUser.Id);
        }

        public void Update(Company model)
        {
            var entity = RepoCompany.Get(n => n.Id == LoggedUser.Id);
            entity.Name = model.Name;
            entity.Email = model.Email;
            entity.Password = model.Password;
            entity.Address = model.Address;
            entity.Description = model.Description;
            if (model.LogoUrl != null) entity.LogoUrl = model.LogoUrl;
            UnitOfWork.SaveChanges();
        }

        public List<CompanyVehicle> GetProfileVehicles()
        {
            return RepoVehicle.GetAll(n => n.CompanyId == LoggedUser.Id).ToList();
        }

        public CompanyTeam GetProfileTeam()
        {
            return RepoTeam.Get(n => n.CompanyId == LoggedUser.Id);
        }

        public void UpdateTeam(CompanyTeam model)
        {
            var entity = GetProfileTeam();
            if (entity == null)
            {
                entity = new CompanyTeam { CompanyId = LoggedUser.Id };
                RepoTeam.Add(entity);
            }
            entity.Driver = model.Driver;
            entity.DriverPhoneNumber = model.DriverPhoneNumber;
            entity.TeamDescription = model.TeamDescription;
            UnitOfWork.SaveChanges();
        }

        public Company SignUp(SignUpModel model)
        {
            var entity = new Company
            {
                Name = model.Name,
                Email = model.Email,
                Password = model.Password,
                CreateDate = DateTime.Now
            };
            RepoCompany.Add(entity);
            UnitOfWork.SaveChanges();
            return entity;
        }

        public void AddVehicle(CompanyVehicle model)
        {
            model.CompanyId = LoggedUser.Id;
            RepoVehicle.Add(model);
            UnitOfWork.SaveChanges();
        }

        public void DeleteVehicle(int id)
        {
            var entity = RepoVehicle.GetById(id);
            RepoVehicle.Delete(entity);
            UnitOfWork.SaveChanges();
        }
    }
}
