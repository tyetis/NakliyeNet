﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportationApp.Application.Models.Membership;
using TransportationApp.Domain.Entity;
using TransportationApp.Domain.Models.Common;
using TransportationApp.Domain.Models.Membership;
using TransportationApp.Domain.Repository;
using TransportationApp.Domain.Services;

namespace TransportationApp.Application.Services
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
            var query = RepoCompany.GetAll();
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
            return RepoCompany.GetAll(n => n.Id == id).Include(n => n.CompanyVehicle).Include(n => n.CompanyTeam)
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

        public CompanyVehicle GetProfileVehicle()
        {
            return RepoVehicle.Get(n => n.CompanyId == LoggedUser.Id);
        }

        public CompanyTeam GetProfileTeam()
        {
            return RepoTeam.Get(n => n.CompanyId == LoggedUser.Id);
        }

        public void UpdateVehicle(CompanyVehicle model)
        {
            var entity = GetProfileVehicle();
            if(entity == null)
            {
                entity = new CompanyVehicle { CompanyId = LoggedUser.Id };
                RepoVehicle.Add(entity);
            }
            entity.BrandModel = model.BrandModel;
            entity.LicenseNo = model.LicenseNo;
            UnitOfWork.SaveChanges();
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
    }
}
