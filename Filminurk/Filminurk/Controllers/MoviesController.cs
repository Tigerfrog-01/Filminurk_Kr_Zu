using System.Reflection;
using System.Security.Cryptography.Xml;
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

        public MoviesController
        (
            FilminurkTARpe24Context context,
            IMoviesServices movieServices
        )
        {
            _context = context;
            _movieservices = movieServices;
        }
        public IActionResult Index()
        {
            var result = _context.Movies.Select(x => new MoviesIndexViewModel
            {
                
                ID = x.ID,
                Title = x.Title,               
                FirstPublished = x.FirstPublished,
                CurrentRating = x.CurrentRating,
                Genre = (Models.Movies.Genre?)x.Genre,
                AgeRating = x.AgeRating,

            });
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var movie = await _movieservices.DetailsAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            var vm = new MoviesCreateUpdateViewModel(); {          
                vm.ID = movie.ID;
                vm.Title = movie.Title;
                vm.Description = movie.Description;
                vm.FirstPublished = movie.FirstPublished;
                vm.Director = movie.Director;
                vm.Actors = movie.Actors;
                vm.CurrentRating = movie.CurrentRating;
                vm.AgeRating = movie.AgeRating;
                vm.Genre = (Models.Movies.Genre)(Models.Movies.Genre?)movie.Genre;
                vm.IMDBrating = movie.IMDBrating;
                vm.EntryCreatedAt = movie.EntryCreatedAt;
                vm.EntryModifedAt = movie.EntryModifedAt;

                return View("CreateUpdate",vm);    

            }



        [HttpGet]
        public IActionResult Create()
        {
            MoviesCreateUpdateViewModel result = new();
            return View("Create",result);
        }

       

        [HttpPost]
        public async Task<IActionResult> Create(MoviesCreateUpdateViewModel vm, Core.Domain.Genre? genre)
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
                Genre = (Core.Domain.Genre?)vm.Genre,
                IMDBrating = vm.IMDBrating,
                EntryCreatedAt = vm.EntryCreatedAt,
                EntryModifiedAT = vm.EntryModifedAt
            };
            var result = await _movieservices.Create(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));

            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var movie = await _movieservices.DetailsAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            var vm = new MoviesDeleteViewModel();
            vm.ID = movie.ID;
            vm.Title = movie.Title;
            vm.Description = movie.Description;
            vm.FirstPublished = movie.FirstPublished;
            vm.Director = movie.Director;
            vm.Actors = movie.Actors;
            vm.CurrentRating = movie.CurrentRating;
            vm.AgeRating = movie.AgeRating;
            vm.Genre = (Models.Movies.Genre?)movie.Genre;
            vm.IMDBrating = movie.IMDBrating;
            vm.EntryCreatedAt = movie.EntryCreatedAt;
            vm.EntryModifedAt = movie.EntryModifedAt;

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var movie = await _movieservices.Delete(id);
            if (movie == null)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
    }   
     

