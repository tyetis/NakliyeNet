using System;
using System.Collections.Generic;

#nullable disable

namespace NakliyeNet.Domain.Entity
{
    public partial class CompanyImage
    {
        public int Id { get; set; }
        public byte[] Url { get; set; }
    }
}
