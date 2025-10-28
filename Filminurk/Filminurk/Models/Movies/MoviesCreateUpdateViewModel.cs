﻿using Filminurk.Core.Domain;

namespace Filminurk.Models.Movies
{
  

    public class MoviesCreateUpdateViewModel
    {
        public Guid? ID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateOnly FirstPublished { get; set; }

        public string Director { get; set; }

        public List<string>? Actors { get; set; }

        public double? CurrentRating { get; set; }

        //public List<UserComment>? Reviews { get; set; }

        /*Kassaolevate piltide andmeomadused*/
        public List<IFormFile> Files { get; set; }
        public List<ImageViewModel> Images { get; set; } = new List<ImageViewModel>();

        /* 3 õpilase valitud andmetüüpi */

        public int? IMDBrating { get; set; }

        public Genre? Genre { get; set; }

        public int? AgeRating { get; set; }
        /* andmebaasi jaoks vajalikud */


        public DateTime? EntryCreatedAt { get; set; }

        public DateTime?  EntryModifedAt { get; set; }
    
    }
}
