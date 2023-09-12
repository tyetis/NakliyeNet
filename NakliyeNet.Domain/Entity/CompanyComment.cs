using System;
using System.Collections.Generic;

#nullable disable

namespace TransportationApp.Domain.Entity
{
    public partial class CompanyComment
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public int CompanyId { get; set; }
        public int UserId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual Request Request { get; set; }
        public virtual User User { get; set; }
    }
}
