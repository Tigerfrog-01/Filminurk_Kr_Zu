using System.Data.Entity;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Xml;
using Filminurk.Core.Domain;
using Filminurk.Core.dto;
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
        private readonly IFilesServices _filesServices; //Piltide lisamiseks vajalik fileservices injection

        public MoviesController
        (
            FilminurkTARpe24Context context,
            IMoviesServices movieServices     ,
            IFilesServices filesServices

        )
        {
            _context = context;
            _movieservices = movieServices;
            _filesServices = filesServices;

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
        public async Task<IActionResult> Update(Guid id)
        {
            var movie = await _movieservices.DetailsAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            var images = await _context.FilesToApi
                .Where(x => x.MovieID == id)
                .Select(y => new ImageViewModel
                {
                    FilePath = y.ExistingFilePath,
                    ImageID = id

                }).ToArrayAsync();

            var vm = new MoviesCreateUpdateViewModel();
            {
                vm.ID = movie.ID;
                vm.Title = movie.Title;
                vm.Description = movie.Description;
                vm.FirstPublished = movie.FirstPublished;
                vm.Director = movie.Director;
                vm.Actors = movie.Actors;
                vm.CurrentRating = movie.CurrentRating;
                vm.AgeRating = movie.AgeRating;
                vm.Genre = movie.Genre;
                vm.IMDBrating = movie.IMDBrating;
                vm.EntryCreatedAt = movie.EntryCreatedAt;
                vm.EntryModifedAt = movie.EntryModifedAt;
                vm.Images.AddRange(images);


                return View("CreateUpdate", vm);

            }
        }
        [HttpPost]

        public async Task<IActionResult> Update(MoviesCreateUpdateViewModel vm)
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
                Genre = vm.Genre,
                IMDBrating = vm.IMDBrating,
                EntryCreatedAt = vm.EntryCreatedAt,
                EntryModifedAt = vm.EntryModifedAt,
                Files = vm.Files,
                FileToApiDT0s = vm.Images
                .Select(x => new FileToApiDTO
                {
                    ImageID = x.ImageID,
                    MovieID = x.MovieID,
                    FilePath = x.FilePath,
                }).ToArray()


            }

        }




            [HttpGet]
            public IActionResult Create()
            {
                MoviesCreateUpdateViewModel result = new();
                return View("CreateUpdate", result);
            }
        


        [HttpPost]
        public async Task<IActionResult> Create(MoviesCreateUpdateViewModel vm)
        {

            if (ModelState.IsValid)
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
                    Genre = vm.Genre,
                    IMDBrating = vm.IMDBrating,
                    EntryCreatedAt = vm.EntryCreatedAt,
                    EntryModifiedAT = vm.EntryModifedAt,
                    Files = vm.Files,
                    FileToApiDT0s = vm.Images
                    .Select(x => new FileToApiDTO 
                    {
                        ImageID = x.ImageID,
                        FilePath = x.FilePath,
                        MovieID = x.MovieID,
                        IsPoster = x.IsPoster,                   
                    }).ToArray()

                };
                var result = await _movieservices.Create(dto);
                if (result == null)
                {
                    return NotFound();

                }
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
                var images = await _context.FilesToApi
                .Where(x => x.MovieID == id)
                .Select(y => new ImageViewModel
                {
                FilePath = y.ExistingFilePath,
                ImageID = y.ImageID,

                }).ToArrayAsync();

                var vm = new MoviesDeleteViewModel();
                vm.ID = movie.ID;
                vm.Title = movie.Title;
                vm.Description = movie.Description;
                vm.FirstPublished = movie.FirstPublished;
                vm.Director = movie.Director;
                vm.Actors = movie.Actors;
                vm.CurrentRating = movie.CurrentRating;
                vm.AgeRating = movie.AgeRating;
                vm.Genre = movie.Genre;
                vm.IMDBrating = movie.IMDBrating;
                vm.EntryCreatedAt = movie.EntryCreatedAt;
                vm.EntryModifedAt = movie.EntryModifedAt;
                vm.Images.AddRange(images);

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
        private async Task<ImageViewModel[]> FileFromDatabase(Guid id)
        {
            return await _context.FilesToApi
                .Where(x => x.MovieID == id)
                .Select(y => new ImageViewModel
                {
                    ImageID = y.ImageID,
                    MovieID = y.MovieID,
                    IsPoster = y.IsPoster,
                    FilePath = y.ExistingFilePath,
                }).ToArrayAsync();

        }
        }
    }

    
     

