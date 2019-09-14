using System.Collections.Generic;

namespace SportLeagueAPI.Models
{
    public class UpdatePlayer
    {
        public int Id {get;set;}
        public int SettlementId {get;set;}
        public string Name {get;set;}
        public List<string> Links {get;set;}
    }
}