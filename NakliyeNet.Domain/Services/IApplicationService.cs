using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NakliyeNet.Domain.Entity;
using NakliyeNet.Domain.Models.Common;

namespace NakliyeNet.Domain.Services
{
    public interface IApplicationService
    {
        void Cancel(int id);
        PaginationResult<RequestApplication> GetApplications(int companyId);
        void Send(int requestId, decimal amount);
        void Approve(int requestId);
        void SetReservation(int applicationId, DateTime date, string desc);
    }
}
