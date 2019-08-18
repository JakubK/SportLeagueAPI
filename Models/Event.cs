using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportLeagueAPI.Models
{
    public class Event
    {
        [Key]
        public int Id {get;set;}
        public string Name {get;set;}
        public string Description {get;set;}
        public DateTime Date {get;set;}
        public List<string> MediaUrls {get;set;}
    }
}