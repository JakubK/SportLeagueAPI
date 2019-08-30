using System.IO;

namespace SportLeagueAPI.Services
{
  public class PathsProvider : IPathsProvider
  {
    public string MediaPath
    {
        get 
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/media");
            Directory.CreateDirectory(path);
            return path;
        }
    }
  }
}