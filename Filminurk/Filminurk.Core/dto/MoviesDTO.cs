using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filminurk.Core.Domain;
using Filminurk.Core.dto;
using Microsoft.AspNetCore.Http;

namespace Filminurk.Core.Dto
{
    public class MoviesDTO
    {



        public Guid? ID { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateOnly? FirstPublished { get; set; }

        public string? Director { get; set; }

        public List<string>? Actors { get; set; }

        public double? CurrentRating { get; set; }


        //public List<UserComment>? Reviews { get; set; }

        /**/
        public List<IFormFile> Files { get; set; }
        public IEnumerable<FileToApiDTO> FileToApiDT0s { get; set; }

        /* 3 õpilase valitud andmetüüpi */

        public int? IMDBrating { get; set; }

        public Genre? Genre { get; set; }

        public int? AgeRating { get; set; }
        /* andmebaasi jaoks vajalikud */


        public DateTime? EntryCreatedAt { get; set; }

        public DateTime? EntryModifedAt { get; set; }
        public DateTime? EntryModifiedAT { get; set; }
    }
}
