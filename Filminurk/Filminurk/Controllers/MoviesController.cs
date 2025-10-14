using System.Reflection;
using Filminurk.Core.Domain;
using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using Filminurk.Models.Movies;
using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Controllers
{
    public class MoviesController : Controller
    {
        private readonly FilminurkTARpe24Context _context;
        private readonly IMoviesServices _movieservices;
        public MoviesController ( FilminurkTARpe24Context context )
        {
            _context = context;
            IMoviesServices movieServices;
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
        public async Task<IActionResult> Create(MoviesCreateViewModel vm, Genre? genre)
        {
            var dto = new MoviesDTO()
            {
                ID = vm.ID,
                Title = vm.Title,
                Description = vm.Description,
                FirstPublished = vm.FirstPublished,
                Director = vm.Director,
                Actors = vm.Actors,
                CurrentRating = vm.CurrentRating,
                AgeRating = vm.AgeRating,
                genre = vm.Genre,
                IMDBrating = vm.IMDBrating,


            };
            var result = await _movieservices.Create(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));

            }
            return RedirectToAction(nameof(Index));
        }


        }
    }   
     

