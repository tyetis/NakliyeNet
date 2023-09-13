using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NakliyeNet.Domain.Models.Message;

namespace NakliyeNet.Domain.Services
{
    public interface IMessageService
    {
        MessagesModel GetCompanyMessages(int userId);
        MessagesModel GetUserMessages(int companyId);
        void SendToCompany(int companyId, string text);
        void SendToUser(int userId, string text);
    }
}
