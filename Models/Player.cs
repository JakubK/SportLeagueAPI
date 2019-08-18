using System.ComponentModel.DataAnnotations;

namespace SportLeagueAPI.Models
{
    public class Player
    {
        [Key]
        public int Id {get;set;}
        public string Name {get;set;}
    }
}