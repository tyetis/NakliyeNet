using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NakliyeNet.Domain.Common;

namespace NakliyeNet.Application.Models.Membership
{
    public class LoggedUser
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string UserName { get; set; }
        public string LogoUrl { get; set; }
    }
}
