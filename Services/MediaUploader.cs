using System.IO;
using Microsoft.AspNetCore.Http;

namespace SportLeagueAPI.Services
{
  public class MediaUploader : IMediaUploader
  {
    IHttpContextAccessor _httpContext;
    IPathsProvider _pathsProvider;
    public MediaUploader(IHttpContextAccessor httpContext, IPathsProvider pathsProvider)
    {
        _httpContext = httpContext;
        _pathsProvider = pathsProvider;
    }
    public string UploadMedia(IFormFile file)
    {
      var request = _httpContext.HttpContext.Request;
      var fileName = Path.GetFileName(file.FileName);
      var filePath = _pathsProvider.MediaPath;

      Directory.CreateDirectory(filePath);
      filePath = Path.Combine(filePath, fileName);
      
      using (var fileStream = new FileStream(filePath, FileMode.Create))
      {
          file.CopyTo(fileStream);
      }

      return $"{request.Scheme}//:{request.Host}/media/{fileName}";
    }
  }
}