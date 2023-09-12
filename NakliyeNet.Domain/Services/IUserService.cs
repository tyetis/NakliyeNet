using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportationApp.Domain.Entity;
using TransportationApp.Domain.Models.Common;
using TransportationApp.Domain.Models.Membership;

namespace TransportationApp.Domain.Services
{
    public interface IUserService
    {
        User GetUser(string email, string password);
        PaginationResult<User> GetUsers();
        User SignUp(SignUpModel model);
    }
}
