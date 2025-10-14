using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filminurk.Core.Domain;
using Filminurk.Core.Dto;
using Filminurk.Data;

namespace Filminurk.ApplicationServices.Services
{
   
    
        public class MovieServices :IMovieServices
    {
        private readonly FilminurkTARpe24Context _context;

        public MovieServices(FilminurkTARpe24Context context)
        {
            _context = context;
        }

        public async Task<Movie> Create(MoviesDTO dto)
        {
            Movie movie = new Movie();
            movie.ID = Guid.NewGuid();
            movie.Title = dto.Title;
            movie.Description = dto.Description;
            movie.CurrentRating = dto.CurrentRating;
            movie.FirstPublished = (DateOnly)dto.FirstPublished;
            movie.IMDBrating = (int)dto.IMDBrating;
            movie.Genre = dto.Genre;
            movie.AgeRating = dto.AgeRating;
            //movie.EntryCreatedAt = DateTime.Now;
            //movie.EntryModifiedAt = DateTime.Now;

            await _context.AddAsync(movie);
            await _context.SaveChangesAsync();  

            return movie;


        }
        public async Task<Movie> DetailAsync(Guid id)
        {
            var result = await(_context.Movies.FirstOrDefaultAsync(x => x.ID == id));
            return result;
        }
    }
    
}
