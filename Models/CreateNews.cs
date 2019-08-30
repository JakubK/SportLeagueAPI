using System;

namespace SportLeagueAPI.Models
{
    public class CreateNews
    {
        public string Name {get;set;}
        public string Description {get;set;}
        public DateTime Date {get;set;}
        public string Link {get;set;}
    }
}