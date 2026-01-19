using AspNetCoreGeneratedDocument;
using Filminurk.ApplicationServices.Services;
using Filminurk.Core.Domain;
using Filminurk.Core.dto;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using Filminurk.Models.Actors;
using Filminurk.Models.Movies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;

namespace Filminurk.Controllers
{
    public class ActorsController : Controller
    {
        private readonly FilminurkTARpe24Context _context;
        private readonly IActorServices _actorService;



        public ActorsController
       (
           FilminurkTARpe24Context context,
           IActorServices actorsService


       )
        {
            _context = context;
            _actorService = actorsService;


        }
        public IActionResult Index()
        {
            var result = _context.Actors.Select(x => new ActorsIndexViewModel
            {

                ActorID = x.ActorID,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Nickname = x.Nickname,
                Age = x.Age,
                MoviesActedFor = x.MoviesActedFor,
                Addiction = x.Addiction,
                Crimes = x.Crimes,

            });
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ActorsIndexViewModel result = new();
            return View("Create", result);
        }





        [HttpPost]
        public async Task<IActionResult> Create(ActorsCreateViewModel vm)
        {

            if (ModelState.IsValid)
            {
                var dto = new ActorDTO()
                {
                    ActorID = vm.ActorID,
                    FirstName = vm.FirstName,
                    LastName = vm.LastName,
                    Nickname = vm.Nickname,
                    Age = vm.Age,
                    MoviesActedFor = vm.MoviesActedFor,
                    Crimes = vm.Crimes,
                    Addiction = vm.Addiction,




                };
                var result = await _actorService.Create(dto);
                if (result == null)
                {
                    return NotFound();
                }
                if (!ModelState.IsValid)
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
            var actors = await _actorService.Delete(id);

            if (actors == null)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));





        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var actors = await _actorService.View(id);

            if (actors == null)
            {
                return NotFound();
            }



            var vm = new ActorsDeleteView();
            {
                vm.ActorID = actors.ActorID;
                vm.FirstName = actors.FirstName;
                vm.LastName = actors.LastName;
                vm.Nickname = actors.Nickname;
                vm.Age = actors.Age;
                vm.MoviesActedFor = actors.MoviesActedFor;
                vm.Crimes = actors.Crimes;
                vm.Addiction = actors.Addiction;
            }

            return View(vm);




        }
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var vm = new ActorsDetailViewModel();
            var actor = await _context.Actors.FirstOrDefaultAsync(x => x.ActorID == id);
            if (actor != null)            
            {
                vm.ActorID = actor.ActorID;
                vm.FirstName = actor.FirstName;
                vm.LastName = actor.LastName;
                vm.Nickname = actor.Nickname;
                vm.Age = actor.Age;
                vm.MoviesActedFor = actor.MoviesActedFor;
                vm.Crimes = actor.Crimes;
                vm.Addiction = actor.Addiction;
            }
            return View(vm);

        }

        [HttpPost]
        public async Task<IActionResult> Update(ActorsDetailViewModel vm)
        {

            var actor = await _context.Actors.FirstOrDefaultAsync(x => x.ActorID == vm.ActorID);



            actor.ActorID = vm.ActorID;
            actor.FirstName = vm.FirstName;
            actor.LastName = vm.LastName;
            actor.Nickname = vm.Nickname;
            actor.Age = vm.Age;
            actor.MoviesActedFor = vm.MoviesActedFor;
            actor.Crimes = vm.Crimes;
            actor.Addiction = vm.Addiction;
           

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index)); ;
        }
    }
}
