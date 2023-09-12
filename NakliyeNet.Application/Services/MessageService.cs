using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportationApp.Application.Models.Membership;
using TransportationApp.Domain.Common;
using TransportationApp.Domain.Entity;
using TransportationApp.Domain.Models.Message;
using TransportationApp.Domain.Repository;
using TransportationApp.Domain.Services;

namespace TransportationApp.Application.Services
{
    public class MessageService : BaseService, IMessageService
    {
        IRepository<Message> RepoMessage { get; set; }
        IRepository<User> RepoUser { get; set; }
        IRepository<Company> RepoCompany { get; set; }
        LoggedUser LoggedUser { get; set; }

        public MessageService(IUnitOfWork unitOfWork, LoggedUser loggedUser) : base(unitOfWork)
        {
            RepoMessage = unitOfWork.GetRepository<Message>();
            RepoUser = unitOfWork.GetRepository<User>();
            RepoCompany = unitOfWork.GetRepository<Company>();
            LoggedUser = loggedUser;
        }

        public MessagesModel GetCompanyMessages(int userId)
        {
            return new MessagesModel
            {
                RecipientId = userId,
                RecipientName = RepoUser.GetAll(n => n.Id == userId).Select(n => $"{n.Name} {n.Surname}").FirstOrDefault(),
                Messages = (from m in RepoMessage.GetAll()
                            where (m.FromType == (int)MemberType.Company && m.From == LoggedUser.Id && m.To == userId ||
                                   m.FromType == (int)MemberType.User && m.From == userId && m.To == LoggedUser.Id)
                            select new MessageModel
                            {
                                Id = m.Id,
                                Message = m.Text,
                                IsReceived = m.FromType == (int)MemberType.User,
                                SentDate = m.SentDate
                            }).ToList()
            };
        }

        public MessagesModel GetUserMessages(int companyId)
        {
            var company = RepoCompany.GetAll(n => n.Id == companyId).FirstOrDefault();
            return new MessagesModel
            {
                RecipientId = companyId,
                RecipientName = company.Name,
                RecipientImageUrl = company.LogoUrl,
                Messages = (from m in RepoMessage.GetAll()
                            where (m.FromType == (int)MemberType.Company && m.From == companyId && m.To == LoggedUser.Id ||
                                   m.FromType == (int)MemberType.User && m.From == LoggedUser.Id && m.To == companyId)
                            select new MessageModel
                            {
                                Id = m.Id,
                                Message = m.Text,
                                IsReceived = m.FromType == (int)MemberType.Company,
                                SentDate = m.SentDate
                            }).ToList()
            };
        }

        public void SendToCompany(int companyId, string text)
        {
            RepoMessage.Add(new Message
            {
                FromType = (int)MemberType.User,
                From = LoggedUser.Id,
                To = companyId,
                Text = text,
                SentDate = DateTime.Now
            });
            UnitOfWork.SaveChanges();
        }

        public void SendToUser(int userId, string text)
        {
            RepoMessage.Add(new Message
            {
                FromType = (int)MemberType.Company,
                From = LoggedUser.Id,
                To = userId,
                Text = text,
                SentDate = DateTime.Now
            });
            UnitOfWork.SaveChanges();
        }
    }
}
