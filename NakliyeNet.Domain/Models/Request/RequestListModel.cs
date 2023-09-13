using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportationApp.Domain.Common;
using TransportationApp.Domain.Entity;

namespace TransportationApp.Domain.Models.Request
{
    public class RequestListModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string UserImageUrl { get; set; }
        public int ApplicationCount { get; set; }
        public decimal ApplicationAmount { get; set; }
        public List<string> ApplicationImages{ get; set; }
        public string ImageUrl { get; set; }
        public List<RequestDetail> Details { get; set; }
        public RequestStatus Status { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
