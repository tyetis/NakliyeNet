using System;
using System.Collections.Generic;

#nullable disable

namespace TransportationApp.Domain.Entity
{
    public partial class Reservation
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public int CompanyId { get; set; }
        public string Description { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual Request Request { get; set; }
        public virtual Company Company { get; set; }
    }
}
