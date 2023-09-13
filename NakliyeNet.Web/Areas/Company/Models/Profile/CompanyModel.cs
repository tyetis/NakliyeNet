using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NakliyeNet.Web.Areas.Company.Models.Profile
{
    public class CompanyModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public IFormFile LogoImage { get; set; }
    }
}
