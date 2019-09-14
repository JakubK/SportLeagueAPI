using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SportLeagueAPI.Models
{
    public class CreatePlayer
    {
        public string Name {get;set;}
        public List<string> Links {get;set;}
        public int SettlementId {get;set;}
    }
}