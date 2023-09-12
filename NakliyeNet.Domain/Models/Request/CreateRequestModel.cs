using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportationApp.Domain.Models.Request
{
    public class CreateRequestModel
    {
        public int CategoryId { get; set; }
        public string RoomType { get; set; }
        public string OldHomeType { get; set; }
        public string NewHomeType { get; set; }
        public string PackagingType { get; set; }
        public string InsuranceType { get; set; }
        public string Description { get; set; }
        public string FromCity { get; set; }
        public string FromDistrict { get; set; }
        public string ToCity { get; set; }
        public string ToDistrict { get; set; }
        public string TransportationTime { get; set; }
        public string LoadType { get; set; }
        public string LoadWeight { get; set; }
        public string ItemType { get; set; }
    }
}
