using SportLeagueAPI.Models;

namespace SportLeagueAPI.Services
{
  public class AuthService : IAuthService
  {
    IJwtHandler _jwtHandler;
    public AuthService(IJwtHandler jwtHandler)
    {
        _jwtHandler = jwtHandler;
    }
    public JsonWebToken SignIn(SignIn signIn)
    {
        if(signIn.Email == "admin" && signIn.Password == "admin")
            return _jwtHandler.Create(signIn.Email);
        return null;
    }
  }
}