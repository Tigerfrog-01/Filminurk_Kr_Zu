using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Controllers
{
    public class Jututuba : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
