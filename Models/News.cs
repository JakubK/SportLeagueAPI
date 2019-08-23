using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportLeagueAPI.Models
{
    public class News
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get;set;}
        public string Name {get;set;}
        public string Description {get;set;}
        public DateTime Date {get;set;}

        public int? MediaId {get;set;}
        public Media Media {get;set;}
    }
}