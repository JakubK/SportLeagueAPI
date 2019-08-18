using System.ComponentModel.DataAnnotations;

namespace SportLeagueAPI.Models
{
    public class Settlement
    {
        [Key]
        public string Name {get;set;}
        public string Description {get;set;}
    }
}