
using System.Data.Entity;
using System.Reflection.Metadata.Ecma335;
using Filminurk.Core.Domain;
using Filminurk.Core.dto;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using Filminurk.Data.Migrations;

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

        public async Task<Core.Domain.Actors> Create(ActorDTO dto)
        {
            Core.Domain.Actors actors = new Core.Domain.Actors();


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


        public async Task<Core.Domain.Actors> Delete(Guid id)
        {
            Core.Domain.Actors actors = new Core.Domain.Actors();
            var result = _context.Actors
          .FirstOrDefault(m => m.ActorID == id);

            _context.Actors.Remove(result);
            await _context.SaveChangesAsync();

            return result;


        }

        public async Task<Core.Domain.Actors> View(Guid id)
        {
            Core.Domain.Actors actors = new Core.Domain.Actors();
            var result = _context.Actors
          .FirstOrDefault(m => m.ActorID == id);



            return result;


        }

        public async Task<Core.Domain.Actors> Update(ActorDTO dto)
        {
            Core.Domain.Actors actors = new Core.Domain.Actors();


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

      
      






}
    
}

