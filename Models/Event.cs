using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportLeagueAPI.Models
{
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get;set;}
        public string Name {get;set;}
        public string Description {get;set;}
        public DateTime Date {get;set;}

        public ICollection<Media> Medias {get;set;}

        public int? SettlementId {get;set;}
        public Settlement Settlement {get;set;}
    }
}