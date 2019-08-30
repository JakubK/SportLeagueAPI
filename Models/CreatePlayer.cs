using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SportLeagueAPI.Models
{
    public class CreatePlayer
    {
        public string Name {get;set;}
        public string Link {get;set;}
    }
}