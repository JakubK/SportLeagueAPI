using System;

namespace SportLeagueAPI.Models
{
    public class UpdateEvent
    {
        public int Id {get;set;}
        public string Name {get;set;}
        public string Description {get;set;}
        public DateTime Date {get;set;}
        public string[] Links {get;set;}
    }
}