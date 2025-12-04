using Filminurk.ApplicationServices.Services;
using Filminurk.Core.dto;
using Filminurk.Core.ServiceInterface;
using Filminurk.Models.Email;
using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Controllers
{
    public class EmailsController : Controller
    {
        private readonly IEmailsServices _emailServices;
        public EmailsController(EmailsServices emailServices)
        {
            _emailServices = emailServices;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SendEmail(EmailViewModel vm)
        {
            var dto = new EmailDTO
            {
                SendToThisAddress = vm.SendToThisAddress,
                EmailSubject = vm.EmailSubject,
                EmailContent = vm.EmailContent
            };
            _emailServices.SendEmail(dto);  
            return RedirectToAction(nameof(Index));
        }
    }
}
