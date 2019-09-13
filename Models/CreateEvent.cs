using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportLeagueAPI.Models
{
    public class CreateEvent
    {
        public string Name {get;set;}
        public string Description {get;set;}
        public int Season {get;set;}
        public string Date {get;set;}
        public List<string> Links {get;set;}
        public AddScore[] Scores {get;set;}
    }
}