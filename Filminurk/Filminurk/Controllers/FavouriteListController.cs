using System.ComponentModel;
using AspNetCoreGeneratedDocument;
using Filminurk.Core.Domain;
using Filminurk.Core.dto;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using Filminurk.Models.FavouriteList;
using Filminurk.Models.Movies;
using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Controllers
{
    public class FavouriteListController : Controller
    {
        private readonly FilminurkTARpe24Context _context;
        private readonly IFavouriteListsServices _favouriteListsServices;
        public FavouriteListController( FilminurkTARpe24Context context, IFavouriteListsServices favouriteListsServices )
        {
            _context = context;
            _favouriteListsServices = favouriteListsServices;
        }
        public IActionResult Index()
        {
            var resultingLists = _context.FavouriteLists.OrderByDescending(y => y.ListCreatedAt)
            .Select(x => new FavouriteListIndexViewModel
            {
                FavouriteListID = x.FavouriteListID,
                ListBelongsToUser = x.ListBelongsToUser,
                IsMovieOrActor = x.IsMovieOrActor,
                ListName = x.ListName,
                ListDescription = x.ListDescription,
                ListCreatedAt = x.ListCreatedAt,
                Image =(List<FavouriteListIndexImageViewModel>)_context.FilesToDatabase.Where(ml => ml.ListID == x.FavouriteListID)
                .Select(li => new FavouriteListIndexImageViewModel
                {
                    ListID = li.ListID,
                    ImageID = li.ImageID,
                    ImageData = li.ImageData,
                    ImageTitle = li.ImageTitle,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(li.ImageData)),
                })

            });
            return View();
        }
        /* create get, create post */

        [HttpGet]
        
        public IActionResult Create()
        {
            var movies = _context.Movies.OrderBy(m => m.Title).Select(mo => new MoviesIndexViewModel
            {
                ID = mo.ID,
                Title = mo.Title,
                FirstPublished = mo.FirstPublished,
                Genre = mo.Genre,


            }).ToList();
            ViewData["allmovies"] = movies;
            ViewData["UserHasSelected"] = new List<string>();
            //this for normal user
            FavouriteCreateViewModel vm = new();
            return View("Create", vm);
        }


        [HttpPost]
        public async Task<IActionResult> Create(FavouriteCreateViewModel vm, List<string> userhasSelected,List<MoviesIndexViewModel> movies)
        {

            List<Guid> tempParse = new();
            foreach (var stringID in userhasSelected)
            {
                tempParse.Add(Guid.Parse(stringID));

            }

            var newListDto = new FavouriteListDTO() { };
            newListDto.ListName = vm.ListName;
            newListDto.ListDescription = vm.ListDescription;
            newListDto.IsMovieOrActor = vm.IsMovieOrActor;
            newListDto.IsPrivate = vm.IsPrivate;
            newListDto.ListCreatedAt = DateTime.UtcNow;
            newListDto.ListBelongsToUser = "00000000-0000-0000-000000000001";
            newListDto.ListModifiedAt = DateTime.UtcNow;    
            newListDto.ListDeletedAt = vm.ListDeletedAt;

            List<Guid> convertedIDs= new List<Guid>();
            if (newListDto.ListOfMovies !=null)         
            {
                convertedIDs = MovieToid(newListDto.ListOfMovies);


             }
            var newlist = await _favouriteListsServices.Create(newListDto, convertedIDs);
            if (newlist != null)
            {
                return BadRequest();
            }
            return RedirectToAction("Index", vm);


        
    }

        private List<Guid> MovieToid(List<Movie> listOfMovies)
        {
            var result = new List<Guid>();
            foreach (var movie in listOfMovies)
            {
                result.Add(movie.ID);
            }
            return result;
        }
        }
    }
