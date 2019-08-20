using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportLeagueAPI.Models
{
    public class Media
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get;set;}
        public string Url {get;set;}

        public Event Event {get;set;}
        public News News {get;set;}
        public Player Player {get;set;}
        public Settlement Settlement {get;set;}
    }
}