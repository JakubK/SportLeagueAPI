using System.ComponentModel.DataAnnotations;

namespace SportLeagueAPI.Models
{
    public class Media
    {
        [Key]
        public string Url {get;set;}
    }
}