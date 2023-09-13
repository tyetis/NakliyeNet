using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportationApp.Application.Models.Membership;
using TransportationApp.Domain.Common;
using TransportationApp.Domain.Entity;
using TransportationApp.Domain.Models.Common;
using TransportationApp.Domain.Repository;
using TransportationApp.Domain.Services;
using TransportationApp.Infrastructure.Common;

namespace TransportationApp.Application.Services
{
    public class ApplicationService : BaseService, IApplicationService
    {
        IRepository<RequestApplication> RepoApplication { get; set; }
        IRepository<Reservation> RepoReservation { get; set; }
        LoggedUser LoggedUser { get; set; }

        public ApplicationService(IUnitOfWork unitOfWork, LoggedUser loggedUser): base(unitOfWork)
        {
            RepoApplication = unitOfWork.GetRepository<RequestApplication>();
            RepoReservation = unitOfWork.GetRepository<Reservation>();
            LoggedUser = loggedUser;
        }

        public PaginationResult<RequestApplication> GetApplications(int companyId)
        {
            var query = RepoApplication.GetAll(n => n.CompanyId == companyId).Include(n => n.Request).ThenInclude(n => n.Category);
            return new PaginationResult<RequestApplication>
            {
                Data = query.ApplyPagination(20, 0).ToList(),
                Total = query.Count()
            };
        }

        public RequestApplication GetRequest(int id)
        {
            return RepoApplication.GetAll(n => n.Id == id).FirstOrDefault();
        }

        public void Cancel(int id)
        {
            var app = RepoApplication.GetById(id);
            app.Status = (int)RequestApplicationStatus.Cancelled;
            UnitOfWork.SaveChanges();
        }

        public void Send(int requestId, decimal amount)
        {
            RepoApplication.Add(new RequestApplication
            {
                RequestId = requestId,
                Amount = amount,
                CompanyId = LoggedUser.Id,
                Status = (int)RequestApplicationStatus.Sent,
                CreateDate = DateTime.Now
            });
            UnitOfWork.SaveChanges();
        }

        public void Approve(int requestId)
        {
            var entity = RepoApplication.GetAll(n => n.Id == requestId).Include(n => n.Request).FirstOrDefault();
            entity.Status = (int)RequestApplicationStatus.Approved;
            entity.Request.Status = (int)RequestStatus.InProgress;
            UnitOfWork.SaveChanges();
        }

        public void SetReservation(int applicationId, DateTime date, string desc)
        {
            var application = RepoApplication.GetById(applicationId);
            RepoReservation.Add(new Reservation
            {
                RequestId = application.RequestId,
                CompanyId = application.CompanyId,
                ReservationDate = date,
                Description = desc,
                CreateDate = DateTime.Now
            });
            UnitOfWork.SaveChanges();
        }
    }
}
