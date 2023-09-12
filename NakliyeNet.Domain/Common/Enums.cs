using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportationApp.Domain.Common
{
    public enum MemberType
    {
        User = 1,
        Company = 2
    }

    public enum RequestStatus
    {
        Active = 1,
        Completed = 2,
        Cancelled = 3,
        InProgress = 4
    }

    public enum RequestApplicationStatus
    {
        Sent = 1,
        Approved = 2,
        Cancelled = 3
    }

    public class Enums
    {
    }
}
