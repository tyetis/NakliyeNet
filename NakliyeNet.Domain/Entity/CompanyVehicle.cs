﻿using System;
using System.Collections.Generic;

#nullable disable

namespace TransportationApp.Domain.Entity
{
    public partial class CompanyVehicle
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string BrandModel { get; set; }
        public string LicenseNo { get; set; }

        public virtual Company Company { get; set; }
    }
}