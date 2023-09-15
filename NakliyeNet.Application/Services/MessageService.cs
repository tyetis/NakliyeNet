using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NakliyeNet.Application.Models.Membership;
using NakliyeNet.Domain.Common;
using NakliyeNet.Domain.Entity;
using NakliyeNet.Domain.Models.Message;
using NakliyeNet.Domain.Repository;
using NakliyeNet.Domain.Services;

namespace NakliyeNet.Application.Services
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
                People = GetCompanyPeople(LoggedUser.Id),
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
                RecipientName = company?.Name,
                RecipientImageUrl = company?.LogoUrl,
                People = GetUserPeople(LoggedUser.Id),
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

        public List<MessagesModel> GetCompanyPeople(int companyId)
        {
            return (from n in RepoMessage.GetAll()
                    from u in RepoUser.GetAll().Where(c => n.FromType == (int)MemberType.User && n.To == companyId && n.From == c.Id || n.FromType == (int)MemberType.Company && n.From == companyId && n.To == c.Id)
                    where (n.From == companyId && n.FromType == (int)MemberType.Company) || (n.To == companyId && n.FromType == (int)MemberType.User)
                    select new { n, u }).AsEnumerable().GroupBy(n => n.u.Id).Select(n => new MessagesModel
                    {
                        RecipientId = n.Key,
                        RecipientName = $"{n.FirstOrDefault().u.Name} {n.FirstOrDefault().u.Surname}",
                        RecipientImageUrl = n.FirstOrDefault().u.ImageUrl,
                        Messages = new List<MessageModel>
                        {
                            new MessageModel
                            {
                                Message = n.LastOrDefault().n.Text
                            }
                        }
                    }).ToList();
        }

        public List<MessagesModel> GetUserPeople(int userId)
        {
            return (from n in RepoMessage.GetAll()
                    from c in RepoCompany.GetAll().Where(c => n.FromType == (int)MemberType.Company && n.To == userId && n.From == c.Id || n.FromType == (int)MemberType.User && n.From == userId && n.To == c.Id)
                    where (n.From == userId && n.FromType == (int)MemberType.User) || (n.To == userId && n.FromType == (int)MemberType.Company)
                    select new { n,c }).AsEnumerable().GroupBy(n => n.c.Id).Select(n => new MessagesModel
                    {
                        RecipientId = n.Key,
                        RecipientName = n.FirstOrDefault().c.Name,
                        RecipientImageUrl = n.FirstOrDefault().c.LogoUrl,
                        Messages = new List<MessageModel>
                        {
                            new MessageModel
                            {
                                Message = n.LastOrDefault().n.Text
                            }
                        }
                    }).ToList();
        }
    }
}
