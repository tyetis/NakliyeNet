using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NakliyeNet.Domain.Entity;
using NakliyeNet.Domain.Models.Common;
using NakliyeNet.Domain.Models.Membership;

namespace NakliyeNet.Domain.Services
{
    public interface IUserService
    {
        User GetUser(string email, string password);
        PaginationResult<User> GetUsers();
        User SignUp(SignUpModel model);
    }
}
