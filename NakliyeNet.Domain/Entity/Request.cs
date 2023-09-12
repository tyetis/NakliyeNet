using System;
using System.Collections.Generic;

#nullable disable

namespace TransportationApp.Domain.Entity
{
    public partial class Request
    {
        public Request()
        {
            RequestDetails = new HashSet<RequestDetail>();
            Reservations = new HashSet<Reservation>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual JobCategory Category { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<RequestDetail> RequestDetails { get; set; }
        public virtual ICollection<RequestApplication> RequestApplications { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
