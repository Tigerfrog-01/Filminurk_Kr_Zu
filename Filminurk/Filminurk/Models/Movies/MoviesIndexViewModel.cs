using Filminurk.Core.Domain;

namespace Filminurk.Models.Movies
{
    public class MoviesIndexViewModel
    {
        public Guid ID { get; set; }

        public string Title { get; set; }


        public DateOnly FirstPublished { get; set; }

        public decimal? CurrentRating { get; set; }

        //public List<UserComment>? Reviews { get; set; }

        /**/

        /* 3 õpilase valitud andmetüüpi */

     

        public string Genre? Genre { get; set; }

        public int AgeRating { get; set; }

    }
}
