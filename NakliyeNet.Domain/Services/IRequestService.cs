using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportationApp.Domain.Entity;
using TransportationApp.Domain.Models.Common;
using TransportationApp.Domain.Models.Request;

namespace TransportationApp.Domain.Services
{
    public interface IRequestService
    {
        Request GetRequest(int id);
        PaginationResult<RequestListModel> GetRequests(int? userId = null, bool? active = null);
        bool Create(CreateRequestModel model);
        bool Cancel(int id);
        bool Complete(int id, int rating, string comment);
    }
}
