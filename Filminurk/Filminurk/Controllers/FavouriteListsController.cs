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
    public class FavouriteListsController : Controller
    {
        private readonly FilminurkTARpe24Context _context;
        private readonly IFavouriteListsServices _favouriteListsServices;
        public FavouriteListsController(FilminurkTARpe24Context context, IFavouriteListsServices favouriteListsServices)
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
                Image = (List<FavouriteListIndexImageViewModel>)_context.FilesToDatabase.Where(ml => ml.ListID == x.FavouriteListID)
                .Select(li => new FavouriteListIndexImageViewModel
                {
                    ListID = li.ListID,
                    ImageID = li.ImageID,
                    ImageData = li.ImageData,
                    ImageTitle = li.ImageTitle,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(li.ImageData)),
                })

            });
            return View(resultingLists);
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
        public async Task<IActionResult> Create(FavouriteCreateViewModel vm, List<string> userhasSelected, List<MoviesIndexViewModel> movies)
        {

            List<Guid> tempParse = new();
            //tekkib ajutine guid list movieid-de hoidmiseks
            foreach (var stringID in userhasSelected)
            {
                //lisame iga stringi kohta järjendis userhasselected teisendatud guidi
                tempParse.Add(Guid.Parse(stringID));

            }

            var newListDto = new FavouriteListDTO() { };
            newListDto.ListName = vm.ListName;
            newListDto.ListDescription = vm.ListDescription;
            newListDto.IsMovieOrActor = vm.IsMovieOrActor;
            newListDto.IsPrivate = vm.IsPrivate;
            newListDto.ListCreatedAt = DateTime.UtcNow;
            newListDto.ListBelongsToUser = Guid.NewGuid().ToString();
            newListDto.ListModifiedAt = DateTime.UtcNow;
            newListDto.ListDeletedAt = vm.ListDeletedAt;


            //lisa filmid nimekirja,olemasolevate id-de põhiliselt 
            var listofmoviestoadd = new List<Movie>();
            foreach (var movieId in tempParse)
            {
                Movie thismovie = (Movie)_context.Movies.Where(tm => tm.ID == movieId).ToList().First();
                
            }
            newListDto.ListOfMovies = vm.ListOfMovies;
            //List<Guid> convertedIDs= new List<Guid>();
            //if (newListDto.ListOfMovies !=null)         
            //{
            //    convertedIDs = MovieToid(newListDto.ListOfMovies);
            // }
            var newlist = await _favouriteListsServices.Create(newListDto /*, convertedIDs*/);
            if (newlist == null)
            {
                return BadRequest();
            }
            return RedirectToAction("Index", vm);

        }

        public async Task<IActionResult> UserDetails(Guid id,Guid thisuserid)
        {
            if (id==null || thisuserid == null)
            {
                return BadRequest();
                //T0D0 return corresponding errorviews. id not found for list, and user login error for userid
            }
           // var images = _context.FilesToDatabase
            //    .Where(i => i.ListID == id)
            //    .Select(si => new FavouriteListIndexImageViewModel
            //    {
            //        ImageID = si.ImageID,
                  //  ListID = si.ListID,
                 //   ImageData = si.ImageData,
                //    ImageTitle = si.ImageTitle,
               //     Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(si.ImageData))
             //   }).ToList().First();

            var thislist = _context.FavouriteLists.Where(tl => tl.FavouriteListID == id && tl.ListBelongsToUser == thisuserid.ToString())
            .Select(stl => new FavouriteListDetailsViewModel
            {
                FavouriteListID = stl.FavouriteListID,
                ListBelongsToUser = stl.ListBelongsToUser,
                IsMovieOrActor = stl.IsMovieOrActor,
                ListName = stl.ListName,
                ListDescription = stl.ListDescription,
                IsPrivate = stl.IsPrivate,
                ListOfMovies = stl.ListOfMovies,
                IsReported = stl.IsReported,
               // Image = _context.FilesToDatabase
                //.Where(i => i.ListID == stl.FavouriteListID)
                //.Select(si => new FavouriteListIndexImageViewModel
              //  {
              //      ImageID = si.ImageID,
              //      ListID = si.ListID,
              //      ImageData = si.ImageData,
              //      ImageTitle = si.ImageTitle,
             //       
             //   }).ToList()
           }).First();
       // Image = images
         //   if(!ModelState.IsValid)
          //  {
         // //     return NotFound();
           // }
            //add viewdata attribute here later, to discern between user and admin
            if(thislist == null)
            {
                return NotFound();
            }
            return View("Details", thislist);
        }
        [HttpPost]
        public  IActionResult UserTogglePrivacy(Guid id)
        {
            FavouriteList thisList = _favouriteListsServices.DetailsAsync(id);

            FavouriteListDTO updatedList = new FavouriteListDTO();
            updatedList.FavouriteListID = thisList.FavouriteListID;
            updatedList.ListBelongsToUser = thisList.ListBelongsToUser;
            updatedList.ListName = thisList.ListName;
            updatedList.ListDescription = thisList.ListDescription;
            updatedList.IsPrivate = thisList.IsPrivate;
            updatedList.ListOfMovies = thisList.ListOfMovies;
            updatedList.IsReported = thisList.IsReported;
            updatedList.IsMovieOrActor = thisList.IsMovieOrActor;
            updatedList.ListCreatedAt = (DateTime)thisList.ListCreatedAt;
            updatedList.ListModifiedAt = DateTime.Now;
            updatedList.ListDeletedAt = thisList.ListDeletedAt;




            thisList.IsPrivate = !thisList.IsPrivate;
            _favouriteListsServices.Update(thisList);
           
            return View("Details");
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
