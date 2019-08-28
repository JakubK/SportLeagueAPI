using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SportLeagueAPI.Models
{
    public class AddPlayer
    {
        public string Name {get;set;}
        public IFormFile File {get;set;}
    }
}