using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Controllers
{
    public class ImportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Movie(string Movie);
        {

        }
            


    }
}
