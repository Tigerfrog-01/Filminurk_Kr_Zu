using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filminurk.Core.Domain;
using Filminurk.Core.dto;
using Filminurk.Core.Dto;

namespace Filminurk.Core.ServiceInterface
{
    public interface IActorServices
    {
        Task<Actors> Create(ActorDTO dto);

       Task<Actors> Delete(Guid id);
       // Task<Actors> DetailsAsync(Guid id);
      //  Task<Actors> Update(ActorDTO dto);
    }
}
