using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Filminurk.Core.Domain;
using Filminurk.Core.dto;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;

namespace Filminurk.ApplicationServices.Services
{
    public class FavouriteListsServices : IFavouriteListsServices
    {
        private readonly FilminurkTARpe24Context _context;
        private readonly IFilesServices _filesServices;

        public FavouriteListsServices(FilminurkTARpe24Context context, IFilesServices filesServices)
        {
            _context = context;
            _filesServices = filesServices;
        }
        public async Task<FavouriteList> DetailsAsync(Guid id)
        {
            var result = await _context.FavouriteLists
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.FavouriteListID == id);
            return result;
        }
        public async Task<FavouriteList> Create(FavouriteListDTO dto /*,List<Movie> selectedMovies*/)
        {
            FavouriteList newlist = new();
            newlist.FavouriteListID = Guid.NewGuid();
            newlist.ListName = dto.ListName;
            newlist.ListDescription = dto.ListDescription;
            newlist.ListCreatedAt = dto.ListCreatedAt;
            newlist.ListModified = (DateTime)dto.ListModifiedAt;
    
            newlist.ListOfMovies = dto.ListOfMovies;
            newlist.ListBelongsToUser = dto.ListBelongsToUser;
            await _context.FavouriteLists.AddAsync(newlist);
            await _context.SaveChangesAsync();

                // foreach (var movieid in selectedMovies)
              // {
             //     _context.FavouriteLists.Entry
            //
            //
           // }
           return newlist;
                
        }
        public async Task<FavouriteList> Update(FavouriteListDTO updatedList,string typeOfMethod)
        {
          
            FavouriteList UpdatedListInDB = new();

            UpdatedListInDB.FavouriteListID = updatedList.FavouriteListID;
            UpdatedListInDB.ListBelongsToUser = updatedList.ListBelongsToUser;
            UpdatedListInDB.IsMovieOrActor = updatedList.IsMovieOrActor;
            UpdatedListInDB.ListName = updatedList.ListName;
            UpdatedListInDB.ListDescription = updatedList.ListDescription;
            UpdatedListInDB.IsPrivate = updatedList.IsPrivate;
            UpdatedListInDB.ListOfMovies = updatedList.ListOfMovies;
            UpdatedListInDB.ListCreatedAt = updatedList.ListCreatedAt;
            UpdatedListInDB.ListDeletedAt = UpdatedListInDB.ListDeletedAt;
            UpdatedListInDB.ListModified = UpdatedListInDB.ListModified;
            if(typeOfMethod == "Delete")
            {
                _context.FavouriteLists.Attach(UpdatedListInDB);
                _context.Entry(UpdatedListInDB).Property(l => l.ListDeletedAt).IsModified = true;
                _context.Entry(UpdatedListInDB).Property(l => l.ListModified).IsModified = true;
            }
            else if (typeOfMethod == "Private")
            {
                _context.FavouriteLists.Attach(UpdatedListInDB);
                _context.Entry(UpdatedListInDB).Property(l => l.IsPrivate).IsModified = true;
                
            }            
            _context.Entry(UpdatedListInDB).Property(l => l.IsPrivate).IsModified = true;
            await _context.SaveChangesAsync();
            return UpdatedListInDB;
            
        
        }
    }
}
