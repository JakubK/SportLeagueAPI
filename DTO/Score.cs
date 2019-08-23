using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportLeagueAPI.DTO
{
    public class Score
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id {get;set;}

        public int Value {get;set;}

        public Player Player {get;set;}
        public int? PlayerId {get;set;}

        public Event Event {get;set;}
        public int? EventId {get;set;}
    }
}