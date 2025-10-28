using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filminurk.Core.Domain;
using Filminurk.Core.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Core.ServiceInterface
{
    public interface IMoviesServices
    {
        Task<Movie> Create(MoviesDTO dto);
        Task<Movie> Delete(Guid id);
        Task<Movie> DetailsAsync(Guid id);
        Task<Movie> Update(MoviesDTO dto);
    }
     
        
    }


  