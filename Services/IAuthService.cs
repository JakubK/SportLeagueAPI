using SportLeagueAPI.Models;

namespace SportLeagueAPI.Services
{
    public interface IAuthService
    {
         JsonWebToken SignIn(SignIn signIn);
    }
}