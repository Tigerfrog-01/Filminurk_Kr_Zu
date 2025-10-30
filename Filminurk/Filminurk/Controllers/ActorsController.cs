using Filminurk.ApplicationServices.Services;
using Filminurk.Core.dto;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using Filminurk.Models.Actors;
using Filminurk.Models.Movies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

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
          
           

            var vm = new ActorsDeleteView();
            vm.ActorID = actors.ActorID;
            vm.FirstName = actors.FirstName;
            vm.LastName = actors.LastName;
            vm.Nickname = actors.Nickname;
            vm.Age = actors.Age;
            vm.MoviesActedFor = actors.MoviesActedFor;
            vm.Crimes = actors.Crimes;
            vm.Addiction = actors.Addiction;
         

            return View(vm);


        }


    }

}
