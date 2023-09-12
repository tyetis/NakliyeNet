using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportationApp.Domain.Models.Message
{
    public class MessageModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public bool IsReceived { get; set; }
        public DateTime SentDate { get; set; }
    }
}
