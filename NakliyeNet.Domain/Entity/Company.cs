using System;
using System.Collections.Generic;

#nullable disable

namespace TransportationApp.Domain.Entity
{
    public partial class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string LogoUrl { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public DateTime? CreateDate { get; set; }

        public virtual CompanyTeam CompanyTeam { get; set; }
        public virtual ICollection<CompanyVehicle> CompanyVehicles { get; set; }
        public virtual ICollection<CompanyComment> CompanyComments { get; set; }
        public virtual ICollection<RequestApplication> RequestApplications { get; set; }
    }
}
