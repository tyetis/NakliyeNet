using System;
using System.Collections.Generic;

#nullable disable

namespace NakliyeNet.Domain.Entity
{
    public partial class RequestDetail
    {
        public int Id { get; set; }
        public int? RequestId { get; set; }
        public string Property { get; set; }
        public string Value { get; set; }

        public virtual Request Request { get; set; }
    }
}
