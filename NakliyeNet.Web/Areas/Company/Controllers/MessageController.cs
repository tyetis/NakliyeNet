using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportationApp.Domain.Services;

namespace TransportationApp.Areas.Company.Controllers
{
    [Area("Company")]
    public class MessageController : Controller
    {
        IMessageService MessageService { get; set; }

        public MessageController(IMessageService messageService)
        {
            MessageService = messageService;
        }

        public IActionResult List(int id)
        {
            var messages = MessageService.GetCompanyMessages(id);
            return View(messages);
        }

        [HttpPost]
        public IActionResult Send(int recipientId, string message)
        {
            MessageService.SendToUser(recipientId, message);
            return Json(true);
        }
    }
}
