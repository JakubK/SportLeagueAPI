using Microsoft.AspNetCore.Http;

namespace SportLeagueAPI.Services
{
    public interface IMediaUploader
    {
         string UploadMedia(IFormFile file);
    }
}