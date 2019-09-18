using System;
using System.Collections.Generic;

namespace SportLeagueAPI.Models
{
    public class UpdateNews
    {
        public int Id {get;set;}
        public string Name {get;set;}
        public string Description {get;set;}
        public string Date {get;set;}
        public List<string> Links {get;set;}
    }
}