using System;
using System.Collections.Generic;

#nullable disable

namespace NakliyeNet.Domain.Entity
{
    public partial class City
    {
        public City()
        {
            Districts = new HashSet<District>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<District> Districts { get; set; }
    }
}
