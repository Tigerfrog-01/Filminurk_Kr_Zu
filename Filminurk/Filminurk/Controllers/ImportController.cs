using Filminurk.Core.dto;
using Filminurk.Core.ServiceInterface;
using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Controllers
{
    public class ImportController : Controller
    {
        private readonly IImportServices _importServices;

      
        public ImportController(IImportServices importServices)
        {
            _importServices = importServices;
        }

       
        [HttpGet]
        public IActionResult Index()
        {
            return View("Index");
        }

        
        [HttpPost]
        public async Task<IActionResult> Import(string movieTitle)
        {
            if (string.IsNullOrEmpty(movieTitle))
            {
                ViewBag.Error = "Sisesta filmi pealkiri";
                return View("Index");
            }

           
            var dto = new ImportDTO { Title = movieTitle };

            
            var result = await _importServices.ImportMovieService(dto);

            if (result != null)
            {
                
                ViewBag.Message = $"Film '{result.Title}' ({result.Year}) Palju onne, film on edukalt valmis!";

                ViewBag.Poster = result.Poster;
            }
            else
            {
                ViewBag.Error = "Film kas ei eksisteeri voi tekkis sinul midagi error ";
            }

            return View("Index");
        }
    }
}

