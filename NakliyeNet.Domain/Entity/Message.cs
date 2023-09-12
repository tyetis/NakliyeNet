using System;
using System.Collections.Generic;

#nullable disable

namespace TransportationApp.Domain.Entity
{
    public partial class Message
    {
        public int Id { get; set; }
        public int From { get; set; }
        public int FromType { get; set; }
        public int To { get; set; }
        public string Text { get; set; }
        public DateTime SentDate { get; set; }
    }
}
