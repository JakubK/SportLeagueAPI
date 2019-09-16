using System.Collections.Generic;

namespace SportLeagueAPI.Models
{
    public class CreateSettlement
    {
        public string Name {get;set;}
        public string Description {get;set;}
        public List<string> Links {get;set;}
    }
}