using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportLeagueAPI.Models
{
    public class Settlement
    {
        [Key]
        public int Id {get;set;}
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Name {get;set;}
        public string Description {get;set;}

        public int? MediaId {get;set;}

        public Media Media {get;set;}
        public ICollection<Player> Players {get;set;}
        public ICollection<Event> Events {get;set;}
    }
}