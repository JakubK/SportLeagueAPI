using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using SportLeagueAPI.Context;

namespace SportLeagueAPI.Services
{
  public class FileNameHasher : IHasher
  {
    public string Hash(string input)
    {
        return Guid.NewGuid().ToString();
    }
  }
}