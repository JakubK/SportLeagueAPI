using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportLeagueAPI.Models
{
    public class Player
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get;set;}
        public string Name {get;set;}

        public int? SettlementId {get;set;}
        public Settlement Settlement {get;set;}

        public int? MediaId {get;set;}
        public Media Media {get;set;}
        public ICollection<Score> Scores {get;set;}
    }
}