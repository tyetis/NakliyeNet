using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NakliyeNet.Domain.Services;

namespace NakliyeNet.Areas.User.Controllers
{
    [Area("User")]
    public class MessageController : Controller
    {
        IMessageService MessageService { get; set; }

        public MessageController(IMessageService messageService)
        {
            MessageService = messageService;
        }

        public IActionResult List(int id)
        {
            var messages = MessageService.GetUserMessages(id);
            return View(messages);
        }

        [HttpPost]
        public IActionResult Send(int recipientId, string message)
        {
            MessageService.SendToCompany(recipientId, message);
            return Json(true);
        }
    }
}
