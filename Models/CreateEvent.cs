using System;
using System.Collections.Generic;

namespace SportLeagueAPI.Models
{
    public class CreateEvent
    {
        public string Name {get;set;}
        public string Description {get;set;}
        public DateTime Date {get;set;}
        public string[] Links {get;set;}

        public AddScore[] Scores {get;set;}
    }
}