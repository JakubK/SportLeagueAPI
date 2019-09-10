using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportLeagueAPI.DTO
{
    public class Score
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get;set;}

        public int Points {get;set;}

        public Player Player {get;set;}
        public int? PlayerId {get;set;}

        public Event Event {get;set;}
        public int? EventId {get;set;}
    }
}