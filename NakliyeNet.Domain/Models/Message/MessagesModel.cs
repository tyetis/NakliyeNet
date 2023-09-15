using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NakliyeNet.Domain.Models.Message
{
    public class MessagesModel
    {
        public int RecipientId { get; set; }
        public string RecipientName { get; set; }
        public string RecipientImageUrl { get; set; }
        public List<MessageModel> Messages { get; set; }
        public List<MessagesModel> People { get; set; }
    }
}
