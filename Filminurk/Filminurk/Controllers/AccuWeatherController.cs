using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Controllers
{
    public class AccuWeatherController : Controller
    {
        private readonly IWeatherForecastServices _weatherForecastServices;

        public AccurateWeatherController(IWeatherForecastServices weatherForecastServices)
        {
            _weatherForecastServices = weatherForecastServices;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
