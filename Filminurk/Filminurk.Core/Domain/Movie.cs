using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filminurk.Core.Domain
{




    public class Movie
    {
        public Guid ID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateOnly FirstPublished { get; set; }

        public string  Director { get; set; }

        public List<string>? Actors { get; set; }

        public double? CurrentRating { get; set; }

        //public List<UserComment>? Reviews { get; set; }

        /**/

        /* 3 õpilase valitud andmetüüpi */

        public int IMDBrating { get; set; }

        public Genre Genre { get; set; }

        public int AgeRating { get; set; }

        public DateTime? EntryCreatedAt { get; set; }

        public DateTime? EntryModifedAt { get; set; }


    }
}
