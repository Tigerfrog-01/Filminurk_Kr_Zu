using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filminurk.ApplicationServices.Services
{
    public class ImportMovie
    {
        string apikey = Filminurk.Data.Enviroment.Importkey; //key tuleb environmentist, ega pole hardcodedud
        var baseurl = "http://www.omdbapi.com/?apikey=[28bd6ace]&";
        var cityurl = "http://img.omdbapi.com/?apikey=[28bd6ace]&";
    }
}
