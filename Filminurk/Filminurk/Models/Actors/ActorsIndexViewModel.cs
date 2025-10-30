using System.ComponentModel.DataAnnotations;
using Filminurk.Core.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Models.Actors
{
    public class ActorsIndexViewModel : Controller
    {
        [Key]
        public Guid ActorID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public List<string>? MoviesActedFor { get; set; }
        public Guid PortraitID { get; set; }
        public int Age { get; set; }
        public Addiction Addiction { get; set; }
        public Crimes Crimes { get; set; }

    }
}
