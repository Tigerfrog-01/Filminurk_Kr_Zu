﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filminurk.Core.Domain
{
    public class Actors
    {
        [Key]
        public Guid ActorID { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string Nickname { get; set; }
        public List<string>? MoviesActedFor { get; set; }
        public Guid PortraitID { get; set; }
        public int Age { get; set; }
        public Addiction Addiction { get; set; }
        public Crimes Crimes { get; set; }
       


    }
}
