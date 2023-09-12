using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportationApp.Domain.Models.Message;

namespace TransportationApp.Domain.Services
{
    public interface IMessageService
    {
        MessagesModel GetCompanyMessages(int userId);
        MessagesModel GetUserMessages(int companyId);
        void SendToCompany(int companyId, string text);
        void SendToUser(int userId, string text);
    }
}
