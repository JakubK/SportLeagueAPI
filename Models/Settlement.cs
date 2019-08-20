using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportLeagueAPI.Models
{
    public class Settlement
    {
        [Key]
        public int Id {get;set;}
        public string Name {get;set;}
        public string Description {get;set;}

        public int? MediaId {get;set;}

        public Media Media {get;set;}
        public ICollection<Player> Players {get;set;}
        public ICollection<Event> Events {get;set;}
    }
}