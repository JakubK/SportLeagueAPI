using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportLeagueAPI.DTO
{
    public class Media
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get;set;}
        public string Url {get;set;}

        public int? EventId {get;set;}
        public Event Event {get;set;}
    }
}