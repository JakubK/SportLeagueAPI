using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SportLeagueAPI.Models;
using SportLeagueAPI.Models.Options;

namespace SportLeagueAPI.Services
{
  public class JwtHandler : IJwtHandler
  {
    JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
    SecurityKey _securityKey;
    SigningCredentials _signingCredentials;
    JwtOptions _options;

    public JwtHandler(IOptions<JwtOptions> options)
    {
      _options = options.Value;
      _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
      _signingCredentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256);
    }
    public JsonWebToken Create(string email)
    {
      var nowUtc = DateTime.UtcNow;
      var expires = nowUtc.AddMinutes(_options.ExpiryMinutes);
      var centuryBegin = new DateTime(1970,1,1).ToUniversalTime();
      var exp = (long)(new TimeSpan(expires.Ticks - centuryBegin.Ticks).TotalSeconds);

      var claims = new List<Claim>()
      {
        new Claim(ClaimTypes.Email, email)
      };

      var securityToken = new JwtSecurityToken
            (
                issuer : _options.Issuer,
                audience : _options.Audience,
                claims : claims,
                expires : expires,
                signingCredentials : _signingCredentials
            );

      var token = _jwtSecurityTokenHandler.WriteToken(securityToken);

      return new JsonWebToken
      {
          AccessToken = token,
          Expires = exp
      };
    }
  }
}