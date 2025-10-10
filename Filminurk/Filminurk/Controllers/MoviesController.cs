using System.Reflection;
using Filminurk.Data;
using Filminurk.Models.Movies;
using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Controllers
{
    public class MoviesController : Controller
    {
        private readonly FilminurkTARpe24Context _context;
        public MoviesController ( FilminurkTARpe24Context context )
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var result = _context.Movies.Select(x => new MoviesIndexViewModel
            {
                ID = x.ID,
                Title = x.Title,
                FirstPublished = x.FirstPublished,
                CurrentRating = x.CurrentRating,
                Genre = x.Genre,
                AgeRating = x.AgeRating,

            });
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            MoviesCreateViewModel result = new();
            return View("Create",result);
        }
        [HttpPost]
        public async Task<IActionResult> Create(MoviesCreateViewModel vm)
        {
            var dto = new MoviesDTO()
            {
                ID = vm.ID,
                Title = vm.Title,
                Description = vm.Description,
                FirstPublished = vm.FirstPublished,
                Directory = vm.Director,
                Actors = vm.Actors,
                CurrentRating = vm.CurrentRating,
                AgeRating = vm.AgeRating,
                Genre = vm.Genre,
                IMDBrating = vm.IMDBrating,


            };
            var result = await _context.Create(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));

            }
            return RedirectToAction(nameof(Index));
        }


        }
    }   
     

