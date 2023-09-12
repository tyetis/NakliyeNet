using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportationApp.Domain.Models.Common
{
    public class PaginationResult<T>
    {
        public List<T> Data { get; set; }
        public int Total { get; set; }
    }
}
