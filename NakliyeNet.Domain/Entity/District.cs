using System;
using System.Collections.Generic;

#nullable disable

namespace NakliyeNet.Domain.Entity
{
    public partial class District
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public string Name { get; set; }

        public virtual City City { get; set; }
    }
}
