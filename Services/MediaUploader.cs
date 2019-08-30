using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SportLeagueAPI.Context;
using SportLeagueAPI.DTO;

namespace SportLeagueAPI.Services
{
  public class MediaUploader : IMediaUploader
  {
    IHttpContextAccessor _httpContext;
    IPathsProvider _pathsProvider;
    IHasher _hasher;
    LeagueDbContext _ctx;
    public MediaUploader(IHttpContextAccessor httpContext, IPathsProvider pathsProvider, IHasher hasher, LeagueDbContext ctx)
    {
        _httpContext = httpContext;
        _pathsProvider = pathsProvider;
        _hasher = hasher;
        _ctx = ctx;
    }
    public string UploadMedia(IFormFile file)
    {
      var extension = file.FileName.Split('.').Last();
      var newName = _hasher.Hash(file.FileName) + "." + extension;
      var request = _httpContext.HttpContext.Request;
      var filePath = _pathsProvider.MediaPath;

      string url = $"{request.Scheme}//:{request.Host}/media/{newName}";

      _ctx.Medias.Add(new Media{
        Url = url
      });
      _ctx.SaveChanges();

      Directory.CreateDirectory(filePath);
      filePath = Path.Combine(filePath, newName);
      
      using (var fileStream = new FileStream(filePath, FileMode.Create))
      {
          file.CopyTo(fileStream);
      }

      return url;
    }

    public async Task<string> UploadMediaAsync(IFormFile file)
    {
      var extension = file.FileName.Split('.').Last();
      var newName = _hasher.Hash(file.FileName) + "." + extension;
      var request = _httpContext.HttpContext.Request;
      var filePath = _pathsProvider.MediaPath;

      string url = $"{request.Scheme}//:{request.Host}/media/{newName}";

      _ctx.Medias.Add(new Media{
        Url = url
      });
      _ctx.SaveChanges();

      Directory.CreateDirectory(filePath);
      filePath = Path.Combine(filePath, newName);
      
      using (var fileStream = new FileStream(filePath, FileMode.Create))
      {
         await file.CopyToAsync(fileStream);
      }

      return url;
    }
  }
}