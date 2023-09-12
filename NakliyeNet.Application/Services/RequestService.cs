using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TransportationApp.Application.Models.Membership;
using TransportationApp.Domain.Common;
using TransportationApp.Domain.Entity;
using TransportationApp.Domain.Models.Common;
using TransportationApp.Domain.Models.Request;
using TransportationApp.Domain.Repository;
using TransportationApp.Domain.Services;
using TransportationApp.Infrastructure.Common;

namespace TransportationApp.Application.Services
{
    public class RequestService : BaseService, IRequestService
    {
        IRepository<Request> RepoRequest { get; set; }
        IRepository<CompanyComment> RepoCompanyComment { get; set; }
        LoggedUser LoggedUser { get; set; }

        public RequestService(IUnitOfWork unitOfWork, LoggedUser loggedUser): base(unitOfWork)
        {
            RepoRequest = unitOfWork.GetRepository<Request>();
            RepoCompanyComment = unitOfWork.GetRepository<CompanyComment>();
            LoggedUser = loggedUser;
        }

        public PaginationResult<RequestListModel> GetRequests(int? userId = null, bool? active = null)
        {
            var query = RepoRequest.GetAll(n => (userId == null || n.UserId == userId) && (active == null || n.Status != (int)RequestStatus.Cancelled)).Select(n => new RequestListModel
            {
                Id = n.Id,
                Title = n.Title,
                Category = n.Category.Name,
                UserImageUrl = n.User.ImageUrl,
                ApplicationCount = n.RequestApplications.Count,
                ApplicationAmount = n.RequestApplications.Where(a => a.CompanyId == LoggedUser.Id).Select(n => n.Amount).FirstOrDefault(),
                Details = n.RequestDetails.ToList(),
                Status = (RequestStatus)n.Status,
                CreateDate = n.CreateDate
            });
            return new PaginationResult<RequestListModel>
            {
                Data = query.ApplyPagination(10, 0).ToList(),
                Total = query.Count()
            };
        }

        public Request GetRequest(int id)
        {
            return RepoRequest.GetAll(n => n.Id == id).Include(n => n.User)
                              .Include(n => n.Category).Include(n => n.RequestApplications)
                              .ThenInclude(n => n.Company).Include(n => n.RequestDetails).Include(n => n.Reservations).FirstOrDefault();
        }

        public bool Create(CreateRequestModel model)
        {
            var entity = new Request
            {
                UserId = LoggedUser.Id,
                CategoryId = model.CategoryId,
                Description = model.Description,
                Status = (int)RequestStatus.Active,
                CreateDate = DateTime.Now
            };
            var properties = model.GetType().GetProperties().ToList();
            foreach(var p in properties)
            {
                var value = p.GetValue(model);
                if (value == null || !Constants.RequestPropertiesDict.ContainsKey(p.Name) || p.Name == "Description") continue;
                entity.RequestDetails.Add(new RequestDetail
                {
                    Property = Constants.RequestPropertiesDict[p.Name],
                    Value = p.GetValue(model).ToString()
                });
            }
            RepoRequest.Add(entity);
            UnitOfWork.SaveChanges();
            return true;
        }

        public bool Cancel(int id)
        {
            var entity = RepoRequest.GetById(id);
            entity.Status = (int)RequestStatus.Cancelled;
            UnitOfWork.SaveChanges();
            return true;
        }

        public bool Complete(int id, int rating, string comment)
        {
            var request = RepoRequest.GetAll(n => n.Id == id).Include(n => n.RequestApplications).FirstOrDefault();
            var companyId = request.RequestApplications.FirstOrDefault(n => n.Status == (int)RequestApplicationStatus.Approved).CompanyId;
            RepoCompanyComment.Add(new CompanyComment
            {
                UserId = LoggedUser.Id,
                CompanyId = companyId,
                RequestId = id,
                Rating = rating,
                Comment = comment,
                CreateDate = DateTime.Now
            });
            request.Status = (int)RequestStatus.Completed;
            UnitOfWork.SaveChanges();
            return true;
        }
    }
}
