using System;
using System.Collections.Generic;

#nullable disable

namespace TransportationApp.Domain.Entity
{
    public partial class RequestApplication
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public int CompanyId { get; set; }
        public decimal Amount { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual Request Request { get; set; }
        public virtual Company Company { get; set; }
    }
}
