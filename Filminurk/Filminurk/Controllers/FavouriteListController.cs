using System.ComponentModel;
using Filminurk.Core.Domain;
using Filminurk.Data;
using Filminurk.Models.FavouriteList;
using Filminurk.Models.Movies;
using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Controllers
{
    public class FavouriteListController : Controller
    {
        private readonly FilminurkTARpe24Context _context;
        public FavouriteListController( FilminurkTARpe24Context context )
        {
            _context = context;
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
    }
}
