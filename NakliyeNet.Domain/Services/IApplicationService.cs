using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportationApp.Domain.Entity;
using TransportationApp.Domain.Models.Common;

namespace TransportationApp.Domain.Services
{
    public interface IApplicationService
    {
        void Cancel(int id);
        PaginationResult<RequestApplication> GetApplications(int companyId);
        void Send(int requestId, decimal amount);
        void Approve(int requestId);
        void SetReservation(int applicationId, DateTime date);
    }
}
