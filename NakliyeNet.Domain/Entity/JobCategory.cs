using System;
using System.Collections.Generic;

#nullable disable

namespace NakliyeNet.Domain.Entity
{
    public partial class JobCategory
    {
        public JobCategory()
        {
            Requests = new HashSet<Request>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
    }
}
