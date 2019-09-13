using System;

namespace SportLeagueAPI.Models
{
    public class UpdateNews
    {
        public int Id {get;set;}
        public string Name {get;set;}
        public string Description {get;set;}
        public string Date {get;set;}
        public string Link {get;set;}
    }
}