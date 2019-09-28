using SportLeagueAPI.Models;

namespace SportLeagueAPI.Services
{
  public interface IJwtHandler
  {
    JsonWebToken Create(string email);
  }
}