
using System.Data.Entity;
using Filminurk.Core.Domain;
using Filminurk.Core.dto;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;

namespace Filminurk.ApplicationServices.Services
{
    public class ActorService : IActorServices

    {
        private readonly FilminurkTARpe24Context _context;


        public ActorService
            (FilminurkTARpe24Context context)

        {
            _context = context;

        }

        public async Task<Actors> Create(ActorDTO dto)
        {
            Actors actors = new Actors();


            actors.ActorID = Guid.NewGuid();
            actors.FirstName = dto.FirstName;
            actors.LastName = dto.LastName;
            actors.Nickname = dto.Nickname;
            actors.Age = dto.Age;
            actors.MoviesActedFor = dto.MoviesActedFor;
            actors.Crimes = dto.Crimes;
            actors.Addiction = dto.Addiction;
       

            
            

            await _context.AddAsync(actors);
            await _context.SaveChangesAsync();

            return actors;
        }

        public async Task<Actors> Delete(Guid id)
        {
            Actors actors = new Actors();

            var result = await _context.Actors
          .FirstOrDefaultAsync(m => m.ActorID == id);


            _context.Actors.Remove(actors);
            await _context.SaveChangesAsync();

            return actors;


        }
    }
    }

