namespace SportLeagueAPI.Models.Options
{
    public class JwtOptions
    {
      public int ExpiryMinutes {get;set;}
      public string Audience {get;set;}
      public string Key {get;set;}
      public string Issuer {get;set;}
    }
}