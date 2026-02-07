using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filminurk.Core.dto;

namespace Filminurk.Core.ServiceInterface
{
    public interface IImportServices
    {
        Task<ImportDTO> ImportMovieService(ImportDTO dto);
       


    }
}
