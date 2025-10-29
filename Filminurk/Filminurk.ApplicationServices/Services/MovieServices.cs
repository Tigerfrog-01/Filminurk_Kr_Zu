using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Filminurk.Core.Domain;
using Filminurk.Core.dto;
using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using Microsoft.AspNetCore.Mvc;

namespace Filminurk.ApplicationServices.Services
{
   
    
        public class MovieServices : IMoviesServices

        {
        private readonly FilminurkTARpe24Context _context;
        private readonly IFilesServices _filesServices;

        public MovieServices
            (FilminurkTARpe24Context context
            , IFilesServices filesServices)
        {
            _context = context;
            _filesServices = filesServices; 
        }

        public async Task<Movie> Create(MoviesDTO dto)
        {
            Movie movie = new Movie();


            movie.ID = Guid.NewGuid();
            movie.Title = dto.Title;
            movie.Description = dto.Description;
            movie.CurrentRating = dto.CurrentRating;
            movie.FirstPublished = (DateOnly)dto.FirstPublished;
            movie.Actors = dto.Actors;
            movie.Director = dto.Director;
            movie.IMDBrating = (int)dto.IMDBrating;
            movie.Genre = (Genre)dto.Genre;
            movie.AgeRating = (int)dto.AgeRating;

            movie.EntryCreatedAt = DateTime.Now;
            movie.EntryModifedAt = DateTime.Now;
            _filesServices.FilesToApi(dto, movie);

            await _context.AddAsync(movie);
            await _context.SaveChangesAsync();  

            return movie;


        }
        public async Task<Movie> DetailsAsync(Guid id)
        {
            var result = await _context.Movies.FirstOrDefaultAsync(x => x.ID == id);
            return result;
        }

        public async Task<Movie> Update(MoviesDTO dto)
        {
            Movie movie = new Movie();

            movie.ID = Guid.NewGuid();
            movie.Title = dto.Title;
            movie.Description = dto.Description;
            movie.CurrentRating = dto.CurrentRating;
            movie.FirstPublished = (DateOnly)dto.FirstPublished;
            movie.Actors = dto.Actors;
            movie.Director = dto.Director;
            movie.IMDBrating = (int)dto.IMDBrating;
            movie.Genre = (Genre)dto.Genre;
            movie.AgeRating = (int)dto.AgeRating;
           

            movie.EntryCreatedAt = DateTime.Now;
            movie.EntryModifedAt = DateTime.Now;

            _filesServices.FilesToApi(dto, movie);

            await _context.AddAsync(movie);
            await _context.SaveChangesAsync();

            return movie;

        }

   
        public async Task<Movie> Delete(Guid id)
        {
            var result = await _context.Movies
           .FirstOrDefaultAsync(m => m.ID == id);  
            

            var images = await _context.FilesToApi
                .Where(x => x.MovieID == id)
                .Select(y => new FileToApiDTO
                {
                    ImageID = y.ImageID,
                    MovieID = y.MovieID,
                    FilePath = y.ExistingFilePath,
                }).ToArrayAsync();

            await _filesServices.RemoveImagesFromApi(images);


            _context.Movies.Remove(result); 
            await _context.SaveChangesAsync();

            return result;


        }
    
    }
    
}
