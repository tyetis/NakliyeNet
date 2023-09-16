using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NakliyeNet.Application.Models.Membership;
using NakliyeNet.Domain.Common;
using NakliyeNet.Domain.Entity;
using NakliyeNet.Domain.Models.Common;
using NakliyeNet.Domain.Models.Request;
using NakliyeNet.Domain.Repository;
using NakliyeNet.Domain.Services;
using NakliyeNet.Infrastructure.Common;

namespace NakliyeNet.Application.Services
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
                UserName = $"{n.User.Name} {n.User.Surname}",
                UserImageUrl = n.User.ImageUrl,
                ApplicationCount = n.RequestApplications.Count,
                ApplicationAmount = n.RequestApplications.Where(a => a.CompanyId == LoggedUser.Id).Select(n => n.Amount).FirstOrDefault(),
                ApplicationImages = n.RequestApplications.Select(r => r.Company.LogoUrl).ToList(),
                Details = n.RequestDetails.ToList(),
                Status = (RequestStatus)n.Status,
                CreateDate = n.CreateDate
            });
            return new PaginationResult<RequestListModel>
            {
                Data = query.OrderByDescending(n => n.CreateDate).ApplyPagination(10, 0).ToList(),
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
                EstimatedDistance = model.EstimatedDistance,
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

        public string CalculateAmount(CreateRequestModel model)
        {
            decimal baseAmount = 4000;
            if (model.CategoryId == 1) baseAmount += 1000;
            else if (model.CategoryId == 2) baseAmount += 15500;
            else if (model.CategoryId == 3) baseAmount += 2500;
            else if (model.CategoryId == 4) baseAmount += 500;
            else if (model.CategoryId == 5) baseAmount += 2000;
            else if (model.CategoryId == 6) baseAmount += 500;
            else if (model.CategoryId == 1) baseAmount += 1000;

            if (model.RoomType == "1+1") baseAmount += 1000;
            else if (model.RoomType == "1+1") baseAmount += 1500;
            else if (model.RoomType == "2+1") baseAmount += 2000;
            else if (model.RoomType == "3+1") baseAmount += 3500;
            else if (model.RoomType == "4+1") baseAmount += 4000;
            else if (model.RoomType == "5+1") baseAmount += 5500;
            else if (model.RoomType == "Daha Büyük") baseAmount += 7000;

            if (model.OldFloorType == "Merdiven Kullanılmalı") baseAmount += 2000;
            else if (model.OldFloorType == "Modüler Asansör Kullanılmalı") baseAmount += 3500;

            if (model.NewFloorType == "Merdiven Kullanılmalı") baseAmount += 2000;
            else if (model.NewFloorType == "Modüler Asansör Kullanılmalı") baseAmount += 3500;

            if (model.PackagingType == "Evet, paketlemeyi nakliyeci yapsın") baseAmount += 1500;

            if (model.InsuranceType == "Hayır, istemiyorum") baseAmount += 1000;

            if (model.LoadType == "Paletli Yük Taşıma") baseAmount += 3000;
            else if (model.LoadType == "Konteyner Taşıma") baseAmount += 10000;
            else if (model.LoadType == "Ticari Yük Taşıma") baseAmount += 4500;

            if (model.LoadWeight == "500 kg") baseAmount += 1500;
            else if (model.LoadWeight == "5-10 Ton") baseAmount += 3000;
            else if (model.LoadWeight == "5-10 Ton") baseAmount += 3000;
            else if (model.LoadWeight == "10 Ton veya daha fazla") baseAmount += 3500;

            return baseAmount.ToString("C0");
        }
    }
}
