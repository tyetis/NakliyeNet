using System;
using System.Collections.Generic;

#nullable disable

namespace NakliyeNet.Domain.Entity
{
    public partial class CompanyTeam
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Driver { get; set; }
        public string DriverPhoneNumber { get; set; }
        public string TeamDescription { get; set; }

        public virtual Company Company { get; set; }
    }
}
