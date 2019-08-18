using System;
using System.ComponentModel.DataAnnotations;

namespace SportLeagueAPI.Models
{
    public class News
    {
        [Key]
        public int Id {get;set;}
        public string Name {get;set;}
        public string Description {get;set;}
        public DateTime Date {get;set;}
    }
}