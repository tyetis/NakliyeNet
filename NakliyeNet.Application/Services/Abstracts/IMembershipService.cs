using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NakliyeNet.Application.Models.Membership;

namespace NakliyeNet.Application.Services
{
    public interface IMembershipService
    {
        LoggedUser LoggedUser { get; }
        Task LoginAsync(LoggedUser user);
        Task Logout();
    }
}
